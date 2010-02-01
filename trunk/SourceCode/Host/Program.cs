using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace See3PO
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            //Image myimage = new Bitmap("testImage.jpg"); // TODO: Initialize to an appropriate value
            //FloorPlan fp = new FloorPlan(myimage); ; // TODO: Initialize to an appropriate value
            //fp.setStartTile(3, 3);
            //fp.setTargetTile(5, 5);

            //QGPathFinder target = new QGPathFinder(fp); // TODO: Initialize to an appropriate value

            //List<FloorTile> expected = null; // TODO: Initialize to an appropriate value
            //List<FloorTile> actual;
            //actual = target.getPath();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());


		}
	}
}
