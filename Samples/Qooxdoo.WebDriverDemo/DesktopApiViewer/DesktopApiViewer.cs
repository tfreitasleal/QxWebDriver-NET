namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    using By = Qooxdoo.WebDriver.By;
    using IWidget = Qooxdoo.WebDriver.UI.IWidget;
    using Table = Qooxdoo.WebDriver.UI.Table.Table;
    using WebElement = OpenQA.Selenium.IWebElement;

    public abstract class DesktopApiViewer : IntegrationTest
    {

        protected internal string tabButtonPath = "*/apiviewer.DetailFrameTabView/*/qx.ui.tabview.TabButton";

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            IntegrationTest.setUpBeforeClass();
            driver.JsExecutor.ExecuteScript("qx.locale.Manager.getInstance().setLocale('en');");
        }

        protected internal static void SelectView(string label)
        {
            string path = "*/qx.ui.toolbar.ToolBar/*/[@label=" + label + "]";
            IWidget button = driver.FindWidget(By.Qxh(path));
            bool selected = (bool?) button.GetPropertyValue("value").Value;
            if (!selected)
            {
                button.Click();
                selected = (bool?) button.GetPropertyValue("value").Value;
                Assert.True(selected);
            }
        }

        protected internal static void TypeInSearch(string query)
        {
            string searchFieldPath = "*/apiviewer.ui.SearchView/*/qx.ui.form.TextField";
            IWidget searchField = driver.FindWidget(By.Qxh(searchFieldPath));
            searchField.clear();
            searchField.SendKeys(query);
        }

        protected internal static void SelectClass(string className)
        {
            SelectView("Search");
            TypeInSearch(className);
            string tablePath = "*/apiviewer.ui.SearchView/*/qx.ui.Table.Table";
            Table table = (Table) driver.FindWidget(By.Qxh(tablePath));
            WebElement row = table.ScrollToRow(0);
            row.Click();
        }
    }

}