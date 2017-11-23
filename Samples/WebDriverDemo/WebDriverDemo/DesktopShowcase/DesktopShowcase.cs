using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using By = Qooxdoo.WebDriver.By;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    public abstract class DesktopShowcase : IntegrationTest
    {
        public string QxVersion = null;

        public virtual void SelectPage(string title)
        {
            QxVersion = (string) Driver.ExecuteScript("return qx.core.Environment.get('qx.version')");
            IWebElement root = Driver.FindElement(By.Id("showcase"));
            By locator = By.Qxh("*/showcase.ui.PreviewList/*/[@label=" + title + "]");
            IWebElement item = root.FindElement(locator);
            item.Click();
            WaitUntilPageLoaded();
            CheckLinks();
        }

        public virtual IWebElement Root
        {
            get { return Driver.FindElement(OpenQA.Selenium.By.Id("showcase")); }
        }

        public virtual bool? Loading
        {
            get
            {
                By locator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/[@source=loading]", false);
                IWebElement spinner = Root.FindElement(locator);
                return spinner.Displayed;
            }
        }

        public virtual Func<IWebDriver, bool?> ShowcaseLoaded()
        {
            return driver => { return !Loading; };
        }

        /*public ExpectedCondition<Boolean> showcaseLoaded() {
            return new ExpectedCondition<Boolean>() {
                @Override
                public Boolean apply(WebDriver webDriver) {
                    return !isLoading();
                }

                @Override
                public String toString() {
                    return "Showcase page has finished loading.";
                }
            };
        }*/

        public virtual void WaitUntilPageLoaded()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(20)).Until(ShowcaseLoaded());
        }

        public virtual void CheckLinks()
        {
            string initialHandle = Driver.CurrentWindowHandle;
            IList<IWebElement> links = Driver.FindElements(
                OpenQA.Selenium.By.XPath("//div[@id='showcase']/descendant::div[@id='description']/descendant::a"));
            IEnumerator<IWebElement> iter = links.GetEnumerator();
            while (iter.MoveNext())
            {
                IWebElement link = iter.Current;
                string linkText = link.Text;
                if (linkText.Equals("Unicode Common Locale Data Repository"))
                {
                    // doesn't open in new window
                    continue;
                }
                string href = link.GetAttribute("href");
                link.Click();
                ReadOnlyCollection<string> handles = Driver.WindowHandles;
                Assert.Equals(2, handles.Count);
                IEnumerator<string> itr = handles.GetEnumerator();
                while (itr.MoveNext())
                {
                    string handle = itr.Current;
                    if (!handle.Equals(initialHandle))
                    {
                        Driver.SwitchTo().Window(handle);
                        if (!linkText.Equals("YQL"))
                        {
                            string newUrl = Driver.Url;
                            Assert.AreEqual(href, newUrl, "Link " + linkText + " did not open URI " + href);
                            Assert.True(QxVersionMatches(newUrl),
                                "Link " + linkText + " does not point to qx version " + QxVersion);
                        }
                        Driver.Close();
                    }
                }
                Driver.SwitchTo().Window(initialHandle);
            }
        }

        public virtual bool? QxVersionMatches(string uri)
        {
            string[] split0 = uri.Split("\\.org\\/", true);
            string[] split1 = split0[1].Split("\\/", true);
            return split1[0].Equals(QxVersion);
        }
    }
}