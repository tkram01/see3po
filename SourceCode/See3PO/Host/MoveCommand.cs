/*
$license$
$Rev$
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace See3PO
{
    public class MoveCommand
    {
        public enum Direction {Forward, CW, CCW}

        public Direction direction;
        public int duration;

        public MoveCommand(Direction direction, int duration) 
        {
            this.direction = direction;
            this.duration = duration;
        }

        public String toString() 
        {
            //String dir;
            //switch (direction)
            //{
            //    case Forward: 
            //}

            return direction + " " + duration;

        }
    }
}
