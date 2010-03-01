using See3PO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FloorPlanAndTile;
using System.Collections.Generic;
using System.Drawing;
using System;
using QuickGraph;
using System.IO;
using System.Windows.Forms;

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
            TextWriter tw = new StreamWriter("../../getPathResult.txt");

            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5); // TODO: Initialize to an appropriate value
            int StartX = 8;
            int StartY = 8;
            int TargetX = 50;
            int TargetY = 20;
            floorPlan.setStartTile(StartX, StartY);
            floorPlan.setTargetTile(TargetX, TargetY);

            String TargetString = TargetX + "_" + TargetY;

            QGPathFinder qgpf = new QGPathFinder(floorPlan); 

            //show neighbors
            List<FloorTile> TargetNeighbors = floorPlan.getTargetTile().getNeighbours();
            String neighborString = "";
            for (int i = 0; i < TargetNeighbors.Count; i++)
            {
                neighborString = TargetNeighbors[i].Position.X + "_" + TargetNeighbors[i].Position.Y;
                
                //tw.WriteLine("Target Neighbors: " + neighborString + ": " + isContainEdge);
            }
            
            
          
            
            List<FloorTile> actual;
            actual = qgpf.getPath();
            
            tw.WriteLine(qgpf.GetMessages());
            tw.Close();

            visualizeFloorPlan(floorPlan, StartX, StartY, TargetX, TargetY);


            List<FloorTile> expected = new List<FloorTile>();
            FloorTile t1 = new FloorTile(0, 0, true, floorPlan);
            FloorTile t2 = new FloorTile(0, 1, true, floorPlan);
            FloorTile t3 = new FloorTile(0, 2, true, floorPlan);
            expected.Add(t1);
            expected.Add(t2);
            expected.Add(t3);
            //String actualString = actual[1].Position.X + "," + actual[1].Position.Y;
            //String expectedString = expected[1].Position.X + "," + expected[1].Position.Y;

            //Assert.AreEqual(expectedString, actualString);

        }
        /// <summary>
        ///A test for getPath
        ///</summary>
        [TestMethod()]
        public void getPathTimeTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5);

            TextWriter tw = new StreamWriter("../../ExecutingTime.txt");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int m = 0; m<3; m++)
                    {
                        for (int n = 0; n < 3; n++)
                        {
                            int StartX = i;
                            int StartY = j;
                            int TargetX = m;
                            int TargetY = n;
                            if (floorPlan.getWalkableValue(i, j) == 1 & floorPlan.getWalkableValue(m, n) == 1)
                            {
                                Int64 t = getPathExecutingTime(floorPlan, StartX, StartY, TargetX, TargetY);
                                tw.WriteLine(StartX + "," + StartY + "," + TargetX + "," + TargetY + "," + t);
                            }
                        }
                    }
                }
            }

            tw.Close();

        }

        /// <summary>
        ///get executing time for a path
        ///</summary>
        public Int64 getPathExecutingTime(FloorPlan floorPlan, int StartX, int StartY, int TargetX, int TargetY)
        {
            Int64 ExecutingTime = 0;

            floorPlan.setStartTile(StartX, StartY);
            floorPlan.setTargetTile(TargetX, TargetY);
            QGPathFinder target = new QGPathFinder(floorPlan);
            List<FloorTile> actual;
            
            
            DateTime StartTime = DateTime.Now; 
            actual = target.getPath();
            DateTime EndTime = DateTime.Now; 
            TimeSpan t= EndTime - StartTime; 

            if (actual != null){
                ExecutingTime = (Int64) t.TotalMilliseconds;
            }

            return ExecutingTime;
        }

        /// <summary>
        ///A test for getPath
        ///</summary>
        [TestMethod()]
        public void getPathVisualizationTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg");
            FloorPlan floorPlan = new FloorPlan(myimage, 5);
            int StartX = 8;
            int StartY = 8;
            int TargetX = 40;
            int TargetY = 20;

            TextWriter tw = new StreamWriter("../../ExecutingTimeVis.txt");
            Int64 t = getPathExecutingTime(floorPlan, StartX, StartY, TargetX, TargetY);
            tw.WriteLine(StartX + "," + StartY + "," + TargetX + "," + TargetY + "," + t);
            tw.Close();

            visualizeFloorPlan(floorPlan, StartX, StartY, TargetX, TargetY);


        }

        /// <summary>
        ///get executing time for a path
        ///</summary>
        public void visualizeFloorPlan(FloorPlan floorPlan, int StartX, int StartY, int TargetX, int TargetY)
        {

            floorPlan.setStartTile(StartX, StartY);
            floorPlan.setTargetTile(TargetX, TargetY);
            QGPathFinder target = new QGPathFinder(floorPlan);
            List<FloorTile> actual;

            actual = target.getPath();

            Form f = new WindowsFormsApplication1.Form1(floorPlan, actual);
            Application.Run(f);

            //f.Invoke();
            //f.Draw(floorPlan, actual);
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
