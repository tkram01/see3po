using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

/* Created by Debarati-- This class can work with both .jpg and .bmp files,
 * other image types has not been tested yet.
 * we need to provide it with the full location of the image file for m_myImage property and also the
 * value of m_pixelsperfoot for 
 * the array to be generated */

namespace See3PO
{
    public class FloorPlan
    {
        private Bitmap m_myImage;
        private double m_pixelsperfoot;
        private int m_height;
        private int m_width;

        public FloorPlan(Image myimage, double pixelsPerFoot)
        {
            this.m_myImage = new Bitmap(myimage);
            this.m_pixelsperfoot = pixelsPerFoot;
        }

        public FloorTile[,] createArray()
        {
            //height and width of the original image file
            int image_width = m_myImage.Width;
            int image_height = m_myImage.Height;

            //height and width of the floor plan (in tiles)
            m_height = (int)(image_height / this.m_pixelsperfoot);
            m_width = (int)(image_width / this.m_pixelsperfoot);

            FloorTile[,] m_floorPlanArray = new FloorTile[m_height,m_width];

            // height and width of the array representation of the new array to be created
            // each cell represent the walkable or non-walkable block defined earlier
            for (int row = 0; row < m_height; row++)
            {
               for (int column = 0; column < m_width; column++)
                {
                   if(getWalkableValue(column, row)== 0)
                       m_floorPlanArray[row,column]= new FloorTile(column, row, true);
                   else
                       m_floorPlanArray[row, column] = new FloorTile(column, row, false);
                }
            }
            return m_floorPlanArray;
        }

        // returns the walkability value 0-99
        public int getWalkableValue(int X, int Y) 
        {
            //any color darker than gray is non walkable
            Color pixelColor;

            if (m_pixelsperfoot > 1)    //if there are multiple pixels per walkable area then get the least walkable pixel color
            {
                Color tempColor;
                pixelColor = Color.White;
                for (int row = 0; row < m_pixelsperfoot; row++)
                {
                    for (int column = 0; column < m_pixelsperfoot; column++)
                    {
                        tempColor = m_myImage.GetPixel((int)(X * m_pixelsperfoot) + column, (int)(Y * m_pixelsperfoot) + row);
                        if (tempColor.GetBrightness() < pixelColor.GetBrightness())
                        {
                            pixelColor = tempColor;
                        }
                    }
                }
            }
            else            // if there are multiple walkable areas per pixel, then multiply it
            {
                pixelColor = m_myImage.GetPixel((int)(X * m_pixelsperfoot), (int)(Y * m_pixelsperfoot));
            }

            Color gray = Color.Gray;
            if (pixelColor.GetBrightness() < gray.GetBrightness())
                return 1;
            return 0;
        }

        // prints the 2D array generated in a file.
        public void printArray(FloorTile[,] arr, String path)
        {
            // for now need to modify the location here manually

            using (TextWriter tw = new StreamWriter(path))
            {
                for (int row = 0; row < arr.GetLength(0); row++)
                {
                    for (int col = 0; col < arr.GetLength(1); col++)
                    {
                        tw.Write(arr[row, col]);
                    }
                    tw.WriteLine();
                }
            }
        }

        public Bitmap toImage() {
            Bitmap b = new Bitmap(m_width, m_height);
            FloorTile[,] arr = createArray();
            for (int row = 0; row < m_height; row++) 
            {
                for (int column = 0; column < m_width; column++) 
                { 
                  b.SetPixel(column, row, arr[row,column].toPixel());
                }
            }
            return b;
        }
    }
}