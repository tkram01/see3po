using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Remote_Servo_Controller
{
	public partial class ServoPositionDialog : Form
	{
		private int m_iServoNumber;
		private int m_iPosition;
		private string m_sLastPosition;
		
		public int ServoNumber
		{
			get { return m_iServoNumber; }
		}
		public int Position
		{
			get { return m_iPosition; }
		}

		public ServoPositionDialog()
		{
			InitializeComponent();

			m_sLastPosition = "1500";
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void setButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void positionBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				m_iPosition = int.Parse(positionBox.Text);
				m_sLastPosition = positionBox.Text;
			}
			catch(Exception)
			{
				MessageBox.Show(this, "Positions are integers between 900 and 2100.", "Bad Position Entered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				positionBox.Text = m_sLastPosition;
				positionBox.SelectAll();
			}
		}

		private void servoList_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_iServoNumber = servoList.SelectedIndex + 16;
		}
	}

}