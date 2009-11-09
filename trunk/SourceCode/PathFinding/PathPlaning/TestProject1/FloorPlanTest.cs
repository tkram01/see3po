using PathPlaning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace TestProject1
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
        ///A test for GetYSize
        ///</summary>
        [TestMethod()]
        public void GetYSizeTest()
        {
            string fileName = "../../sampleMap.txt"; // TODO: Initialize to an appropriate value
            FloorPlan target = new FloorPlan(fileName); // TODO: Initialize to an appropriate value
            int expected = 6; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetYSize();
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for GetXSize
        ///</summary>
        [TestMethod()]
        public void GetXSizeTest()
        {
            string fileName = "../../sampleMap.txt"; // TODO: Initialize to an appropriate value
            FloorPlan target = new FloorPlan(fileName); // TODO: Initialize to an appropriate value
            int expected = 13; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetXSize();
            Assert.AreEqual(expected, actual);
        }
    }
}
