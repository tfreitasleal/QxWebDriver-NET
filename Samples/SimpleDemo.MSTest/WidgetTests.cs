using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SD_Tests
{
    public static class WidgetTests
    {
        public static void W01_MainPage_openWindow_Click(QxWebDriver driver)
        {
            By button = By.Qxh(By.Namespace("MainPage.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = driver.FindWidget(By.Name("FirstWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void W02_FirstWindow_openWindow_Click(QxWebDriver driver)
        {
            By button = By.Qxh(By.Namespace("FirstWindow.openWindow"));
            Assert.IsNotNull(button);
            IWidget openButton = driver.FindWidget(button);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = driver.FindWidget(By.Name("SecondWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void W03_SecondWindow_openWindow_LabelContents(QxWebDriver driver)
        {
            By label = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(label);
            IWebElement labelElement = driver.FindWidget(label);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label) labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);
        }
    }
}