using System;
using System.Collections.Generic;
using System.Threading;
//using SelendroidLauncher = io.selendroid.standalone.SelendroidLauncher;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using ISelectable = Qooxdoo.WebDriver.UI.Mobile.ISelectable;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    public abstract class Mobileshowcase : IntegrationTest
    {
        //public static SelendroidLauncher selendroidServer;
        public new static QxWebDriver Driver;

        public static IWebDriver webDriver;

        protected internal string navigationList =
            "//div[contains(@class, 'layout-card')]/descendant::div[contains(@class, 'group')]";

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            //SelendroidConfiguration config = new SelendroidConfiguration();
            //selendroidServer = new SelendroidLauncher(config);
            //selendroidServer.lauchSelendroid();

            Driver = Configuration.QxWebDriver;
            webDriver = Driver.WebDriver;
            Driver.Manage().Window.Maximize();
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");

            Driver.RegisterLogAppender();
            Driver.RegisterGlobalErrorHandler();
        }

        public static void ScrollTo(int x, int y)
        {
            string script = "qx.ui.mobile.core.Widget.getWidgetById(arguments[0].id).ScrollTo(" + x + ", " + y + ")";
            IList<IWebElement> scrollers = Driver.FindElements(OpenQA.Selenium.By.ClassName("scroll"));
            IEnumerator<IWebElement> itr = scrollers.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement scroller = itr.Current;
                if (scroller.Displayed)
                {
                    Driver.ExecuteScript(script, scroller);
                }
            }
        }

        public static void SelectItem(string title)
        {
            string overviewButtonLoc =
                "//div[text() = 'Overview']/ancestor::div[contains(@class, 'navigationbar-button')]";
            try
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                ITouchable overviewButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(overviewButtonLoc));
                Console.WriteLine("Tapping Overview button");
                overviewButton.Tap();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                // wait until the navigation list animation has finished
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
            }

            Console.WriteLine("Selecting item '" + title + "'");
            ISelectable list = (ISelectable) Driver.FindWidget(OpenQA.Selenium.By.XPath(
                "//div[contains(@class, 'master-detail-master')]/descendant::ul[contains(@class, 'list')]"));
            list.SelectItem(title);
            // wait until the page change animation has finished
            Thread.Sleep(1000);
        }

        public static void VerifyTitle(string title)
        {
            string titleXpath = "//h1[contains(@class, 'title') and text() = '" + title + "']";
            IWebElement titleElement = Driver.FindElement(OpenQA.Selenium.By.XPath(titleXpath));
            Assert.Equals(title, titleElement.Text);
        }

        public static void GoBack()
        {
            ITouchable backButton =
                (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'navigationbar-backbutton')]"));
            if (backButton.Displayed)
            {
                Console.WriteLine("Going back");
                backButton.Tap();
                // wait until the page change animation has finished
                Thread.Sleep(1000);
            }
        }

        [OneTimeTearDown]
        public new static void TearDownAfterClass()
        {
            GoBack();
            IntegrationTest.PrintQxLog(Driver);
            IntegrationTest.PrintQxErrors(Driver);

            Driver.Quit();
            //selendroidServer.stopSelendroid();
        }
    }
}