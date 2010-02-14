namespace ViLAN_Explorer
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
			this.stopButton = new System.Windows.Forms.Button();
			this.backRightButton = new System.Windows.Forms.Button();
			this.backButton = new System.Windows.Forms.Button();
			this.backLeftButton = new System.Windows.Forms.Button();
			this.turnLeftButton = new System.Windows.Forms.Button();
			this.turnRightButton = new System.Windows.Forms.Button();
			this.forwardRightButton = new System.Windows.Forms.Button();
			this.forwardButton = new System.Windows.Forms.Button();
			this.forwardLeftButton = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuDivider1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectionMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.connectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.livePanel = new System.Windows.Forms.Panel();
			this.messageBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.speedBar = new System.Windows.Forms.TrackBar();
			this.statusStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
			this.SuspendLayout();
			// 
			// stopButton
			// 
			this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
			this.stopButton.Location = new System.Drawing.Point(793, 75);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(40, 40);
			this.stopButton.TabIndex = 22;
			this.stopButton.Tag = "0";
			// 
			// backRightButton
			// 
			this.backRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.backRightButton.Image = ((System.Drawing.Image)(resources.GetObject("backRightButton.Image")));
			this.backRightButton.Location = new System.Drawing.Point(841, 123);
			this.backRightButton.Name = "backRightButton";
			this.backRightButton.Size = new System.Drawing.Size(40, 40);
			this.backRightButton.TabIndex = 21;
			this.backRightButton.Tag = "8";
			this.backRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.backRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// backButton
			// 
			this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
			this.backButton.Location = new System.Drawing.Point(793, 123);
			this.backButton.Name = "backButton";
			this.backButton.Size = new System.Drawing.Size(40, 40);
			this.backButton.TabIndex = 20;
			this.backButton.Tag = "6";
			this.backButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.backButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// backLeftButton
			// 
			this.backLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.backLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("backLeftButton.Image")));
			this.backLeftButton.Location = new System.Drawing.Point(745, 123);
			this.backLeftButton.Name = "backLeftButton";
			this.backLeftButton.Size = new System.Drawing.Size(40, 40);
			this.backLeftButton.TabIndex = 19;
			this.backLeftButton.Tag = "7";
			this.backLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.backLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// turnLeftButton
			// 
			this.turnLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.turnLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("turnLeftButton.Image")));
			this.turnLeftButton.Location = new System.Drawing.Point(745, 75);
			this.turnLeftButton.Name = "turnLeftButton";
			this.turnLeftButton.Size = new System.Drawing.Size(40, 40);
			this.turnLeftButton.TabIndex = 18;
			this.turnLeftButton.Tag = "4";
			this.turnLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.turnLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// turnRightButton
			// 
			this.turnRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.turnRightButton.Image = ((System.Drawing.Image)(resources.GetObject("turnRightButton.Image")));
			this.turnRightButton.Location = new System.Drawing.Point(841, 75);
			this.turnRightButton.Name = "turnRightButton";
			this.turnRightButton.Size = new System.Drawing.Size(40, 40);
			this.turnRightButton.TabIndex = 17;
			this.turnRightButton.Tag = "5";
			this.turnRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.turnRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// forwardRightButton
			// 
			this.forwardRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.forwardRightButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardRightButton.Image")));
			this.forwardRightButton.Location = new System.Drawing.Point(841, 27);
			this.forwardRightButton.Name = "forwardRightButton";
			this.forwardRightButton.Size = new System.Drawing.Size(40, 40);
			this.forwardRightButton.TabIndex = 16;
			this.forwardRightButton.Tag = "3";
			this.forwardRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.forwardRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// forwardButton
			// 
			this.forwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
			this.forwardButton.Location = new System.Drawing.Point(793, 27);
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new System.Drawing.Size(40, 40);
			this.forwardButton.TabIndex = 15;
			this.forwardButton.Tag = "1";
			this.forwardButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.forwardButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// forwardLeftButton
			// 
			this.forwardLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.forwardLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardLeftButton.Image")));
			this.forwardLeftButton.Location = new System.Drawing.Point(745, 27);
			this.forwardLeftButton.Name = "forwardLeftButton";
			this.forwardLeftButton.Size = new System.Drawing.Size(40, 40);
			this.forwardLeftButton.TabIndex = 14;
			this.forwardLeftButton.Tag = "2";
			this.forwardLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.driveButton_Click);
			this.forwardLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backRightButton_MouseUp);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 621);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(892, 22);
			this.statusStrip.TabIndex = 23;
			this.statusStrip.Text = "statusStrip";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(60, 17);
			this.statusLabel.Text = "status: idle";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.connectionMenu});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(892, 24);
			this.menuStrip1.TabIndex = 24;
			this.menuStrip1.Text = "mainMenu";
			// 
			// fileMenu
			// 
			this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDivider1,
            this.exitMenuItem});
			this.fileMenu.Name = "fileMenu";
			this.fileMenu.Size = new System.Drawing.Size(35, 20);
			this.fileMenu.Text = "File";
			// 
			// menuDivider1
			// 
			this.menuDivider1.Name = "menuDivider1";
			this.menuDivider1.Size = new System.Drawing.Size(100, 6);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Size = new System.Drawing.Size(103, 22);
			this.exitMenuItem.Text = "Exit";
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
			this.connectMenuItem.Size = new System.Drawing.Size(113, 22);
			this.connectMenuItem.Text = "Listen";
			this.connectMenuItem.Click += new System.EventHandler(this.connectMenuItem_Click);
			// 
			// livePanel
			// 
			this.livePanel.Location = new System.Drawing.Point(12, 27);
			this.livePanel.Name = "livePanel";
			this.livePanel.Size = new System.Drawing.Size(720, 480);
			this.livePanel.TabIndex = 25;
			// 
			// messageBox
			// 
			this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.messageBox.BackColor = System.Drawing.Color.White;
			this.messageBox.Location = new System.Drawing.Point(12, 521);
			this.messageBox.Multiline = true;
			this.messageBox.Name = "messageBox";
			this.messageBox.ReadOnly = true;
			this.messageBox.Size = new System.Drawing.Size(869, 87);
			this.messageBox.TabIndex = 58;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(745, 169);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(27, 24);
			this.button1.TabIndex = 59;
			this.button1.Tag = "1";
			this.button1.Text = "1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(778, 169);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(27, 24);
			this.button2.TabIndex = 60;
			this.button2.Tag = "2";
			this.button2.Text = "2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button1_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(811, 169);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(27, 24);
			this.button3.TabIndex = 61;
			this.button3.Tag = "3";
			this.button3.Text = "3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button1_Click);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(745, 199);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(27, 24);
			this.button4.TabIndex = 62;
			this.button4.Tag = "4";
			this.button4.Text = "4";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button1_Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Location = new System.Drawing.Point(778, 199);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(27, 24);
			this.button5.TabIndex = 63;
			this.button5.Tag = "5";
			this.button5.Text = "5";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button1_Click);
			// 
			// button6
			// 
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button6.Location = new System.Drawing.Point(811, 199);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(27, 24);
			this.button6.TabIndex = 64;
			this.button6.Tag = "6";
			this.button6.Text = "6";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button1_Click);
			// 
			// speedBar
			// 
			this.speedBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.speedBar.LargeChange = 50;
			this.speedBar.Location = new System.Drawing.Point(844, 169);
			this.speedBar.Maximum = 1000;
			this.speedBar.Minimum = 100;
			this.speedBar.Name = "speedBar";
			this.speedBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.speedBar.Size = new System.Drawing.Size(42, 104);
			this.speedBar.TabIndex = 65;
			this.speedBar.TickFrequency = 50;
			this.speedBar.Value = 350;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 643);
			this.Controls.Add(this.speedBar);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.messageBox);
			this.Controls.Add(this.livePanel);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.backRightButton);
			this.Controls.Add(this.backButton);
			this.Controls.Add(this.backLeftButton);
			this.Controls.Add(this.turnLeftButton);
			this.Controls.Add(this.turnRightButton);
			this.Controls.Add(this.forwardRightButton);
			this.Controls.Add(this.forwardButton);
			this.Controls.Add(this.forwardLeftButton);
			this.Location = new System.Drawing.Point(1, 1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ViLAN Explorer";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Button backRightButton;
		private System.Windows.Forms.Button backButton;
		private System.Windows.Forms.Button backLeftButton;
		private System.Windows.Forms.Button turnLeftButton;
		private System.Windows.Forms.Button turnRightButton;
		private System.Windows.Forms.Button forwardRightButton;
		private System.Windows.Forms.Button forwardButton;
		private System.Windows.Forms.Button forwardLeftButton;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripSeparator menuDivider1;
		private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
		private System.Windows.Forms.Panel livePanel;
		private System.Windows.Forms.ToolStripMenuItem connectionMenu;
		private System.Windows.Forms.ToolStripMenuItem connectMenuItem;
		private System.Windows.Forms.TextBox messageBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.TrackBar speedBar;

	}
}

