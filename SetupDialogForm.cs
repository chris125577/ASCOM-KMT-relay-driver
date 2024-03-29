using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.RelayPwr;
using System.IO;

namespace ASCOM.RelayPwr
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here
            // Update the state variables with results from the dialogue
            Switch.comPort = (string)comboBoxComPort.SelectedItem;
            Switch.traceState = chkTrace.Checked;
            Switch.numSwitch = (short)Int32.Parse(relaycount.Text);
            Switch.configure[0] = RelayName0.Text;
            Switch.configure[1] = RelayName1.Text;
            Switch.configure[2] = RelayName2.Text;
            Switch.configure[3] = RelayName3.Text;
            Switch.configure[4] = RelayName4.Text;
            Switch.configure[5] = RelayName5.Text;
            Switch.configure[6] = RelayName6.Text;
            Switch.configure[7] = RelayName7.Text;
            // override relays that are not present
            if (Switch.numSwitch <8)
            {
                for (int i= Switch.numSwitch; i<8; i++)
                {
                    Switch.configure[i] = "not used";
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {          
            chkTrace.Checked = Switch.traceState;
            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            relaycount.Text = Switch.numSwitch.ToString();
            RelayName0.Text = Switch.configure[0];
            RelayName1.Text = Switch.configure[1];
            RelayName2.Text = Switch.configure[2];
            RelayName3.Text = Switch.configure[3];
            RelayName4.Text = Switch.configure[4];
            RelayName5.Text = Switch.configure[5]; 
            RelayName6.Text = Switch.configure[6];
            RelayName7.Text = Switch.configure[7];

            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Switch.comPort))
            {
                comboBoxComPort.SelectedItem = Switch.comPort;
            }
        }
    }
}