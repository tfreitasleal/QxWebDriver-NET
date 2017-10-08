using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Table;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopApiViewer
{
    [TestFixture]
    public class Search : DesktopApiViewer
    {
        public virtual void SearchApi()
        {
            SelectView("Search");

            string tablePath = "*/apiviewer.ui.SearchView/*/qx.ui.table.Table";
            Table table = (Table) Driver.FindWidget(By.Qxh(tablePath));
            long? initialRowCount = (long?) table.RowCount;
            Assert.Equals(Convert.ToDouble(0), Convert.ToDouble(initialRowCount));

            TypeInSearch("window");
            Thread.Sleep(500);
            long? resultRowCount = (long?) table.RowCount;
            Assert.True(resultRowCount > 0);

            // the namespace filter isn't applied in FF/Chrome on Linux while the screen is turned off
            //if (!SystemProperties.GetProperty("org.qooxdoo.demo.platform").Equals("linux"))
            if (Platform.CurrentPlatform.PlatformType != PlatformType.Linux)
            {
                string namespaceFieldPath = "*/apiviewer.ui.SearchView/qx.ui.container.Composite/child[3]";
                IWidget namespaceField = Driver.FindWidget(By.Qxh(namespaceFieldPath));
                namespaceField.SendKeys("window");

                // Focus the searchField again to make sure the namespace filter is applied
                string searchFieldPath = "*/apiviewer.ui.SearchView/*/qx.ui.form.TextField";
                IWidget searchField = Driver.FindWidget(By.Qxh(searchFieldPath));
                searchField.Click();
                Thread.Sleep(500);

                long? filteredRowCount = (long?) table.RowCount;
                string msg = "Namespace filter failed: " + resultRowCount + " before, " + filteredRowCount +
                             " after adding filter.";
                Assert.True(filteredRowCount < resultRowCount, msg);
            }

            IWidget toggleButton = Driver.FindWidget(By.Qxh("*/apiviewer.ui.SearchView/*/[@label=Toggle Filters]"));
            toggleButton.Click();
            Thread.Sleep(500);
            long? toggledCount = (long?) table.RowCount;
            Assert.Equals(Convert.ToDouble(0), Convert.ToDouble(toggledCount));

            string[] itemIcons = {"package", "class", "method_public", "constant", "property", "event", "childcontrol"};
            foreach (string icon in itemIcons)
            {
                string buttonPath = "*/apiviewer.ui.SearchView/*/[@icon=" + icon + "]";
                IWidget button = Driver.FindWidget(By.Qxh(buttonPath));
                button.Click();
                Thread.Sleep(500);
                if (!icon.Equals("event") && !icon.Equals("childcontrol"))
                {
                    long? newCount = (long?) table.RowCount;
                    Assert.True(newCount > toggledCount, "Toggling '" + icon + "' did not change the result count! ");
                    toggledCount = newCount;
                }
            }
        }
    }
}