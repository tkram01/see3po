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
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 5);

            FloorTile[,] arr = image1.createArray();
            image1.printArray(arr, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block5.txt");
            
        }

        [TestMethod]
        public void TestBlockSizefour() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 4);

            FloorTile[,] arr = image1.createArray();
            image1.printArray(arr, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block4.txt");
           
        }
        [TestMethod]
        public void TestBlockSizesix() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 6);

            FloorTile[,] arr = image1.createArray();
            image1.printArray(arr, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block6.txt");
           
        }

        [TestMethod]
        public void TestCreateImageOfBlockFive() // test block size of 5
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 5);

            FloorTile[,] arr = image1.createArray();
            Bitmap b = image1.toImage();
            b.Save("C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block5.bmp");
        }

        [TestMethod]
        public void TestCreateImageOfBlockOne() // test block size of 1
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 1);

            FloorTile[,] arr = image1.createArray();
            Bitmap b = image1.toImage();
            b.Save("C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block1.bmp");
        }
     
        [TestMethod]
        public void CompareFileTestBlockSizeone() // test block size of 4
        {
            FloorPlan image1 = new FloorPlan(new Bitmap("C:\\Users\\Debarati\\Desktop\\CS682\\project\\floorplan.jpg"), 5);

            FloorTile[,] arr = image1.createArray();
            image1.printArray(arr, "C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block5.txt");
            int filebyte1;
            int filebyte2;
            // fs1 contains the newly created array file while fs2 contains the standard to check against for this block size 5
           
            FileStream fs1 = new FileStream("C:\\Users\\Debarati\\Desktop\\CS682\\project\\test_block5.txt",FileMode.Open);
            FileStream fs2 = new FileStream("C:\\Users\\Debarati\\Desktop\\CS682\\project\\created.txt", FileMode.Open);
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
