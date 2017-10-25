using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Table;
using By = Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class TableIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("Table");
        }

        public Table Table;

        //ORIGINAL LINE: @Before public void setUp()
        [SetUp]
        public virtual void SetUp()
        {
            Table = (Table) tabPage.FindWidget(By.Qxh("*/qx.ui.table.Table"));
        }

        protected internal virtual bool Ie
        {
            get
            {
                string browser = SystemProperties.GetProperty("org.qooxdoo.demo.browsername");
                if (browser.Contains("explorer"))
                {
                    return true;
                }

                return false;
            }
        }

        protected internal virtual bool Firefox
        {
            get
            {
                if (SystemProperties.GetProperty("org.qooxdoo.demo.platform").Equals("windows", StringComparison.OrdinalIgnoreCase) &&
                    SystemProperties.GetProperty("org.qooxdoo.demo.browsername").Equals("firefox", StringComparison.OrdinalIgnoreCase) &&
                    SystemProperties.GetProperty("org.qooxdoo.demo.browserversion").Equals("stable", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        //ORIGINAL LINE: @Test public void ScrollToRow()
        [Test]
        public virtual void ScrollToRow()
        {
            if (Ie)
            {
                return;
            }
            // select rows by index
            IWebElement row = Table.ScrollToRow(23);
            IWebElement firstCell =
                row.FindElement(OpenQA.Selenium.By.XPath("div[contains(@class, 'qooxdoo-table-cell')]"));
            Assert.Equals("23", firstCell.Text);

            row = Table.ScrollToRow(3);
            firstCell = row.FindElement(OpenQA.Selenium.By.XPath("div[contains(@class, 'qooxdoo-table-cell')]"));
            Assert.Equals("3", firstCell.Text);
        }

        //ORIGINAL LINE: @Test public void GetCellByText()
        [Test]
        public virtual void GetCellByText()
        {
            // ctrl-Click doesn't work in FF stable/Win
            if (Ie || Firefox)
            {
                return;
            }
            // ctrl-Click two rows and verify the selection ranges
            Actions builder = new Actions(Driver.WebDriver);
            builder.KeyDown(Keys.Control).Click(Table.GetCellByText("26")).Click(Table.GetCellByText("32"))
                .KeyUp(Keys.Control).Perform();

            IList<Dictionary<string, long?>> selectedRanges = Table.SelectedRanges;
            Assert.Equals(2, selectedRanges.Count);

            Dictionary<string, long?> range0 = selectedRanges[0];
            Assert.Equals(26, (int) (long) range0["minIndex"]);
            Assert.Equals(26, (int) (long) range0["maxIndex"]);

            Dictionary<string, long?> range1 = selectedRanges[1];
            Assert.Equals(32, (int) (long) range1["minIndex"]);
            Assert.Equals(32, (int) (long) range1["maxIndex"]);
        }

        //ORIGINAL LINE: @Test public void editCell() throws InterruptedException
        [Test]
        public virtual void EditCell()
        {
            string browserName = SystemProperties.GetProperty("org.qooxdoo.demo.browsername");
            string browserVersion = SystemProperties.GetProperty("org.qooxdoo.demo.browserversion");
            bool condition = browserName.Contains("internet") && browserVersion.Equals("8");
            Assert.IsTrue(!condition);
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 3]";
            string newText = "Hello WebDriver!";

            // Scroll to row #12 and select cell #3
            IWebElement row = Table.ScrollToRow(12);
            IWebElement dateCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            dateCell.Click();

            // Double Click cell #3 to activate edit mode
            Actions builder = new Actions(Driver.WebDriver);
            builder.DoubleClick(dateCell).Perform();

            IWidget editor = Table.CellEditor;
            string old = (string) editor.GetPropertyValue("value");

            // Clear old content
            Actions keyBuilder = (new Actions(Driver.WebDriver)).SendKeys(Keys.End);
            for (int i = 0; i < old.Length; i++)
            {
                keyBuilder.SendKeys(Keys.Backspace);
            }
            keyBuilder.Perform();

            // Type new cell content
            editor.SendKeys(newText);
            editor.SendKeys(Keys.Return);

            // update the cell element and check the new content
            row = Table.ScrollToRow(12);
            dateCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, dateCell.Text);
        }

        //ORIGINAL LINE: @Test public void columnMenu()
        [Test]
        public virtual void ColumnMenu()
        {
            // use the column menu to hide a column
            IList<string> headerLabels = Table.HeaderLabels;
            Assert.AreEqual(new[] {"ID", "A number", "A date", "Boolean"}, headerLabels.ToArray());

            ISelectable colMenuButton = (ISelectable) Table.ColumnMenuButton;
            colMenuButton.SelectItem("A number");

            headerLabels = Table.HeaderLabels;
            Assert.AreEqual(new[] {"ID", "A date", "Boolean"}, headerLabels.ToArray());
            colMenuButton.SelectItem("A number");
        }

        //ORIGINAL LINE: @Test public void sortByColumn()
        [Test]
        public virtual void SortByColumn()
        {
            if (Ie)
            {
                return;
            }
            // Click column headers to set the table's sorting order
            IWidget idColumnHeader = Table.GetHeaderCell("ID");
            string sortIcon = (string) idColumnHeader.GetPropertyValue("sortIcon");
            Assert.IsNull(sortIcon);
            idColumnHeader.Click();
            sortIcon = (string) idColumnHeader.GetPropertyValue("sortIcon");
            Assert.True(sortIcon.Contains("ascending"));
            idColumnHeader.Click();
            sortIcon = (string) idColumnHeader.GetPropertyValue("sortIcon");
            Assert.True(sortIcon.Contains("descending"));
            // back to default sorting
            idColumnHeader.Click();
        }
    }
}