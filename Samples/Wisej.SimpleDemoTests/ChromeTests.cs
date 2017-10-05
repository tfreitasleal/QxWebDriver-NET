using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Wisej.WebDriver;
using Wisej.WebDriver.UI;
using By = Wisej.WebDriver.By;

namespace Wisej.SimpleDemoTests
{
    [TestFixture]
    public class ChromeTests
    {
        private QxWebDriver _driver;

        [OneTimeSetUp]
        public void OpenMainPage()
        {
            _driver = new QxWebDriver(new ChromeDriver());
            _driver.Url = "http://localhost:16461/";
        }

       
        [Test]
        public void ElementExists()
        {
            IWebElement element = _driver.FindElement(By.Name("openWindow"));
            Assert.IsNotNull(element);
        }

        [Test]
        public void ButtonExists()
        {
            OpenQA.Selenium.By button = By.Name("openWindow");
            IWidget openButton = _driver.FindWidget(button);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }
        }
    }
}