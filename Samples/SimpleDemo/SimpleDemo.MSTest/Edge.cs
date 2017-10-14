﻿using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public static void E_Setup(TestContext testContext)
        {
            _internalWebDriver = new EdgeDriver();
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
        //[Order(1010)]
        public void E_A01_ClickSearch()
        {
            HandleTestUri.SetUri(Driver, TestUri.ApiViewer);
            ApiViewerTests.A01_ClickSearch(Driver);
        }

        [TestMethod]
        //[Order(1020)]
        public void E_A02_ClickLegend()
        {
            ApiViewerTests.A02_ClickLegend(Driver);
        }

        [TestMethod]
        //[Order(1030)]
        public void E_A03_ClickContent()
        {
            ApiViewerTests.A03_ClickContent(Driver);
        }

        [TestMethod]
        //[Order(1040)]
        public void E_A04_ClickTreeItem()
        {
            ApiViewerTests.A04_ClickTreeItem(Driver);
            Thread.Sleep(2000);
        }

        [TestMethod]
        //[Order(1050)]
        public void E_F01_MainPage_openWindow_Click()
        {
            HandleTestUri.SetUri(Driver, TestUri.Wisej);
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(1060)]
        public void E_F02_FirstWindow_openWindow_Click()
        {
            FirstRound.F02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(1070)]
        public void E_F03_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [TestMethod]
        //[Order(1080)]
        public void E_F04_CloseWindows()
        {
            FirstRound.F04_CloseWindows(Driver);
        }


        [TestMethod]
        //[Order(1100)]
        public void E_S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(1110)]
        public void E_S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [TestMethod]
        //[Order(1120)]
        public void E_S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }
    }
}