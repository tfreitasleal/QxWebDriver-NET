using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Basic;
using Qooxdoo.WebDriver.UI.Tree;
using By = Qooxdoo.WebDriver.By;
using List = Qooxdoo.WebDriver.UI.Form.List;

namespace Qooxdoo.WebDriverDemo.FeedReaderDesktop
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class FeedReader : IntegrationTest
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            IntegrationTest.SetUpBeforeClass();
            Driver.JsExecutor.ExecuteScript("qx.locale.Manager.getInstance().setLocale('en');");
        }

        public static string toolbarLocator = "qx.ui.container.Composite/feedreader.view.desktop.ToolBar";
        public static string prefWinLocator = "feedreader.view.desktop.PreferenceWindow";
        public static string treeLocator = "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.tree.Tree";

        public static string articleLoc =
            "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.splitpane.Pane/feedreader.view.desktop.Article";

        public static string addFeedLoc = "feedreader.view.desktop.AddFeedWindow";

        //ORIGINAL LINE: @Before public void waitUntilFeedsLoaded()
        [SetUp]
        public virtual void WaitUntilFeedsLoaded()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until(FeedsReady());
        }

        private List _postList;

        public virtual List PostList
        {
            get
            {
                if (_postList == null)
                {
                    string listLoc =
                        "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.splitpane.Pane/feedreader.view.desktop.List";
                    IWidget frList = Driver.FindWidget(By.Qxh(listLoc));
                    IWebElement listEl = (IWebElement) frList.ExecuteJavascript(
                        "return qx.ui.core.Widget.getWidgetByElement(arguments[0]).getList().getContentElement().getDomElement()");
                    List list = (List) Driver.GetWidgetForElement(listEl);
                    _postList = list;
                }

                return _postList;
            }
        }

        public Func<IWebDriver, bool> FeedsReady()
        {
            return driver =>
            {
                By treeLoc = By.Qxh(treeLocator);
                Tree tree = (Tree) ((QxWebDriver) driver).FindWidget(treeLoc);
                List<IWidget> items = (List<IWidget>) tree.GetWidgetListFromProperty("items");
                List<IWidget>.Enumerator itr = items.GetEnumerator();
                while (itr.MoveNext())
                {
                    IWidget item = itr.Current;
                    string iconSrc = item.GetChildControl("icon").GetPropertyValue("source") as string;
                    if (iconSrc.Contains("loading"))
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        /*public ExpectedCondition<Boolean> feedsReady() {
            return new ExpectedCondition<Boolean>() {
                @Override
                public Boolean apply(WebDriver webDriver) {
                    By treeLoc = By.qxh(treeLocator);
                    Tree tree = (Tree) driver.findWidget(treeLoc);
                    java.util.List<Widget> items = (java.util.List<Widget>) tree.getWidgetListFromProperty("items");
                    Iterator<Widget> itr = items.iterator();
                    while (itr.hasNext()) {
                        Widget item = (Widget) itr.next();
                        String iconSrc = (String) item.getChildControl("icon").getPropertyValue("source");
                        if (iconSrc.contains("loading")) {
                            return false;
                        }
                    }
                    return true;
                }

                @Override
                public String toString() {
                    return "All feeds have finished loading.";
                }
            };
        }*/

        //ORIGINAL LINE: @Test public void feedsLoaded()
        [Test]
        public virtual void FeedsLoaded()
        {
            By treeLoc = By.Qxh(treeLocator);
            Tree tree = (Tree) Driver.FindWidget(treeLoc);
            IList<IWidget> items = tree.GetWidgetListFromProperty("items");
            IEnumerator<IWidget> itr = items.GetEnumerator();
            while (itr.MoveNext())
            {
                IWidget item = itr.Current;
                Label label = (Label) item.GetChildControl("label");
                string feedTitle = label.Value;
                string iconSrc = (string) item.GetChildControl("icon").GetPropertyValue("source");
                if (iconSrc.Contains("process-stop"))
                {
                    Console.Error.WriteLine("Feed '" + feedTitle + "' did not load!");
                }
                else if (!iconSrc.Contains("folder"))
                {
                    CheckFeed(item);
                }
            }
        }

        public virtual string EscapeJsRegEx(string str)
        {
            // Java converted code
            // string result = str.ReplaceAll("([()\\[{*+.$^\\|?])", "\\\\$1");

            var result = Regex.Replace(str, "([()\\[{*+.$^\\|?])", "\\\\$1");
            return result;
        }

        public virtual void CheckFeed(IWidget item)
        {
            string title = item.Text;
            Console.WriteLine("Checking feed " + title);
            Assert.IsNotNull(item, "item is null");
            item.Click();
            List postList = PostList;
            IList<IWidget> items = postList.Children;
            Assert.AreNotEqual(0, items.Count, "Feed '" + title + "' has no entries.");
            Random rnd = new Random();
            int? feedIndex = rnd.Next(items.Count);
            IWidget listItem = items[feedIndex.Value];
            Assert.IsNotNull(listItem, "list item is null");
            string newItemLabel = (string) listItem.GetPropertyValue("label");
            Assert.IsNotNull("new item label is null", newItemLabel);
            // scroll the feed item into view
            IWidget feedItem = postList.GetSelectableItem("^" + EscapeJsRegEx(newItemLabel) + "$");
            if (feedItem == null)
            {
                Console.Error.WriteLine("Feed item '" + newItemLabel + "' is null");
                return;
            }
            string label = (string) feedItem.GetPropertyValue("label");
            Assert.IsNotNull(label);
            Assert.Equals(newItemLabel, label);
            feedItem.Click();
            CheckFeedItem();
        }

        public virtual void CheckFeedItem()
        {
            IWidget article = Driver.FindWidget(By.Qxh(articleLoc));
            string html = (string) article.GetPropertyValue("html");
            Assert.AreNotEqual(0, html.Length);
        }

        //ORIGINAL LINE: @Test public void changeLocale() throws InterruptedException
        [Test]
        public virtual void ChangeLocale()
        {
            SelectLocale("Italiano");
            Thread.Sleep(500);
            // translate a string (only works if the locale was loaded correctly)
            string preferences = Driver.GetTranslation("Preferences");
            Assert.Equals("Preferenze", preferences);

            string prefsLocator = toolbarLocator + "/[@label=" + preferences + "]";
            IWidget prefsButton = Driver.FindWidget(By.Qxh(prefsLocator));
            Assert.IsNotNull(prefsButton);

            string folderLocator =
                treeLocator + "/child[0]/child[0]/child[0]/qx.ui.tree.TreeFolder/[@value=Feed statici]";
            IWidget treeFolder = Driver.FindWidget(By.Qxh(folderLocator));
            Assert.IsNotNull(treeFolder);

            SelectLocale("English");

            folderLocator = treeLocator + "/child[0]/child[0]/child[0]/qx.ui.tree.TreeFolder/[@value=Static Feeds]";
            treeFolder = Driver.FindWidget(By.Qxh(folderLocator));
            Assert.IsNotNull(treeFolder);
        }

        public virtual void SelectLocale(string language)
        {
            string preferences = Driver.GetTranslation("Preferences");
            string prefsLocator = toolbarLocator + "/[@label=" + preferences + "]";
            IWidget prefsButton = Driver.FindWidget(By.Qxh(prefsLocator));
            prefsButton.Click();

            string italianLocator = prefWinLocator + "/*/[@label=" + language + "]";
            IWidget italianLabel = Driver.FindWidget(By.Qxh(italianLocator));
            italianLabel.Click();

            string ok = Driver.GetTranslation("OK");
            string okLocator = prefWinLocator + "/qx.ui.container.Composite/[@label=" + ok + "]";
            IWidget okButton = Driver.FindWidget(By.Qxh(okLocator));
            okButton.Click();
        }

        //ORIGINAL LINE: @Test public void addFeed() throws InterruptedException
        [Test]
        public virtual void AddFeed()
        {
            string newFeedTitle = "The Register";
            string newFeedUrl = "http://www.theregister.co.uk/headlines.atom";

            string addFeed = Driver.GetTranslation("Add feed");
            string addWinLocator = toolbarLocator + "/[@label=" + addFeed + "]";
            IWidget addWinButton = Driver.FindWidget(By.Qxh(addWinLocator));
            addWinButton.Click();

            string title = Driver.GetTranslation("Title");
            IWidget titleInput = Driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@placeholder=" + title + "]"));
            titleInput.SendKeys(newFeedTitle);

            string url = Driver.GetTranslation("URL");
            IWidget urlInput = Driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@placeholder=" + url + "]"));
            urlInput.SendKeys(newFeedUrl);

            string add = Driver.GetTranslation("Add");
            IWidget addButton = Driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@label=" + add + "]"));
            addButton.Click();

            IWidget newFeedItem = Driver.WaitForWidget(By.Qxh(treeLocator + "/*/[@label=" + newFeedTitle + "]"), 15);
            Assert.IsNotNull(newFeedItem);
            WaitUntilFeedsLoaded();
            CheckFeed(newFeedItem);
        }
    }
}