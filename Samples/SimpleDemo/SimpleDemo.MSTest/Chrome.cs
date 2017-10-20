using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.MSTest
{
    [TestClass]
    public class Chrome
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _internalWebDriver = new ChromeDriver();
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
        //[Order(3010)]
        public void C_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        //[Order(3020)]
        public void C_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        //[Order(3030)]
        public void C_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        //[Order(3040)]
        public void C_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }

        [TestMethod]
        //[Order(3050)]
        public void C_F01_MainPage_openWindow_Click()
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
        //[Order(3060)]
        public void C_F02_FirstWindow_openWindow_Click()
        {
            FirstRound.F02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(3070)]
        public void C_F03_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        //[Order(3080)]
        public void C_F04_CloseWindows()
        {
            FirstRound.F04_CloseWindows(Driver);
        }


        [TestMethod]
        //[Order(3100)]
        public void C_S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(3110)]
        public void C_S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(3120)]
        public void C_S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }
    }
}