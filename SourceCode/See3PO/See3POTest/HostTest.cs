using See3PO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace See3POTest
{
    
    
    /// <summary>
    ///This is a test class for HostTest and is intended
    ///to contain all HostTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HostTest
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
        ///A test for isConnected
        ///</summary>
        [TestMethod()]
        public void isConnectedTest()
        {
            Host target = new Host(new EmptyUI()); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.isConnected();
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for RequestImage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("See3PO.dll")]
        public void RequestImageTest()
        {
            Host target = new Host(new EmptyUI()); // TODO: Initialize to an appropriate value
            target.ToggleConnection();
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;

            //We should manually click "Connections->Connect to Remote Brain" in 5 secs
            System.Threading.Thread.Sleep(5000);

            actual = target.isConnected();
            
            //request image
            target.RequestImage();
            //give it some time to receive image
            System.Threading.Thread.Sleep(3000);

            Assert.AreEqual(expected, actual);

            //disconnect
            target.ToggleConnection();
        }
    }
}
