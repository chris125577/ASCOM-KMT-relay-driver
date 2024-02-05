//tabs=4
// --------------------------------------------------------------------------------
// 
// ASCOM Switch driver for RelayPwr
//
// Description:	Serial Relay Driver - n-way single byte commands
//
// Implements:	ASCOM Switch interface version: 
// Author:		(CJW) Chris Woodhouse <chris@beyondmonochrome.co.uk>
//
// Edit Log: 
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 20-Oct-2016	CJW	1.0	Initial edit, created from ASCOM driver template
// 22-Oct-2016  CJW 1.1.0   Attempt to improve receive character
// 17 Feb 2017  CJW 1.2     Using new relay board KMtronic- more complex and robust serial commands
// 06-Nov-2020  CJW 2.0     Using ASCOM profile store to keep names
// 04-Nov-2021  CJW 2.5      Updated to allow different relay counts
// 07-Nov-2021  CJW 2.7  Had to implement analog switches to double up on binary switches to avoid conformance issues.
// 13-Nov-2021  CJW 2.8  changed over to ASCOM serial from .Net serial, hoping to make work with hub!!?
// 13-Nov-2021  CJW 2.9  made the status instantenous, rather than read relay before giving status. updated after setswitch
// 14-Nov-2021  CJW 3.0  went back to .NET serial (seems faster) and expanded so that it can handle 8, 4, 2 or single relay modules
// 05-Jan-2024  CJW 3.1  changed read status to single relays for 1- and 2- relay boards
// --------------------------------------------------------------------------------
// The author provides this driver as-is. This is not a product and offers no guarantee of its performance
// This is used to define code in the template that is specific to one class implementation
// unused code can be deleted and this definition removed.
#define Switch

using System;
using System.Runtime.InteropServices;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;
using System.IO.Ports;
using System.Threading;


namespace ASCOM.RelayPwr
{
    //
    // Your driver's DeviceID is ASCOM.RelayPwr.Switch
    //
    // The Guid attribute sets the CLSID for ASCOM.RelayPwr.Switch
    // The ClassInterface/None addribute prevents an empty interface called
    // _RelayPwr from being created and used as the [default] interface


    /// <summary>
    /// ASCOM Switch Driver for RelayPwr.
    /// </summary>
    [Guid("77ca436f-7d49-4138-9334-383d6cc498d5")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Switch : ISwitchV2
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.RelayPwr.Switch";
        private static string driverDescription = "KMT Relay Control";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string comPort; // Variables to hold the currrent device com port configuration
        internal static bool traceState;
        //  change the following line according to the number of relays
        internal static short numSwitch = 8; // number of relays in this driver
        
        internal static string[] configure = new string[8]; // for relay names into profile store
        
        // for serial commands
        internal byte startByte = 0xFF;  // start character of relay command
        private byte[] relayStatus = new byte[numSwitch];  //  xx xx xx xx  01 /00 for each relay
        internal byte[] command = new byte[3];  // relay command string
        internal byte[] readRelay = new byte[] { 0xFF, 0x09, 0x0 }; //  1- and 2-ways do not use this
        internal byte setpin = 0x01, clearpin = 0x00;  // control bytes to set and reset relay
        internal byte readpin = 0x03;  // change over to reading relay status individually (due to 1 and 2 way relays)
       
        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        private TraceLogger tl;
        private SerialPort Serial;  // .net serial port
        //private Utilities.Serial Serial; // my serial port instance of ASCOM serial port

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayPwr"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Switch()
        {
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl = new TraceLogger("", "RelayPwr");
            tl.Enabled = traceState;
            tl.LogMessage("Switch", "Starting initialisation");

            connectedState = false; // Initialise connected to false
            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            Serial = new SerialPort();  // standard .net serial port
            tl.LogMessage("Switch", "Completed initialisation");
        }

        // sets up the serial port in the FTDI virtual com port on the relay board
        private bool SetupSwitch()
        {
            Serial.BaudRate = 9600;
            Serial.PortName = comPort;
            Serial.Parity = Parity.None;
            Serial.DataBits = 8;
            Serial.Handshake = System.IO.Ports.Handshake.None;
            Serial.ReceivedBytesThreshold = 1;
            try
            {
                Serial.Open();              // open port
                Serial.DiscardInBuffer();   // and clear it out just in case
                Thread.Sleep(1000); // suspect the board has an Arduino
                return Serial.IsOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //
        // PUBLIC COM INTERFACE ISwitchV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
            Serial.Dispose();
            Serial = null;

        }

        public bool Connected
        {
            get
            {
                tl.LogMessage("Connected Get", IsConnected.ToString());
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected Set", value.ToString());
                if (value == IsConnected)
                    return;

                if (value)
                {
                    connectedState = true;
                    tl.LogMessage("Connected Set", "Connecting to port " + comPort);
                    SetupSwitch();  // turn on serial
                    updatefromboard();  // find real status
                }
                else
                {
                    connectedState = false;
                    tl.LogMessage("Connected Set", "Disconnecting from port " + comPort);
                    Serial.DtrEnable = false;
                    Serial.Close();
                    Serial.Dispose();
                }
            }
        }

        public string Description
        {
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                tl.LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        public string Name
        {
            get
            {
                string name = "RelayPwr";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region ISwitchV2 Implementation

      
        /// <summary>
        /// The number of switches managed by this driver
        /// </summary>
        public short MaxSwitch
        {
            get
            {
                tl.LogMessage("MaxSwitch Get", numSwitch.ToString());
                return numSwitch;
            }
        }

        /// <summary>
        /// Return the name of switch n
        /// </summary>
        /// <param name="id">The switch number to return</param>
        /// <returns>
        /// The name of the switch
        /// </returns>
        public string GetSwitchName(short id)
        {
            Validate("GetSwitchName", id);
            tl.LogMessage("GetSwitchName", string.Format("GetSwitchName({0})", id));
            return configure[id];
        }

        /// <summary>
        /// Sets a switch name to a specified value
        /// </summary>
        /// <param name="id">The number of the switch whose name is to be set</param>
        /// <param name="name">The name of the switch</param>
        public void SetSwitchName(short id, string name)
        {
            Validate("SetSwitchName", id);
            {
                tl.LogMessage("SetSwitchName", string.Format("SetSwitchName({0}) = d", id, name));
                configure[id] = name;
                WriteProfile();
            }
            tl.LogMessage("GetSwitchName", string.Format("GetSwitchName({0}) - not implemented", id));
            throw new InvalidValueException("GetSwitchName");
        }

        /// <summary>
        /// Gets the switch description.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string GetSwitchDescription(short id)
        {
            Validate("GetSwitchDescription", id);
            return "relay " + id + " - normally closed";
        }

        /// <summary>
        /// Reports if the specified switch can be written to.
        /// This is false if the switch cannot be written to, for example a limit switch or a sensor.
        /// The default is true.
        /// </summary>
        /// <param name="id">The number of the switch whose write state is to be returned</param><returns>
        ///   <c>true</c> if the switch can be written to, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
        /// <exception cref="InvalidValueException">If id is outside the range 0 to MaxSwitch - 1</exception>
        public bool CanWrite(short id)
        {
            Validate("CanWrite", id);
            tl.LogMessage("CanWrite", string.Format("CanWrite({0}) - default true", id));
            return true;
        }

        #region boolean switch members

        /// <summary>
        /// Return the state of switch n
        /// a multi-value switch must throw a not implemented exception
        /// </summary>
        /// <param name="id">The switch number to return</param>
        /// <returns>
        /// True or false
        /// </returns>
        
        
        public bool GetSwitch(short id)
        {
            Validate("GetSwitch", id);
            return (relayStatus[id] > 0);
        }
        private void updatefromboard()
        {            
            try
            {
                Serial.DiscardInBuffer(); // clear garbage (.net)
                byte[] relaycmd = new byte[3];
                byte[] relayrx = new byte[3];
                if (numSwitch > 2)
                {
                    Serial.Write(readRelay, 0, 3); // send the read relay status command (not universal)
                    for (int i = 0; i < numSwitch; i++)  // read in bytes, one for each relay
                    {
                        relayStatus[i] = (byte)Serial.ReadByte(); //.net version
                    }
                }
                else
                {
                    relaycmd[0] = startByte;
                    relaycmd[2] = readpin;
                    for (int i = 0; i < numSwitch; i++)  // read in bytes, one for each relay
                    {

                        relaycmd[1] = (byte)(i + 1);  // form relay number
                        Serial.Write(relaycmd, 0, 3);         // send command .net version
                        for (int y = 0; y < 3; y++) relayrx[y] = (byte)Serial.ReadByte(); //.net version
                        relayStatus[i] = relayrx[2];
                    }
                }
                return;
            }
            catch
            {
                throw new ASCOM.NotConnectedException("GetSwitch");
            }
        }

        /// <summary>
        /// Sets a switch to the specified state
        /// If the switch cannot be set then throws a MethodNotImplementedException.
        /// A multi-value switch must throw a not implemented exception
        /// setting it to false will set it to its minimum value.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void SetSwitch(short id, bool state)  // id is relay number, state is action
        {
            Validate("SetSwitch", id); // check that the command is legal first
            if (!CanWrite(id))  // if not ok
            {
                var str = string.Format("SetSwitch({0}) - Cannot Write", id);
                tl.LogMessage("SetSwitch", str);
                throw new PropertyNotImplementedException(str);
            }
            else  // ok
            {
                byte[] relaycmd = new byte[3];
                relaycmd[0] = startByte;
                relaycmd[1] = (byte)(id + 1);  // form relay number
                if (state)   // set relay
                {
                    relaycmd[2] = (byte)(setpin);  
                    if (state) relayStatus[id] = 1; // update status
                }
                else                // release relay
                {
                    relaycmd[2] = (byte)(clearpin);
                    relayStatus[id] = 0; // update status
                }
                Serial.Write(relaycmd,0,3);         // send command .net version
                var str = string.Format("SetSwitch({0}) -  Write", id);
                tl.LogMessage("SetSwitch", str);
                updatefromboard(); // update actual switch states
            }
        }

        #endregion

        #region analogue members

        /// <summary>
        /// returns the maximum value for this switch
        /// boolean switches must return 1.0
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// not used by relay devices
        public double MaxSwitchValue(short id)
        {
            Validate("MaxSwitchValue", id);
            // boolean switch implementation:
            return 1.0;
        }

        /// <summary>
        /// returns the minimum value for this switch
        /// boolean switches must return 0.0
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double MinSwitchValue(short id)
        {
            Validate("MinSwitchValue", id);
            // boolean switch implementation:
            return 0.0;
        }

        /// <summary>
        /// returns the step size that this switch supports. This gives the difference between
        /// successive values of the switch.
        /// The number of values is ((MaxSwitchValue - MinSwitchValue) / SwitchStep) + 1
        /// boolean switches must return 1.0, giving two states.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double SwitchStep(short id)
        {
            Validate("SwitchStep", id);
            // boolean switch implementation:
            return 1.0;
        }

        /// <summary>
        /// returns the analogue switch value for switch id
        /// boolean switches must throw a not implemented exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double GetSwitchValue(short id)
        {
            Validate("GetSwitchValue", id);
            if (GetSwitch(id) == false) return 0.0;
                else return 1.0; 
        }

        /// <summary>
        /// set the analogue value for this switch.
        /// If the switch cannot be set then throws a MethodNotImplementedException.
        /// If the value is not between the maximum and minimum then throws an InvalidValueException
        /// boolean switches must throw a not implemented exception.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void SetSwitchValue(short id, double value)
        {
            Validate("SetSwitchValue", id, value);
            if (value < 0.5) SetSwitch(id, false);
            else SetSwitch(id, true);
        }

        #endregion
        #endregion

        #region private methods

        /// <summary>
        /// Checks that the switch id is in range and throws an InvalidValueException if it isn't
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        private void Validate(string message, short id)
        {
            if (id < 0 || id >= numSwitch)
            {
                tl.LogMessage(message, string.Format("Switch {0} not available, range is 0 to {1}", id, numSwitch - 1));
                throw new InvalidValueException(message, id.ToString(), string.Format("0 to {0}", numSwitch - 1));
            }
        }

        /// <summary>
        /// Checks that the switch id and value are in range and throws an
        /// InvalidValueException if they are not.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        private void Validate(string message, short id, double value)
        {
            Validate(message, id);
            var min = MinSwitchValue(id);
            var max = MaxSwitchValue(id);
            if (value < min || value > max)
            {
                tl.LogMessage(message, string.Format("Value {1} for Switch {0} is out of the allowed range {2} to {3}", id, value, min, max));
                throw new InvalidValueException(message, value.ToString(), string.Format("Switch({0}) range {1} to {2}", id, min, max));
            }
        }

        /// <summary>
        /// Checks that the number of states for the switch is correct and throws a methodNotImplemented exception if not.
        /// Boolean switches must have 2 states and multi-value switches more than 2.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="id"></param>
        /// <param name="expectBoolean"></param>
        private void Validate(string message, short id, bool expectBoolean)
        {
           Validate(message, id);
            var ns = (int)(((MaxSwitchValue(id) - MinSwitchValue(id)) / SwitchStep(id)) + 1);
            if ((expectBoolean && ns != 2) || (!expectBoolean && ns <= 2))
            {
                tl.LogMessage(message, string.Format("Switch {0} has the wriong number of states", id, ns));
                throw new InvalidValueException(string.Format("{0}({1})", message, id));
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Switch";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // check the actual serial connection (checks for unplugged)
                connectedState = Serial.IsOpen; //.net version
                //connectedState = Serial.Connected; // ascom version
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";
                traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                numSwitch = (short)Int32.Parse(driverProfile.GetValue(driverID, "relaycount", string.Empty, "4")); // default 4-way relay
                for (int i = 0; i < numSwitch; i++)  
                {
                    configure[i] = driverProfile.GetValue(driverID, "relay" + i.ToString(), string.Empty, "relay" + (i+1).ToString());
                }
                if (numSwitch<8)
                {
                    for (int i = numSwitch; i<8; i++)
                    {
                        configure[i] = driverProfile.GetValue(driverID, "relay" + i.ToString(), string.Empty, "not used");
                    }
                }
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";
                driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString());
                driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
                driverProfile.WriteValue(driverID, "relaycount", numSwitch.ToString());
                for (int i = 0; i < 8; i++)
                {
                    driverProfile.WriteValue(driverID, "relay" + i.ToString(), configure[i]);
                }
            }
        }
                #endregion
    }
}
