using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LocalBrainEmulator
{
    class RobotSprite
    {
        private const int HEIGHT = 50;
        private const int WIDTH = 50;
        private const double degToRads = Math.PI / 180;

        
        private Image c_originalImage;
        private Image c_image;
        public Image image { get { return c_image; } }

        private int c_facing;
        public int facing { get { return c_facing; } }
        private Point c_position;
        public Point position{get{ return c_position; } }

        public RobotSprite(Image sprite, Point position) {

            c_facing = 0; //east
            c_image = new Bitmap(sprite);
            c_originalImage = new Bitmap(sprite);
            c_position = position;
        }

        public void move(int leftSpeed, int rightSpeed) {

            int speedDiff = (leftSpeed - rightSpeed)/2;
            if (speedDiff != 0)
                changeFacing(speedDiff);

            int distance = (leftSpeed + rightSpeed);
            if (distance != 0)
            {
                int yDist = -(int)(distance * Math.Sin((float)c_facing * degToRads));
                int xDist = (int)(distance * Math.Cos((float)c_facing * degToRads));

                c_position.Y += yDist;
                c_position.X += xDist;
            }
        }

        private void changeFacing(int change) {
             c_facing =  (c_facing + change) % 360;
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
            g.RotateTransform(-c_facing);
                //move image back
            g.TranslateTransform(-(float)WIDTH / 2, -(float)HEIGHT / 2);
                //draw passed in image onto graphics object
            g.DrawImage(c_originalImage, new Point(0, 0));
            return returnBitmap;
        }






    }
}
