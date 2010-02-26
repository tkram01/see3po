using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FloorTile = See3PO.FloorTile;
using Position = See3PO.Position;
using Locator = See3PO.Locator;

namespace GUI
{
    public partial class PathForm : Form, Locator
    {
        MainForm m_parent;


        public PathForm(MainForm parent)
        {
            InitializeComponent();

            m_parent = parent;

            if (m_parent.Status != null)
            {
                TileList.DataSource = m_parent.Status.Path;
            }
        }

        private void TileList_Click(object sender, EventArgs e)
        {
            m_parent.highlightPoint(m_parent.Status.Path[TileList.SelectedIndex].Position);
        }

        private void TileList_DoubleClick(object sender, EventArgs e)
        {
            if (m_parent.Status.Path != null)
                m_parent.Status.Path.RemoveRange(0, TileList.SelectedIndex);

            TileList.Refresh();
        }




        public List<FloorTile> condenseList(List<FloorTile> path)
        {
            List<FloorTile> condensedList = new List<FloorTile>();

            FloorTile lastTile = path[0];

            condensedList.Add(path[0]);

            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].Position.Y == lastTile.Position.Y) // moving vertically
                {
                    while (i < path.Count && path[i].Position.Y == lastTile.Position.Y && i < path.Count - 1) // walk until the next turn
                    {
                        i++;
                    }
                }
                else                                           // moving horizontally
                {
                    while (i < path.Count && path[i].Position.X == lastTile.Position.X) // walk until the next turn
                    {
                        i++;
                    }
                }
                lastTile = path[i - 1];
                condensedList.Add(path[i - 1]);                 // add the turning point to the new list;
            }

            return condensedList;
        }

        public Position GetPosition (Image[] images)
        {
            return new Position(m_parent.Status.Path[0].Position, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            m_parent.Host.Locator = null;                       // Before closing, set the Locator to null (maybe set to a default later)
        }
    }
}
