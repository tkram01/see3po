namespace Remote_Servo_Controller
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.left18 = new System.Windows.Forms.Button();
			this.right18 = new System.Windows.Forms.Button();
			this.bar18 = new System.Windows.Forms.TrackBar();
			this.forwardLeftButton = new System.Windows.Forms.Button();
			this.up16 = new System.Windows.Forms.Button();
			this.forwardRightButton = new System.Windows.Forms.Button();
			this.right17 = new System.Windows.Forms.Button();
			this.left17 = new System.Windows.Forms.Button();
			this.backLeftButton = new System.Windows.Forms.Button();
			this.down16 = new System.Windows.Forms.Button();
			this.backRightButton = new System.Windows.Forms.Button();
			this.bar17 = new System.Windows.Forms.TrackBar();
			this.bar16 = new System.Windows.Forms.TrackBar();
			this.bar20 = new System.Windows.Forms.TrackBar();
			this.bar19 = new System.Windows.Forms.TrackBar();
			this.button1 = new System.Windows.Forms.Button();
			this.down20 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.left19 = new System.Windows.Forms.Button();
			this.right19 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.up20 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectionMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.connectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.messageBox = new System.Windows.Forms.TextBox();
			this.commandMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.setServoPositionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.centerAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.bar18)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar17)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar16)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar20)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bar19)).BeginInit();
			this.mainMenu.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// left18
			// 
			this.left18.Image = ((System.Drawing.Image)(resources.GetObject("left18.Image")));
			this.left18.Location = new System.Drawing.Point(12, 266);
			this.left18.Name = "left18";
			this.left18.Size = new System.Drawing.Size(40, 40);
			this.left18.TabIndex = 33;
			this.left18.Tag = "4";
			this.left18.Click += new System.EventHandler(this.left17_Click);
			// 
			// right18
			// 
			this.right18.Image = ((System.Drawing.Image)(resources.GetObject("right18.Image")));
			this.right18.Location = new System.Drawing.Point(408, 266);
			this.right18.Name = "right18";
			this.right18.Size = new System.Drawing.Size(40, 40);
			this.right18.TabIndex = 32;
			this.right18.Tag = "5";
			this.right18.Click += new System.EventHandler(this.right17_Click);
			// 
			// bar18
			// 
			this.bar18.LargeChange = 50;
			this.bar18.Location = new System.Drawing.Point(58, 266);
			this.bar18.Maximum = 2100;
			this.bar18.Minimum = 900;
			this.bar18.Name = "bar18";
			this.bar18.Size = new System.Drawing.Size(344, 42);
			this.bar18.SmallChange = 25;
			this.bar18.TabIndex = 34;
			this.bar18.TickFrequency = 50;
			this.bar18.Value = 1500;
			this.bar18.ValueChanged += new System.EventHandler(this.bar18_ValueChanged);
			// 
			// forwardLeftButton
			// 
			this.forwardLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardLeftButton.Image")));
			this.forwardLeftButton.Location = new System.Drawing.Point(60, 61);
			this.forwardLeftButton.Name = "forwardLeftButton";
			this.forwardLeftButton.Size = new System.Drawing.Size(40, 40);
			this.forwardLeftButton.TabIndex = 23;
			this.forwardLeftButton.Tag = "2";
			// 
			// up16
			// 
			this.up16.Image = ((System.Drawing.Image)(resources.GetObject("up16.Image")));
			this.up16.Location = new System.Drawing.Point(106, 61);
			this.up16.Name = "up16";
			this.up16.Size = new System.Drawing.Size(40, 40);
			this.up16.TabIndex = 24;
			this.up16.Tag = "1";
			this.up16.Click += new System.EventHandler(this.up16_Click);
			// 
			// forwardRightButton
			// 
			this.forwardRightButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardRightButton.Image")));
			this.forwardRightButton.Location = new System.Drawing.Point(152, 61);
			this.forwardRightButton.Name = "forwardRightButton";
			this.forwardRightButton.Size = new System.Drawing.Size(40, 40);
			this.forwardRightButton.TabIndex = 25;
			this.forwardRightButton.Tag = "3";
			// 
			// right17
			// 
			this.right17.Image = ((System.Drawing.Image)(resources.GetObject("right17.Image")));
			this.right17.Location = new System.Drawing.Point(152, 107);
			this.right17.Name = "right17";
			this.right17.Size = new System.Drawing.Size(40, 40);
			this.right17.TabIndex = 26;
			this.right17.Tag = "5";
			this.right17.Click += new System.EventHandler(this.right17_Click);
			// 
			// left17
			// 
			this.left17.Image = ((System.Drawing.Image)(resources.GetObject("left17.Image")));
			this.left17.Location = new System.Drawing.Point(60, 107);
			this.left17.Name = "left17";
			this.left17.Size = new System.Drawing.Size(40, 40);
			this.left17.TabIndex = 27;
			this.left17.Tag = "4";
			this.left17.Click += new System.EventHandler(this.left17_Click);
			// 
			// backLeftButton
			// 
			this.backLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("backLeftButton.Image")));
			this.backLeftButton.Location = new System.Drawing.Point(60, 155);
			this.backLeftButton.Name = "backLeftButton";
			this.backLeftButton.Size = new System.Drawing.Size(40, 40);
			this.backLeftButton.TabIndex = 28;
			this.backLeftButton.Tag = "6";
			// 
			// down16
			// 
			this.down16.Image = ((System.Drawing.Image)(resources.GetObject("down16.Image")));
			this.down16.Location = new System.Drawing.Point(106, 155);
			this.down16.Name = "down16";
			this.down16.Size = new System.Drawing.Size(40, 40);
			this.down16.TabIndex = 29;
			this.down16.Tag = "7";
			this.down16.Click += new System.EventHandler(this.down16_Click);
			// 
			// backRightButton
			// 
			this.backRightButton.Image = ((System.Drawing.Image)(resources.GetObject("backRightButton.Image")));
			this.backRightButton.Location = new System.Drawing.Point(152, 155);
			this.backRightButton.Name = "backRightButton";
			this.backRightButton.Size = new System.Drawing.Size(40, 40);
			this.backRightButton.TabIndex = 30;
			this.backRightButton.Tag = "8";
			// 
			// bar17
			// 
			this.bar17.LargeChange = 50;
			this.bar17.Location = new System.Drawing.Point(60, 201);
			this.bar17.Maximum = 2100;
			this.bar17.Minimum = 900;
			this.bar17.Name = "bar17";
			this.bar17.Size = new System.Drawing.Size(132, 42);
			this.bar17.SmallChange = 25;
			this.bar17.TabIndex = 43;
			this.bar17.TickFrequency = 50;
			this.bar17.Value = 1500;
			this.bar17.ValueChanged += new System.EventHandler(this.bar17_ValueChanged);
			// 
			// bar16
			// 
			this.bar16.LargeChange = 50;
			this.bar16.Location = new System.Drawing.Point(12, 61);
			this.bar16.Maximum = 2100;
			this.bar16.Minimum = 900;
			this.bar16.Name = "bar16";
			this.bar16.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.bar16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.bar16.RightToLeftLayout = true;
			this.bar16.Size = new System.Drawing.Size(42, 134);
			this.bar16.SmallChange = 25;
			this.bar16.TabIndex = 44;
			this.bar16.TickFrequency = 50;
			this.bar16.Value = 1500;
			this.bar16.ValueChanged += new System.EventHandler(this.bar16_ValueChanged_1);
			// 
			// bar20
			// 
			this.bar20.LargeChange = 50;
			this.bar20.Location = new System.Drawing.Point(406, 61);
			this.bar20.Maximum = 2100;
			this.bar20.Minimum = 900;
			this.bar20.Name = "bar20";
			this.bar20.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.bar20.Size = new System.Drawing.Size(42, 134);
			this.bar20.SmallChange = 25;
			this.bar20.TabIndex = 54;
			this.bar20.TickFrequency = 50;
			this.bar20.Value = 1500;
			this.bar20.ValueChanged += new System.EventHandler(this.bar20_ValueChanged);
			// 
			// bar19
			// 
			this.bar19.LargeChange = 50;
			this.bar19.Location = new System.Drawing.Point(268, 201);
			this.bar19.Maximum = 2100;
			this.bar19.Minimum = 900;
			this.bar19.Name = "bar19";
			this.bar19.Size = new System.Drawing.Size(132, 42);
			this.bar19.SmallChange = 25;
			this.bar19.TabIndex = 53;
			this.bar19.TickFrequency = 50;
			this.bar19.Value = 1500;
			this.bar19.ValueChanged += new System.EventHandler(this.bar19_ValueChanged);
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(360, 155);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(40, 40);
			this.button1.TabIndex = 52;
			this.button1.Tag = "8";
			// 
			// down20
			// 
			this.down20.Image = ((System.Drawing.Image)(resources.GetObject("down20.Image")));
			this.down20.Location = new System.Drawing.Point(314, 155);
			this.down20.Name = "down20";
			this.down20.Size = new System.Drawing.Size(40, 40);
			this.down20.TabIndex = 51;
			this.down20.Tag = "7";
			this.down20.Click += new System.EventHandler(this.down20_Click);
			// 
			// button3
			// 
			this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
			this.button3.Location = new System.Drawing.Point(268, 155);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(40, 40);
			this.button3.TabIndex = 50;
			this.button3.Tag = "6";
			// 
			// left19
			// 
			this.left19.Image = ((System.Drawing.Image)(resources.GetObject("left19.Image")));
			this.left19.Location = new System.Drawing.Point(268, 107);
			this.left19.Name = "left19";
			this.left19.Size = new System.Drawing.Size(40, 40);
			this.left19.TabIndex = 49;
			this.left19.Tag = "4";
			this.left19.Click += new System.EventHandler(this.left19_Click);
			// 
			// right19
			// 
			this.right19.Image = ((System.Drawing.Image)(resources.GetObject("right19.Image")));
			this.right19.Location = new System.Drawing.Point(360, 107);
			this.right19.Name = "right19";
			this.right19.Size = new System.Drawing.Size(40, 40);
			this.right19.TabIndex = 48;
			this.right19.Tag = "5";
			this.right19.Click += new System.EventHandler(this.right19_Click);
			// 
			// button6
			// 
			this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
			this.button6.Location = new System.Drawing.Point(360, 61);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(40, 40);
			this.button6.TabIndex = 47;
			this.button6.Tag = "3";
			// 
			// up20
			// 
			this.up20.Image = ((System.Drawing.Image)(resources.GetObject("up20.Image")));
			this.up20.Location = new System.Drawing.Point(314, 61);
			this.up20.Name = "up20";
			this.up20.Size = new System.Drawing.Size(40, 40);
			this.up20.TabIndex = 46;
			this.up20.Tag = "1";
			this.up20.Click += new System.EventHandler(this.up20_Click);
			// 
			// button8
			// 
			this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
			this.button8.Location = new System.Drawing.Point(268, 61);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(40, 40);
			this.button8.TabIndex = 45;
			this.button8.Tag = "2";
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.connectionMenu,
            this.commandMenu});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(459, 24);
			this.mainMenu.TabIndex = 55;
			// 
			// fileMenu
			// 
			this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
			this.fileMenu.Name = "fileMenu";
			this.fileMenu.Size = new System.Drawing.Size(35, 20);
			this.fileMenu.Text = "File";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitMenuItem.Text = "Exit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// connectionMenu
			// 
			this.connectionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectMenuItem});
			this.connectionMenu.Name = "connectionMenu";
			this.connectionMenu.Size = new System.Drawing.Size(73, 20);
			this.connectionMenu.Text = "Connection";
			// 
			// connectMenuItem
			// 
			this.connectMenuItem.Name = "connectMenuItem";
			this.connectMenuItem.Size = new System.Drawing.Size(152, 22);
			this.connectMenuItem.Text = "Listen";
			this.connectMenuItem.Click += new System.EventHandler(this.connectMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 404);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(459, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 56;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(63, 17);
			this.statusLabel.Text = "Status: Idle";
			// 
			// messageBox
			// 
			this.messageBox.BackColor = System.Drawing.Color.White;
			this.messageBox.Location = new System.Drawing.Point(12, 314);
			this.messageBox.Multiline = true;
			this.messageBox.Name = "messageBox";
			this.messageBox.ReadOnly = true;
			this.messageBox.Size = new System.Drawing.Size(436, 87);
			this.messageBox.TabIndex = 57;
			// 
			// commandMenu
			// 
			this.commandMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setServoPositionMenuItem,
            this.centerAllMenuItem});
			this.commandMenu.Name = "commandMenu";
			this.commandMenu.Size = new System.Drawing.Size(66, 20);
			this.commandMenu.Text = "Command";
			// 
			// setServoPositionMenuItem
			// 
			this.setServoPositionMenuItem.Name = "setServoPositionMenuItem";
			this.setServoPositionMenuItem.Size = new System.Drawing.Size(187, 22);
			this.setServoPositionMenuItem.Text = "Set Servo Position ...";
			this.setServoPositionMenuItem.Click += new System.EventHandler(this.setServoPositionMenuItem_Click);
			// 
			// centerAllMenuItem
			// 
			this.centerAllMenuItem.Name = "centerAllMenuItem";
			this.centerAllMenuItem.Size = new System.Drawing.Size(187, 22);
			this.centerAllMenuItem.Text = "Center All Servos";
			this.centerAllMenuItem.Click += new System.EventHandler(this.centerAllMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(459, 426);
			this.Controls.Add(this.messageBox);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.bar20);
			this.Controls.Add(this.bar19);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.down20);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.left19);
			this.Controls.Add(this.right19);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.up20);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.bar16);
			this.Controls.Add(this.bar17);
			this.Controls.Add(this.bar18);
			this.Controls.Add(this.left18);
			this.Controls.Add(this.right18);
			this.Controls.Add(this.backRightButton);
			this.Controls.Add(this.down16);
			this.Controls.Add(this.backLeftButton);
			this.Controls.Add(this.left17);
			this.Controls.Add(this.right17);
			this.Controls.Add(this.forwardRightButton);
			this.Controls.Add(this.up16);
			this.Controls.Add(this.forwardLeftButton);
			this.Controls.Add(this.mainMenu);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.mainMenu;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ViLAN Servo Controller";
			((System.ComponentModel.ISupportInitialize)(this.bar18)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar17)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar16)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar20)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bar19)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button left18;
		private System.Windows.Forms.Button right18;
		private System.Windows.Forms.TrackBar bar18;
		private System.Windows.Forms.Button forwardLeftButton;
		private System.Windows.Forms.Button up16;
		private System.Windows.Forms.Button forwardRightButton;
		private System.Windows.Forms.Button right17;
		private System.Windows.Forms.Button left17;
		private System.Windows.Forms.Button backLeftButton;
		private System.Windows.Forms.Button down16;
		private System.Windows.Forms.Button backRightButton;
		private System.Windows.Forms.TrackBar bar17;
		private System.Windows.Forms.TrackBar bar16;
		private System.Windows.Forms.TrackBar bar20;
		private System.Windows.Forms.TrackBar bar19;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button down20;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button left19;
		private System.Windows.Forms.Button right19;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button up20;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectionMenu;
		private System.Windows.Forms.ToolStripMenuItem connectMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.TextBox messageBox;
		private System.Windows.Forms.ToolStripMenuItem commandMenu;
		private System.Windows.Forms.ToolStripMenuItem setServoPositionMenuItem;
		private System.Windows.Forms.ToolStripMenuItem centerAllMenuItem;
	}
}

