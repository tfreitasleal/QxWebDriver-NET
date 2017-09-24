using System;
using System.Threading;

namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    using Assert = NUnit.Framework.Assert;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using Table = Qooxdoo.WebDriver.UI.Table.Table;

    public class Search : DesktopApiViewer
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void searchApi() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void searchApi()
        {
            SelectView("Search");

            string tablePath = "*/apiviewer.ui.SearchView/*/qx.ui.Table.Table";
            Table table = (Table) driver.FindWidget(By.Qxh(tablePath));
            long? initialRowCount = (long?) table.RowCount;
            Assert.Equals(Convert.ToDouble(0), Convert.ToDouble(initialRowCount));

            TypeInSearch("window");
            Thread.Sleep(500);
            long? resultRowCount = (long?) table.RowCount;
            Assert.True(resultRowCount > 0);

            // the namespace filter isn't applied in FF/Chrome on Linux while the screen is turned off
            if (!System.getProperty("org.qooxdoo.demo.platform").Equals("linux"))
            {
                string namespaceFieldPath = "*/apiviewer.ui.SearchView/qx.ui.container.Composite/child[3]";
                Widget namespaceField = driver.FindWidget(By.Qxh(namespaceFieldPath));
                namespaceField.SendKeys("window");

                // Focus the searchField again to make sure the namespace filter is applied
                string searchFieldPath = "*/apiviewer.ui.SearchView/*/qx.ui.form.TextField";
                Widget searchField = driver.FindWidget(By.Qxh(searchFieldPath));
                searchField.Click();
                Thread.Sleep(500);

                long? filteredRowCount = (long?) table.RowCount;
                string msg = "Namespace filter failed: " + resultRowCount + " before, " + filteredRowCount + " after adding filter.";
                Assert.True(msg, filteredRowCount < resultRowCount);
            }

            Widget toggleButton = driver.FindWidget(By.Qxh("*/apiviewer.ui.SearchView/*/[@label=Toggle Filters]"));
            toggleButton.Click();
            Thread.Sleep(500);
            long? toggledCount = (long?) table.RowCount;
            Assert.Equals(Convert.ToDouble(0), Convert.ToDouble(toggledCount));

            string[] itemIcons = new string[] {"package","class","method_public", "constant", "property", "event", "childcontrol"};
            foreach (string icon in itemIcons)
            {
                string buttonPath = "*/apiviewer.ui.SearchView/*/[@icon=" + icon + "]";
                Widget button = driver.FindWidget(By.Qxh(buttonPath));
                button.Click();
                Thread.Sleep(500);
                if (!icon.Equals("event") && !icon.Equals("childcontrol"))
                {
                    long? newCount = (long?) table.RowCount;
                    Assert.True("Toggling '" + icon + "' did not change the result count! ", newCount > toggledCount);
                    toggledCount = newCount;
                }
            }
        }
    }

}