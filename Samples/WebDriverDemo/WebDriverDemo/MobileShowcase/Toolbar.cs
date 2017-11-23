using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Toolbar : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Toolbar");
            VerifyTitle("Toolbar");
        }

        //ORIGINAL LINE: @Test public void search()
        [Test]
        public virtual void Search()
        {
            ITouchable button = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Search']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            ITouchable popupButton = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Search']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            Assert.False(popupButton.Displayed);
        }

        //ORIGINAL LINE: @Test public void back()
        [Test]
        public virtual void Back()
        {
            ITouchable button = (ITouchable) Driver.FindWidget(By.XPath(
                "//img[contains(@src, 'arrowleft')]/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            ITouchable popupButton = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Are you sure?']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            Assert.False(popupButton.Displayed);
        }

        //ORIGINAL LINE: @Test public void camera() throws InterruptedException
        [Test]
        public virtual void Camera()
        {
            ITouchable button = (ITouchable) Driver.FindWidget(By.XPath(
                "//img[contains(@src, 'camera')]/ancestor::div[contains(@class, 'button')]"));
            button.Tap();

            IWebElement popup = Driver.FindElement(By.XPath(
                "//div[text() = 'Data connection...']/ancestor::div[contains(@class, 'popup-content')]"));
            Assert.True(popup.Displayed);
            Thread.Sleep(5000);
            Assert.False(popup.Displayed);
        }

        //ORIGINAL LINE: @Test public void delete()
        [Test]
        public virtual void Delete()
        {
            ITouchable button = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Delete']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            ITouchable popupButton = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Are you sure?']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            try
            {
                Assert.False(popupButton.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }
    }
}