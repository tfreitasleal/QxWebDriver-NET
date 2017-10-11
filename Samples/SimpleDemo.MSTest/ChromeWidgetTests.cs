using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SD_Tests
{
    [TestClass]
    public class ChromeWidgetTests
    {
        private QxWebDriver Driver;

        [ClassInitialize]
        public void OpenMainPage()
        {
            Driver = new QxWebDriver(new ChromeDriver());
            Driver.Url = "http://localhost:16461/";
        }


        [TestMethod]
        public void W01_MainPage_openWindow_Click()
        {
            By button = By.Qxh(By.Namespace("MainPage.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = Driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = Driver.FindWidget(By.Name("FirstWindow"), 10);
            Assert.IsNotNull(widget);
        }

        [TestMethod]
        public void W02_FirstWindow_openWindow_Click()
        {
            By button = By.Qxh(By.Namespace("FirstWindow.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = Driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = Driver.FindWidget(By.Name("SecondWindow"), 10);
            Assert.IsNotNull(widget);
        }

        [TestMethod]
        public void W03_SecondWindow_openWindow_LabelContents()
        {
            By label = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(label);
            IWebElement labelElement = Driver.FindWidget(label);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label) labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);
        }
    }
}