using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class FacingForm : Form
    {
        MainForm m_parent;

        public FacingForm(MainForm parent)
        {
            InitializeComponent();
            facingbox.Text = parent.Host.Status.Position.facing.ToString();
            m_parent = parent;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            int facing;
            int.TryParse(facingbox.Text, out facing);
            m_parent.Host.Status.Position.facing = facing;
            this.Close();
        }

    }
}
