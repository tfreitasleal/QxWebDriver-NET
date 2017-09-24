using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
//using After = NUnit.Framework.After;
//using AfterClass = NUnit.Framework.AfterClass;
using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Widget = Qooxdoo.WebDriver.UI.IWidget;
using Alert = OpenQA.Selenium.IAlert;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.Playground
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.False;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.True;

    public class PlaygroundIT : IntegrationTest
    {

        public static string qxVersion = null;

        public static string initialHandle = null;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            IntegrationTest.setUpBeforeClass();
            initialHandle = driver.CurrentWindowHandle;
            qxVersion = (string) driver.ExecuteScript("return qx.core.Environment.get('qx.version')");
        }

        /// <summary>
        /// Check if syntax highlighting is displayed before tests starting
        /// and turned it on
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUpBeforeTest() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void SetUpBeforeTest()
        {
            Thread.Sleep(1000);
            Widget hightlightButton = driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]"));
            bool? displayed = (bool?)hightlightButton.GetPropertyValue("value");
            if (!displayed.Value)
            {
                driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkLink(String expectedUrl) throws InterruptedException
        public virtual void checkLink(string expectedUrl)
        {
            checkLink(expectedUrl, true);
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkLink(String expectedUrl, Nullable<bool> exact) throws InterruptedException
        public virtual void checkLink(string expectedUrl, bool? exact)
        {
            ReadOnlyCollection<string> handles = driver.WindowHandles;
            IEnumerator<string> itr = handles.GetEnumerator();
            while (itr.MoveNext())
            {
                string handle = itr.Current;
                if (!handle.Equals(initialHandle))
                {
                    driver.SwitchTo().Window(handle);
                    Thread.Sleep(1000);
                    string newUrl = driver.Url;
                    driver.Close();
                    driver.SwitchTo().Window(initialHandle);
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
            Widget widgetCell = driver.FindWidget(widgetCellLocator);
            IList<Widget> children = widgetCell.Children;
            IEnumerator<Widget> iter = children.GetEnumerator();
            WebElement aceContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'ace_content')]"));
            string contentText = null;

            // skip over 'Static'
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
            iter.Next();
            string label = null;
            while (iter.MoveNext())
            {
                Widget item = iter.Current;
                label = item.Text;
                //skip over 'User', if there are user scripts saved
                if (!label.Equals("User"))
                {
                item.Click();
                Widget playArea = driver.FindWidget(By.Qxh("*/qx.ui.root.Inline"));
                IList<Widget> playAreaType = playArea.Children;
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
        public virtual void ToggleSyntaxHighlighting()
        {
            By widgetCellLocator = By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell");
            Widget widgetCell = driver.FindWidget(widgetCellLocator);
            IList<Widget> children = widgetCell.Children;
            IEnumerator<Widget> iter = children.GetEnumerator();
            // skip over 'Static'
//JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
            iter.Next();

            while (iter.MoveNext())
            {
                Widget item = iter.Current;
                item.Click();
                Thread.Sleep(900);
                // Syntax should be highlighted
                //before the toggle button has been clicked at the first time
                WebElement aceContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'ace_layer ace_gutter-layer ace_folding-enabled')]"));
                bool? isHighlighted = aceContent.Displayed;
                Assert.True(isHighlighted);
                //after clicking toggle button, syntax highlighting should be turned off
                driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
                aceContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'ace_layer ace_gutter-layer ace_folding-enabled')]"));
                isHighlighted = aceContent.Displayed;
                Assert.False(isHighlighted);
                driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            }
        }

        /*
         * test to Click 'Log' button, clear log content
         * and check if the content has been cleared
         */
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void checkLogClearButton()
        public virtual void CheckLogClearButton()
        {

            driver.FindWidget(By.Qxh("*/Playground.view.Toolbar/*/[@label=Log]")).Click();
            //after clicking 'Log' button content should not be empty
            WebElement LogContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qxappender')]"));
            Assert.True(!LogContent.Text.Equals(""));
            //clearing log content
            driver.FindWidget(By.Qxh("*/qx.ui.splitpane.Pane/*/[@label=Clear]")).Click();
            LogContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qxappender')]"));
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
        public virtual void RunningChangedCode()
        {
            // switch to text area (without syntax highlighting) to edit the code
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            Widget editor = driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            WebElement textarea = editor.FindElement(OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            //clear code
            textarea.Clear();
            //'Hello World' sample, which creates a button with label 'First Button'
            // will be changed to 'Second Button'
            textarea.SendKeys("// Create a button\n" + "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" + "// Document is the application root\n" + "var doc = this.getRoot();\n" + "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" + " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" + "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" + "});\n");
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Run]")).Click();
            //check if a button with new label has been found after running
            Widget playArea = driver.WaitForWidget(By.Qxh("*/qx.ui.root.Inline/[@label=Second Button]"), 10);
            Assert.True(playArea.Displayed);
        }

        /// <summary>
        /// test to check if saving an example works correctly
        /// the code has not been modified before
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void saveExample()
        public virtual void SaveExample()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            Widget saveButton = driver.FindWidget(locator);
            saveButton.Click();
            Alert savePrompt = driver.SwitchTo().Alert();
            savePrompt.SendKeys("Saved Sample");
            savePrompt.Accept();
        }

        /// <summary>
        /// test to check 'Saving As' button with modified code
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void saveExampleAs()
        public virtual void SaveExampleAs()
        {
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            Widget editor = driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            WebElement textarea = editor.FindElement(OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" + "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" + "// Document is the application root\n" + "var doc = this.getRoot();\n" + "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" + " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" + "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" + "});\n");
            By locator = By.Qxh("*/[@source=document-save-as.png]");
            Widget saveButton = driver.FindWidget(locator);
            saveButton.Click();
            Alert savePrompt = driver.SwitchTo().Alert();
            savePrompt.SendKeys("Saved(As) Sample");
            savePrompt.Accept();
        }

        /// <summary>
        /// test to delete a saved user sample
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void deleteUserScript() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void DeleteUserScript()
        {

            By locator = By.Qxh("*/[@source=document-save.png]");
            Widget saveButton = driver.FindWidget(locator);
            saveButton.Click();
            Alert savePrompt = driver.SwitchTo().Alert();
            savePrompt.SendKeys("test example 2");
            savePrompt.Accept();
            WebElement widgetCell = driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 2]"));
            widgetCell.Click();
            By locatorDelete = By.Qxh("*/[@source=user-trash.png]");
            Widget deleteButton = driver.FindWidget(locatorDelete);
            deleteButton.Click();
            Thread.Sleep(1000);
            WebElement deletedSample = driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 2]"));
            Assert.True(deletedSample == null);

        }

        /// <summary>
        /// test to rename a saved user sample
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void renameSample()
        public virtual void RenameSample()
        {

            By locator = By.Qxh("*/[@source=document-save.png]");
            Widget saveButton = driver.FindWidget(locator);
            saveButton.Click();
            Alert savePrompt = driver.SwitchTo().Alert();
            savePrompt.SendKeys("test example 1");
            savePrompt.Accept();
            WebElement widgetCell = driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=test example 1]"));
            widgetCell.Click();
            By locatorRename = By.Qxh("*/[@source=format-text-direction-ltr.png]");
            Widget renameButton = driver.FindWidget(locatorRename);
            renameButton.Click();
            Alert renamePrompt = driver.SwitchTo().Alert();
            renamePrompt.SendKeys("Renamed Sample");
            renamePrompt.Accept();
            WebElement renamedSample = driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=Renamed Sample]"));
            Assert.True(renamedSample != null);
        }

        /// <summary>
        /// test to reload website after user script has been saved
        /// the saved sample should be found after reload </summary>
        /// <exception cref="InterruptedException">  </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void userSamplesReload() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void UserSamplesReload()
        {
            By locator = By.Qxh("*/[@source=document-save.png]");
            Widget saveButton = driver.FindWidget(locator);
            saveButton.Click();
            Alert savePrompt = driver.SwitchTo().Alert();
            savePrompt.SendKeys("reload example");
            savePrompt.Accept();
            //reload
            Thread.Sleep(2000);
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            WebElement reloadedSample = driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=reload example]"));
            Assert.True(reloadedSample != null);
        }

        /// <summary>
        /// test to check if an alert is displayed after discarding a modified code
        /// by switching to another sample
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void discardCode()
        public virtual void DiscardCode()
        {
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            Widget editor = driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            WebElement textarea = editor.FindElement(OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" + "var button1 = new qx.ui.form.Button(\"Third Button\", \"icon/22/apps/internet-web-browser.png\"\n);" + "// Document is the application root\n" + "var doc = this.getRoot();\n" + "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" + " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" + "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" + "});\n");
            driver.FindElement(By.Qxh("*/qx.ui.List.List/*/qx.ui.virtual.layer.WidgetCell/[@label=Window]")).Click();
            Alert discardPrompt = driver.SwitchTo().Alert();
            discardPrompt.Accept();
        }

        /// <summary>
        /// test to check URL after clicking 'API Viewer' button </summary>
        /// <exception cref="InterruptedException">  </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void apiViewerLink() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void ApiViewerLink()
        {
            driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=API Viewer]")).Click();
            Thread.Sleep(1000);
            checkLink("http://demo.qooxdoo.org/" + qxVersion + "/apiviewer/#qx", false);
        }

        /// <summary>
        /// test to check URL after clicking 'Manual' button
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void manualLink() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void ManualLink()
        {
            driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Manual]")).Click();
            Thread.Sleep(1000);
            checkLink("http://manual.qooxdoo.org/" + qxVersion + "/");
        }

        /// <summary>
        /// test to check URL after clicking 'Demo Browser' button
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void demoBrowserLink() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void DemoBrowserLink()
        {
            driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Demo Browser]")).Click();
            Thread.Sleep(1000);
            checkLink("http://demo.qooxdoo.org/" + qxVersion + "/demobrowser/#");
        }

        /// <summary>
        /// test to check URL after clicking 'Shorten URL' button
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void shortenURLLink() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void ShortenUrlLink()
        {
            driver.FindElement(By.Qxh("*/Playground.view.Toolbar/[@label=Shorten URL]")).Click();
            Thread.Sleep(1000);
            checkLink("http://tinyurl.com/create.php?url=", false);
        }

        /// <summary>
        /// test to check URL after clicking the 'CodePen' link in the 'Website'tab.
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void openCodePenLink() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void OpenCodePenLink()
        {
            driver.FindElement(By.Qxh("*/Playground.view.Header/[@label=Website]")).Click();
            Thread.Sleep(2000);
            //there are two inputs, the first is hidden
            IList<WebElement> inputs = driver.FindElements(OpenQA.Selenium.By.XPath("//input[@value='CodePen']"));
            inputs[1].Click();
            checkLink("http://codepen.io/pen");
        }

        /// <summary>
        /// test to check URL after modified code is running
        /// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void checkChangedCodeURL() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void CheckChangedCodeUrl()
        {
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Syntax Highlighting]")).Click();
            Widget editor = driver.FindWidget(By.Qxh("*/Playground.view.Editor"));
            WebElement textarea = editor.FindElement(OpenQA.Selenium.By.XPath("//textarea[contains(@class, 'qx-abstract-field qx-placeholder-color')]"));
            textarea.Clear();
            textarea.SendKeys("// Create a button\n" + "var button1 = new qx.ui.form.Button(\"Second Button\", \"icon/22/apps/internet-web-browser.png\"\n);" + "// Document is the application root\n" + "var doc = this.getRoot();\n" + "// Add button to document at fixed coordinates\n" + "doc.add(button1,\n" + "{\n" + " left : 100,\n" + " top  : 50\n" + "});\n" + "// Add an event listener\n" + "button1.addListener(\"execute\", function(e) {\n" + "alert(\"Hello World!\");\n" + "});\n");
            driver.FindWidget(By.Qxh("*/qx.ui.container.Composite/*/[@label=Run]")).Click();
            string currentURL = driver.Url;
            Assert.True(currentURL.Contains("Second"));
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            driver.Url = currentURL;
            Widget playArea = driver.WaitForWidget(By.Qxh("*/qx.ui.root.Inline/[@label=Second Button]"), 10);
            Assert.True(playArea.Displayed);
        }

        // reload after every test
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @After public void tearDownAfterTest() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void tearDownAfterTest()
        {
            driver.SwitchTo().Window(initialHandle);
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            driver.Manage().Window().Maximize();
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void tearDownAfterClass()
        {
            driver.Quit();
        }

    }

}