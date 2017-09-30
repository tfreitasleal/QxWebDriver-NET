using System;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
using Widget = Qooxdoo.WebDriver.UI.IWidget;
using NoSuchElementException = OpenQA.Selenium.NoSuchElementException;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    public class ClassItems : DesktopApiViewer
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            DesktopApiViewer.setUpBeforeClass();
            string className = "qx.ui.table.pane.Scroller";
            SelectClass(className);
        }

        protected internal virtual void testProperties()
        {
            string propertiesPath = "*/qx.ui.toolbar.ToolBar/*/[@label=Properties]";
            Widget propertiesButton = driver.FindWidget(By.Qxh(propertiesPath));
            bool propertiesActive = ((bool?) propertiesButton.GetPropertyValue("value")).Value;
            Assert.True(propertiesActive);
            string propertyItemPath = "//div[contains(@class, 'info-panel')]/descendant::span[text()='getLiveResize']";
            WebElement propertyItem = driver.FindElement(OpenQA.Selenium.By.XPath(propertyItemPath));
            Assert.True(propertyItem.Displayed);
            propertiesButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                propertyItem = driver.FindElement(OpenQA.Selenium.By.XPath(propertyItemPath));
                Assert.True("Property method was not hidden!", false);
            }
            catch (NoSuchElementException)
            {
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            propertiesButton.Click();
        }

        protected internal virtual void testClassItem(string item, string method)
        {
            string itemPath = "*/qx.ui.toolbar.ToolBar/*/[@label=" + item + "]";
            Widget itemButton = driver.FindWidget(By.Qxh(itemPath));
            bool itemActive = ((bool?) itemButton.GetPropertyValue("value")).Value;
            Assert.False(itemActive);
            string methodPath = "//div[contains(@class, 'info-panel')]/descendant::span[text()='" + method + "']";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
                Assert.True(item + " item " + method + " was not hidden initially!", false);
            }
            catch (NoSuchElementException)
            {
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            itemButton.Click();

            WebElement methodItem = driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
            Assert.True(methodItem.Displayed);
            itemButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                driver.FindElement(OpenQA.Selenium.By.XPath(methodPath));
                Assert.True(item + " item " + method + " was not hidden!", false);
            }
            catch (NoSuchElementException)
            {
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        protected internal virtual void testIncludes()
        {
            string inheritedItemPath = "//div[contains(@class, 'info-panel')]/descendant::span[text()='changeTextColor']";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
                Assert.True("Inherited method was not hidden!", false);
            }
            catch (NoSuchElementException)
            {
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);

            string includesPath = "*/qx.ui.toolbar.ToolBar/*/[@label=Includes]";
            Selectable includesButton = (Selectable) driver.FindWidget(By.Qxh(includesPath));
            includesButton.SelectItem("Inherited");
            WebElement inheritedItem = driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
            Assert.True(inheritedItem.Displayed);

            includesButton.SelectItem("Inherited");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            try
            {
                driver.FindElement(OpenQA.Selenium.By.XPath(inheritedItemPath));
                Assert.True("Inherited method was not hidden!", false);
            }
            catch (NoSuchElementException)
            {
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void classItems()
        public virtual void classItems()
        {
            testProperties();
            testIncludes();
            testClassItem("Protected", "_hideResizeLine");
            testClassItem("Private", "__isAtEdge");
            testClassItem("Internal", "getVerticalScrollBarWidth");
        }
    }

}