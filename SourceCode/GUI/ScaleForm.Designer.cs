namespace See3PO
{
    partial class ScaleForm
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
            this.pixelBox = new System.Windows.Forms.MaskedTextBox();
            this.feetBox = new System.Windows.Forms.MaskedTextBox();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.DrawButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.scaleText = new System.Windows.Forms.Label();
            this.scaleBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // pixelBox
            // 
            this.pixelBox.Location = new System.Drawing.Point(12, 12);
            this.pixelBox.Name = "pixelBox";
            this.pixelBox.Size = new System.Drawing.Size(75, 20);
            this.pixelBox.TabIndex = 0;
            this.pixelBox.Validated += new System.EventHandler(this.pixels_edit);
            this.pixelBox.TextChanged += new System.EventHandler(this.pixels_edit);
            // 
            // feetBox
            // 
            this.feetBox.Location = new System.Drawing.Point(12, 52);
            this.feetBox.Name = "feetBox";
            this.feetBox.Size = new System.Drawing.Size(75, 20);
            this.feetBox.TabIndex = 1;
            this.feetBox.Validated += new System.EventHandler(this.feet_edit);
            this.feetBox.TextChanged += new System.EventHandler(this.feet_edit);
            // 
            // AcceptButton
            // 
            this.AcceptButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AcceptButton.Location = new System.Drawing.Point(12, 105);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptButton.TabIndex = 2;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(105, 105);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DrawButton
            // 
            this.DrawButton.Location = new System.Drawing.Point(131, 10);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(49, 23);
            this.DrawButton.TabIndex = 4;
            this.DrawButton.Text = "Draw";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "pixels";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "feet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "equal";
            // 
            // scaleText
            // 
            this.scaleText.AutoSize = true;
            this.scaleText.Location = new System.Drawing.Point(92, 83);
            this.scaleText.Name = "scaleText";
            this.scaleText.Size = new System.Drawing.Size(51, 13);
            this.scaleText.TabIndex = 9;
            this.scaleText.Text = "feet/pixel";
            // 
            // scaleBox
            // 
            this.scaleBox.BackColor = System.Drawing.SystemColors.Control;
            this.scaleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scaleBox.Location = new System.Drawing.Point(12, 82);
            this.scaleBox.Name = "scaleBox";
            this.scaleBox.Size = new System.Drawing.Size(75, 13);
            this.scaleBox.TabIndex = 10;
            // 
            // ScaleForm
            // 
            this.AcceptButton = this.AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(186, 140);
            this.Controls.Add(this.scaleBox);
            this.Controls.Add(this.scaleText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.feetBox);
            this.Controls.Add(this.pixelBox);
            this.Name = "ScaleForm";
            this.Text = "Scale";
            this.Load += new System.EventHandler(this.Scale_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox pixelBox;
        private System.Windows.Forms.MaskedTextBox feetBox;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label scaleText;
        private System.Windows.Forms.MaskedTextBox scaleBox;
    }
}