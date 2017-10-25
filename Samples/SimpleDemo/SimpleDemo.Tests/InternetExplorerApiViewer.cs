using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class InternetExplorerApiViewer
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _internalWebDriver = new InternetExplorerDriver();
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
        [Order(4010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [Test]
        [Order(4020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [Test]
        [Order(4030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [Test]
        [Order(4040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
    }
}