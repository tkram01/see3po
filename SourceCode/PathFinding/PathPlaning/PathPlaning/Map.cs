using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;

namespace PathPlaning
{
    class FloorPlan
    {
        private ArrayList Tiles = new ArrayList();
        private int Scale;

        public FloorPlan(String fileName)
        {
            if (fileName.EndsWith(".txt")){

                StreamReader re = File.OpenText(fileName);

                string input = null;
                int rowNum = 0;
                while ((input = re.ReadLine()) != null)
                {
                    ArrayList col = new ArrayList();

                    for (int i = 0; i < input.Length; i++)
                    {
                        FloorTile t = new FloorTile(i, rowNum, input.Substring(i, 1));
                        col.Add(t);
                    }

                    Tiles.Add(col);
                    rowNum++;
                }

                re.Close();

            }




        }

        public FloorPlan(String fileName, int Scale)
        {
            this.Scale = Scale;

            if (fileName.EndsWith(".jpg"))
            {
                //Image img = Image.FromFile(fileName);
                Bitmap img = new Bitmap(@fileName);

                int QX, QY, RX, RY = 0;
                QX = Math.DivRem(img.Width, Scale, out RX);
                QY = Math.DivRem(img.Height, Scale, out RY);

                

                //Two loops for Tiles
                for (int i = 0; i < QY; i++)
                {
                    ArrayList col = new ArrayList();

                    for (int j = 0; j < QX; j++)
                    {
                        String Walkable = "1";
                        Point CurrentUpLeftPoint = new Point();
                        CurrentUpLeftPoint.X = j * Scale;
                        CurrentUpLeftPoint.Y = i * Scale;

                        //Two loops inside a FloorTile
                        for (int ti = CurrentUpLeftPoint.Y; ti < CurrentUpLeftPoint.Y + Scale; ti++)
                        {
                            for (int tj = CurrentUpLeftPoint.X; tj < CurrentUpLeftPoint.X + Scale; tj++)
                            {
                                Color pixelColor = img.GetPixel(tj, ti);
                                //Console.WriteLine(tj + " " + ti + " " + pixelColor.ToString());
                             
                                //if (! (pixelColor.R.Equals(Color.White.R) &&
                                //    pixelColor.G.Equals(Color.White.G) &&
                                //    pixelColor.B.Equals(Color.White.B)))
                                if (! (pixelColor.R > 200 &&
                                    pixelColor.G > 200 &&
                                    pixelColor.B > 200))                                {
                                    //Console.WriteLine(j + " " + i);
                                    Walkable = "0";
                                    break;
                                }

                            }
                            if (Walkable.Equals("0"))
                            {
                                break;
                            }
                        }

                        //create a FloorTile instance
                        FloorTile t = new FloorTile(j, i, Walkable);
                        col.Add(t);
                    }

                    Tiles.Add(col);
                }
            }
        }


        public FloorTile GetTile(int x, int y){

            if (this.GetXSize() > 0 && x < this.GetXSize() &&
                this.GetYSize() > 0 && y < this.GetYSize())
            {
                return (FloorTile)((ArrayList)Tiles[y])[x];
            }
            else
            {
                return null;
            }
            
        }

        public int GetYSize()
        {
            return Tiles.Count;
        }

        public int GetXSize()
        {
            if (GetYSize() > 0)
            {
                return ((ArrayList)Tiles[0]).Count;
            }
            else
            {
                return 0;
            }

        }

        public void SaveTextFile(String FileName)
        {
            TextWriter tw = new StreamWriter(FileName);

            for (int i = 0; i < this.GetYSize(); i++)
            {
                String output = "";

                for (int j = 0; j < this.GetXSize(); j++)
                {
                    output += this.GetTile(j, i).ToString();
                }

                // write a line of text to the file
                tw.WriteLine(output);
            }

            // close the stream
            tw.Close();
        }

        public void SaveImage(String FileName)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }


    }
}
