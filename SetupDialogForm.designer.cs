namespace ASCOM.RelayPwr
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.RelayName0 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RelayName1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RelayName2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RelayName3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.RelayName7 = new System.Windows.Forms.TextBox();
            this.RelayName6 = new System.Windows.Forms.TextBox();
            this.RelayName5 = new System.Windows.Forms.TextBox();
            this.RelayName4 = new System.Windows.Forms.TextBox();
            this.relaycount = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(479, 433);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(6);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(118, 46);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(479, 502);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(6);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(118, 48);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 60);
            this.label1.TabIndex = 2;
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.RelayPwr.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(599, 17);
            this.picASCOM.Margin = new System.Windows.Forms.Padding(6);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Com Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(372, 15);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(6);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(123, 29);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(148, 19);
            this.comboBoxComPort.Margin = new System.Windows.Forms.Padding(6);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(176, 33);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // RelayName0
            // 
            this.RelayName0.Location = new System.Drawing.Point(148, 171);
            this.RelayName0.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName0.MaxLength = 10;
            this.RelayName0.Name = "RelayName0";
            this.RelayName0.Size = new System.Drawing.Size(196, 31);
            this.RelayName0.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Relay Port             Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 177);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Relay 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 227);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Relay 2";
            // 
            // RelayName1
            // 
            this.RelayName1.Location = new System.Drawing.Point(148, 221);
            this.RelayName1.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName1.MaxLength = 10;
            this.RelayName1.Name = "RelayName1";
            this.RelayName1.Size = new System.Drawing.Size(196, 31);
            this.RelayName1.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 277);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Relay 3";
            // 
            // RelayName2
            // 
            this.RelayName2.Location = new System.Drawing.Point(148, 271);
            this.RelayName2.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName2.MaxLength = 10;
            this.RelayName2.Name = "RelayName2";
            this.RelayName2.Size = new System.Drawing.Size(196, 31);
            this.RelayName2.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 327);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 25);
            this.label7.TabIndex = 16;
            this.label7.Text = "Relay 4";
            // 
            // RelayName3
            // 
            this.RelayName3.Location = new System.Drawing.Point(148, 321);
            this.RelayName3.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName3.MaxLength = 10;
            this.RelayName3.Name = "RelayName3";
            this.RelayName3.Size = new System.Drawing.Size(196, 31);
            this.RelayName3.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 564);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "Relay 8";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 514);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "Relay 7";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 464);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 25);
            this.label10.TabIndex = 18;
            this.label10.Text = "Relay 6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 414);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 25);
            this.label11.TabIndex = 17;
            this.label11.Text = "Relay 5";
            // 
            // RelayName7
            // 
            this.RelayName7.Location = new System.Drawing.Point(148, 561);
            this.RelayName7.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName7.MaxLength = 10;
            this.RelayName7.Name = "RelayName7";
            this.RelayName7.Size = new System.Drawing.Size(196, 31);
            this.RelayName7.TabIndex = 24;
            // 
            // RelayName6
            // 
            this.RelayName6.Location = new System.Drawing.Point(148, 511);
            this.RelayName6.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName6.MaxLength = 10;
            this.RelayName6.Name = "RelayName6";
            this.RelayName6.Size = new System.Drawing.Size(196, 31);
            this.RelayName6.TabIndex = 23;
            // 
            // RelayName5
            // 
            this.RelayName5.Location = new System.Drawing.Point(148, 461);
            this.RelayName5.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName5.MaxLength = 10;
            this.RelayName5.Name = "RelayName5";
            this.RelayName5.Size = new System.Drawing.Size(196, 31);
            this.RelayName5.TabIndex = 22;
            // 
            // RelayName4
            // 
            this.RelayName4.Location = new System.Drawing.Point(148, 411);
            this.RelayName4.Margin = new System.Windows.Forms.Padding(6);
            this.RelayName4.MaxLength = 10;
            this.RelayName4.Name = "RelayName4";
            this.RelayName4.Size = new System.Drawing.Size(196, 31);
            this.RelayName4.TabIndex = 21;
            // 
            // relaycount
            // 
            this.relaycount.FormattingEnabled = true;
            this.relaycount.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8"});
            this.relaycount.Location = new System.Drawing.Point(492, 169);
            this.relaycount.Name = "relaycount";
            this.relaycount.Size = new System.Drawing.Size(121, 33);
            this.relaycount.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(495, 122);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 25);
            this.label12.TabIndex = 26;
            this.label12.Text = "# Relays";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.OrangeRed;
            this.label13.Location = new System.Drawing.Point(476, 205);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(154, 25);
            this.label13.TabIndex = 27;
            this.label13.Text = "do not exceed ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.OrangeRed;
            this.label14.Location = new System.Drawing.Point(455, 230);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(203, 25);
            this.label14.TabIndex = 28;
            this.label14.Text = "physical relay count";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 610);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.relaycount);
            this.Controls.Add(this.RelayName7);
            this.Controls.Add(this.RelayName6);
            this.Controls.Add(this.RelayName5);
            this.Controls.Add(this.RelayName4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RelayName3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RelayName2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RelayName1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RelayName0);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Power Relay Setup 3.0";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.TextBox RelayName0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RelayName1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RelayName2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RelayName3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox RelayName7;
        private System.Windows.Forms.TextBox RelayName6;
        private System.Windows.Forms.TextBox RelayName5;
        private System.Windows.Forms.TextBox RelayName4;
        private System.Windows.Forms.ComboBox relaycount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}