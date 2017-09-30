using System.Collections;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
using Widget = Qooxdoo.WebDriver.UI.IWidget;
using Table = Qooxdoo.WebDriver.UI.Table.Table;
using Keys = OpenQA.Selenium.Keys;
using WebElement = OpenQA.Selenium.IWebElement;
using Actions = OpenQA.Selenium.Interactions.Actions;

namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

    public class TableIT : WidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            WidgetBrowser.setUpBeforeClass();
            selectTab("Table");
        }

    public Table table;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp()
        public virtual void setUp()
        {
            table = (Table) tabPage.FindWidget(By.Qxh("*/qx.ui.table.Table"));
        }

        protected internal virtual bool Ie
        {
            get
            {
                string browser = System.getProperty("org.qooxdoo.demo.browsername");
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
                if (System.getProperty("org.qooxdoo.demo.platform").equalsIgnoreCase("windows") && System.getProperty("org.qooxdoo.demo.browsername").equalsIgnoreCase("firefox") && System.getProperty("org.qooxdoo.demo.browserversion").equalsIgnoreCase("stable"))
                {
                    return true;
                }
                return false;
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void ScrollToRow()
        public virtual void ScrollToRow()
        {
            if (Ie)
            {
                return;
            }
            // select rows by index
            WebElement row = table.ScrollToRow(23);
            WebElement firstCell = row.FindElement(OpenQA.Selenium.By.XPath("div[contains(@class, 'qooxdoo-table-cell')]"));
            Assert.Equals("23", firstCell.Text);

            row = table.ScrollToRow(3);
            firstCell = row.FindElement(OpenQA.Selenium.By.XPath("div[contains(@class, 'qooxdoo-table-cell')]"));
            Assert.Equals("3", firstCell.Text);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void GetCellByText()
        public virtual void GetCellByText()
        {
            // ctrl-Click doesn't work in FF stable/Win
            if (Ie || Firefox)
            {
                return;
            }
            // ctrl-Click two rows and verify the selection ranges
            Actions builder = new Actions(driver.WebDriver);
            builder.KeyDown(Keys.Control).Click(table.GetCellByText("26")).Click(table.GetCellByText("32")).keyUp(Keys.Control).perform();

            IList<Hashtable> selectedRanges = table.SelectedRanges;
            Assert.Equals(2, selectedRanges.Count);

            Dictionary<string, long?> range0 = selectedRanges[0];
            Assert.Equals(26, (int)(long) range0["minIndex"]);
            Assert.Equals(26, (int)(long) range0["maxIndex"]);

            Dictionary<string, long?> range1 = selectedRanges[1];
            Assert.Equals(32, (int)(long) range1["minIndex"]);
            Assert.Equals(32, (int)(long) range1["maxIndex"]);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void editCell() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void editCell()
        {
            string browserName = System.getProperty("org.qooxdoo.demo.browsername");
            string browserVersion = System.getProperty("org.qooxdoo.demo.browserversion");
            bool condition = browserName.Contains("internet") && browserVersion.Equals("8");
            NUnit.Framework.Assume.assumeTrue(!condition);
            string cellXpath = "div[contains(@class, 'qooxdoo-table-cell') and position() = 3]";
            string newText = "Hello WebDriver!";

            // Scroll to row #12 and select cell #3
            WebElement row = table.ScrollToRow(12);
            WebElement dateCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            dateCell.Click();

            // Double Click cell #3 to activate edit mode
            Actions builder = new Actions(driver.WebDriver);
            builder.DoubleClick(dateCell).Perform();

            Widget editor = table.CellEditor;
            string old = (string) editor.GetPropertyValue("value");

            // Clear old content
            Actions keyBuilder = (new Actions(driver.WebDriver)).SendKeys(Keys.End);
            for (int i = 0; i < old.Length; i++)
            {
                keyBuilder.SendKeys(Keys.Backspace);
            }
            keyBuilder.Perform();

            // Type new cell content
            editor.SendKeys(newText);
            editor.SendKeys(Keys.Return);

            // update the cell element and check the new content
            row = table.ScrollToRow(12);
            dateCell = row.FindElement(OpenQA.Selenium.By.XPath(cellXpath));
            Assert.Equals(newText, dateCell.Text);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void columnMenu()
        public virtual void ColumnMenu()
        {
            // use the column menu to hide a column
            IList<string> headerLabels = table.HeaderLabels;
            Assert.ArrayAreEqual(new string[] {"ID", "A number", "A date", "Boolean"}, headerLabels.ToArray());

            Selectable colMenuButton = (Selectable) table.ColumnMenuButton;
            colMenuButton.SelectItem("A number");

            headerLabels = table.HeaderLabels;
            Assert.assertArrayEquals(new string[] {"ID", "A date", "Boolean"}, headerLabels.ToArray());
            colMenuButton.SelectItem("A number");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void sortByColumn()
        public virtual void SortByColumn()
        {
            if (Ie)
            {
                return;
            }
            // Click column headers to set the table's sorting order
            Widget idColumnHeader = table.GetHeaderCell("ID");
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