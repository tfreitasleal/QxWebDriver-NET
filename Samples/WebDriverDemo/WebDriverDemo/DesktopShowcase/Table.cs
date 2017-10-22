using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Table : DesktopShowcase
    {
        public By tableLocator =
                By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/qx.ui.table.Table");

        //JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
        public WebDriver.UI.Table.Table fieldTable = null;

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Table");
            fieldTable = GetTable();
        }

        public virtual WebDriver.UI.Table.Table GetTable()
        {
            IWebElement tableEl = Root.FindElement(tableLocator);
            WebDriver.UI.Table.Table table = (WebDriver.UI.Table.Table) Driver.GetWidgetForElement(tableEl);
            return table;
        }

        public virtual Func<IWebDriver, bool?> TableDataLoaded()
        {
            return driver =>
            {
                return fieldTable.RowCount > 1;
                ;
            };
        }

        /*public ExpectedCondition<Boolean> tableDataLoaded() {
            return new ExpectedCondition<Boolean>() {
                @Override
                public Boolean apply(WebDriver webDriver) {
                    return table.getRowCount() > 1;
                }

                @Override
                public String toString() {
                    return "Showcase page has finished loading.";
                }
            };
        }*/

        public virtual void WaitUntilTableDataLoaded()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(20)).Until(TableDataLoaded());
        }

        //ORIGINAL LINE: @Test public void table() throws InterruptedException
        [Test]
        public virtual void TestTable()
        {
            Assert.True(fieldTable.Displayed);
            WaitUntilTableDataLoaded();
        }
    }
}