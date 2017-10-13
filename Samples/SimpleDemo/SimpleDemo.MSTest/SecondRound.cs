using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;
using Wait = SimpleDemo.MSTest.Waiter;

namespace SimpleDemo.MSTest
{
    public static class SecondRound
    {
        public static void S01_MainPage_openWindow_Click(QxWebDriver driver)
        {
            OpenQA.Selenium.By buttonBy = By.Qxh(By.Namespace("MainPage.openWindow"));
            Assert.IsNotNull(buttonBy);
            IWidget openButton = driver.WaitForWidget(buttonBy, 10);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("FirstWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void S02_FirstWindow_openWindow_Click(QxWebDriver driver)
        {
            OpenQA.Selenium.By buttonBy = By.Qxh(By.Namespace("FirstWindow.openWindow"));
            Assert.IsNotNull(buttonBy);
            IWidget openButton = driver.WaitForWidget(buttonBy, 10);
            Assert.IsNotNull(openButton);
            // Click the button if it's not already selected
            if (!openButton.Selected)
            {
                openButton.Click();
            }

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("SecondWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void S03_SecondWindow_openWindow_LabelContents(QxWebDriver driver)
        {
            OpenQA.Selenium.By labelBy = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(labelBy);
            IWebElement labelElement = driver.WaitForWidget(labelBy, 10);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label) labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);

            Thread.Sleep(Wait.Duration);
        }
    }
}