using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.SimpleDemoTests
{
    public class ApiViewerTests
    {
        private QxWebDriver _driver;

        [Test]
        public void ChromeTest()
        {
            _driver = new QxWebDriver(new ChromeDriver());
            DoTest();
        }

        [Test]
        public void FirefoxTest()
        {
            _driver = new QxWebDriver(new FirefoxDriver());
            DoTest();
        }

        [Test]
        public void EdgeTest()
        {
            _driver = new QxWebDriver(new EdgeDriver());
            DoTest();
        }

        public void DoTest()
        {
            // Open the page and wait until the qooxdoo application is loaded
            _driver.Url = "http://www.qooxdoo.org/current/api/index.html";

            // Find the 'Search' button in the tool bar
            // @label is the button text
            By buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Search]");
            IWidget buttonWidget = _driver.FindWidget(buttonByLabel);

            // Click the button if it's not already selected
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }

            // Now click the 'Legend' button
            buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Legend]");
            buttonWidget = _driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }

            // Now click the 'Content' button
            buttonByLabel = By.Qxh("apiviewer.Viewer/*/[@label=Content]");
            buttonWidget = _driver.FindWidget(buttonByLabel);
            if (!buttonWidget.Selected)
            {
                buttonWidget.Click();
            }

            // Select the "data" item from the package tree
            By tree = By.Qxh("apiviewer.Viewer/*/apiviewer.ui.PackageTree");
            ISelectable packageTree = (ISelectable) _driver.FindWidget(tree);
            packageTree.SelectItem("data");
        }
    }
}