using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace PathPlaning
{
    class Map
    {
        private ArrayList data = new ArrayList();

        public Map(String fileName)
        {
            StreamReader re = File.OpenText(fileName);

            

            string input = null;
            int rowNum = 0;
            while ((input = re.ReadLine()) != null)
            {
                ArrayList col = new ArrayList();

                for (int i = 0; i < input.Length; i++)
                {
                    Tile t = new Tile(i, rowNum, input.Substring(i, 1));
                    col.Add(t);
                }

                data.Add(col);
                rowNum ++;
            }




            re.Close();

        }

        public Tile getTile(int i, int j){

            return (Tile)((ArrayList)data[i])[j];
        }

        public void connectNeighbors(){

        }
    }
}
