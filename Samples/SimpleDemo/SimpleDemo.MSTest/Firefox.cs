using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.MSTest
{
    [TestClass]
    public class Firefox
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _internalWebDriver = new FirefoxDriver();
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

#if !DEBUGJS
        [TestMethod]
        public void F_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        public void F_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        public void F_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        public void F_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
#endif

        [TestMethod]
        public void F_F01_AskQuitNo()
        {
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/Default.html";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_AskQuitNo(Driver);
        }

        [TestMethod]
        public void F_F02_MainPage_openWindow_Click()
        {
            FirstRound.F02_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        public void F_F03_FirstWindow_openWindow_Click()
        {
            FirstRound.F03_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        public void F_F04_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F04_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        public void F_F05_CloseWindows()
        {
            FirstRound.F05_CloseWindows(Driver);
        }

        [TestMethod]
        public void F_S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        public void F_S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        public void F_S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        public void F_S04_CloseWindows()
        {
            SecondRound.S04_CloseWindows(Driver);
        }

        [TestMethod]
        public void F_S05_AskQuitYes()
        {
            SecondRound.S05_AskQuitYes(Driver);
        }
    }
}