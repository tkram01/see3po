using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
    public class MoveCommand
    {
        public enum Direction {Forward, CW, CCW}

        public Direction direction;
        public int distance;

        public MoveCommand(Direction direction, int distance) 
        {
            this.direction = direction;
            this.distance = distance;
        }

        public String toString() 
        {
            //String dir;
            //switch (direction)
            //{
            //    case Forward: 
            //}

            return direction + " " + distance;

        }
    }
}
