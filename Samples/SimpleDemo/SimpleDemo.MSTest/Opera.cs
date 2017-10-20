using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.MSTest
{
    [TestClass]
    public class Opera
    {
        private static string _operaBinary = @"C:\Program Files (x86)\Opera\48.0.2685.39\opera.exe";

        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var options = new OperaOptions();
            options.BinaryLocation = _operaBinary;
            _internalWebDriver = new OperaDriver(options);
            _internalWebDriver.Manage().Window.Maximize();
            Driver = new QxWebDriver(_internalWebDriver);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _internalWebDriver.Quit();
            _internalWebDriver.Dispose();
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        //[Order(5010)]
        public void O_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        //[Order(5020)]
        public void O_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        //[Order(5030)]
        public void O_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        //[Order(5040)]
        public void O_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }

        [TestMethod]
        //[Order(5050)]
        public void O_F01_MainPage_openWindow_Click()
        {
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(5060)]
        public void O_F02_FirstWindow_openWindow_Click()
        {
            FirstRound.F02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(5070)]
        public void O_F03_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        //[Order(5080)]
        public void O_F04_CloseWindows()
        {
            FirstRound.F04_CloseWindows(Driver);
        }


        [TestMethod]
        //[Order(5100)]
        public void O_S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(5110)]
        public void O_S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(5120)]
        public void O_S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }
    }
}