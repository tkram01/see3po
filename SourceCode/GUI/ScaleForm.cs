using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace See3PO
{
    public partial class ScaleForm : Form
    {
        double m_pixels;
        double m_feet;
        public double m_scale;
        MainForm m_mainform;

        public ScaleForm(double pixels, double feet, MainForm mainform)
        {
            InitializeComponent();
            m_pixels = pixels;
            m_feet = feet;
            this.pixelBox.Text = m_pixels.ToString();
            this.feetBox.Text = m_feet.ToString();
            m_mainform = mainform;
        }

        private void Scale_Load(object sender, EventArgs e)
        {

        }

        private void pixels_edit(object sender, EventArgs e)
        {
            double.TryParse(pixelBox.Text, out m_pixels);
            m_scale = m_pixels / m_feet;
            scaleBox.Text = "" +  m_scale + " pixels/foot";

        }

        private void feet_edit(object sender, EventArgs e)
        {
            double.TryParse(feetBox.Text, out m_feet);
            m_scale = m_pixels / m_feet;
            scaleBox.Text = "" + m_scale + " pixels/foot";
        }

        private void DrawButton_click(object sender, EventArgs e)
        {
            m_mainform.m_fpState = MainForm.fpState.IMAGE;
            m_mainform.DrawFloor();
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {

            double.TryParse(pixelBox.Text, out m_pixels);
            double.TryParse(feetBox.Text, out m_feet);
            m_scale = m_pixels / m_feet;
            m_mainform.m_pixelsperfoot = m_scale;
        }







    }
}
