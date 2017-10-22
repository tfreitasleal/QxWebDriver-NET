using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Form;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DemoBrowser.Table
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class TableCellEditor : IntegrationTest
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            SystemProperties.SetProperty("org.qooxdoo.demo.auturl",
                "http://demo.qooxdoo.org/current/demobrowser/demo/table/Table_Cell_Editor.html");
            IntegrationTest.SetUpBeforeClass();
        }

        public WebDriver.UI.Table.Table table;

        [SetUp]
        public virtual void SetUp()
        {
            table = (WebDriver.UI.Table.Table) Driver.FindWidget(By.Qxh("*/qx.ui.table.Table"));
        }

        [OneTimeTearDown]
        public new static void TearDownAfterClass()
        {
            Driver.Close();
        }

        //ORIGINAL LINE: @Test public void textField()
        [Test]
        public virtual void TextField()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";
            string newText = "hsimpson";

            IWebElement row = table.ScrollToRow(0);
            IWebElement userNameCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(Driver.WebDriver);
            builder.DoubleClick(userNameCell).Perform();

            IWebElement editor = table.CellEditor;
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

        //ORIGINAL LINE: @Test public void comboBox()
        [Test]
        public virtual void ComboBox()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";
            string newText = "admin";

            IWebElement row = table.ScrollToRow(2);
            IWebElement roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(Driver.WebDriver);
            builder.DoubleClick(roleCell).Perform();

            ISelectable editor = (ISelectable) table.CellEditor;
            editor.SelectItem("admin");
            editor.SendKeys(Keys.Return);

            row = table.ScrollToRow(2);
            roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, roleCell.Text);

            newText = "safety inspector";
            builder.DoubleClick(roleCell).Perform();
            editor = (ISelectable) table.CellEditor;
            editor.SendKeys(newText);
            editor.SendKeys(Keys.Return);
            row = table.ScrollToRow(2);
            roleCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, roleCell.Text);
        }

        //ORIGINAL LINE: @Test public void checkBox()
        [Test]
        public virtual void CheckBox()
        {
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 2]";

            IWebElement row = table.ScrollToRow(7);
            IWebElement newsletterCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Actions builder = new Actions(Driver.WebDriver);
            builder.DoubleClick(newsletterCell).Perform();

            BooleanForm editor = (BooleanForm) table.CellEditor;
            Assert.True(editor.Selected);
            editor.Click();
            Assert.False(editor.Selected);
            table.Click();
        }
    }
}