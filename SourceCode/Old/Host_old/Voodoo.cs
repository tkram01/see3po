﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Host
{
    class Voodoo
    {
        private Position CPosition;
        public Position position {
            get { return position; }
        }

        public Voodoo(Position position){
            CPosition = position; 
        }
    }


}
