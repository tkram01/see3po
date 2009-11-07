using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace See3PO
{
    public class Status
    {
        private const int IMG_CAP = 10;

        private Stack<Image> _Images;
        private FloorPlan _FloorPlan;
        private Queue<MoveCommand> _Moves;
        private Position _Position;

        public Status(Image floorPlan){
            _FloorPlan = new FloorPlan(floorPlan);
            _Images = new Stack<Image>(IMG_CAP);
            _Moves = new Queue<MoveCommand>();
        }

        public Image nextImage
        {
            get { return _Images.Pop(); }
            set { _Images.Push(value); }
        }

        public void clearImages() {
            _Images = new Stack<Image>(IMG_CAP);
        }

        public MoveCommand nextMove
        {
            get { return _Moves.Dequeue(); }
            set { _Moves.Enqueue(value); }
        }



        public Queue<MoveCommand> path
        {
            get { return _Moves; }
            set { _Moves = value; }
        }

        public Position position
        {
            get { return _Position; }
            set { _Position = value; }
        }

    }
}
