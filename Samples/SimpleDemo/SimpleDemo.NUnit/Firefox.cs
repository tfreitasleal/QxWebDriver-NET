using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.NUnit
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Firefox
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _internalWebDriver = new FirefoxDriver();
            Driver = new QxWebDriver(_internalWebDriver);
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
        [Order(2010)]
        public void A01_ClickSearch()
        {
            HandleTestUri.SetUri(Driver, TestUri.ApiViewer);
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [Test]
        [Order(2020)]
        public void A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [Test]
        [Order(2030)]
        public void A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [Test]
        [Order(2040)]
        public void A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
            Thread.Sleep(2000);
        }

        [Test]
        [Order(2050)]
        public void F01_MainPage_openWindow_Click()
        {
            HandleTestUri.SetUri(Driver, TestUri.Wisej);
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(2060)]
        public void F02_FirstWindow_openWindow_Click()
        {
            FirstRound.F02_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(2070)]
        public void F03_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [Test]
        [Order(2080)]
        public void F04_CloseWindows()
        {
            FirstRound.F04_CloseWindows(Driver);
        }


        [Test]
        [Order(2100)]
        public void S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(2110)]
        public void S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(2120)]
        public void S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }
    }
}