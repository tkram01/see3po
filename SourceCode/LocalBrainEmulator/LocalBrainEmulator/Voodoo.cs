using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LocalBrainEmulator
{
    class Voodoo
    {
        private Point CLocation;
        public Point location {
            get { return CLocation; }
        }

        public Voodoo(int x, int y){
            CLocation = new Point(x, y); 
        }
    }


}
