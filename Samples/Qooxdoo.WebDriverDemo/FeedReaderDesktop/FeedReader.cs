using System;
using System.Collections.Generic;
using System.Threading;

namespace Qooxdoo.WebDriverDemo.FeedReaderDesktop
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using Label = Qooxdoo.WebDriver.UI.Basic.Label;
    using List = Qooxdoo.WebDriver.UI.Form.List;
    using Tree = Qooxdoo.WebDriver.UI.Tree.Tree;
    using WebDriver = OpenQA.Selenium.IWebDriver;
    using WebElement = OpenQA.Selenium.IWebElement;
    using ExpectedCondition = OpenQA.Selenium.Support.UI.ExpectedCondition;
    using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;

    public class FeedReader : IntegrationTest
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            IntegrationTest.setUpBeforeClass();
            driver.JsExecutor.ExecuteScript("qx.locale.Manager.getInstance().setLocale('en');");
        }

        public static string toolbarLocator = "qx.ui.container.Composite/feedreader.view.desktop.ToolBar";
        public static string prefWinLocator = "feedreader.view.desktop.PreferenceWindow";
        public static string treeLocator = "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.tree.Tree";
        public static string articleLoc = "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.splitpane.Pane/feedreader.view.desktop.Article";
        public static string addFeedLoc = "feedreader.view.desktop.AddFeedWindow";

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void waitUntilFeedsLoaded()
        public virtual void WaitUntilFeedsLoaded()
        {
            (new WebDriverWait(driver, 30, 250)).Until(feedsReady());
        }

        private List __postList;

        public virtual List PostList
        {
            get
            {
                if (__postList == null)
                {
                    string listLoc = "qx.ui.container.Composite/qx.ui.splitpane.Pane/qx.ui.splitpane.Pane/feedreader.view.desktop.List";
                    Widget frList = (Widget) driver.FindWidget(By.Qxh(listLoc));
                    WebElement listEl = (WebElement) frList.ExecuteJavascript("return qx.ui.core.Widget.getWidgetByElement(arguments[0]).getList().getContentElement().getDomElement()");
                    List list = (List) driver.getWidgetForElement(listEl);
                    __postList = list;
                }

                return __postList;
            }
        }

        public virtual ExpectedCondition<bool?> feedsReady()
        {
            return new ExpectedConditionAnonymousInnerClass(this);
        }

        private class ExpectedConditionAnonymousInnerClass : ExpectedCondition<bool?>
        {
            private readonly FeedReader outerInstance;

            public ExpectedConditionAnonymousInnerClass(FeedReader outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public bool? Apply(WebDriver webDriver)
            {
                By treeLoc = By.Qxh(treeLocator);
                Tree tree = (Tree) driver.FindWidget(treeLoc);
                IList<Widget> items = (IList<Widget>) tree.GetWidgetListFromProperty("items");
                IEnumerator<Widget> itr = items.GetEnumerator();
                while (itr.MoveNext())
                {
                    Widget item = (Widget) itr.Current;
                    string iconSrc = (string) item.GetChildControl("icon").GetPropertyValue("source");
                    if (iconSrc.Contains("loading"))
                    {
                        return false;
                    }
                }
                return true;
            }

            public string ToString()
            {
                return "All feeds have finished loading.";
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void feedsLoaded()
        public virtual void FeedsLoaded()
        {
            By treeLoc = By.Qxh(treeLocator);
            Tree tree = (Tree) driver.FindWidget(treeLoc);
            IList<Widget> items = (IList<Widget>) tree.GetWidgetListFromProperty("items");
            IEnumerator<Widget> itr = items.GetEnumerator();
            while (itr.MoveNext())
            {
                Widget item = (Widget) itr.Current;
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
            string result = str.ReplaceAll("([()\\[{*+.$^\\|?])", "\\\\$1");
            return result;
        }

        public virtual void CheckFeed(Widget item)
        {
            string title = item.Text;
            Console.WriteLine("Checking feed " + title);
            Assert.IsNotNull("item is null", item);
            item.Click();
            List postList = PostList;
            IList<Widget> items = postList.Children;
            Assert.AreNotEqual("Feed '" + title + "' has no entries.", items.Count, 0);
            Random rnd = new Random();
            int? feedIndex = rnd.Next(items.Count);
            Widget listItem = items[feedIndex];
            Assert.IsNotNull("list item is null", listItem);
            string newItemLabel = (string) listItem.GetPropertyValue("label");
            Assert.IsNotNull("new item label is null", newItemLabel);
            // scroll the feed item into view
            Widget feedItem = postList.GetSelectableItem("^" + EscapeJsRegEx(newItemLabel) + "$");
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
            Widget article = driver.FindWidget(By.Qxh(articleLoc));
            string html = (string) article.GetPropertyValue("html");
            Assert.AreNotEqual(0, html.Length);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void changeLocale() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void ChangeLocale()
        {
            SelectLocale("Italiano");
            Thread.Sleep(500);
            // translate a string (only works if the locale was loaded correctly)
            string preferences = driver.getTranslation("Preferences");
            Assert.Equals("Preferenze", preferences);

            string prefsLocator = toolbarLocator + "/[@label=" + preferences + "]";
            Widget prefsButton = driver.FindWidget(By.Qxh(prefsLocator));
            Assert.IsNotNull(prefsButton);

            string folderLocator = treeLocator + "/child[0]/child[0]/child[0]/qx.ui.tree.TreeFolder/[@value=Feed statici]";
            Widget treeFolder = driver.FindWidget(By.Qxh(folderLocator));
            Assert.IsNotNull(treeFolder);

            SelectLocale("English");

            folderLocator = treeLocator + "/child[0]/child[0]/child[0]/qx.ui.tree.TreeFolder/[@value=Static Feeds]";
            treeFolder = driver.FindWidget(By.Qxh(folderLocator));
            Assert.IsNotNull(treeFolder);
        }

        public virtual void SelectLocale(string language)
        {
            string preferences = driver.getTranslation("Preferences");
            string prefsLocator = toolbarLocator + "/[@label=" + preferences + "]";
            Widget prefsButton = driver.FindWidget(By.Qxh(prefsLocator));
            prefsButton.Click();

            string italianLocator = prefWinLocator + "/*/[@label=" + language + "]";
            Widget italianLabel = driver.FindWidget(By.Qxh(italianLocator));
            italianLabel.Click();

            string ok = driver.getTranslation("OK");
            string okLocator = prefWinLocator + "/qx.ui.container.Composite/[@label=" + ok + "]";
            Widget okButton = driver.FindWidget(By.Qxh(okLocator));
            okButton.Click();
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void addFeed() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void AddFeed()
        {
            string newFeedTitle = "The Register";
            string newFeedUrl = "http://www.theregister.co.uk/headlines.atom";

            string addFeed = driver.getTranslation("Add feed");
            string addWinLocator = toolbarLocator + "/[@label=" + addFeed + "]";
            Widget addWinButton = driver.FindWidget(By.Qxh(addWinLocator));
            addWinButton.Click();

            string title = driver.getTranslation("Title");
            Widget titleInput = driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@placeholder=" + title + "]"));
            titleInput.SendKeys(newFeedTitle);

            string url = driver.getTranslation("URL");
            Widget urlInput = driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@placeholder=" + url + "]"));
            urlInput.SendKeys(newFeedUrl);

            string add = driver.getTranslation("Add");
            Widget addButton = driver.FindWidget(By.Qxh(addFeedLoc + "/*/[@label=" + add + "]"));
            addButton.Click();

            Widget newFeedItem = driver.waitForWidget(By.Qxh(treeLocator + "/*/[@label=" + newFeedTitle + "]"), 15);
            Assert.IsNotNull(newFeedItem);
            WaitUntilFeedsLoaded();
            CheckFeed(newFeedItem);
        }

    }

}