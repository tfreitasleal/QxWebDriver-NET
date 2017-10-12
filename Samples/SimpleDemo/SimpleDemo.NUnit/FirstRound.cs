using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SimpleDemo.NUnit
{
    public static class FirstRound
    {
        public static void F01_MainPage_openWindow_Click(QxWebDriver driver)
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

        public static void F02_FirstWindow_openWindow_Click(QxWebDriver driver)
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

        public static void F03_SecondWindow_openWindow_LabelContents(QxWebDriver driver)
        {
            OpenQA.Selenium.By labelBy = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(labelBy);
            IWebElement labelElement = driver.WaitForWidget(labelBy, 10);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label)labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);
        }

        public static void F04_CloseWindows(QxWebDriver driver)
        {
            OpenQA.Selenium.By secondWindowsBy = By.Qxh(By.Namespace("SecondWindow"));
            IWidget secondWindow = driver.WaitForWidget(secondWindowsBy, 10);
            OpenQA.Selenium.By secondWindowCaptionBarBy = By.Qxh("qx.ui.container.Composite");
            IWidget secondWindowCaptionBar = secondWindow.FindWidget(secondWindowCaptionBarBy);
            secondWindowCaptionBar.Children[1].Click();

            OpenQA.Selenium.By firstWindowsBy = By.Qxh(By.Namespace("FirstWindow"));
            IWidget firstWindow = driver.WaitForWidget(firstWindowsBy, 10);
            OpenQA.Selenium.By firstWindowCaptionBarBy = By.Qxh("qx.ui.container.Composite");
            IWidget firstWindowCaptionBar = firstWindow.FindWidget(firstWindowCaptionBarBy);
            firstWindowCaptionBar.Children[1].Click();
        }
    }
}