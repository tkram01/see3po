using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PathPlaning
{
    class Tile
    {
        String Value;
        private Point Location;
        private FloorPlan FloorPlan;

        public Tile(int x, int y, String value){
            Location.X = x;
            Location.Y = y;
            this.Value = value;
        }

        public override String ToString()
        {
            return this.Value;
        }
    }
}
