using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Wisej.Qooxdoo.WebDriver;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.SimpleDemoTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class FirefoxElementTests
    {
        private IWebDriver _driver;

        [OneTimeSetUp] // class SetUp?
        public void OpenMainPage()
        {
            _driver = new FirefoxDriver();
            _driver.Url = "http://localhost:16461/";
        }

        [Test]
        [Order(10)]
        //[NonParallelizable]
        //[Parallelizable(ParallelScope.Fixtures)]
        //[MaxTime(1000)]
        public void Elem_01_MainPageElementsExist()
        {
            try
            {
                Assert.Multiple(() =>
                {
                    IWebElement element = _driver.FindElement(By.Name("MainPage"), 10);
                    Assert.IsNotNull(element);
                    element = element.FindElement(By.Name("openWindow"));
                    Assert.IsNotNull(element);
                });
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

        [Test]
        [Order(20)]
        public void Elem_02_FirstWindowElementsDontExist()
        {
            IWebElement element = null;
            try
            {
                Assert.Multiple(() =>
                {
                    element = _driver.FindElement(By.Name("FirstWindow"));

                    element = element.FindElement(By.Name("openWindow"));
                    Assert.IsNull(element);
                });
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

        [Test]
        [Order(30)]
        public void Elem_03_SecondWindowElementsDontExist()
        {
            IWebElement element = null;

            try
            {
                Assert.Multiple(() =>
                {
                    element = _driver.FindElement(By.Name("SecondWindow"));

                    element = element.FindElement(By.Name("label1"));
                    Assert.IsNull(element);
                });
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

        [Test]
        [Order(100)]
        public void Elem_04_AllElementsExist_LabelContent()
        {
            try
            {
                Assert.Multiple(() =>
                {
                    // find MainPage.openWindow
                    IWebElement element = _driver.FindElement(By.Name("MainPage"));
                    Assert.IsNotNull(element);
                    element = element.FindElement(By.Name("openWindow"));
                    Assert.IsNotNull(element);
                    element.Click();

                    // find FirstWindow.openWindow
                    element = _driver.FindElement(By.Name("FirstWindow"), 10);
                    Assert.IsNotNull(element);
                    element = element.FindElement(By.Name("openWindow"));
                    Assert.IsNotNull(element);
                    element.Click();

                    // find SecondWindow.label1
                    element = _driver.FindElement(By.Name("SecondWindow"), 10);
                    Assert.IsNotNull(element);
                    element = element.FindElement(By.Name("label1"));
                    Assert.IsNotNull(element);
                    Assert.AreEqual("End of windows", element.Text);
                });
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