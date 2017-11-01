using System.Threading;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Wait = SimpleDemo.Tests.Waiter;

namespace SimpleDemo.Tests
{
    public static class ApiViewerTests
    {
        public static QxWebDriver Driver;

        public static void A01_ClickSearch()
        {
            // Find the 'Search' button in the tool bar
            // @label is the button text
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Search]");
            IWidget buttonWidget = Driver.FindWidget(buttonByLabel);

            // Click the button if it's not already selected
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A02_ClickLegend()
        {
            // Now click the 'Legend' button
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Legend]");
            IWidget buttonWidget = Driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A03_ClickContent()
        {
            // Now click the 'Content' button
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Content]");
            IWidget buttonWidget = Driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A04_ClickTreeItem()
        {
            // Select the "data" item from the package tree
            OpenQA.Selenium.By tree = By.Qxh("apiviewer.Viewer/*/apiviewer.ui.PackageTree");
            ISelectable packageTree = (ISelectable) Driver.FindWidget(tree);
            packageTree.SelectItem("data");

            Thread.Sleep(Wait.Duration);
        }
    }
}