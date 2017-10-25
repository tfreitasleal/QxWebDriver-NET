using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class FirefoxApiViewer
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _internalWebDriver = new FirefoxDriver();
            Driver = new QxWebDriver(_internalWebDriver);
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [Test]
        [Order(3010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [Test]
        [Order(3020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [Test]
        [Order(3030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [Test]
        [Order(3040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
    }
}