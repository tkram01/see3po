using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace See3PO
{
    class RobotSprite
    {
        private const int HEIGHT = 20;
        private const int WIDTH = 20;

        private const double degToRads = Math.PI / 180;

        private Image c_originalImage;
        private Image c_image;
        public Image image { get { return c_image; } }

        private Position c_position;
        public Position position 
        {
            get { return c_position; }
            set { c_position = value; }
        }

        public RobotSprite(Image sprite, Position position) {
            c_position = position;
            c_image = new Bitmap(sprite);
            c_originalImage = new Bitmap(sprite);

        }

        public void move(int leftSpeed, int rightSpeed) {

            int speedDiff = (int)((leftSpeed - rightSpeed) * 2.5);
            if (speedDiff != 0)
                changeFacing(speedDiff);

            int distance = (int)((leftSpeed + rightSpeed) * 2.5);
            if (distance != 0)
            {
                int yDist = -(int)(distance * Math.Sin((float)c_position.facing * degToRads));
                int xDist = (int)(distance * Math.Cos((float)c_position.facing * degToRads));

                c_position.location.Y += yDist;
                c_position.location.X += xDist;
            }
        }

        private void changeFacing(int change) {
            c_position.facing = c_position.facing + change;
             c_image = rotateSprite();
        }

        private Bitmap rotateSprite()
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(WIDTH, HEIGHT);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)WIDTH / 2, (float)HEIGHT / 2);
            //rotate
            g.RotateTransform(-c_position.facing);
            //move image back
            g.TranslateTransform(-(float)WIDTH / 2, -(float)HEIGHT / 2);
            //draw passed in image onto graphics object
            g.DrawImage(c_originalImage, new Point(0, 0));
            return returnBitmap;
        }






    }
}
