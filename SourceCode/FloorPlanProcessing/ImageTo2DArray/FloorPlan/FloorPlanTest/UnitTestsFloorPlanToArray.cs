using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using See3PO;


namespace FloorPlanTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTestsFloorPlanToArray
    {
        public UnitTestsFloorPlanToArray()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
/* :::::::::::IMPORTANT :::::::::: 
 * For all the tests below we need to update proper locations of the image file and also the location and name of the file to be created */
        [TestMethod]
        public void TestBlockSizefive() // test block size of 5
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 5);

            image1.createArray();
            image1.printArray("../../testImage_block5.txt");
            
        }

        [TestMethod]
        public void TestBlockSizefour() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 4);

            image1.createArray();
            image1.printArray( "../../testImage_block4.txt");
           
        }
        [TestMethod]
        public void TestBlockSizesix() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 6);
            image1.createArray();
            image1.printArray("../../testImage_block6.txt");
           
        }

        [TestMethod]
        public void TestCreateImageOfBlockFive() 
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 5);

            image1.createArray();
            Bitmap b = image1.toImage();
            b.Save("../../testImage_block5.bmp");
        }

        [TestMethod]
        public void TestCreateImageOfBlockOne() 
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 1);

            image1.createArray();
            Bitmap test_image = image1.toImage();
            test_image.Save("../../testImage_block1.bmp");
        }

        [TestMethod]
        public void TestConnectBlockFive() 
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"));

            int width = image1.getXTileNum();
            int height = image1.getYTileNum();
            System.Diagnostics.Debug.WriteLine("Height " + height + " Width " + width);
            FloorTile tile = image1.getTile(2, 4);
            image1.Connect();
            FloorTile n1,n2,n3,n4;
            System.Diagnostics.Debug.WriteLine("");
            List<FloorTile> neighbour = tile.getNeighbours();
            if (neighbour.Count > 3)
            {
                n1 = neighbour[0];
                n2 = neighbour[1];
                n3 = neighbour[2];
                n4 = neighbour[3];
                System.Diagnostics.Debug.WriteLine("1st \t" + n1.Position.X + "\t" + n1.Position.Y + "\t" + n1.Iswalkable());
                System.Diagnostics.Debug.WriteLine("2nd \t" + n2.Position.X + "\t" + n2.Position.Y + "\t" + n2.Iswalkable());
                System.Diagnostics.Debug.WriteLine("3rd \t" + n3.Position.X + "\t" + n3.Position.Y + "\t" + n3.Iswalkable());
                System.Diagnostics.Debug.WriteLine("4th \t" + n4.Position.X + "\t" + n4.Position.Y + "\t" + n4.Iswalkable());
         
            }
            else if (neighbour.Count == 3)
            {
                n1 = neighbour[0];
                n2 = neighbour[1];
                n3 = neighbour[2];
                System.Diagnostics.Debug.WriteLine("1st \t" + n1.Position.X + "\t" + n1.Position.Y + "\t" + n1.Iswalkable());
                System.Diagnostics.Debug.WriteLine("2nd \t" + n2.Position.X + "\t" + n2.Position.Y + "\t" + n2.Iswalkable());
                System.Diagnostics.Debug.WriteLine("3rd \t" + n3.Position.X + "\t" + n3.Position.Y + "\t" + n3.Iswalkable());
               
            }
            else if (neighbour.Count == 2)
            {
                n1 = neighbour[0];
                n2 = neighbour[1];
                System.Diagnostics.Debug.WriteLine("1st \t" + n1.Position.X + "\t" + n1.Position.Y + "\t" + n1.Iswalkable());
                System.Diagnostics.Debug.WriteLine("2nd \t" + n2.Position.X + "\t" + n2.Position.Y + "\t" + n2.Iswalkable());
               
            }
            else if (neighbour.Count == 1)
            {
                n1 = neighbour[0];
                System.Diagnostics.Debug.WriteLine("1st \t" + n1.Position.X + "\t" + n1.Position.Y + "\t" + n1.Iswalkable());
               
            }
            else
                System.Diagnostics.Debug.WriteLine("No Walkable Neighbours");
          

        }
        
        [TestMethod]
        public void TestGetTile() // test block size of 1
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 5);

            image1.createArray();
            FloorTile tile = image1.getTile(10,12);
            System.Diagnostics.Debug.WriteLine(tile.Position.X);
            System.Diagnostics.Debug.WriteLine(tile.Position.Y);
            System.Diagnostics.Debug.WriteLine(tile.Iswalkable());
            Assert.IsTrue((tile.ToString().Equals("1")), "Incorret Tile Returned!");
                
        }
     
        [TestMethod]
        public void CompareFileTestBlockSizeone() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("../../testImage.jpg"), 5);

            image1.createArray();
            image1.printArray("../../testImagecomp_block5.txt");
            int filebyte1;
            int filebyte2;
            // fs1 contains the newly created array file while fs2 contains the standard to check against for this block size 5

            FileStream fs1 = new FileStream("../../testImagecomp_block5.txt", FileMode.Open);
            FileStream fs2 = new FileStream("../../created.txt", FileMode.Open);
            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                Assert.IsTrue(false,"Incorrect file created - File lengths are different!");
                return;
            }

            do
            {
                filebyte1 = fs1.ReadByte();
                filebyte2 = fs2.ReadByte();
            } while ((filebyte1 == filebyte2) && (filebyte1 != -1));

            Assert.IsTrue((filebyte1 == filebyte2), "Newly created file and Original file does not match");

            fs1.Close();
            fs2.Close();
        }
    }
}
