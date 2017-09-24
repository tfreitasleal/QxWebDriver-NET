using System;
using System.Collections.Generic;
using System.Threading;
using Qooxdoo.WebDriver:
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Accordion : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Accordion");
        }

        protected internal virtual string getActivePageText(WebElement tabs)
        {
            IList<WebElement> pages = tabs.FindElements(OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'qx-tabs-page')]"));
            IEnumerator<WebElement> itr = pages.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement page = itr.Current;
                if (page.Displayed)
                {
                    return page.FindElement(OpenQA.Selenium.By.XPath("h3")).Text;
                }
            }
            return null;
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void accordion() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void accordion()
        {
            WebElement tabs = webDriver.FindElement(By.Id("accordion-default"));
            IList<WebElement> tabButtons = tabs.FindElements(OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'qx-tabs-button')]/button"));
            tabButtons.Reverse();
            IEnumerator<WebElement> itr = tabButtons.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement tabButton = itr.Current;
                string buttonText = tabButton.Text;
                tabButton.Click();
                Thread.Sleep(1000);
                string activePageText = getActivePageText(tabs);
                Assert.True(activePageText.StartsWith(buttonText, StringComparison.Ordinal));
            }
        }
    }

}