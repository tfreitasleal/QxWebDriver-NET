using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
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

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void basicWidgets() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
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