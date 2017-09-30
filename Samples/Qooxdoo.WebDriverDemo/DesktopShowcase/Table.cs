using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;
using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    public class Table : DesktopShowcase
    {

        public By tableLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/qx.ui.table.Table");
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
        public Qooxdoo.WebDriver.UI.Table.Table table_Renamed = null;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void SetUp()
        {
            SelectPage("Table");
            table_Renamed = GetTable();
        }

        public virtual Qooxdoo.WebDriver.UI.Table.Table GetTable()
        {
            WebElement tableEl = Root.FindElement(tableLocator);
            Qooxdoo.WebDriver.UI.Table.Table table = (Qooxdoo.WebDriver.UI.Table.Table) driver.GetWidgetForElement(tableEl);
            return table;
        }

        public virtual ExpectedCondition<bool?> tableDataLoaded()
        {
            return new ExpectedConditionAnonymousInnerClass(this);
        }

        private class ExpectedConditionAnonymousInnerClass : ExpectedCondition<bool?>
        {
            private readonly Table outerInstance;

            public ExpectedConditionAnonymousInnerClass(Table outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public override bool? Apply(WebDriver webDriver)
            {
                return outerInstance.Table_Renamed.RowCount > 1;
            }

            public override string ToString()
            {
                return "Showcase page has finished loading.";
            }
        }

        public virtual void WaitUntilTableDataLoaded()
        {
            (new WebDriverWait(driver, 20, 250)).Until(tableDataLoaded());
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void table() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void table()
        {
            Assert.True(table_Renamed.Displayed);
            WaitUntilTableDataLoaded();
        }
    }

}