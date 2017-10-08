using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.SimpleDemoTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class ChromeWidgetTests
    {
        private QxWebDriver _driver;

        [OneTimeSetUp] // class SetUp?
        public void OpenMainPage()
        {
            _driver = new QxWebDriver(new ChromeDriver());
            _driver.Url = "http://localhost:16461/";
        }


        [Test]
        [Order(50)]
        public void Wdg_01_MainPage_openWindow_Click()
        {
            By button = By.Qxh(By.Namespace("MainPage.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = _driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = _driver.FindWidget(By.Name("FirstWindow"), 10);
            Assert.IsNotNull(widget);
        }

        [Test]
        [Order(60)]
        public void Wdg_02_FirstWindow_openWindow_Click()
        {
            By button = By.Qxh(By.Namespace("FirstWindow.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = _driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = _driver.FindWidget(By.Name("SecondWindow"), 10);
            Assert.IsNotNull(widget);
        }

        [Test]
        [Order(70)]
        public void Wdg_03_SecondWindow_openWindow_LabelContents()
        {
            By label = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(label);
            IWebElement labelElement = _driver.FindWidget(label);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label) labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);
        }
    }
}