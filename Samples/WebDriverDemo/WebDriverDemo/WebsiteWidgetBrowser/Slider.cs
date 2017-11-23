using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Slider : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Slider");
        }

        protected internal virtual void Drag(IWebElement element, int x, int y)
        {
            Actions mouseAction = new Actions(webDriver);
            mouseAction.DragAndDropToOffset(element, x, y);
            mouseAction.Perform();
        }

        //ORIGINAL LINE: @Test public void slider() throws InterruptedException
        [Test]
        public virtual void SliderTest()
        {
            string getValue = "return qxWeb(arguments[0]).getValue();";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;

            IList<IWebElement> sliders =
                webDriver.FindElements(By.XPath("//div[@id = 'slider-page']/descendant::div[contains(@class, 'qx-slider')]"));
            Assert.Equals(2, sliders.Count);
            IEnumerator<IWebElement> itr = sliders.GetEnumerator();
            while (itr.MoveNext())
            {
                IWebElement slider = itr.Current;
                long? valueBefore = (long?) exec.ExecuteScript(getValue, slider);
                IWebElement knob = slider.FindElement(By.XPath("button[contains(@class, 'qx-slider-knob')]"));
                Drag(knob, 150, 0);
                long? valueAfter = (long?) exec.ExecuteScript(getValue, slider);
                Assert.True(valueAfter > valueBefore);
            }
        }
    }
}