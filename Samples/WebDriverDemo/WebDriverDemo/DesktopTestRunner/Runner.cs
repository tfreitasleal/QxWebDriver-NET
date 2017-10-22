using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;
using Scrollable = Wisej.Qooxdoo.WebDriver.UI.IScrollable;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopTestRunner
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Runner : IntegrationTest
    {
        //ORIGINAL LINE: @Test public void testAppLoads()
        [Test]
        public virtual void TestAppLoads()
        {
            IWidget treeItem = Driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTreeItem"));
            Assert.True(treeItem.Text.Equals("qx"));
        }

        //ORIGINAL LINE: @Test public void selectTests() throws InterruptedException
        [Test]
        public virtual void SelectTests()
        {
            //select test : qx -> test -> bom -> client -> Css -> 'testBorderImageSyntax'
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Css]")).Click();
            IWidget test = Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testBorderImageSyntax]"));
            Assert.True(test.Displayed);

            //select test: qx -> test -> media -> Audio -> 'testVolume'
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=media]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Audio]")).Click();
            Thread.Sleep(750);
            IWidget test2 = Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testVolume]"));
            test2.Click();

            Assert.True(test2.Displayed);

            //select test: qx -> test -> Basic ->  'testElementAttributes'
            Scrollable scroll = (Scrollable) Driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTree"));
            IWidget basic = scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text()='Basic']"));
            Thread.Sleep(750);
            basic.Click();
            Thread.Sleep(750);
            IWidget testElementAttributes =
                scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text() = 'testElementAttributes']"));
            Thread.Sleep(750);
            testElementAttributes.Click();
            Assert.True(testElementAttributes.Displayed);
        }

        //ORIGINAL LINE: @Test public void runTests() throws InterruptedException
        [Test]
        public virtual void RunTests()
        {
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Css]")).Click();
            //Click 'Run Tests!' button
            IWebElement run =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            IWebElement results = Driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results.Text.Equals("qx.test.bom.client.Css:testBorderImageSyntax"));

            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=application]")).Click();
            Thread.Sleep(1000);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Routing]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testAny]")).Click();
            run.Click();
            IWebElement results2 = Driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results2.Text.Equals("qx.test.application.Routing:testAny"));

            Thread.Sleep(750);
            Scrollable scroll = (Scrollable) Driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTree"));
            scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text() = 'Basic']"));
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Basic]")).Click();
            run.Click();
            IWebElement results3 = Driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results3.Text.Equals("qx.test.bom.Basic:testElementAttributes"));
        }

        //ORIGINAL LINE: @Test public void settingLogLevelWorks() throws InterruptedException
        [Test]
        public virtual void SettingLogLevelWorks()
        {
            //  check debug mode after initializing
            By locator = By.Qxh("*/[@source=system.png]");
            IWidget logLevelButton = Driver.FindWidget(locator);
            Assert.True(logLevelButton.Displayed);
            IWidget logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //check if log content is empty
            Assert.True(logContent.Text.Equals(""));
            //run a test
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            IWebElement run =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            //switch to log level 'Warning'
            logLevelButton.Click();
            By locatorWarning = By.Qxh("*/[@source=dialog-warning.png]");
            IWidget logLevelButtonWarning = Driver.FindWidget(locatorWarning);
            logLevelButtonWarning.Click();
            run.Click();
            Thread.Sleep(750);
            //after running a test, content should be empty
            Assert.True(logContent.Text.Equals(""));
        }

        //ORIGINAL LINE: @Test public void reload() throws InterruptedException
        [Test]
        public virtual void Reload()
        {
            IWidget logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            IWebElement run =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            IWidget reload = Driver.FindWidget(By.Qxh("*/qx.ui.toolbar.PartContainer/*/[@label=Reload]"));
            reload.Click();
            Thread.Sleep(1500);
            //log content should be empty after reloading
            Assert.True(logContent.Text.Equals(""));

            //result pane should be empty after reloading
            IWebElement results = Driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results.Text.Equals(""));
        }

        //ORIGINAL LINE: @Test public void autoReload() throws InterruptedException
        [Test]
        public virtual void AutoReload()
        {
            IWidget logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            Driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            IWebElement run =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = Driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            IWidget autoReload = Driver.FindWidget(By.Qxh("*/qx.ui.toolbar.ToolBar/*/[@label=Auto Reload]"));
            autoReload.Click();

            run.Click();
            Thread.Sleep(1000);
            //result pane should be empty after reloading
            IWebElement results = Driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(!results.Text.Equals(""));
            autoReload.Click();
        }

        [TearDown]
        public virtual void SetUpAfterTest()
        {
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Driver.Manage().Window.Maximize();
            Driver.RegisterLogAppender();
            Driver.RegisterGlobalErrorHandler();
        }
    }
}