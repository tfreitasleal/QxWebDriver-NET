using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using By = Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Accordion : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Accordion");
        }

        protected internal virtual string GetActivePageText(IWebElement tabs)
        {
            IList<IWebElement> pages =
                tabs.FindElements(OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'qx-tabs-page')]"));
            IEnumerator<IWebElement> itr = pages.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement page = itr.Current;
                if (page.Displayed)
                {
                    return page.FindElement(OpenQA.Selenium.By.XPath("h3")).Text;
                }
            }
            return null;
        }

        //ORIGINAL LINE: @Test public void accordion() throws InterruptedException
        [Test]
        public virtual void TestAccordion()
        {
            IWebElement tabs = webDriver.FindElement(By.Id("accordion-default"));
            IList<IWebElement> tabButtons =
                tabs.FindElements(
                    OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'qx-tabs-button')]/button"));
            tabButtons.Reverse();
            IEnumerator<IWebElement> itr = tabButtons.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement tabButton = itr.Current;
                string buttonText = tabButton.Text;
                tabButton.Click();
                Thread.Sleep(1000);
                string activePageText = GetActivePageText(tabs);
                Assert.True(activePageText.StartsWith(buttonText, StringComparison.Ordinal));
            }
        }
    }
}