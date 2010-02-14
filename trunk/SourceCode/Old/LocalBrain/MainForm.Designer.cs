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
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.messageBox = new System.Windows.Forms.TextBox();
			this.servosConnectMenuItem = new System.Windows.Forms.MenuItem();
			this.motorsConnectMenuItem = new System.Windows.Forms.MenuItem();
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
			this.messageBox.Location = new System.Drawing.Point(0, 331);
			this.messageBox.Multiline = true;
			this.messageBox.Name = "messageBox";
			this.messageBox.ReadOnly = true;
			this.messageBox.Size = new System.Drawing.Size(638, 100);
			this.messageBox.TabIndex = 1;
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(638, 455);
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
	}
}

