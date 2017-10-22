using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Tabs : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Tabs");
        }

        protected internal virtual string GetActivePageText(IWebElement tabs)
        {
            IList<IWebElement> pages = tabs.FindElements(By.XPath("descendant::div[contains(@class, 'qx-tabs-page')]"));
            IEnumerator<IWebElement> itr = pages.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement page = itr.Current;
                if (page.Displayed)
                {
                    return page.Text;
                }
            }
            return null;
        }

        //ORIGINAL LINE: @Test public void tabs() throws InterruptedException
        [Test]
        public virtual void SetTabs()
        {
            IList<IWebElement> alignmentRadios =
                webDriver.FindElements(By.XPath("//div[@id = 'tabs-page']/descendant::input[@name = 'tabalign']"));
            IEnumerator<IWebElement> itr = alignmentRadios.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement radio = itr.Current;
                radio.Click();
                TestTabs();
            }
        }

        protected internal virtual void TestTabs()
        {
            IWebElement tabs = webDriver.FindElement(By.XPath("//div[@id = 'tabs-page']/descendant::div[contains(@class, 'qx-tabs')]"));
            IList<IWebElement> tabButtons = tabs.FindElements(By.XPath("descendant::li[contains(@class, 'qx-tabs-button')]/button"));
            IEnumerator<IWebElement> itr = tabButtons.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement tabButton = itr.Current;
                string buttonText = tabButton.Text;
                tabButton.Click();
                string activePageText = GetActivePageText(tabs);
                Assert.Equals(buttonText, activePageText);
            }
        }
    }
}