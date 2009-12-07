using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    class RobotSprite
    {

        private const double degToRads = Math.PI / 180;

        private Image m_originalImage;
        private Image m_resizedImage;
        private Image m_image;
        public Image image { get { return m_image; } }

        private int m_pixelsPerFoot;
        public int pixelsPerFoot
        {
            get { return m_pixelsPerFoot; }
            set 
            {
                m_pixelsPerFoot = toPixels(value);
                m_resizedImage = new Bitmap(m_originalImage, new Size(m_pixelsPerFoot, m_pixelsPerFoot));
                m_image = rotateSprite();
            }
        }

        private Position m_position;
        public Position position 
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public RobotSprite(Image sprite, double pixelsPerFoot, Position position)
        {
            m_pixelsPerFoot = toPixels(pixelsPerFoot);
            m_position = position;
            
            m_originalImage = new Bitmap(sprite);
            m_resizedImage = new Bitmap(sprite, new Size(m_pixelsPerFoot, m_pixelsPerFoot));
            m_image = new Bitmap(m_resizedImage);
        }



        public void move(int leftSpeed, int rightSpeed) {

            int speedDiff = (int)((leftSpeed - rightSpeed) * 2.5);
            if (speedDiff != 0)
                changeFacing(speedDiff);

            int distance = (int)((leftSpeed + rightSpeed) * 2.5);
            if (distance != 0)
            {
                int yDist = -(int)(distance * Math.Sin((float)m_position.facing * degToRads));
                int xDist = (int)(distance * Math.Cos((float)m_position.facing * degToRads));

                m_position.location.Y += yDist;
                m_position.location.X += xDist;
            }
        }

        private void changeFacing(int change) {
            m_position.facing = m_position.facing + change;
            m_image = rotateSprite();
        }

        private Bitmap rotateSprite()
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(m_resizedImage.Width, m_resizedImage.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)m_resizedImage.Width / 2, (float)m_resizedImage.Height / 2);
            //rotate
            g.RotateTransform(-m_position.facing);
            //move image back
            g.TranslateTransform(-(float)m_resizedImage.Width / 2, -(float)m_resizedImage.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(m_resizedImage, new Point(0, 0));
            return returnBitmap;
        }


        private int toPixels(double x) {
            if (x < 1)
                return 1;
            else
                return (int)(x + 0.5);
        }




    }
}
