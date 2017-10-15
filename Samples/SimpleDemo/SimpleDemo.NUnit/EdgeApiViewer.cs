using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.NUnit
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class EdgeApiViewer
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _internalWebDriver = new EdgeDriver();
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
        [Order(1010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [Test]
        [Order(1020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [Test]
        [Order(1030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [Test]
        [Order(1040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
    }
}