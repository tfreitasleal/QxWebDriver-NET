using System.Threading;
using Qooxdoo.WebDriver;
//using AfterClass = NUnit.Framework.AfterClass;
//using BeforeClass = NUnit.Framework.BeforeClass;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public abstract class WebsiteWidgetBrowser : IntegrationTest
    {

        public static WebDriver webDriver;

        public static void selectTab(string title)
        {
            string xpath = "//div[contains(@class, 'qx-tabs')]/descendant::button[text() = '" + title + "']";
            WebElement button = webDriver.FindElement(OpenQA.Selenium.By.XPath(xpath));
            button.Click();
            try
            {
                Thread.Sleep(1000);
            }
            catch (InterruptedException)
            {
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            webDriver = Configuration.WebDriver;
            webDriver.Manage().Window().Maximize();
            webDriver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            Thread.Sleep(1000);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void tearDownAfterClass()
        {
            webDriver.Quit();
        }
    }

}