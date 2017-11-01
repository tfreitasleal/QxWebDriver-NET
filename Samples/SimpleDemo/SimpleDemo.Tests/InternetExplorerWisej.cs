using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using Qooxdoo.WebDriver;

namespace SimpleDemo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class InternetExplorerWisej
    {
        public static QxWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            Driver = new QxWebDriver(Browser.IE);
            Cache.Clear();
            WisejTests.Driver = Driver;
#if !DEBUGJS
            Driver.Url = "http://localhost:16461/Default.html";
#else
            Driver.Url = "http://localhost:16461/Debug.html";
#endif
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [Test]
        [Order(4050)]
        public void W01_AskQuitNo()
        {
            ExpectedConditions.TitleIs("Main Page");
            WisejTests.W01_AskQuitNo();
        }

        [Test]
        [Order(4060)]
        public void W02_MainPage_customerEditor_Click()
        {
            WisejTests.W02_MainPage_customerEditor_Click();
        }

        [Test]
        [Order(4070)]
        public void W03_ButtonsWindow_customerEditor_Click()
        {
            WisejTests.W03_ButtonsWindow_customerEditor_Click();
        }

        [Test]
        [Order(4080)]
        public void W04_CustomerEditor_customerEditor_LabelContents()
        {
            WisejTests.W04_CustomerEditor_customerEditor_LabelContents();
        }

        [Test]
        [Order(4090)]
        public void W05_CloseWindow()
        {
            WisejTests.W05_CloseWindow();
        }

        [Test]
        [Order(4100)]
        public void W06_MainPage_customerEditor_Click()
        {
            WisejTests.W06_MainPage_customerEditor_Click();
        }

        [Test]
        [Order(4110)]
        public void W07_ButtonsWindow_supplierEditor_Click()
        {
            WisejTests.W07_ButtonsWindow_supplierEditor_Click();
        }

        [Test]
        [Order(4120)]
        public void W08_CustomerEditor_customerEditor_LabelContents()
        {
            WisejTests.W08_CustomerEditor_customerEditor_LabelContents();
        }

        [Test]
        [Order(4130)]
        public void W09_CloseWindow()
        {
            WisejTests.W09_CloseWindow();
        }

        [Test]
        [Order(4140)]
        public void W10_AskQuitYes()
        {
            WisejTests.W10_AskQuitYes();
        }
    }
}