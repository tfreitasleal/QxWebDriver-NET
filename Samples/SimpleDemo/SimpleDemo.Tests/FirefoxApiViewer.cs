using NUnit.Framework;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class FirefoxApiViewer
    {
        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            Driver = new QxWebDriver(Browser.Firefox);
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
        [Order(3010)]
        public void A01_ClickSearch()
        {
            ApiViewerTests.A01_ClickSearch();
        }

        [Test]
        [Order(3020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend();
        }

        [Test]
        [Order(3030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent();
        }

        [Test]
        [Order(3040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem();
        }
    }
}