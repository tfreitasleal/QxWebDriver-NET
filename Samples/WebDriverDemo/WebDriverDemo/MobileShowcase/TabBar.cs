using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class TabBar : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Tab Bar");
            VerifyTitle("Tabs");
        }

        //ORIGINAL LINE: @Test public void tabBar() throws InterruptedException
        [Test]
        public void TestTabBar()
        {
            string[] tabs = {"Desktop", "Server", "Mobile", "Website"};
            foreach (string tab in tabs)
            {
                ITouchable tabButton = (ITouchable) Driver.FindWidget(
                    By.XPath("//div[text() = '" + tab + "']/ancestor::div[contains(@class, 'tabButton')]"));
                tabButton.Tap();
                Thread.Sleep(500);
                IWebElement tabContent = Driver.FindElement(
                        By.XPath("//b[text() = 'qx." + tab + "']/ancestor::div[contains(@class, 'content')]"));
                Assert.True(tabContent.Displayed);
            }
        }
    }
}