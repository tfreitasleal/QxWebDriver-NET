using System;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.WebDriver.UI;
using By = Wisej.WebDriver.By;

namespace Wisej.WebDriverDemo.DesktopApiViewer
{
    [TestFixture]
    public class ClassItems : DesktopApiViewer
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            DesktopApiViewer.SetUpBeforeClass();
            string className = "qx.ui.table.pane.Scroller";
            SelectClass(className);
        }

        protected internal virtual void TestProperties()
        {
            string propertiesPath = "*/qx.ui.toolbar.ToolBar/*/[@label=Properties]";
            IWidget propertiesButton = Driver.FindWidget(By.Qxh(propertiesPath));
            bool propertiesActive = ((bool?) propertiesButton.GetPropertyValue("value")).Value;
            Assert.True(propertiesActive);
            string propertyItemPath = "//div[contains(@class, 'info-panel')]/descendant::span[text()='getLiveResize']";
            IWebElement propertyItem = Driver.FindElement(OpenQA.Selenium.By.XPath(propertyItemPath));
            Assert.True(propertyItem.Displayed);
            propertiesButton.Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                propertyItem = Driver.FindElement(OpenQA.Selenium.By.XPath(propertyItemPath));
                Assert.True(false, "Property method was not hidden!");
            }
            catch (NoSuchElementException)
            {
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            propertiesButton.Click();
        }

        protected internal virtual void TestClassItem(string item, string method)
        {
            string itemPath = "*/qx.ui.toolbar.ToolBar/*/[@label=" + item + "]";
            IWidget itemButton = Driver.FindWidget(By.Qxh(itemPath));
            bool itemActive = ((bool?) itemButton.GetPropertyValue("value")).Value;
            Assert.False(itemActive);
            string methodPath = "//div[contains(@class, 'info-panel')]/descendant::span[text()='" + method + "']";
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                Driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
                Assert.True(false, item + " item " + method + " was not hidden initially!");
            }
            catch (NoSuchElementException)
            {
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            itemButton.Click();

            IWebElement methodItem = Driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
            Assert.True(methodItem.Displayed);
            itemButton.Click();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                Driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
                Assert.True(false, item + " item " + method + " was not hidden!");
            }
            catch (NoSuchElementException)
            {
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        protected internal virtual void TestIncludes()
        {
            string inheritedItemPath =
                "//div[contains(@class, 'info-panel')]/descendant::span[text()='changeTextColor']";

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                Driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
                Assert.True(false, "Inherited method was not hidden!");
            }
            catch (NoSuchElementException)
            {
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

            string includesPath = "*/qx.ui.toolbar.ToolBar/*/[@label=Includes]";
            ISelectable includesButton = (ISelectable) Driver.FindWidget(By.Qxh(includesPath));
            includesButton.SelectItem("Inherited");
            IWebElement inheritedItem = Driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
            Assert.True(inheritedItem.Displayed);

            includesButton.SelectItem("Inherited");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                Driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
                Assert.True(false, "Inherited method was not hidden!");
            }
            catch (NoSuchElementException)
            {
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void classItems()
        [Test]
        public virtual void ClassItemsTest()
        {
            TestProperties();
            TestIncludes();
            TestClassItem("Protected", "_hideResizeLine");
            TestClassItem("Private", "__isAtEdge");
            TestClassItem("Internal", "getVerticalScrollBarWidth");
        }
    }
}