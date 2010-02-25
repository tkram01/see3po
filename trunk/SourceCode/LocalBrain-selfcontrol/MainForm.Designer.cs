namespace LocalBrain
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.connectionsMenuItem = new System.Windows.Forms.MenuItem();
            this.remoteConnectMenuItem = new System.Windows.Forms.MenuItem();
            this.servosConnectMenuItem = new System.Windows.Forms.MenuItem();
            this.motorsConnectMenuItem = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.backRightButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.backLeftButton = new System.Windows.Forms.Button();
            this.turnLeftButton = new System.Windows.Forms.Button();
            this.turnRightButton = new System.Windows.Forms.Button();
            this.forwardRightButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.forwardLeftButton = new System.Windows.Forms.Button();
            this.IPaddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.T_left90 = new System.Windows.Forms.TextBox();
            this.T_block = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.T_right90 = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.movementtimer = new System.Windows.Forms.Timer();
            this.drivingtimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.fileMenu);
            this.mainMenu.MenuItems.Add(this.connectionsMenuItem);
            // 
            // fileMenu
            // 
            this.fileMenu.MenuItems.Add(this.exitMenuItem);
            this.fileMenu.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // connectionsMenuItem
            // 
            this.connectionsMenuItem.MenuItems.Add(this.remoteConnectMenuItem);
            this.connectionsMenuItem.MenuItems.Add(this.servosConnectMenuItem);
            this.connectionsMenuItem.MenuItems.Add(this.motorsConnectMenuItem);
            this.connectionsMenuItem.Text = "Connections";
            // 
            // remoteConnectMenuItem
            // 
            this.remoteConnectMenuItem.Text = "Connect to Remote Brain";
            this.remoteConnectMenuItem.Click += new System.EventHandler(this.remoteConnectMenuItem_Click);
            // 
            // servosConnectMenuItem
            // 
            this.servosConnectMenuItem.Text = "Connect to Servos";
            this.servosConnectMenuItem.Click += new System.EventHandler(this.servosConnectMenuItem_Click);
            // 
            // motorsConnectMenuItem
            // 
            this.motorsConnectMenuItem.Text = "Connect to Motors";
            this.motorsConnectMenuItem.Click += new System.EventHandler(this.motorsConnectMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 431);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(638, 24);
            // 
            // messageBox
            // 
            this.messageBox.BackColor = System.Drawing.Color.White;
            this.messageBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messageBox.Location = new System.Drawing.Point(0, 303);
            this.messageBox.Multiline = true;
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(638, 128);
            this.messageBox.TabIndex = 1;
            // 
            // speedBar
            // 
            this.speedBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedBar.LargeChange = 50;
            this.speedBar.Location = new System.Drawing.Point(570, 3);
            this.speedBar.Maximum = 1000;
            this.speedBar.Minimum = 100;
            this.speedBar.Name = "speedBar";
            this.speedBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.speedBar.Size = new System.Drawing.Size(52, 243);
            this.speedBar.TabIndex = 81;
            this.speedBar.TickFrequency = 50;
            this.speedBar.Value = 350;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(511, 207);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 35);
            this.button6.TabIndex = 80;
            this.button6.Tag = "6";
            this.button6.Text = "6";
            this.button6.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(463, 207);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 35);
            this.button5.TabIndex = 79;
            this.button5.Tag = "5";
            this.button5.Text = "5";
            this.button5.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(415, 207);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 35);
            this.button4.TabIndex = 78;
            this.button4.Tag = "4";
            this.button4.Text = "4";
            this.button4.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(511, 174);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(40, 36);
            this.button3.TabIndex = 77;
            this.button3.Tag = "3";
            this.button3.Text = "3";
            this.button3.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(463, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 36);
            this.button2.TabIndex = 76;
            this.button2.Tag = "2";
            this.button2.Text = "2";
            this.button2.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(415, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 36);
            this.button1.TabIndex = 75;
            this.button1.Tag = "1";
            this.button1.Text = "1";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(463, 80);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(40, 40);
            this.stopButton.TabIndex = 74;
            this.stopButton.Tag = "0";
            this.stopButton.Text = "STOP";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // backRightButton
            // 
            this.backRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backRightButton.Location = new System.Drawing.Point(511, 128);
            this.backRightButton.Name = "backRightButton";
            this.backRightButton.Size = new System.Drawing.Size(40, 40);
            this.backRightButton.TabIndex = 73;
            this.backRightButton.Tag = "8";
            this.backRightButton.Text = "RB";
            this.backRightButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.Location = new System.Drawing.Point(463, 128);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(40, 40);
            this.backButton.TabIndex = 72;
            this.backButton.Tag = "6";
            this.backButton.Text = "B";
            this.backButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // backLeftButton
            // 
            this.backLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backLeftButton.Location = new System.Drawing.Point(415, 128);
            this.backLeftButton.Name = "backLeftButton";
            this.backLeftButton.Size = new System.Drawing.Size(40, 40);
            this.backLeftButton.TabIndex = 71;
            this.backLeftButton.Tag = "7";
            this.backLeftButton.Text = "LB";
            this.backLeftButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // turnLeftButton
            // 
            this.turnLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.turnLeftButton.Location = new System.Drawing.Point(415, 80);
            this.turnLeftButton.Name = "turnLeftButton";
            this.turnLeftButton.Size = new System.Drawing.Size(40, 40);
            this.turnLeftButton.TabIndex = 70;
            this.turnLeftButton.Tag = "4";
            this.turnLeftButton.Text = "L";
            this.turnLeftButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // turnRightButton
            // 
            this.turnRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.turnRightButton.Location = new System.Drawing.Point(511, 80);
            this.turnRightButton.Name = "turnRightButton";
            this.turnRightButton.Size = new System.Drawing.Size(40, 40);
            this.turnRightButton.TabIndex = 69;
            this.turnRightButton.Tag = "5";
            this.turnRightButton.Text = "R";
            this.turnRightButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // forwardRightButton
            // 
            this.forwardRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forwardRightButton.Location = new System.Drawing.Point(511, 32);
            this.forwardRightButton.Name = "forwardRightButton";
            this.forwardRightButton.Size = new System.Drawing.Size(40, 40);
            this.forwardRightButton.TabIndex = 68;
            this.forwardRightButton.Tag = "3";
            this.forwardRightButton.Text = "RF";
            this.forwardRightButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forwardButton.Location = new System.Drawing.Point(463, 32);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(40, 40);
            this.forwardButton.TabIndex = 67;
            this.forwardButton.Tag = "1";
            this.forwardButton.Text = "F";
            this.forwardButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // forwardLeftButton
            // 
            this.forwardLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.forwardLeftButton.Location = new System.Drawing.Point(415, 32);
            this.forwardLeftButton.Name = "forwardLeftButton";
            this.forwardLeftButton.Size = new System.Drawing.Size(40, 40);
            this.forwardLeftButton.TabIndex = 66;
            this.forwardLeftButton.Tag = "2";
            this.forwardLeftButton.Text = "LF";
            this.forwardLeftButton.Click += new System.EventHandler(this.driveButton_Click);
            // 
            // IPaddr
            // 
            this.IPaddr.Location = new System.Drawing.Point(263, 32);
            this.IPaddr.Name = "IPaddr";
            this.IPaddr.Size = new System.Drawing.Size(104, 23);
            this.IPaddr.TabIndex = 84;
            this.IPaddr.Text = "192.168.2.166";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(203, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.Text = "Host IP:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(212, 108);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(54, 49);
            this.button7.TabIndex = 98;
            this.button7.Text = "left 90";
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // T_left90
            // 
            this.T_left90.Location = new System.Drawing.Point(212, 80);
            this.T_left90.Name = "T_left90";
            this.T_left90.Size = new System.Drawing.Size(54, 23);
            this.T_left90.TabIndex = 99;
            this.T_left90.Text = "600";
            // 
            // T_block
            // 
            this.T_block.Location = new System.Drawing.Point(272, 80);
            this.T_block.Name = "T_block";
            this.T_block.Size = new System.Drawing.Size(54, 23);
            this.T_block.TabIndex = 101;
            this.T_block.Text = "600";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(272, 108);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(54, 49);
            this.button8.TabIndex = 100;
            this.button8.Text = "1 block";
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // T_right90
            // 
            this.T_right90.Location = new System.Drawing.Point(332, 80);
            this.T_right90.Name = "T_right90";
            this.T_right90.Size = new System.Drawing.Size(54, 23);
            this.T_right90.TabIndex = 103;
            this.T_right90.Text = "500";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(332, 108);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(54, 49);
            this.button9.TabIndex = 102;
            this.button9.Text = "right 90";
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // movementtimer
            // 
            this.movementtimer.Tick += new System.EventHandler(this.movementtimer_Tick);
            // 
            // drivingtimer
            // 
            this.drivingtimer.Tick += new System.EventHandler(this.drivingtimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.T_right90);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.T_block);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.T_left90);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IPaddr);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.backRightButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.backLeftButton);
            this.Controls.Add(this.turnLeftButton);
            this.Controls.Add(this.turnRightButton);
            this.Controls.Add(this.forwardRightButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.forwardLeftButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.statusBar);
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "ViLAN Local Brain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuItem fileMenu;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem connectionsMenuItem;
		private System.Windows.Forms.MenuItem remoteConnectMenuItem;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TextBox messageBox;
		private System.Windows.Forms.MenuItem servosConnectMenuItem;
		private System.Windows.Forms.MenuItem motorsConnectMenuItem;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button backRightButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button backLeftButton;
        private System.Windows.Forms.Button turnLeftButton;
        private System.Windows.Forms.Button turnRightButton;
        private System.Windows.Forms.Button forwardRightButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button forwardLeftButton;
        private System.Windows.Forms.TextBox IPaddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox T_left90;
        private System.Windows.Forms.TextBox T_block;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox T_right90;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Timer movementtimer;
        private System.Windows.Forms.Timer drivingtimer;
	}
}

