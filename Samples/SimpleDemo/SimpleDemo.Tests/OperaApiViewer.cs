using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class OperaApiViewer
    {
        private static string _operaBinary = @"C:\Program Files (x86)\Opera\48.0.2685.39\opera.exe";

        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new OperaOptions();
            options.BinaryLocation = _operaBinary;
            _internalWebDriver = new OperaDriver(options);
            _internalWebDriver.Manage().Window.Maximize();
            Driver = new QxWebDriver(_internalWebDriver);
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _internalWebDriver.Quit();
            _internalWebDriver.Dispose();
            Driver.Quit();
            Driver.Dispose();
        }

        [Test]
        [Order(5010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [Test]
        [Order(5020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [Test]
        [Order(5030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [Test]
        [Order(5040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
    }
}