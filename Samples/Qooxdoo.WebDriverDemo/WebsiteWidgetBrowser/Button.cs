using NUnit.Framework;
using OpenQA.Selenium;
using By = Qooxdoo.WebDriver.By;

namespace Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    public class Button : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Button");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void menuButton()
        [Test]
        public virtual void MenuButton()
        {
            IWebElement menu = webDriver.FindElement(By.Id("menu"));
            Assert.False(menu.Displayed);

            IWebElement menuButton = webDriver.FindElement(By.Id("menu-button"));
            menuButton.Click();
            Assert.True(menu.Displayed);

            menuButton.Click();
            Assert.False(menu.Displayed);

            menuButton.Click();
            Assert.True(menu.Displayed);

            IWebElement header = webDriver.FindElement(OpenQA.Selenium.By.XPath("//h1"));
            header.Click();
            Assert.False(menu.Displayed);
        }
    }
}