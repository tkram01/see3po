using See3PO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FloorPlanAndTile;
using System.Collections.Generic;
using System.Drawing;
using System;
using QuickGraph;

namespace PathFindingTest
{
    
    
    /// <summary>
    ///This is a test class for QGPathFinderTest and is intended
    ///to contain all QGPathFinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QGPathFinderTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getTileByIndex
        ///</summary>
        [TestMethod()]
        public void getTileByIndexTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5); // TODO: Initialize to an appropriate value
            String index = "1_1"; // TODO: Initialize to an appropriate value
            FloorTile expected = floorPlan.getTile(1, 1); // TODO: Initialize to an appropriate value
            FloorTile actual;
            actual = QGPathFinder.getTileByIndex(floorPlan, index);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getPath
        ///</summary>
        [TestMethod()]
        public void getPathTest()
        {
            
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5); // TODO: Initialize to an appropriate value
            floorPlan.setStartTile(0, 0);
            floorPlan.setTargetTile(0, 2);

            FloorTile st = new FloorTile();
            System.Diagnostics.Debug.WriteLine("Start");
            System.Diagnostics.Debug.WriteLine(floorPlan.getStartTile().Position.X.ToString());
            System.Diagnostics.Debug.WriteLine(floorPlan.getStartTile().Position.Y.ToString());
            System.Diagnostics.Debug.WriteLine("Target");
            System.Diagnostics.Debug.WriteLine(floorPlan.getTargetTile().Position.X.ToString());
            System.Diagnostics.Debug.WriteLine(floorPlan.getTargetTile().Position.Y.ToString());

            QGPathFinder target = new QGPathFinder(floorPlan); // TODO: Initialize to an appropriate value
            List<FloorTile> expected = new List<FloorTile>(); // TODO: Initialize to an appropriate value
            FloorTile t1 = new FloorTile(0, 0, true, floorPlan);
            FloorTile t2 = new FloorTile(0, 1, true, floorPlan);
            FloorTile t3 = new FloorTile(0, 2, true, floorPlan);
            expected.Add(t1);
            expected.Add(t2);
            expected.Add(t3);
            
            List<FloorTile> actual;
            actual = target.getPath();
            String actualString = actual[1].Position.X + "," + actual[1].Position.Y;
            String expectedString = expected[1].Position.X + "," + expected[1].Position.Y;

            Assert.AreEqual(expectedString, actualString);
        }



        /// <summary>
        ///A test for QGPathFinder Constructor
        ///</summary>
        [TestMethod()]
        public void QGPathFinderConstructorTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5); // TODO: Initialize to an appropriate value
            QGPathFinder target = new QGPathFinder(floorPlan);
            Type actual = target.GetType();
            Type expected = typeof(QGPathFinder);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for buildGraph
        ///</summary>
        [TestMethod()]
        public void buildGraphTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5); // TODO: Initialize to an appropriate value
            QGPathFinder target = new QGPathFinder(floorPlan); // TODO: Initialize to an appropriate value
            int expected = 6354; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.buildGraph().VertexCount;
            Assert.AreEqual(expected, actual);
        }

 
    }
}
