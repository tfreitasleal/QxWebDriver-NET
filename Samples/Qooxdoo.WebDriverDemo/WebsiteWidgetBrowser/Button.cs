using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using WebElement = OpenQA.Selenium.IWebElement;
using Qooxdoo.WebDriver;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Button : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Button");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void menuButton()
        public virtual void menuButton()
        {
            WebElement menu = webDriver.FindElement(By.Id("menu"));
            Assert.False(menu.Displayed);

            WebElement menuButton = webDriver.FindElement(By.Id("menu-button"));
            menuButton.Click();
            Assert.True(menu.Displayed);

            menuButton.Click();
            Assert.False(menu.Displayed);

            menuButton.Click();
            Assert.True(menu.Displayed);

            WebElement header = webDriver.FindElement(OpenQA.Selenium.By.XPath("//h1"));
            header.Click();
            Assert.False(menu.Displayed);
        }
    }

}