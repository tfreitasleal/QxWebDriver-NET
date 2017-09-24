using System.Threading;
using Assert = NUnit.Framework.Assert;
//using After = NUnit.Framework.After;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Scrollable = Qooxdoo.WebDriver.UI.IScrollable;
using Widget = Qooxdoo.WebDriver.UI.IWidget;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.DesktopTestRunner
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.True;

    public class Runner : IntegrationTest
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void testAppLoads()
        public virtual void testAppLoads()
        {
            Widget treeItem = driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTreeItem"));
            Assert.True(treeItem.Text.Equals("qx"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void selectTests() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void selectTests()
        {
            //select test : qx -> test -> bom -> client -> Css -> 'testBorderImageSyntax'
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Css]")).Click();
            Widget test = driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testBorderImageSyntax]"));
            Assert.True(test.Displayed);

            //select test: qx -> test -> media -> Audio -> 'testVolume'
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=media]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Audio]")).Click();
            Thread.Sleep(750);
            Widget test2 = driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testVolume]"));
            test2.Click();

            Assert.True(test2.Displayed);

            //select test: qx -> test -> Basic ->  'testElementAttributes'
            Scrollable scroll = (Scrollable) driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTree"));
            Widget basic = scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text()='Basic']"));
            Thread.Sleep(750);
            basic.Click();
            Thread.Sleep(750);
            Widget testElementAttributes = scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text() = 'testElementAttributes']"));
            Thread.Sleep(750);
            testElementAttributes.Click();
            Assert.True(testElementAttributes.Displayed);

        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void runTests() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void runTests()
        {

            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Css]")).Click();
            //Click 'Run Tests!' button
            WebElement run = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            WebElement results = driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results.Text.Equals("qx.test.bom.client.Css:testBorderImageSyntax"));

            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=application]")).Click();
            Thread.Sleep(1000);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Routing]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testAny]")).Click();
            run.Click();
            WebElement results2 = driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results2.Text.Equals("qx.test.application.Routing:testAny"));

            Thread.Sleep(750);
            Scrollable scroll = (Scrollable) driver.FindWidget(By.Qxh("*/qx.ui.tree.VirtualTree"));
            scroll.ScrollToChild("y", OpenQA.Selenium.By.XPath("//div[text() = 'Basic']"));
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Basic]")).Click();
            run.Click();
            WebElement results3 = driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results3.Text.Equals("qx.test.bom.Basic:testElementAttributes"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void settingLogLevelWorks() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void SettingLogLevelWorks()
        {
            //  check debug mode after initializing
            By locator = By.Qxh("*/[@source=system.png]");
            Widget logLevelButton = driver.FindWidget(locator);
            Assert.True(logLevelButton.Displayed);
            Widget logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //check if log content is empty
            Assert.True(logContent.Text.Equals(""));
            //run a test
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            WebElement run = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            //switch to log level 'Warning'
            logLevelButton.Click();
            By locatorWarning = By.Qxh("*/[@source=dialog-warning.png]");
            Widget logLevelButtonWarning = driver.FindWidget(locatorWarning);
            logLevelButtonWarning.Click();
            run.Click();
            Thread.Sleep(750);
            //after running a test, content should be empty
            Assert.True(logContent.Text.Equals(""));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void reload() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void reload()
        {
            Widget logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            WebElement run = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            Widget reload = driver.FindWidget(By.Qxh("*/qx.ui.toolbar.PartContainer/*/[@label=Reload]"));
            reload.Click();
            Thread.Sleep(1500);
            //log content should be empty after reloading
            Assert.True(logContent.Text.Equals(""));

            //result pane should be empty after reloading
            WebElement results = driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(results.Text.Equals(""));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void autoReload() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void autoReload()
        {
            Widget logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=bom]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=client]")).Click();
            Thread.Sleep(750);
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=Device]")).Click();
            driver.FindWidget(By.Qxh("*/qx.ui.virtual.layer.WidgetCell/[@label=testDetectDeviceType]")).Click();
            //Click 'Run Tests!' button
            WebElement run = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-button-box-left')]"));
            run.Click();
            logContent = driver.FindWidget(By.Qxh("*/qx.ui.embed.Html"));
            //log content should not be empty after running a test
            Assert.True(!logContent.Text.Equals(""));

            Widget autoReload = driver.FindWidget(By.Qxh("*/qx.ui.toolbar.ToolBar/*/[@label=Auto Reload]"));
            autoReload.Click();

            run.Click();
            Thread.Sleep(1000);
            //result pane should be empty after reloading
            WebElement results = driver.FindElement(OpenQA.Selenium.By.XPath("//ul[contains(@class, 'resultPane')]"));
            Assert.True(!results.Text.Equals(""));
            autoReload.Click();
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @After public void setUpAfterTest() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUpAfterTest()
        {
                driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
                driver.Manage().Window().Maximize();
                driver.RegisterLogAppender();
                driver.RegisterGlobalErrorHandler();
        }

    }

}