using System.Threading;
using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;
using Wait = SimpleDemo.NUnit.Waiter;

namespace SimpleDemo.NUnit
{
    public static class TestSuite
    {
        public static void F01_AskQuitNo(QxWebDriver driver)
        {
            IWidget mainPage = driver.WaitForWidget(By.Qxh(By.Namespace("MainPage")), 10);
            Assert.IsNotNull(mainPage);

            driver.ButtonClick("MainPage.sayGoodBye");
            driver.MessageBoxClick(Wisej.Web.DialogResult.No);
        }

        public static void F02_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            driver.ButtonClick("MainPage.customerEditor");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("ButtonsWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void F03_ButtonsWindow_customerEditor_Click(QxWebDriver driver)
        {
            driver.ButtonClick("ButtonsWindow.buttonsPanel.customerEditor");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("CustomerEditor"), 10);
            Assert.IsNotNull(widget);
        }

        public static void F04_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void F05_CloseWindow(QxWebDriver driver)
        {
            Thread.Sleep(Wait.Duration);

            driver.WindowClose("CustomerEditor");

            Thread.Sleep(Wait.Duration);

            driver.WindowClose("ButtonsWindow");

            Thread.Sleep(Wait.Duration);
        }

        public static void F06_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            driver.ButtonClick("MainPage.customerEditor");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("ButtonsWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void F07_ButtonsWindow_customerEditor_Click(QxWebDriver driver)
        {
            driver.ButtonClick("ButtonsWindow.buttonsPanel.customerEditor");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("CustomerEditor"), 10);
            Assert.IsNotNull(widget);
        }

        public static void F08_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void F09_CloseWindow(QxWebDriver driver)
        {
            Thread.Sleep(Wait.Duration);

            driver.WindowClose("CustomerEditor");

            Thread.Sleep(Wait.Duration);

            driver.WindowClose("ButtonsWindow");

            Thread.Sleep(Wait.Duration);
        }

        public static void F10_AskQuitYes(QxWebDriver driver)
        {
            driver.ButtonClick("MainPage.sayGoodBye");
            driver.MessageBoxClick(Wisej.Web.DialogResult.Yes);

            Thread.Sleep(Wait.Duration * 2);
        }
    }
}