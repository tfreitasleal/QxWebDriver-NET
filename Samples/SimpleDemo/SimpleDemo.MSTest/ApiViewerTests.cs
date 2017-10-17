using System.Threading;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;

namespace SimpleDemo.MSTest
{
    public static class ApiViewerTests
    {
        public static void A01_ClickSearch(QxWebDriver driver)
        {
            // Find the 'Search' button in the tool bar
            // @label is the button text
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Search]");
            IWidget buttonWidget = driver.FindWidget(buttonByLabel);

            // Click the button if it's not already selected
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A02_ClickLegend(QxWebDriver driver)
        {
            // Now click the 'Legend' button
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Legend]");
            IWidget buttonWidget = driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A03_ClickContent(QxWebDriver driver)
        {
            // Now click the 'Content' button
            OpenQA.Selenium.By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Content]");
            IWidget buttonWidget = driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }
        }

        public static void A04_ClickTreeItem(QxWebDriver driver)
        {
            // Select the "data" item from the package tree
            OpenQA.Selenium.By tree = By.Qxh("apiviewer.Viewer/*/apiviewer.ui.PackageTree");
            ISelectable packageTree = (ISelectable) driver.FindWidget(tree);
            packageTree.SelectItem("data");

            Thread.Sleep(2000);
        }
    }
}