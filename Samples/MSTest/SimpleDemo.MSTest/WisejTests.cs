using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Web;
using Wait = SimpleDemo.MSTest.Waiter;

namespace SimpleDemo.MSTest
{
    public static class WisejTests
    {
        public static void W01_AskQuitNo(QxWebDriver driver)
        {
            // get MainPage
            IWidget mainPage = driver.WidgetGet("MainPage", "Page", 10);

            // click sayGoodBye on MainPage
            mainPage.ButtonClick("sayGoodBye", 10);
            driver.MessageBoxClick(DialogResult.No);
        }

        public static void W02_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            // click buttonsWindow on MainPage
            driver.ButtonClick("MainPage.buttonsWindow");

            // check ButtonsWindow exists
            driver.WidgetGet("ButtonsWindow", "Window", 10);
        }

        public static void W03_ButtonsWindow_customerEditor_Click(QxWebDriver driver)
        {
            // get ButtonsWindow
            IWidget buttonsWindow = driver.WidgetGet("ButtonsWindow", "Page", 10);
            // get buttonsPanel
            IWidget buttonsPanel = buttonsWindow.WidgetGet("buttonsPanel", "LayoutPanel");
            // click customerEditor on buttonsPanel
            buttonsPanel.ButtonClick("customerEditor");

            // check CustomerEditor exists
            driver.WaitForWidget(OpenQA.Selenium.By.Name("CustomerEditor"), 10);
        }

        public static void W04_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void W05_CloseWindow(QxWebDriver driver)
        {
            // give enough time for the browser to show the view
            Thread.Sleep(Wait.Duration);

            driver.WindowClose("CustomerEditor");

            Thread.Sleep(Wait.Duration);

            driver.WindowClose("ButtonsWindow");

            Thread.Sleep(Wait.Duration);
        }

        public static void W06_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            driver.ButtonClick("MainPage.buttonsWindow");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("ButtonsWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void W07_ButtonsWindow_supplierEditor_Click(QxWebDriver driver)
        {
            // click supplierEditor on buttonsPanel (LayoutPanel) of ButtonsWindow
            driver.ButtonClick("ButtonsWindow.buttonsPanel.supplierEditor");

            driver.AlertBoxClose(MessageBoxIcon.Error, "Supplier Editor must be implemented");
            driver.AlertBoxClose("Supplier Editor should be implemented");
            driver.AlertBoxClose(MessageBoxIcon.Information);
        }

        public static void W08_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            //driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void W09_CloseWindow(QxWebDriver driver)
        {
            //Thread.Sleep(Wait.Duration);

            //driver.WindowClose("CustomerEditor");

            Thread.Sleep(Wait.Duration);

            driver.WindowClose("ButtonsWindow");

            Thread.Sleep(Wait.Duration);
        }

        public static void W10_AskQuitYes(QxWebDriver driver)
        {
            driver.ButtonClick("MainPage.sayGoodBye");
            driver.MessageBoxClick(DialogResult.Yes);

            Thread.Sleep(Wait.Duration * 2);
        }
    }
}