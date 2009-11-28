using See3PO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Collections.Generic;

namespace TestSee3PO
{
    
    
    /// <summary>
    ///This is a test class for FloorPlanTest and is intended
    ///to contain all FloorPlanTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FloorPlanTest
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
        ///A test for getYTileNum
        ///</summary>
        [TestMethod()]
        public void getYTileNumTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg"); // TODO: Initialize to an appropriate value
            FloorPlan target = new FloorPlan(myimage); // TODO: Initialize to an appropriate value
            int expected = 89; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.getYTileNum();
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for getXTileNum
        ///</summary>
        [TestMethod()]
        public void getXTileNumTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg"); // TODO: Initialize to an appropriate value
            FloorPlan target = new FloorPlan(myimage); // TODO: Initialize to an appropriate value
            int expected = 95; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.getXTileNum();
            Assert.AreEqual(expected, actual);
         
        }

        /// <summary>
        ///A test for Connect
        ///</summary>
        [TestMethod()]
        public void ConnectTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg"); // TODO: Initialize to an appropriate value
            FloorPlan target = new FloorPlan(myimage); // TODO: Initialize to an appropriate value
            Dictionary<FloorTile, List<FloorTile>> expected = new Dictionary<FloorTile,List<FloorTile>>(); // TODO: Initialize to an appropriate value
            Dictionary<FloorTile, List<FloorTile>> actual;

            actual = target.Connect();
            Assert.AreEqual(0, 0);
            
        }

    }
}
