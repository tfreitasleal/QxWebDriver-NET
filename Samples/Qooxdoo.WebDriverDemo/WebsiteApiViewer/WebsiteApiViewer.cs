using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
//using AfterClass = NUnit.Framework.AfterClass;
using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using Qooxdoo.WebDriver;
using IJavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.WebsiteApiViewer
{
    public class WebsiteApiViewer : IntegrationTest
    {

        public static WebDriver webDriver;

        public virtual void scrollNav(int? value)
        {
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            exec.ExecuteScript("arguments[0].ScrollTop = " + value + ";", webDriver.FindElement(By.Id("navContainer")));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            webDriver = Configuration.WebDriver;
            webDriver.Manage().Window().Maximize();
            webDriver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            Thread.Sleep(4000);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void waitForList()
        public virtual void waitForList()
        {
            OpenQA.Selenium.By lastItem = OpenQA.Selenium.By.XPath("//li[@id='list-group-Plugin_API' and contains(@class, 'qx-tabs-page-closed')]");
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriver.FindElement(lastItem);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void warning()
        public virtual void warning()
        {
            WebElement warning = webDriver.FindElement(By.Id("warning"));
            if (warning != null && warning.Displayed)
            {
                Assert.True("Found warning: " + warning.Text, false);
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void syntaxHighlighting() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void syntaxHighlighting()
        {
            webDriver.Url = System.getProperty("org.qooxdoo.demo.auturl") + "#.removeAttribute";
            Thread.Sleep(4000);
            WebElement sampleBlock = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[@id='.removeAttribute']/div[@class='sample']"));
            Assert.IsNotNull(sampleBlock);
            Assert.True(sampleBlock.Displayed);
            WebElement sampleJs = sampleBlock.FindElement(OpenQA.Selenium.By.XPath("//pre[@class='javascript']"));
            Assert.IsNotNull(sampleJs);
            Assert.True(sampleJs.Displayed);
            WebElement sampleSpan = sampleJs.FindElement(OpenQA.Selenium.By.XPath("//span"));
            Assert.IsNotNull(sampleSpan);
            Assert.True(sampleSpan.Displayed);
        }

        public virtual IList<string> Categories
        {
            get
            {
                IList<WebElement> accordionButtons = webDriver.FindElements(OpenQA.Selenium.By.XPath("//div[@id='list']/ul/li[contains(@class, 'qx-tabs-button')]"));
                Assert.AreNotEqual(0, accordionButtons.Count);
                IList<string> categories = new List<string>();
                IEnumerator<WebElement> itr = accordionButtons.GetEnumerator();
                while (itr.MoveNext())
                {
                    categories.Add(itr.Current.Text);
                }
                return categories;
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void listNavigation() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void listNavigation()
        {
            IList<string> categories = Categories;
            Assert.True(categories.Count > 0);
            // Choose a random category from the nav list
            Random rnd = new Random();
            int? catIndex = rnd.Next(categories.Count - 1);
            string categoryName = categories[catIndex.Value];
            string catPath = "//div[@id='list']/ul/li[text()='" + categoryName + "']";
            WebElement catHeader = webDriver.FindElement(OpenQA.Selenium.By.XPath(catPath));
            string catItemPath = catPath + "/following-sibling::li";
            WebElement catItem = webDriver.FindElement(OpenQA.Selenium.By.XPath(catItemPath));
            // The category's corresponding item should be closed initially
            Assert.Equals(0, catItem.Size.Height);
            catHeader.Click();
            Thread.Sleep(1000);
            Assert.True(catItem.Size.Height > 0);

            // Get the category's entries (methods)
            string catEntriesPath = catItemPath + "/descendant::li[starts-with(@class, 'nav-')]/a";
            IList<WebElement> catEntries = webDriver.FindElements(OpenQA.Selenium.By.XPath(catEntriesPath));
            Assert.AreNotEqual(0, catEntries.Count);
            IList<WebElement> displayedEntries = new List<WebElement>();
            IEnumerator<WebElement> itr = catEntries.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement entry = itr.Current;
                if (entry.Displayed)
                {
                    displayedEntries.Add(entry);
                }
            }
            Assert.True("Category '" + categoryName + "' has no displayed entries!", displayedEntries.Count > 0);
            // Click a random entry
            int? entryIndex = 0;
            if (displayedEntries.Count > 1)
            {
                entryIndex = rnd.Next(displayedEntries.Count - 1);
            }
            WebElement entry = displayedEntries[entryIndex.Value];
            entry.Click();
            Assert.Equals(webDriver.Url, entry.GetAttribute("href"));

            // Close the category
            scrollNav(0);
            catHeader.Click();
            Thread.Sleep(1000);
            Assert.Equals(0, catItem.Size.Height);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void listFilter() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void ListFilter()
        {
            string searchTerm = "set";
            WebElement search = webDriver.FindElement(OpenQA.Selenium.By.XPath("//input[@type='search']"));
            search.SendKeys(searchTerm);
            Thread.Sleep(1000);
            // find categories with matching entries
            IList<WebElement> hits = webDriver.FindElements(OpenQA.Selenium.By.XPath("//div[@id='list']/descendant::li[contains(@class, 'qx-tabs-button') and not(contains(@class, 'no-matches'))]"));
            IEnumerator<WebElement> itr = hits.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement hit = itr.Current;
                string hitText = hit.Text;
                hit.Click();
                Thread.Sleep(1000);
                WebElement hitCat = webDriver.FindElement(OpenQA.Selenium.By.XPath("//li[@id='list-group-" + hitText + "']"));
                Assert.True(hitCat.Size.Height > 0);
                IList<WebElement> matchingEntries = hitCat.FindElements(OpenQA.Selenium.By.XPath("ul[not(contains(@style, 'none'))]/li[not(contains(@style, 'none'))]/a"));
                IEnumerator<WebElement> entryItr = matchingEntries.GetEnumerator();
                int hitCount = int.Parse(hit.GetAttribute("data-results"));
                Assert.Equals(hitCount, matchingEntries.Count);
                while (entryItr.MoveNext())
                {
                    WebElement entry = entryItr.Current;
                    string entryLink = entry.GetAttribute("href");
                    WebElement module = entry.FindElement(OpenQA.Selenium.By.XPath("parent::li/parent::ul/preceding-sibling::a/h2"));
                    if (!hitText.Equals("Extras"))
                    {
                        // Contains the 'Dataset' module which matches even though its method names don't
                        Assert.True(entryLink.ToLower().Contains(searchTerm));
                    }
                }

            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void parameterLinks()
        public virtual void parameterLinks()
        {
            string qParamsPath = "//h4[text() = 'Parameters']/following-sibling::div/ul/li";
            WebElement qParams = webDriver.FindElement(OpenQA.Selenium.By.XPath(qParamsPath));
            IList<WebElement> paramLinks = qParams.FindElements(OpenQA.Selenium.By.XPath("a"));
            Assert.True(paramLinks.Count > 0);
            IEnumerator<WebElement> itr = paramLinks.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement link = itr.Current;
                Assert.True(link.GetAttribute("href").StartsWith("https://developer.mozilla.org"));
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void returnTypeLinks() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void returnTypeLinks()
        {
            IDictionary<string, string> mdnLinks = new Dictionary<string, string>();
            mdnLinks["q.messaging.on"] = "String";
            mdnLinks["q.localStorage.getLength"] = "Number";
            mdnLinks[".getTransformBackfaceVisibility"] = "Boolean";
            mdnLinks["Array.every"] = "Array";
            mdnLinks["q.$getEventNormalizationRegistry"] = "Map";
            mdnLinks["q.define"] = "Function";
            IEnumerator itr = mdnLinks.SetOfKeyValuePairs().GetEnumerator();
            while (itr.MoveNext())
            {
                DictionaryEntry pairs = (DictionaryEntry) itr.Current;
                WebElement returnLink = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[@id='" + pairs.Key + "']/div[contains(@class, 'return-desc')]/descendant::a"));
                Assert.Equals("unexpected return type for '" + pairs.Key + "'", pairs.Value, returnLink.Text);
                Assert.True(returnLink.GetAttribute("href").StartsWith("https://developer.mozilla.org"));
//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
                itr.Remove();
            }

            WebElement returnLink = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[@id='.getAncestors']/div[contains(@class, 'return-desc')]/descendant::a"));
            Assert.Equals("unexpected return type for '.getAncestors'", "q", returnLink.Text);
            Assert.True(returnLink.GetAttribute("href").EndsWith("#Core"));

            returnLink = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[@id='q.io.xhr']/div[contains(@class, 'return-desc')]/descendant::a"));
            Assert.Equals("unexpected return type for '.getAncestors'", "Xhr", returnLink.Text);
            Assert.True(returnLink.GetAttribute("href").EndsWith("#Xhr"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void editSample() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void editSample()
        {
            string editorUrl = "http://jsfiddle.net/api/post/library/pure/";
            IList<WebElement> editButtons = webDriver.FindElements(By.ClassName("fiddlebutton"));
            if (editButtons.Count == 0)
            {
                editorUrl = "http://codepen.io/pen";
                editButtons = webDriver.FindElements(By.ClassName("button-codepen"));
                Assert.True(editButtons.Count > 0);
            }
            Random rnd = new Random();
            int? btnIdx = rnd.Next(editButtons.Count - 1);
            editButtons[btnIdx].Click();
            Thread.Sleep(1000);
            string initialHandle = webDriver.CurrentWindowHandle;
            ReadOnlyCollection<string> handles = webDriver.WindowHandles;
            IEnumerator<string> itr = handles.GetEnumerator();
            while (itr.MoveNext())
            {
                string handle = itr.Current;
                if (!handle.Equals(initialHandle))
                {
                    webDriver.SwitchTo().Window(handle);
                    Assert.Equals(editorUrl, webDriver.Url);
                    webDriver.Close();
                }
            }
            webDriver.SwitchTo().Window(initialHandle);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void TearDownAfterClass()
        {
            webDriver.Quit();
        }
    }

}