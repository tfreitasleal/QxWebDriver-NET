using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class BasicWidgets : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "Basic Widgets";
            SelectItem(title);
            VerifyTitle(title);
        }

        //ORIGINAL LINE: @Test public void basicWidgets() throws InterruptedException
        [Test]
        public virtual void TestBasicWidgets()
        {
            // toggle button
            ITouchable toggleButton =
                (ITouchable) Driver.FindWidget(By.XPath("//div[contains(@class, 'togglebutton') and @data-label-checked='ON']"));
            bool? valueBefore = (bool?) toggleButton.GetPropertyValue("value");
            Assert.True(valueBefore);
            Thread.Sleep(250);
            toggleButton.Tap();
            bool? valueAfter = (bool?) toggleButton.GetPropertyValue("value");
            Assert.False(valueAfter);
            Thread.Sleep(500);
            toggleButton.Tap();

            ScrollTo(0, 500);
            Thread.Sleep(500);

            // collapsible
            ITouchable collapsibleHeader =
                (ITouchable) Driver.FindWidget(By.XPath("//div[contains(@class, 'collapsible-header')]"));
            IWebElement collapsibleContent =
                Driver.FindElement(By.XPath("//div[contains(@class, 'collapsible-content')]"));
            Assert.False(collapsibleContent.Displayed);
            collapsibleHeader.Tap();
            Assert.True(collapsibleContent.Displayed);
        }
    }
}