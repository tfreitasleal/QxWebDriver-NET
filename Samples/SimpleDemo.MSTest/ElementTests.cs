using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SD_Tests
{
    public static class ElementTests
    {
        public static void E01_MainPageElementsExist(IWebDriver driver)
        {
            try
            {
                IWebElement element = driver.FindElement(By.Name("MainPage"), 10);
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNotNull(element);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public static void E02_FirstWindowElementsDontExist(IWebDriver driver)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(By.Name("FirstWindow"));

                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNull(element);
            }
            catch (NoSuchElementException)
            {
                Assert.IsNull(element);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public static void E03_SecondWindowElementsDontExist(IWebDriver driver)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(By.Name("SecondWindow"));

                element = element.FindElement(By.Name("label1"));
                Assert.IsNull(element);
            }
            catch (NoSuchElementException)
            {
                Assert.IsNull(element);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public static void E04_AllElementsExist_LabelContent(IWebDriver driver)
        {
            try
            {
                // find MainPage.openWindow
                IWebElement element = driver.FindElement(By.Name("MainPage"));
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNotNull(element);
                element.Click();

                // find FirstWindow.openWindow
                element = driver.FindElement(By.Name("FirstWindow"), 10);
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNotNull(element);
                element.Click();

                // find SecondWindow.label1
                element = driver.FindElement(By.Name("SecondWindow"), 10);
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("label1"));
                Assert.IsNotNull(element);
                Assert.AreEqual("End of windows", element.Text);
            }
            catch (NoSuchElementException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}