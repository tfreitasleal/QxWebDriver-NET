﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.MSTest
{
    [TestClass]
    public class Edge
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _internalWebDriver = new EdgeDriver();
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
        public void E_A01_ClickSearch()
        {
            Driver.Url = "http://www.qooxdoo.org/current/api/index.html";
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        public void E_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        public void E_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        public void E_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
        }
#endif

        [TestMethod]
        public void E_F01_AskQuitNo()
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
        public void E_F02_MainPage_openWindow_Click()
        {
            FirstRound.F02_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        public void E_F03_FirstWindow_openWindow_Click()
        {
            FirstRound.F03_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        public void E_F04_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F04_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        public void E_F05_CloseWindows()
        {
            FirstRound.F05_CloseWindows(Driver);
        }

        [TestMethod]
        public void E_S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        public void E_S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        public void E_S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        public void E_S04_CloseWindows()
        {
            SecondRound.S04_CloseWindows(Driver);
        }

        [TestMethod]
        public void E_S05_AskQuitYes()
        {
            SecondRound.S05_AskQuitYes(Driver);
        }
    }
}