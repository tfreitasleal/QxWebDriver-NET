using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.NUnit
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class OperaWisej
    {
        private static string _operaBinary = @"C:\Program Files (x86)\Opera\48.0.2685.39\opera.exe";

        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new OperaOptions();
            options.BinaryLocation = _operaBinary;
            _internalWebDriver = new OperaDriver(options);
            _internalWebDriver.Manage().Window.Maximize();
            Driver = new QxWebDriver(_internalWebDriver);
            Driver.Url = "http://localhost:16461/";
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _internalWebDriver.Quit();
            _internalWebDriver.Dispose();
            Driver.Quit();
            Driver.Dispose();
        }

        [Test]
        [Order(5050)]
        public void F01_MainPage_openWindow_Click()
        {
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(5060)]
        public void F02_FirstWindow_openWindow_Click()
        {
            FirstRound.F02_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(5070)]
        public void F03_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [Test]
        [Order(5080)]
        public void F04_CloseWindows()
        {
            FirstRound.F04_CloseWindows(Driver);
        }


        [Test]
        [Order(5100)]
        public void S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(5110)]
        public void S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(5120)]
        public void S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }
    }
}