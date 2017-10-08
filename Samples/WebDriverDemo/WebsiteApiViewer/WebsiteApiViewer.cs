using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteApiViewer
{
    [TestFixture]
    public class WebsiteApiViewer : IntegrationTest
    {
        public static IWebDriver webDriver;

        public virtual void ScrollNav(int? value)
        {
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            exec.ExecuteScript("arguments[0].ScrollTop = " + value + ";", webDriver.FindElement(By.Id("navContainer")));
        }

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            webDriver = Configuration.WebDriver;
            webDriver.Manage().Window.Maximize();
            webDriver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Thread.Sleep(4000);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void waitForList()
        [SetUp]
        public virtual void WaitForList()
        {
            By lastItem = By.XPath("//li[@id='list-group-Plugin_API' and contains(@class, 'qx-tabs-page-closed')]");
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriver.FindElement(lastItem);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void warning()
        [Test]
        public virtual void Warning()
        {
            IWebElement warning = webDriver.FindElement(By.Id("warning"));
            if (warning != null && warning.Displayed)
            {
                Assert.True(false, "Found warning: " + warning.Text);
            }
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void syntaxHighlighting() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void SyntaxHighlighting()
        {
            webDriver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl") + "#.removeAttribute";
            Thread.Sleep(4000);
            IWebElement sampleBlock =
                webDriver.FindElement(By.XPath("//div[@id='.removeAttribute']/div[@class='sample']"));
            Assert.IsNotNull(sampleBlock);
            Assert.True(sampleBlock.Displayed);
            IWebElement sampleJs = sampleBlock.FindElement(By.XPath("//pre[@class='javascript']"));
            Assert.IsNotNull(sampleJs);
            Assert.True(sampleJs.Displayed);
            IWebElement sampleSpan = sampleJs.FindElement(By.XPath("//span"));
            Assert.IsNotNull(sampleSpan);
            Assert.True(sampleSpan.Displayed);
        }

        public virtual IList<string> Categories
        {
            get
            {
                IList<IWebElement> accordionButtons =
                    webDriver.FindElements(By.XPath("//div[@id='list']/ul/li[contains(@class, 'qx-tabs-button')]"));
                Assert.AreNotEqual(0, accordionButtons.Count);
                IList<string> categories = new List<string>();
                IEnumerator<IWebElement> itr = accordionButtons.GetEnumerator();
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
        [Test]
        public virtual void ListNavigation()
        {
            IList<string> categories = Categories;
            Assert.True(categories.Count > 0);
            // Choose a random category from the nav list
            Random rnd = new Random();
            int? catIndex = rnd.Next(categories.Count - 1);
            string categoryName = categories[catIndex.Value];
            string catPath = "//div[@id='list']/ul/li[text()='" + categoryName + "']";
            IWebElement catHeader = webDriver.FindElement(By.XPath(catPath));
            string catItemPath = catPath + "/following-sibling::li";
            IWebElement catItem = webDriver.FindElement(By.XPath(catItemPath));
            // The category's corresponding item should be closed initially
            Assert.Equals(0, catItem.Size.Height);
            catHeader.Click();
            Thread.Sleep(1000);
            Assert.True(catItem.Size.Height > 0);

            // Get the category's entries (methods)
            string catEntriesPath = catItemPath + "/descendant::li[starts-with(@class, 'nav-')]/a";
            IList<IWebElement> catEntries = webDriver.FindElements(By.XPath(catEntriesPath));
            Assert.AreNotEqual(0, catEntries.Count);
            IList<IWebElement> displayedEntries = new List<IWebElement>();
            IEnumerator<IWebElement> itr = catEntries.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement entry2 = itr.Current;
                if (entry2.Displayed)
                {
                    displayedEntries.Add(entry2);
                }
            }
            Assert.True(displayedEntries.Count > 0, "Category '" + categoryName + "' has no displayed entries!");
            // Click a random entry
            int? entryIndex = 0;
            if (displayedEntries.Count > 1)
            {
                entryIndex = rnd.Next(displayedEntries.Count - 1);
            }
            IWebElement entry = displayedEntries[entryIndex.Value];
            entry.Click();
            Assert.Equals(webDriver.Url, entry.GetAttribute("href"));

            // Close the category
            ScrollNav(0);
            catHeader.Click();
            Thread.Sleep(1000);
            Assert.Equals(0, catItem.Size.Height);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void listFilter() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ListFilter()
        {
            string searchTerm = "set";
            IWebElement search = webDriver.FindElement(By.XPath("//input[@type='search']"));
            search.SendKeys(searchTerm);
            Thread.Sleep(1000);
            // find categories with matching entries
            IList<IWebElement> hits = webDriver.FindElements(By.XPath(
                "//div[@id='list']/descendant::li[contains(@class, 'qx-tabs-button') and not(contains(@class, 'no-matches'))]"));
            IEnumerator<IWebElement> itr = hits.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement hit = itr.Current;
                string hitText = hit.Text;
                hit.Click();
                Thread.Sleep(1000);
                IWebElement hitCat =
                    webDriver.FindElement(By.XPath("//li[@id='list-group-" + hitText + "']"));
                Assert.True(hitCat.Size.Height > 0);
                IList<IWebElement> matchingEntries =
                    hitCat.FindElements(
                        By.XPath("ul[not(contains(@style, 'none'))]/li[not(contains(@style, 'none'))]/a"));
                IEnumerator<IWebElement> entryItr = matchingEntries.GetEnumerator();
                int hitCount = int.Parse(hit.GetAttribute("data-results"));
                Assert.Equals(hitCount, matchingEntries.Count);
                while (entryItr.MoveNext())
                {
                    IWebElement entry = entryItr.Current;
                    string entryLink = entry.GetAttribute("href");
                    IWebElement module =
                        entry.FindElement(OpenQA.Selenium.By.XPath("parent::li/parent::ul/preceding-sibling::a/h2"));
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
        [Test]
        public virtual void ParameterLinks()
        {
            string qParamsPath = "//h4[text() = 'Parameters']/following-sibling::div/ul/li";
            IWebElement qParams = webDriver.FindElement(OpenQA.Selenium.By.XPath(qParamsPath));
            IList<IWebElement> paramLinks = qParams.FindElements(OpenQA.Selenium.By.XPath("a"));
            Assert.True(paramLinks.Count > 0);
            IEnumerator<IWebElement> itr = paramLinks.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement link = itr.Current;
                Assert.True(link.GetAttribute("href").StartsWith("https://developer.mozilla.org"));
            }
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void returnTypeLinks() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ReturnTypeLinks()
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
                IWebElement returnLink = webDriver.FindElement(OpenQA.Selenium.By.XPath(
                    "//div[@id='" + pairs.Key + "']/div[contains(@class, 'return-desc')]/descendant::a"));
                Assert.AreEqual(returnLink.Text, pairs.Value, "unexpected return type for '" + pairs.Key + "'");
                Assert.True(returnLink.GetAttribute("href").StartsWith("https://developer.mozilla.org"));

                //JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
                //itr.Remove();

                // todo solve itr.Remove()
            }

            IWebElement returnLink2 = webDriver.FindElement(
                By.XPath("//div[@id='.getAncestors']/div[contains(@class, 'return-desc')]/descendant::a"));
            Assert.AreEqual("q", returnLink2.Text, "unexpected return type for '.getAncestors'");
            Assert.True(returnLink2.GetAttribute("href").EndsWith("#Core"));

            returnLink2 =
                webDriver.FindElement(By.XPath("//div[@id='q.io.xhr']/div[contains(@class, 'return-desc')]/descendant::a"));
            Assert.AreEqual("Xhr", returnLink2.Text, "unexpected return type for '.getAncestors'");
            Assert.True(returnLink2.GetAttribute("href").EndsWith("#Xhr"));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void editSample() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void EditSample()
        {
            string editorUrl = "http://jsfiddle.net/api/post/library/pure/";
            IList<IWebElement> editButtons = webDriver.FindElements(By.ClassName("fiddlebutton"));
            if (editButtons.Count == 0)
            {
                editorUrl = "http://codepen.io/pen";
                editButtons = webDriver.FindElements(By.ClassName("button-codepen"));
                Assert.True(editButtons.Count > 0);
            }
            Random rnd = new Random();
            int? btnIdx = rnd.Next(editButtons.Count - 1);
            editButtons[btnIdx.Value].Click();
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

        [OneTimeTearDown]
        public new static void TearDownAfterClass()
        {
            webDriver.Quit();
        }
    }
}