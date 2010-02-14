using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Host
{
    public partial class PathForm : Form
    {
        MainForm m_parent;
        List<FloorTile> m_path;


        public PathForm(MainForm parent)
        {
            InitializeComponent();

            m_parent = parent;

            if (m_parent.status != null)
            {
                TileList.DataSource = m_parent.status.path;
            }
        }

        private void TileList_Click(object sender, EventArgs e)
        {
            m_parent.highlightPoint(m_parent.status.path[TileList.SelectedIndex].Position);
            
        }

        private void TileList_DoubleClick(object sender, EventArgs e)
        {
            if (m_parent.status.path != null)
                m_parent.status.path.RemoveRange(0, TileList.SelectedIndex);

            m_parent.MoveRobot(m_parent.status.path[0].Position, m_parent.status.position.facing);

            m_parent.DrawFloor();

            TileList.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }







    }
}
