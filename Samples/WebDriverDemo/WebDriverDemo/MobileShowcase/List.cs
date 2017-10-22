using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Qooxdoo.WebDriver.UI.Mobile.Core;
using ISelectable = Wisej.Qooxdoo.WebDriver.UI.Mobile.ISelectable;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class List : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "List";
            SelectItem(title);
            VerifyTitle(title);
        }

        //ORIGINAL LINE: @Test public void SelectItem()
        [Test]
        public virtual void SelectItem()
        {
            ISelectable list = (ISelectable) Driver.FindWidget(By.XPath(
                "//div[contains(@class, 'master-detail-detail')]/descendant::ul[contains(@class, 'list')]"));
            list.SelectItem("Item #3");

            IWebElement selected = Driver.FindElement(By.XPath("//div[text() = 'You selected Item #3']"));
            Assert.True(selected.Displayed);
            ITouchable ok = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'You selected Item #3']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'dialog-button')]"));
            ok.Tap();
            try
            {
                Assert.False(selected.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }

        //ORIGINAL LINE: @Test public void removeItem() throws InterruptedException
        [Test]
        public virtual void RemoveItem()
        {
            if (!(Driver.WebDriver is IHasTouchScreen))
            {
                return;
            }
            IWebElement item = Driver.FindElement(By.XPath("//div[text() = 'Item #6']"));
            WidgetImpl.Track(Driver.WebDriver, item, 700, 0, 10);
            Thread.Sleep(1000);
            try
            {
                Assert.False(item.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }
    }
}