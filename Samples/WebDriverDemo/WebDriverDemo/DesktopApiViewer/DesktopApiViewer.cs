using NUnit.Framework;
using OpenQA.Selenium;
using By = Qooxdoo.WebDriver.By;
using IWidget = Qooxdoo.WebDriver.UI.IWidget;
using Table = Qooxdoo.WebDriver.UI.Table.Table;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopApiViewer
{
    public abstract class DesktopApiViewer : IntegrationTest
    {
        protected internal string TabButtonPath = "*/apiviewer.DetailFrameTabView/*/qx.ui.tabview.TabButton";

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            IntegrationTest.SetUpBeforeClass();
            Driver.JsExecutor.ExecuteScript("qx.locale.Manager.getInstance().setLocale('en');");
        }

        protected internal static void SelectView(string label)
        {
            string path = "*/qx.ui.toolbar.ToolBar/*/[@label=" + label + "]";
            IWidget button = Driver.FindWidget(By.Qxh(path));
            bool selected = ((bool?) button.GetPropertyValue("value")).Value;
            if (!selected)
            {
                button.Click();
                selected = ((bool?) button.GetPropertyValue("value")).Value;
                Assert.True(selected);
            }
        }

        protected internal static void TypeInSearch(string query)
        {
            string searchFieldPath = "*/apiviewer.ui.SearchView/*/qx.ui.form.TextField";
            IWidget searchField = Driver.FindWidget(By.Qxh(searchFieldPath));
            searchField.Clear();
            searchField.SendKeys(query);
        }

        protected internal static void SelectClass(string className)
        {
            SelectView("Search");
            TypeInSearch(className);
            string tablePath = "*/apiviewer.ui.SearchView/*/qx.ui.table.Table";
            Table table = (Table) Driver.FindWidget(By.Qxh(tablePath));
            IWebElement row = table.ScrollToRow(0);
            row.Click();
        }
    }
}