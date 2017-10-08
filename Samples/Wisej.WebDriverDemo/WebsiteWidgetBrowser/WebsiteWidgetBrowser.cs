using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    public abstract class WebsiteWidgetBrowser : IntegrationTest
    {
        public static IWebDriver webDriver;

        public static void SelectTab(string title)
        {
            string xpath = "//div[contains(@class, 'qx-tabs')]/descendant::button[text() = '" + title + "']";
            IWebElement button = webDriver.FindElement(OpenQA.Selenium.By.XPath(xpath));
            button.Click();
            try
            {
                Thread.Sleep(1000);
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public new static void SetUpBeforeClass()
        {
            webDriver = Configuration.WebDriver;
            webDriver.Manage().Window.Maximize();
            webDriver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Thread.Sleep(1000);
        }

        public new static void TearDownAfterClass()
        {
            webDriver.Quit();
        }
    }
}