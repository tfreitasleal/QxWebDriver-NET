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

#if !DEBUGJS
        [TestMethod]
        public void O_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        public void O_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        public void O_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        public void O_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
#endif

        [TestMethod]
        public void O_F01_AskQuitNo()
        {
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/Default.html";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
            ExpectedConditions.TitleIs("Main Page");
            TestSuite.F01_AskQuitNo(Driver);
        }

        [TestMethod]
        public void O_F02_MainPage_customerEditor_Click()
        {
            TestSuite.F02_MainPage_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void O_F03_ButtonsWindow_customerEditor_Click()
        {
            TestSuite.F03_ButtonsWindow_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void O_F04_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.F04_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [TestMethod]
        public void O_F05_CloseWindow()
        {
            TestSuite.F05_CloseWindow(Driver);
        }

        [TestMethod]
        public void O_F06_MainPage_customerEditor_Click()
        {
            TestSuite.F06_MainPage_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void O_F07_ButtonsWindow_customerEditor_Click()
        {
            TestSuite.F07_ButtonsWindow_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void O_F08_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.F08_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [TestMethod]
        public void O_F09_CloseWindow()
        {
            TestSuite.F09_CloseWindow(Driver);
        }

        [TestMethod]
        public void O_F10_AskQuitYes()
        {
            TestSuite.F10_AskQuitYes(Driver);
        }
    }
}