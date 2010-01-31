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

        private Stack<Image> m_Images;
        private FloorPlan m_FloorPlan;
        private List<MoveCommand> m_Moves;
        private Position m_Position;

        public Status(Image floorPlan, double scale){
            m_FloorPlan = new FloorPlan(floorPlan, scale);
            m_Moves = new List<MoveCommand>();
            m_Images = new Stack<Image>(IMG_CAP);
            m_Moves = new List<MoveCommand>();
        }


        public Image nextImage
        {
            get { return m_Images.Pop(); }
            set { m_Images.Push(value); }
        }

        public void clearImages() {
            m_Images = new Stack<Image>(IMG_CAP);
        }

        public List<MoveCommand> path
        {
            get { return m_Moves; }
            set { m_Moves = value; }
        }

        public Position position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public FloorPlan floorPlan {
            get { return m_FloorPlan; }
        }
    }
}
