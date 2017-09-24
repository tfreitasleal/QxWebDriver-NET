using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assert = NUnit.Framework.Assert;
using By = Qooxdoo.WebDriver.By;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;
using ExpectedCondition = OpenQA.Selenium.Support.UI.ExpectedCondition;
using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    public abstract class DesktopShowcase : IntegrationTest
    {

        public string QxVersion = null;

        public virtual void SelectPage(string title)
        {
            QxVersion = (string) driver.ExecuteScript("return qx.core.Environment.get('qx.version')");
            WebElement root = driver.FindElement(By.Id("showcase"));
            By locator = By.Qxh("*/showcase.ui.PreviewList/*/[@label=" + title + "]");
            WebElement item = root.FindElement(locator);
            item.Click();
            WaitUntilPageLoaded();
            CheckLinks();
        }

        public virtual WebElement Root
        {
            get
            {
                return driver.FindElement(By.Id("showcase"));
            }
        }

        public virtual bool? Loading
        {
            get
            {
                By locator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/[@source=loading]", false);
                WebElement spinner = Root.FindElement(locator);
                return spinner.Displayed;
            }
        }

        public virtual ExpectedCondition<bool?> ShowcaseLoaded()
        {
            return new ExpectedConditionAnonymousInnerClass(this);
        }

        private class ExpectedConditionAnonymousInnerClass : ExpectedCondition<bool?>
        {
            private readonly DesktopShowcase outerInstance;

            public ExpectedConditionAnonymousInnerClass(DesktopShowcase outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public bool? Apply(WebDriver webDriver)
            {
                return !outerInstance.Loading;
            }

            public string ToString()
            {
                return "Showcase page has finished loading.";
            }
        }

        public virtual void WaitUntilPageLoaded()
        {
            (new WebDriverWait(driver, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(250))).Until(ShowcaseLoaded());
        }

        public virtual void CheckLinks()
        {
            string initialHandle = driver.CurrentWindowHandle;
            IList<WebElement> links = driver.FindElements(OpenQA.Selenium.By.XPath("//div[@id='showcase']/descendant::div[@id='description']/descendant::a"));
            IEnumerator<WebElement> iter = links.GetEnumerator();
            while (iter.MoveNext())
            {
                WebElement link = iter.Current;
                string linkText = link.Text;
                if (linkText.Equals("Unicode Common Locale Data Repository"))
                {
                    // doesn't open in new window
                    continue;
                }
                string href = link.GetAttribute("href");
                link.Click();
                ReadOnlyCollection<string> handles = driver.WindowHandles;
                Assert.Equals(2, handles.Count);
                IEnumerator<string> itr = handles.GetEnumerator();
                while (itr.MoveNext())
                {
                    string handle = itr.Current;
                    if (!handle.Equals(initialHandle))
                    {
                        driver.SwitchTo().Window(handle);
                        if (!linkText.Equals("YQL"))
                        {
                            string newUrl = driver.Url;
                            Assert.Equals("Link " + linkText + " did not open URI " + href, href, newUrl);
                            Assert.True("Link " + linkText + " does not point to qx version " + QxVersion, QxVersionMatches(newUrl));
                        }
                        driver.Close();
                    }
                }
                driver.SwitchTo().Window(initialHandle);
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