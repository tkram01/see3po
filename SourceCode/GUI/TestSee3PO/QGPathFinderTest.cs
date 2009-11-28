using See3PO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace TestSee3PO
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
        ///A test for getPath
        ///</summary>
        [TestMethod()]
        public void getPathTest()
        {
            Image myimage = new Bitmap("../../testImage.jpg"); // TODO: Initialize to an appropriate value
            FloorPlan fp = new FloorPlan(myimage); ; // TODO: Initialize to an appropriate value

            QGPathFinder target = new QGPathFinder(fp); // TODO: Initialize to an appropriate value
   
            List<FloorTile> expected = null; // TODO: Initialize to an appropriate value
            List<FloorTile> actual;
            actual = target.getPath();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
