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
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/Default.html";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
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
        public void F01_AskQuitNo()
        {
            ExpectedConditions.TitleIs("Main Page");
            TestSuite.F01_AskQuitNo(Driver);
        }

        [Test]
        [Order(5060)]
        public void F02_MainPage_customerEditor_Click()
        {
            TestSuite.F02_MainPage_customerEditor_Click(Driver);
        }

        [Test]
        [Order(5070)]
        public void F03_ButtonsWindow_customerEditor_Click()
        {
            TestSuite.F03_ButtonsWindow_customerEditor_Click(Driver);
        }

        [Test]
        [Order(5080)]
        public void F04_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.F04_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [Test]
        [Order(5090)]
        public void F05_CloseWindow()
        {
            TestSuite.F05_CloseWindow(Driver);
        }

        [Test]
        [Order(5100)]
        public void F06_MainPage_customerEditor_Click()
        {
            TestSuite.F06_MainPage_customerEditor_Click(Driver);
        }

        [Test]
        [Order(5110)]
        public void F07_ButtonsWindow_customerEditor_Click()
        {
            TestSuite.F07_ButtonsWindow_customerEditor_Click(Driver);
        }

        [Test]
        [Order(5120)]
        public void F08_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.F08_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [Test]
        [Order(5130)]
        public void F09_CloseWindow()
        {
            TestSuite.F09_CloseWindow(Driver);
        }

        [Test]
        [Order(5140)]
        public void F10_AskQuitYes()
        {
            TestSuite.F10_AskQuitYes(Driver);
        }
    }
}