using NUnit.Framework;
using OpenQA.Selenium.Opera;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class OperaApiViewer
    {
        private const string OperaBinary = @"C:\Program Files (x86)\Opera\48.0.2685.39\opera.exe";

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new OperaOptions();
            options.BinaryLocation = OperaBinary;
            Driver = new QxWebDriver(new OperaDriver(options));
            Driver.Manage().Window.Maximize();
            ApiViewerTests.Driver = Driver;
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [Test]
        [Order(5010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch();
        }

        [Test]
        [Order(5020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend();
        }

        [Test]
        [Order(5030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent();
        }

        [Test]
        [Order(5040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem();
        }
    }
}