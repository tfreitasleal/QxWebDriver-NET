using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Opera;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestClass]
    //[Parallelizable(ParallelScope.None)]
    public class OperaApiViewer
    {
        private const string OperaBinary = @"C:\Program Files (x86)\Opera\48.0.2685.52\opera.exe";

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var options = new OperaOptions();
            options.BinaryLocation = OperaBinary;
            Driver = new QxWebDriver(new OperaDriver(options));
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
        [Priority(5010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch();
        }

        [TestMethod]
        [Priority(5020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend();
        }

        [TestMethod]
        [Priority(5030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent();
        }

        [TestMethod]
        [Priority(5040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem();
        }
    }
}