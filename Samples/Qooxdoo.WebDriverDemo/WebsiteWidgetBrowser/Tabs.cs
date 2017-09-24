using System.Collections.Generic;
using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Tabs : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Tabs");
        }

        protected internal virtual string getActivePageText(WebElement tabs)
        {
            IList<WebElement> pages = tabs.FindElements(OpenQA.Selenium.By.XPath("descendant::div[contains(@class, 'qx-tabs-page')]"));
            IEnumerator<WebElement> itr = pages.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement page = itr.Current;
                if (page.Displayed)
                {
                    return page.Text;
                }
            }
            return null;
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tabs() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void tabs()
        {
            IList<WebElement> alignmentRadios = webDriver.FindElements(OpenQA.Selenium.By.XPath("//div[@id = 'tabs-page']/descendant::input[@name = 'tabalign']"));
            IEnumerator<WebElement> itr = alignmentRadios.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement radio = itr.Current;
                radio.Click();
                testTabs();
            }
        }

        protected internal virtual void testTabs()
        {
            WebElement tabs = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[@id = 'tabs-page']/descendant::div[contains(@class, 'qx-tabs')]"));
            IList<WebElement> tabButtons = tabs.FindElements(OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'qx-tabs-button')]/button"));
            IEnumerator<WebElement> itr = tabButtons.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement tabButton = itr.Current;
                string buttonText = tabButton.Text;
                tabButton.Click();
                string activePageText = getActivePageText(tabs);
                Assert.Equals(buttonText, activePageText);
            }
        }
    }

}