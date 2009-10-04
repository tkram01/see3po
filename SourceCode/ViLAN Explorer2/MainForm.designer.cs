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
			this.drivePanel = new System.Windows.Forms.Panel();
			this.statusStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 617);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1066, 22);
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
			this.menuStrip1.Size = new System.Drawing.Size(1066, 24);
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
			this.livePanel.Size = new System.Drawing.Size(730, 480);
			this.livePanel.TabIndex = 25;
			// 
			// messageBox
			// 
			this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.messageBox.BackColor = System.Drawing.Color.White;
			this.messageBox.Location = new System.Drawing.Point(12, 517);
			this.messageBox.Multiline = true;
			this.messageBox.Name = "messageBox";
			this.messageBox.ReadOnly = true;
			this.messageBox.Size = new System.Drawing.Size(1042, 87);
			this.messageBox.TabIndex = 58;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(754, 333);
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
			this.button2.Location = new System.Drawing.Point(787, 333);
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
			this.button3.Location = new System.Drawing.Point(820, 333);
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
			this.button4.Location = new System.Drawing.Point(754, 363);
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
			this.button5.Location = new System.Drawing.Point(787, 363);
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
			this.button6.Location = new System.Drawing.Point(820, 363);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(27, 24);
			this.button6.TabIndex = 64;
			this.button6.Tag = "6";
			this.button6.Text = "6";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button1_Click);
			// 
			// drivePanel
			// 
			this.drivePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.drivePanel.Location = new System.Drawing.Point(754, 27);
			this.drivePanel.Name = "drivePanel";
			this.drivePanel.Size = new System.Drawing.Size(300, 300);
			this.drivePanel.TabIndex = 65;
			this.drivePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drivePanel_MouseMove);
			this.drivePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drivePanel_MouseDown);
			this.drivePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drivePanel_MouseUp);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1066, 639);
			this.Controls.Add(this.drivePanel);
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
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ViLAN Explorer";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

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
		private System.Windows.Forms.Panel drivePanel;

	}
}

