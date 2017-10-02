using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;
using By = Qooxdoo.WebDriver.By;

namespace Qooxdoo.WebDriverDemo.Playground
{
    [TestFixture]
    public class PlaygroundIT : IntegrationTest
    {
        public static string qxVersion = null;

        public static string initialHandle = null;

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            IntegrationTest.SetUpBeforeClass();
            initialHandle = Driver.CurrentWindowHandle;
            qxVersion = (string) Driver.ExecuteScript("return qx.core.Environment.get('qx.version')");
        }

        /// <summary>
        /// Check if syntax highlighting is displayed before tests starting
        /// and turned it on
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUpBeforeTest() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUpBeforeTest()
        {
            Thread.Sleep(1000);
            IWidget hightlightButton =
                Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]"));
            bool? displayed = (bool?) hightlightButton.GetPropertyValue("value");
            if (!displayed.Value)
            {
                Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkLink(String expectedUrl) throws InterruptedException
        public virtual void CheckLink(string expectedUrl)
        {
            CheckLink(expectedUrl, true);
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkLink(String expectedUrl, Nullable<bool> exact) throws InterruptedException
        public virtual void CheckLink(string expectedUrl, bool? exact)
        {
            ReadOnlyCollection<string> handles = Driver.WindowHandles;
            IEnumerator<string> itr = handles.GetEnumerator();
            while (itr.MoveNext())
            {
                string handle = itr.Current;
                if (!handle.Equals(initialHandle))
                {
                    Driver.SwitchTo().Window(handle);
                    Thread.Sleep(1000);
                    string newUrl = Driver.Url;
                    Driver.Close();
                    Driver.SwitchTo().Window(initialHandle);
                    if (exact.Value)
                    {
                        Assert.Equals(expectedUrl, newUrl);
                    }
                    else
                    {
                        Assert.True(newUrl.StartsWith(expectedUrl, StringComparison.Ordinal));
                    }
                }
            }
        }

        /// <summary>
        /// test to load all samples,
        /// it is correct, if the right play area exists
        /// and ace content text is not the same as before clicking a sample
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void loadSamples() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void LoadSamples()
        {
            //contains sample name and play area type
            IDictionary<string, string> container = new Dictionary<string, string>();
            container["Hello World"] = "qx.ui.form.Button";
            container["Window"] = "qx.ui.window.Window";
            container["Dialog"] = "qx.ui.tabview.TabView";
            container["Calculator"] = "qx.ui.window.Window";
            container["Table"] = "qx.ui.window.Window";
            container["Tree"] = "qx.ui.tree.Tree";
            container["Data Binding"] = "qx.ui.form.TextField";
            container["YQL Binding"] = "qx.ui.form.List";

            By widgetCellLocator = By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell");
            IWidget widgetCell = Driver.FindWidget(widgetCellLocator);
            IList<IWidget> children = widgetCell.Children;
            IEnumerator<IWidget> iter = children.GetEnumerator();
            IWebElement aceContent =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'ace_content')]"));
            string contentText = null;

            // skip over 'Static'
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
            iter.MoveNext();
            string label = null;
            while (iter.MoveNext())
            {
                IWidget item = iter.Current;
                label = item.Text;
                //skip over 'User', if there are user scripts saved
                if (!label.Equals("User"))
                {
                    item.Click();
                    IWidget playArea = Driver.FindWidget(By.Qxh("*/qx.ui.root.Inline"));
                    IList<IWidget> playAreaType = playArea.Children;
                    string type = playAreaType[0].ToString();
                    // check if the type of play area is the same as in the map above
                    Assert.True(type.Contains(container[label]));
                    string newText = aceContent.Text;
                    //check if the next sample has been clicked
                    Assert.AreNotEqual(newText, contentText);
                    contentText = newText;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// checks if the toggle button 'Syntax Highlighting' works correctly
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void toggleSyntaxHighlighting() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ToggleSyntaxHighlighting()
        {
            By widgetCellLocator = By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell");
            IWidget widgetCell = Driver.FindWidget(widgetCellLocator);
            IList<IWidget> children = widgetCell.Children;
            IEnumerator<IWidget> iter = children.GetEnumerator();
            // skip over 'Static'
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
            iter.MoveNext();

            while (iter.MoveNext())
            {
                IWidget item = iter.Current;
                item.Click();
                Thread.Sleep(900);
                // Syntax should be highlighted
                //before the toggle button has been clicked at the first time
                IWebElement aceContent =
                    Driver.FindElement(
                        OpenQA.Selenium.By.XPath(
                            "//div[contains(@class, 'ace_layer ace_gutter-layer ace_folding-enabled')]"));
                bool? isHighlighted = aceContent.Displayed;
                Assert.True(isHighlighted);
                //after clicking toggle button, syntax highlighting should be turned off
                Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
                aceContent =
                    Driver.FindElement(
                        OpenQA.Selenium.By.XPath(
                            "//div[contains(@class, 'ace_layer ace_gutter-layer ace_folding-enabled')]"));
                isHighlighted = aceContent.Displayed;
                Assert.False(isHighlighted);
                Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            }
        }

        /*
         * test to Click 'Log' button, clear log content
         * and check if the content has been cleared
         */
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void checkLogClearButton()
        [Test]
        public virtual void CheckLogClearButton()
        {
            Driver.FindWidget(By.Qxh("*/Playground.view.Toolbar/*/[@label=Log]")).Click();
            //after clicking 'Log' button content should not be empty
            IWebElement LogContent =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qxappender')]"));
            Assert.True(!LogContent.Text.Equals(""));
            //clearing log content
            Driver.FindWidget(By.Qxh("*/qx.ui.splitpane.Pane/*/[@label=Clear]")).Click();
            LogContent = Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qxappender')]"));
            //check if log content has been cleared
            Assert.True(LogContent.Text.Equals(""));
        }

        /// <summary>
        /// test to run code which has been changed without saving it
        ///
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void runningChangedCode() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void RunningChangedCode()
        {
            // switch to text area (without syntax highlighting) to edit the code
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            IWidget editor = Driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            IWebElement textarea =
                editor.FindElement(
                    OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            //clear code
            textarea.Clear();
            //'Hello World' sample, which creates a button with label 'First Button'
            // will be changed to 'Second Button'
            textarea.SendKeys("// Create a button\n" +
                              "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" +
                              "// Document is the application root\n" + "var doc = this.getRoot();\n" +
                              "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" +
                              " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" +
                              "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" +
                              "});\n");
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Run]")).Click();
            //check if a button with new label has been found after running
            IWidget playArea = Driver.WaitForWidget(By.Qxh("*/qx.ui.root.Inline/[@label=Second Button]"), 10);
            Assert.True(playArea.Displayed);
        }

        /// <summary>
        /// test to check if saving an example works correctly
        /// the code has not been modified before
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void saveExample()
        [Test]
        public virtual void SaveExample()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            IWidget saveButton = Driver.FindWidget(locator);
            saveButton.Click();
            IAlert savePrompt = Driver.SwitchTo().Alert();
            savePrompt.SendKeys("Saved Sample");
            savePrompt.Accept();
        }

        /// <summary>
        /// test to check 'Saving As' button with modified code
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void saveExampleAs()
        [Test]
        public virtual void SaveExampleAs()
        {
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            IWidget editor = Driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            IWebElement textarea =
                editor.FindElement(
                    OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" +
                              "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" +
                              "// Document is the application root\n" + "var doc = this.getRoot();\n" +
                              "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" +
                              " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" +
                              "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" +
                              "});\n");
            By locator = By.Qxh("*/[@source=document-save-as.png]");
            IWidget saveButton = Driver.FindWidget(locator);
            saveButton.Click();
            IAlert savePrompt = Driver.SwitchTo().Alert();
            savePrompt.SendKeys("Saved(As) Sample");
            savePrompt.Accept();
        }

        /// <summary>
        /// test to delete a saved user sample
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void deleteUserScript() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void DeleteUserScript()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            IWidget saveButton = Driver.FindWidget(locator);
            saveButton.Click();
            IAlert savePrompt = Driver.SwitchTo().Alert();
            savePrompt.SendKeys("test example 2");
            savePrompt.Accept();
            IWebElement widgetCell =
                Driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 2]"));
            widgetCell.Click();
            By locatorDelete = By.Qxh("*/[@source=user-trash.png]");
            IWidget deleteButton = Driver.FindWidget(locatorDelete);
            deleteButton.Click();
            Thread.Sleep(1000);
            IWebElement deletedSample =
                Driver.FindElement(
                    By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 2]"));
            Assert.True(deletedSample == null);
        }

        /// <summary>
        /// test to rename a saved user sample
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void renameSample()
        [Test]
        public virtual void RenameSample()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            IWidget saveButton = Driver.FindWidget(locator);
            saveButton.Click();
            IAlert savePrompt = Driver.SwitchTo().Alert();
            savePrompt.SendKeys("test example 1");
            savePrompt.Accept();
            IWebElement widgetCell =
                Driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 1]"));
            widgetCell.Click();
            By locatorRename = By.Qxh("*/[@source=format-text-direction-ltr.png]");
            IWidget renameButton = Driver.FindWidget(locatorRename);
            renameButton.Click();
            IAlert renamePrompt = Driver.SwitchTo().Alert();
            renamePrompt.SendKeys("Renamed Sample");
            renamePrompt.Accept();
            IWebElement renamedSample =
                Driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=Renamed Sample]"));
            Assert.True(renamedSample != null);
        }

        /// <summary>
        /// test to reload website after user script has been saved
        /// the saved sample should be found after reload </summary>
        /// <exception cref="ThreadInterruptedException">  </exception>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void userSamplesReload() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void UserSamplesReload()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            IWidget saveButton = Driver.FindWidget(locator);
            saveButton.Click();
            IAlert savePrompt = Driver.SwitchTo().Alert();
            savePrompt.SendKeys("reload example");
            savePrompt.Accept();
            //reload
            Thread.Sleep(2000);
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            IWebElement reloadedSample =
                Driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=reload example]"));
            Assert.True(reloadedSample != null);
        }

        /// <summary>
        /// test to check if an alert is displayed after discarding a modified code
        /// by switching to another sample
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void discardCode()
        [Test]
        public virtual void DiscardCode()
        {
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            IWidget editor = Driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            IWebElement textarea =
                editor.FindElement(OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" +
                              "var button1 = new qx.ui.form.Button(\"Third Button\", \"icon/22/apps/internet-web-browser.png\"\n);" +
                              "// Document is the application root\n" + "var doc = this.getRoot();\n" +
                              "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" +
                              " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" +
                              "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" +
                              "});\n");
            Driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=Window]")).Click();
            IAlert discardPrompt = Driver.SwitchTo().Alert();
            discardPrompt.Accept();
        }

        /// <summary>
        /// test to check URL after clicking 'API Viewer' button </summary>
        /// <exception cref="ThreadInterruptedException">  </exception>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void apiViewerLink() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ApiViewerLink()
        {
            Driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=API Viewer]")).Click();
            Thread.Sleep(1000);
            CheckLink("http://demo.qooxdoo.org/" + qxVersion + "/apiviewer/#qx", false);
        }

        /// <summary>
        /// test to check URL after clicking 'Manual' button
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void manualLink() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ManualLink()
        {
            Driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Manual]")).Click();
            Thread.Sleep(1000);
            CheckLink("http://manual.qooxdoo.org/" + qxVersion + "/");
        }

        /// <summary>
        /// test to check URL after clicking 'Demo Browser' button
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void demoBrowserLink() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void DemoBrowserLink()
        {
            Driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Demo Browser]")).Click();
            Thread.Sleep(1000);
            CheckLink("http://demo.qooxdoo.org/" + qxVersion + "/demobrowser/#");
        }

        /// <summary>
        /// test to check URL after clicking 'Shorten URL' button
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void shortenURLLink() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void ShortenUrlLink()
        {
            Driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Shorten URL]")).Click();
            Thread.Sleep(1000);
            CheckLink("http://tinyurl.com/create.php?url=", false);
        }

        /// <summary>
        /// test to check URL after clicking the 'CodePen' link in the 'Website'tab.
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void openCodePenLink() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void OpenCodePenLink()
        {
            Driver.FindElement(By.Qxh("*/Playground.view.Header/[@label=Website]")).Click();
            Thread.Sleep(2000);
            //there are two inputs, the first is hidden
            IList<IWebElement> inputs = Driver.FindElements(OpenQA.Selenium.By.XPath("//input[@value='CodePen']"));
            inputs[1].Click();
            CheckLink("http://codepen.io/pen");
        }

        /// <summary>
        /// test to check URL after modified code is running
        /// </summary>
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void checkChangedCodeURL() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void CheckChangedCodeUrl()
        {
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            IWidget editor = Driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            IWebElement textarea =
                editor.FindElement(
                    OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" +
                              "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" +
                              "// Document is the application root\n" + "var doc = this.getRoot();\n" +
                              "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" +
                              " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" +
                              "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" +
                              "});\n");
            Driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Run]")).Click();
            string currentURL = Driver.Url;
            Assert.True(currentURL.Contains("Second"));
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Driver.Url = currentURL;
            IWidget playArea = Driver.WaitForWidget(By.Qxh("*/qx.ui.root.Inline/[@label=Second Button]"), 10);
            Assert.True(playArea.Displayed);
        }

        // reload after every test
        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @After public void tearDownAfterTest() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [TearDown]
        public virtual void TearDownAfterTest()
        {
            Driver.SwitchTo().Window(initialHandle);
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public new static void TearDownAfterClass()
        {
            Driver.Quit();
        }
    }
}