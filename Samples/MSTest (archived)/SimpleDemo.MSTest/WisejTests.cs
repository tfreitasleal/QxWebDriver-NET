using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Wait = SimpleDemo.Tests.Waiter;

namespace SimpleDemo.Tests
{
    public static class WisejTests
    {
        public static QxWebDriver Driver;

        public static void W01_AskQuitNo()
        {
            // get MainPage
            IWidget mainPage = Driver.WidgetGet("MainPage", "Page", 10);
            // cache MainPage
            Cache.SetWidget("MainPage", mainPage);

            // click sayGoodBye on MainPage
            mainPage.ButtonClick("sayGoodBye", 10);
            Driver.MessageBoxClick(DialogResult.No);
        }

        public static void W02_MainPage_customerEditor_Click()
        {
            // get MainPage from cache
            IWidget mainPage = Cache.GetWidget("MainPage");
            // click buttonsWindow on MainPage
            mainPage.ButtonClick("buttonsWindow");

            // check ButtonsWindow exists
            IWidget buttonsWindow = Driver.WidgetGet("ButtonsWindow", "Window", 10);
            // cache buttonsWindow
            Cache.SetWidget("ButtonsWindow", buttonsWindow);
        }

        public static void W03_ButtonsWindow_customerEditor_Click()
        {
            // get ButtonsWindow from cache
            IWidget buttonsWindow = Cache.GetWidget("ButtonsWindow");
            // get buttonsPanel
            IWidget buttonsPanel = buttonsWindow.WidgetGet("buttonsPanel", "LayoutPanel");
            // click customerEditor on buttonsPanel
            buttonsPanel.ButtonClick("customerEditor");

            // check CustomerEditor exists
            IWidget customerEditor = Driver.WidgetGet("CustomerEditor", "Window", 10);
            // cache buttonsWindow
            Cache.SetWidget("CustomerEditor", customerEditor);
        }

        public static void W04_CustomerEditor_customerEditor_LabelContents()
        {
            // get CustomerEditor from cache
            IWidget customerEditor = Cache.GetWidget("CustomerEditor");
        }

        public static void W05_CloseWindow()
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

        public static void W06_MainPage_customerEditor_Click()
        {
            // no cache here

            Driver.ButtonClick("MainPage.buttonsWindow");

            var widget = Driver.WaitForWidget(OpenQA.Selenium.By.Name("ButtonsWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void W07_ButtonsWindow_supplierEditor_Click()
        {
            // no cache here

            // click supplierEditor on buttonsPanel (LayoutPanel) of ButtonsWindow
            Driver.ButtonClick("ButtonsWindow.buttonsPanel.supplierEditor");

            Driver.AlertBoxClose(MessageBoxIcon.Error, "Supplier Editor must be implemented");
            Driver.AlertBoxClose("Supplier Editor should be implemented");
            Driver.AlertBoxClose(MessageBoxIcon.Information);
        }

        public static void W08_CustomerEditor_customerEditor_LabelContents()
        {
            // no cache here

            //driver.LabelAssertValue("CustomerEditor.label1", "End of windows");
        }

        public static void W09_CloseWindow()
        {
            // no cache here

            // give enough time so YOU can see the open window
            //Thread.Sleep(Wait.Duration);

            //driver.WindowClose("CustomerEditor");

            // give enough time so YOU can follow the windows closing
            Thread.Sleep(Wait.Duration);

            Driver.WindowClose("ButtonsWindow");

            // give enough time so YOU can see all windows are closed
            Thread.Sleep(Wait.Duration);
        }

        public static void W10_AskQuitYes()
        {
            // no cache here

            Driver.ButtonClick("MainPage.sayGoodBye");
            Driver.MessageBoxClick(DialogResult.Yes);

            // give enough time so YOU can see the root Page before the browser shows an empty screen
            Thread.Sleep(Wait.Duration * 2);
        }
    }
}