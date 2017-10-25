using System.Threading;
using NUnit.Framework;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Wait = SimpleDemo.Tests.Waiter;

namespace SimpleDemo.Tests
{
    public static class WisejTests
    {
        public static void W01_AskQuitNo(QxWebDriver driver)
        {
            // get MainPage
            IWidget mainPage = driver.WidgetGet("MainPage", "Page", 10);
            // cache MainPage
            Cache.SetWidget("MainPage", mainPage);

            // click sayGoodBye on MainPage
            mainPage.ButtonClick("sayGoodBye", 10);
            driver.MessageBoxClick(DialogResult.No);
        }

        public static void W02_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            // get MainPage from cache
            IWidget mainPage = Cache.GetWidget("MainPage");
            // click buttonsWindow on MainPage
            mainPage.ButtonClick("buttonsWindow");

            // check ButtonsWindow exists
            IWidget buttonsWindow = driver.WidgetGet("ButtonsWindow", "Window", 10);
            // cache buttonsWindow
            Cache.SetWidget("ButtonsWindow", buttonsWindow);
        }

        public static void W03_ButtonsWindow_customerEditor_Click(QxWebDriver driver)
        {
            // get ButtonsWindow from cache
            IWidget buttonsWindow = Cache.GetWidget("ButtonsWindow");
            // get buttonsPanel
            IWidget buttonsPanel = buttonsWindow.WidgetGet("buttonsPanel", "LayoutPanel");
            // click customerEditor on buttonsPanel
            buttonsPanel.ButtonClick("customerEditor");

            // check CustomerEditor exists
            IWidget customerEditor = driver.WidgetGet("CustomerEditor", "Window", 10);
            // cache buttonsWindow
            Cache.SetWidget("CustomerEditor", customerEditor);
        }

        public static void W04_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            // get CustomerEditor from cache
            IWidget customerEditor = Cache.GetWidget("CustomerEditor");
        }

        public static void W05_CloseWindow(QxWebDriver driver)
        {
            // give enough time so YOU can see the open window
            Thread.Sleep(Wait.Duration);

            // get CustomerEditor from cache
            IWidget customerEditor = Cache.GetWidget("CustomerEditor");
            customerEditor.WindowClose();

            // give enough time so YOU can follow the windows closing
            Thread.Sleep(Wait.Duration);

            // get ButtonsWindow from cache
            IWidget buttonsWindow = Cache.GetWidget("ButtonsWindow");
            // close ButtonsWindow
            buttonsWindow.WindowClose();

            // give enough time so YOU can see all windows are closed
            Thread.Sleep(Wait.Duration);
        }

        public static void W06_MainPage_customerEditor_Click(QxWebDriver driver)
        {
            // no cache here

            driver.ButtonClick("MainPage.buttonsWindow");

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("ButtonsWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void W07_ButtonsWindow_supplierEditor_Click(QxWebDriver driver)
        {
            // no cache here

            // click supplierEditor on buttonsPanel (LayoutPanel) of ButtonsWindow
            driver.ButtonClick("ButtonsWindow.buttonsPanel.supplierEditor");

            driver.AlertBoxClose(MessageBoxIcon.Error, "Supplier Editor must be implemented");
            driver.AlertBoxClose("Supplier Editor should be implemented");
            driver.AlertBoxClose(MessageBoxIcon.Information);
        }

        public static void W08_CustomerEditor_customerEditor_LabelContents(QxWebDriver driver)
        {
            // no cache here

            //driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void W09_CloseWindow(QxWebDriver driver)
        {
            // no cache here

            // give enough time so YOU can see the open window
            //Thread.Sleep(Wait.Duration);

            //driver.WindowClose("CustomerEditor");

            // give enough time so YOU can follow the windows closing
            Thread.Sleep(Wait.Duration);

            driver.WindowClose("ButtonsWindow");

            // give enough time so YOU can see all windows are closed
            Thread.Sleep(Wait.Duration);
        }

        public static void W10_AskQuitYes(QxWebDriver driver)
        {
            // no cache here

            driver.ButtonClick("MainPage.sayGoodBye");
            driver.MessageBoxClick(DialogResult.Yes);

            // give enough time so YOU can see the root Page before the browser shows an empty screen
            Thread.Sleep(Wait.Duration * 2);
        }
    }
}