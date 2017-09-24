using System;
using System.Collections.Generic;
using System.Threading;
using Qooxdoo.WebDriver.UI.mobile;
using SelendroidLauncher = io.selendroid.standalone.SelendroidLauncher;
//using AfterClass = NUnit.Framework.AfterClass;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
using QxWebDriver = Qooxdoo.WebDriver.QxWebDriver;
using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    public abstract class Mobileshowcase : IntegrationTest
    {

        public static SelendroidLauncher selendroidServer;
        public static QxWebDriver driver;
        public static WebDriver webDriver;

        protected internal string navigationList = "//div[contains(@class, 'layout-card')]/descendant::div[contains(@class, 'group')]";

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            //SelendroidConfiguration config = new SelendroidConfiguration();
            //selendroidServer = new SelendroidLauncher(config);
            //selendroidServer.lauchSelendroid();

            driver = Configuration.QxWebDriver;
            webDriver = driver.WebDriver;
            driver.Manage().Window().Maximize();
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");

            driver.RegisterLogAppender();
            driver.RegisterGlobalErrorHandler();
        }

        public static void ScrollTo(int x, int y)
        {
            string script = "qx.ui.mobile.core.Widget.getWidgetById(arguments[0].id).ScrollTo(" + x + ", " + y + ")";
            IList<WebElement> scrollers = driver.FindElements(OpenQA.Selenium.By.ClassName("scroll"));
            IEnumerator<WebElement> itr = scrollers.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement scroller = itr.Current;
                if (scroller.Displayed)
                {
                    driver.ExecuteScript(script, scroller);
                }
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void SelectItem(String title) throws InterruptedException
        public static void SelectItem(string title)
        {
            string overviewButtonLoc = "//div[text() = 'Overview']/ancestor::div[contains(@class, 'navigationbar-button')]";
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                Touchable overviewButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(overviewButtonLoc));
                Console.WriteLine("Tapping Overview button");
                overviewButton.Tap();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                // wait until the navigation list animation has finished
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
            }

            Console.WriteLine("Selecting item '" + title + "'");
            ISelectable list = (ISelectable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'master-detail-master')]/descendant::ul[contains(@class, 'list')]"));
            list.SelectItem(title);
            // wait until the page change animation has finished
            Thread.Sleep(1000);
        }

        public static void VerifyTitle(string title)
        {
            string titleXpath = "//h1[contains(@class, 'title') and text() = '" + title + "']";
            WebElement titleElement = driver.FindElement(OpenQA.Selenium.By.XPath(titleXpath));
            Assert.Equals(title, titleElement.Text);
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void goBack() throws InterruptedException
        public static void GoBack()
        {
            Touchable backButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'navigationbar-backbutton')]"));
            if (backButton.Displayed)
            {
                Console.WriteLine("Going back");
                backButton.Tap();
                // wait until the page change animation has finished
                Thread.Sleep(1000);
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void TearDownAfterClass()
        {
            GoBack();
            IntegrationTest.printQxLog(driver);
            IntegrationTest.printQxErrors(driver);

            driver.Quit();
            //selendroidServer.stopSelendroid();
        }

    }

}