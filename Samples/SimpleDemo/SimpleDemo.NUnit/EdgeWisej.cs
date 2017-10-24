using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Wisej.Qooxdoo.WebDriver;

namespace SimpleDemo.NUnit
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class EdgeWisej
    {
        private static IWebDriver _internalWebDriver;

        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _internalWebDriver = new EdgeDriver();
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
        [Order(2050)]
        public void W01_AskQuitNo()
        {
            ExpectedConditions.TitleIs("Main Page");
            TestSuite.W01_AskQuitNo(Driver);
        }

        [Test]
        [Order(2060)]
        public void W02_MainPage_customerEditor_Click()
        {
            TestSuite.W02_MainPage_customerEditor_Click(Driver);
        }

        [Test]
        [Order(2070)]
        public void W03_ButtonsWindow_customerEditor_Click()
        {
            TestSuite.W03_ButtonsWindow_customerEditor_Click(Driver);
        }

        [Test]
        [Order(2080)]
        public void W04_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.W04_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [Test]
        [Order(2090)]
        public void W05_CloseWindow()
        {
            TestSuite.W05_CloseWindow(Driver);
        }

        [Test]
        [Order(2100)]
        public void W06_MainPage_customerEditor_Click()
        {
            TestSuite.W06_MainPage_customerEditor_Click(Driver);
        }

        [Test]
        [Order(2110)]
        public void W07_ButtonsWindow_supplierEditor_Click()
        {
            TestSuite.W07_ButtonsWindow_supplierEditor_Click(Driver);
        }

        [Test]
        [Order(2120)]
        public void W08_CustomerEditor_customerEditor_LabelContents()
        {
            TestSuite.W08_CustomerEditor_customerEditor_LabelContents(Driver);
        }

        [Test]
        [Order(2130)]
        public void W09_CloseWindow()
        {
            TestSuite.W09_CloseWindow(Driver);
        }

        [Test]
        [Order(2140)]
        public void W10_AskQuitYes()
        {
            TestSuite.W10_AskQuitYes(Driver);
        }
    }
}