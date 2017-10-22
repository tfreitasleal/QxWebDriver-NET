using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Basic;
using By = Wisej.Qooxdoo.WebDriver.By;
using Wait = SimpleDemo.NUnit.Waiter;

namespace SimpleDemo.NUnit
{
    public static class FirstRound
    {
        public static void F01_AskQuitNo(QxWebDriver driver)
        {
            IWidget mainPage = driver.WaitForWidget(By.Qxh(By.Namespace("MainPage")), 10);
            Assert.IsNotNull(mainPage);

            IWidget buttonSayGoodBye = driver.WaitForWidget(By.Qxh(By.Namespace("MainPage.sayGoodBye")), 10);
            Assert.IsNotNull(buttonSayGoodBye);
            // Click the button if it's not already selected
            if (!buttonSayGoodBye.Selected)
            {
                buttonSayGoodBye.Click();
            }

            driver.AssertMessageBox(Wisej.Web.DialogResult.No);
        }

        public static void F02_MainPage_openWindow_Click(QxWebDriver driver)
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

            var widget = driver.WaitForWidget(OpenQA.Selenium.By.Name("ListWindow"), 10);
            Assert.IsNotNull(widget);
        }

        public static void F03_FirstWindow_openWindow_Click(QxWebDriver driver)
        {
            OpenQA.Selenium.By buttonBy = By.Qxh(By.Namespace("ListWindow.openWindow"));
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

        public static void F04_SecondWindow_openWindow_LabelContents(QxWebDriver driver)
        {
            OpenQA.Selenium.By labelBy = By.Qxh(By.Namespace("SecondWindow.label1"));
            Assert.IsNotNull(labelBy);
            IWebElement labelElement = driver.WaitForWidget(labelBy, 10);
            Assert.IsNotNull(labelElement);
            Label label1 = (Label) labelElement;
            Assert.IsNotNull(label1);
            Assert.AreEqual("End of windows", label1.Value);
        }

        public static void F05_CloseWindows(QxWebDriver driver)
        {
            Thread.Sleep(Wait.Duration);

            OpenQA.Selenium.By secondWindowsBy = By.Qxh(By.Namespace("SecondWindow"));
            IWidget secondWindow = driver.WaitForWidget(secondWindowsBy, 10);
            IWidget secondWindowCloseButton = secondWindow.GetChildControl("close-button");
            secondWindowCloseButton.Click();

            Thread.Sleep(Wait.Duration);

            OpenQA.Selenium.By firstWindowsBy = By.Qxh(By.Namespace("ListWindow"));
            IWidget listWindow = driver.WaitForWidget(firstWindowsBy, 10);
            IWidget firstWindowCloseButton = listWindow.GetChildControl("close-button");
            firstWindowCloseButton.Click();

            Thread.Sleep(Wait.Duration);
        }
    }
}