using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathPlaning
{
    class Tile
    {
        int x, y;
        String value;

        public Tile(int x, int y, String value){
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public override String ToString()
        {
            return this.value;
        }
    }
}
