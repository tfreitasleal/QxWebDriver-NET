using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wisej.Qooxdoo.WebDriver;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SD_Tests
{
    [TestClass]
    public class ChromeElementTests
    {
        private IWebDriver Driver;

        [ClassInitialize]
        public void OpenMainPage()
        {
            Driver = new ChromeDriver();
            Driver.Url = "http://localhost:16461/";
        }

        [TestMethod]
        //[Order(10)]
        public void E01_MainPageElementsExist()
        {
            try
            {
                IWebElement element = Driver.FindElement(By.Name("MainPage"), 10);
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

        [TestMethod]
        //[Order(20)]
        public void E02_FirstWindowElementsDontExist()
        {
            IWebElement element = null;
            try
            {
                element = Driver.FindElement(By.Name("FirstWindow"));

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

        [TestMethod]
        //[Order(30)]
        public void E03_SecondWindowElementsDontExist()
        {
            IWebElement element = null;

            try
            {
                element = Driver.FindElement(By.Name("SecondWindow"));

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

        [TestMethod]
        //[Order(100)]
        public void E04_AllElementsExist_LabelContent()
        {
            try
            {
                // find MainPage.openWindow
                IWebElement element = Driver.FindElement(By.Name("MainPage"));
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNotNull(element);
                element.Click();

                // find FirstWindow.openWindow
                element = Driver.FindElement(By.Name("FirstWindow"), 10);
                Assert.IsNotNull(element);
                element = element.FindElement(By.Name("openWindow"));
                Assert.IsNotNull(element);
                element.Click();

                // find SecondWindow.label1
                element = Driver.FindElement(By.Name("SecondWindow"), 10);
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