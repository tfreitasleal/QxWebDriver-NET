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
        public void F01_AskQuitNo()
        {
            ExpectedConditions.TitleIs("Main Page");
            FirstRound.F01_AskQuitNo(Driver);
        }

        [Test]
        [Order(2060)]
        public void F02_MainPage_openWindow_Click()
        {
            FirstRound.F02_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(2070)]
        public void F03_FirstWindow_openWindow_Click()
        {
            FirstRound.F03_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(2080)]
        public void F04_SecondWindow_openWindow_LabelContents()
        {
            FirstRound.F04_SecondWindow_openWindow_LabelContents(Driver);
        }

        [Test]
        [Order(2090)]
        public void F05_CloseWindows()
        {
            FirstRound.F05_CloseWindows(Driver);
        }

        [Test]
        [Order(2100)]
        public void S01_MainPage_openWindow_Click()
        {
            SecondRound.S01_MainPage_openWindow_Click(Driver);
        }

        [Test]
        [Order(2110)]
        public void S02_FirstWindow_openWindow_Click()
        {
            SecondRound.S02_FirstWindow_openWindow_Click(Driver);
        }

        [Test]
        [Order(2120)]
        public void S03_SecondWindow_openWindow_LabelContents()
        {
            SecondRound.S03_SecondWindow_openWindow_LabelContents(Driver);
        }

        [Test]
        [Order(2130)]
        public void S04_CloseWindows()
        {
            SecondRound.S04_CloseWindows(Driver);
        }

        [Test]
        [Order(2140)]
        public void S05_AskQuitYes()
        {
            SecondRound.S05_AskQuitYes(Driver);
        }
    }
}