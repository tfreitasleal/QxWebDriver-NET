using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestClass]
    //[Parallelizable(ParallelScope.None)]
    public class FirefoxApiViewer
    {
        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Driver = new QxWebDriver(Browser.Firefox);
            Driver.Manage().Window.Maximize();
            ApiViewerTests.Driver = Driver;
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
        }

        [ClassCleanup]
        public static void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        [Priority(3010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch();
        }

        [TestMethod]
        [Priority(3020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend();
        }

        [TestMethod]
        [Priority(3030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent();
        }

        [TestMethod]
        [Priority(3040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem();
        }
    }
}