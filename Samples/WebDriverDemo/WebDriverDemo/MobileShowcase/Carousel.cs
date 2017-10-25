using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Mobile.Core;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Carousel : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Carousel");
            VerifyTitle("Carousel");
        }

        //ORIGINAL LINE: @Test public void carousel() throws InterruptedException
        [Test]
        public void TestCarousel()
        {
            if (!(Driver.WebDriver is IHasTouchScreen))
            {
                return;
            }
            IList<IWebElement> pages =
                Driver.FindElements(By.XPath("//div[contains(@class, 'carousel-page')]"));

            IWebElement label0 =
                Driver.FindElement(By.XPath("//div[contains(text(), 'This is a carousel')]"));
            Assert.True(label0.Displayed);

            WidgetImpl.Track(Driver.WebDriver, pages[0], -350, 0, 50);
            Thread.Sleep(1000);

            IWebElement label1 =
                Driver.FindElement(By.XPath("//div[contains(text(), 'It contains multiple carousel pages')]"));
            Assert.True(label1.Displayed);

            WidgetImpl.Track(Driver.WebDriver, pages[1], -350, 0, 50);
            Thread.Sleep(1000);

            IWebElement label2 =
                Driver.FindElement(By.XPath("//div[contains(text(), 'Carousel pages may contain')]"));
            Assert.True(label2.Displayed);

            ITouchable nextPage =
                (ITouchable) Driver.FindWidget(
                    By.XPath("//div[text() = 'Next Page']/ancestor::div[contains(@class, 'button')]"));
            nextPage.Tap();
            Thread.Sleep(1000);

            IWebElement label3 =
                Driver.FindElement(By.XPath("//div[contains(text(), 'The carousel snaps')]"));
            Assert.True(label3.Displayed);

            WidgetImpl.Track(Driver.WebDriver, pages[3], -350, 0, 50);
            Thread.Sleep(1000);

            IWebElement label4 =
                Driver.FindElement(By.XPath("//div[contains(text(), 'You can add as many')]"));
            Assert.True(label4.Displayed);

            IList<IWebElement> paginationLabels =
                Driver.FindElements(By.XPath("//div[contains(@class, 'carousel-pagination-label')]"));
            Assert.Equals(6, paginationLabels.Count);

            ITouchable addButton =
                (ITouchable) Driver.FindWidget(
                    By.XPath("//div[text() = 'Add more pages']/ancestor::div[contains(@class, 'button')]"));
            addButton.Tap();

            paginationLabels =
                Driver.FindElements(By.XPath("//div[contains(@class, 'carousel-pagination-label')]"));
            Assert.Equals(56, paginationLabels.Count);
        }
    }
}