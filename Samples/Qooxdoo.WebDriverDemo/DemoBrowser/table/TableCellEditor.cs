//using AfterClass = NUnit.Framework.AfterClass;
using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
using IBooleanForm = Qooxdoo.WebDriver.UI.Form.IBooleanForm;
using Table = Qooxdoo.WebDriver.UI.Table.Table;
using Keys = OpenQA.Selenium.Keys;
using WebElement = OpenQA.Selenium.IWebElement;
using Actions = OpenQA.Selenium.Interactions.Actions;

namespace Qooxdoo.WebDriverDemo.DemoBrowser.Table
{
    public class TableCellEditor : IntegrationTest
    {
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            System.setProperty("org.qooxdoo.demo.auturl",
                "http://demo.qooxdoo.org/current/demobrowser/demo/table/Table_Cell_Editor.html");
            IntegrationTest.setUpBeforeClass();
        }

        public Table table;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp()
        public virtual void SetUp()
        {
            table = (Table) driver.FindWidget(By.Qxh("*/qx.ui.table.Table"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void TearDownAfterClass()
        {
            driver.Close();
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void textField()
        public virtual void TextField()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";
            string newText = "hsimpson";

            WebElement row = table.ScrollToRow(0);
            WebElement userNameCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(driver.WebDriver);
            builder.DoubleClick(userNameCell).Perform();

            WebElement editor = table.CellEditor;
            editor.SendKeys(Keys.Backspace);
            editor.SendKeys(Keys.Backspace);
            editor.SendKeys(Keys.Backspace);
            editor.SendKeys(Keys.Backspace);
            editor.SendKeys(newText);
            editor.SendKeys(Keys.Return);

            row = table.ScrollToRow(0);
            userNameCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, userNameCell.Text);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void comboBox()
        public virtual void ComboBox()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";
            string newText = "admin";

            WebElement row = table.ScrollToRow(2);
            WebElement roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(driver.WebDriver);
            builder.DoubleClick(roleCell).Perform();

            Selectable editor = (Selectable) table.CellEditor;
            editor.SelectItem("admin");
            editor.SendKeys(Keys.Return);

            row = table.ScrollToRow(2);
            roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, roleCell.Text);

            newText = "safety inspector";
            builder.DoubleClick(roleCell).Perform();
            editor = (Selectable) table.CellEditor;
            editor.SendKeys(newText);
            editor.SendKeys(Keys.Return);
            row = table.ScrollToRow(2);
            roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, roleCell.Text);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void checkBox()
        public virtual void CheckBox()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";

            WebElement row = table.ScrollToRow(7);
            WebElement newsletterCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(driver.WebDriver);
            builder.DoubleClick(newsletterCell).Perform();

            IBooleanForm editor = (IBooleanForm) table.CellEditor;
            Assert.True(editor.Selected);
            editor.Click();
            Assert.False(editor.Selected);
            table.Click();
        }
    }
}