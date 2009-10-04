namespace Remote_Servo_Controller
{
	partial class ServoPositionDialog
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
			this.setButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.setLabel = new System.Windows.Forms.Label();
			this.servoList = new System.Windows.Forms.ComboBox();
			this.toLabel = new System.Windows.Forms.Label();
			this.positionBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// setButton
			// 
			this.setButton.Location = new System.Drawing.Point(69, 55);
			this.setButton.Name = "setButton";
			this.setButton.Size = new System.Drawing.Size(75, 23);
			this.setButton.TabIndex = 0;
			this.setButton.Text = "Set";
			this.setButton.UseVisualStyleBackColor = true;
			this.setButton.Click += new System.EventHandler(this.setButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(150, 55);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// setLabel
			// 
			this.setLabel.AutoSize = true;
			this.setLabel.Location = new System.Drawing.Point(12, 9);
			this.setLabel.Name = "setLabel";
			this.setLabel.Size = new System.Drawing.Size(23, 13);
			this.setLabel.TabIndex = 2;
			this.setLabel.Text = "Set";
			// 
			// servoList
			// 
			this.servoList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.servoList.FormattingEnabled = true;
			this.servoList.Items.AddRange(new object[] {
            "Servo16",
            "Servo17",
            "Servo18",
            "Servo19",
            "Servo20"});
			this.servoList.Location = new System.Drawing.Point(41, 6);
			this.servoList.Name = "servoList";
			this.servoList.Size = new System.Drawing.Size(71, 21);
			this.servoList.TabIndex = 3;
			this.servoList.SelectedIndexChanged += new System.EventHandler(this.servoList_SelectedIndexChanged);
			// 
			// toLabel
			// 
			this.toLabel.AutoSize = true;
			this.toLabel.Location = new System.Drawing.Point(118, 9);
			this.toLabel.Name = "toLabel";
			this.toLabel.Size = new System.Drawing.Size(16, 13);
			this.toLabel.TabIndex = 4;
			this.toLabel.Text = "to";
			// 
			// positionBox
			// 
			this.positionBox.Location = new System.Drawing.Point(140, 6);
			this.positionBox.Name = "positionBox";
			this.positionBox.Size = new System.Drawing.Size(85, 20);
			this.positionBox.TabIndex = 5;
			this.positionBox.TextChanged += new System.EventHandler(this.positionBox_TextChanged);
			// 
			// ServoPositionDialog
			// 
			this.AcceptButton = this.setButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(237, 90);
			this.Controls.Add(this.positionBox);
			this.Controls.Add(this.toLabel);
			this.Controls.Add(this.servoList);
			this.Controls.Add(this.setLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.setButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServoPositionDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Set Servo Position";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button setButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label setLabel;
		private System.Windows.Forms.ComboBox servoList;
		private System.Windows.Forms.Label toLabel;
		private System.Windows.Forms.TextBox positionBox;
	}
}