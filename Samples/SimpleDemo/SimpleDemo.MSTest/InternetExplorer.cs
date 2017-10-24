﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.MSTest
{
    [TestClass]
    public class InternetExplorer
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _internalWebDriver = new InternetExplorerDriver();
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
        public void I_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        public void I_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        public void I_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        public void I_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
#endif

        [TestMethod]
        public void I_W01_AskQuitNo()
        {
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/Default.html";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
            ExpectedConditions.TitleIs("Main Page");
            WisejTests.W01_AskQuitNo(Driver);
        }

        [TestMethod]
        public void I_W02_MainPage_customerEditor_Click()
        {
            WisejTests.W02_MainPage_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void I_W03_ButtonsWindow_customerEditor_Click()
        {
            WisejTests.W03_ButtonsWindow_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void I_W04_CustomerEditor_customerEditor_LabelContents()
        {
            WisejTests.W04_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [TestMethod]
        public void I_W05_CloseWindow()
        {
            WisejTests.W05_CloseWindow(Driver);
        }

        [TestMethod]
        public void I_W06_MainPage_customerEditor_Click()
        {
            WisejTests.W06_MainPage_customerEditor_Click(Driver);
        }

        [TestMethod]
        public void I_W07_ButtonsWindow_customerEditor_Click()
        {
            WisejTests.W07_ButtonsWindow_supplierEditor_Click(Driver);
        }

        [TestMethod]
        public void I_W08_CustomerEditor_customerEditor_LabelContents()
        {
            WisejTests.W08_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [TestMethod]
        public void I_W09_CloseWindow()
        {
            WisejTests.W09_CloseWindow(Driver);
        }

        [TestMethod]
        public void I_W10_AskQuitYes()
        {
            WisejTests.W10_AskQuitYes(Driver);
        }
    }
}