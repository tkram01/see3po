using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FloorPlanAndTile;
using See3PO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        FloorPlan fp;
        List<FloorTile> path;

        public Form1()
        {
            InitializeComponent();

        }
        public Form1(FloorPlan fp, List<FloorTile> path)
        {
            InitializeComponent();
            this.fp = fp;
            this.path = path;
            this.Draw();
        }

        public void Draw()
        {
            drawFloorPlan(this.fp);
            drawTile(this.fp.getStartTile());
            drawTile(this.fp.getTargetTile());
            drawPath(this.path);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Draw();
            //Image myimage = new Bitmap("../../testImage.jpg");
            //FloorPlan fp = new FloorPlan(myimage, 5);

            //drawFloorPlan(fp);

            //fp.setStartTile(8, 8);
            //fp.setTargetTile(20, 20);
            //drawTile(fp.getStartTile());
            //drawTile(fp.getTargetTile());

            //QGPathFinder pf = new QGPathFinder(fp);
            //List<FloorTile> path = pf.getPath();

            //drawPath(path);

        }

        private void drawFloorPlan(FloorPlan fp)
        {
            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = 0; j < fp.getYTileNum(); j++)
                {
                    drawTile(fp.getTile(i, j));
                }
            }
        }

        private void drawPath(List<FloorTile> path)
        {
            if (path != null)
            {
                for (int i = 1; i < path.Count - 1; i++)
                {
                    //MessageBox.Show(path[i].Position.X + "," + path[i].Position.Y);
                    drawTile(path[i], true);
                }
            }
        }

        private void drawTile(FloorTile ft)
        {
            drawTile(ft, false);
        }

        private void drawTile(FloorTile ft, bool IsOnPath)
        {
            int PixelOfSide = 10;

            System.Drawing.SolidBrush myBrush;
            System.Drawing.SolidBrush myBrushS = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            System.Drawing.SolidBrush myBrushT = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush myBrushW = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.SolidBrush myBrushB = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.SolidBrush myBrushP = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);

            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            int TopLeftX = ft.Position.X * PixelOfSide;
            int TopLeftY = ft.Position.Y * PixelOfSide;
            int BottomRightX = TopLeftX + PixelOfSide;
            int BottomRightY = TopLeftY + PixelOfSide;
            //MessageBox.Show(TopLeftX + "," + TopLeftY + ";" + BottomRightX + "," + BottomRightY);

            if (ft.Iswalkable())
            {
                myBrush = myBrushW;
            }
            else
            {
                myBrush = myBrushB;
            }

            if (ft.IsStart())
            {
                myBrush = myBrushS;
            }
            else if (ft.IsTarget())
            {
                myBrush = myBrushT;
            }

            if (IsOnPath)
            {
                myBrush = myBrushP;
            }

            formGraphics.FillRectangle(myBrush, TopLeftX, TopLeftY, PixelOfSide, PixelOfSide);
            
            //draw bolder
            System.Drawing.Pen myPen;
            System.Drawing.Pen myPenB = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.Pen myPenW = new System.Drawing.Pen(System.Drawing.Color.White);
            if (ft.Iswalkable())
            {
                myPen = myPenB;
            }
            else
            {
                myPen = myPenW;
            }

            formGraphics.DrawRectangle(myPen, TopLeftX, TopLeftY, PixelOfSide, PixelOfSide);
            
            myBrush.Dispose();
            formGraphics.Dispose();

        }
    }
}
