using System.Collections.Generic;
using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using IJavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using WebElement = OpenQA.Selenium.IWebElement;
using Actions = OpenQA.Selenium.Interactions.Actions;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Slider : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Slider");
        }

        protected internal virtual void Drag(WebElement element, int x, int y)
        {
            Actions mouseAction = new Actions(webDriver);
            mouseAction.dragAndDropBy(element, x, y);
            mouseAction.Perform();
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void slider() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void SliderTest()
        {
            string getValue = "return qxWeb(arguments[0]).getValue();";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;

            IList<WebElement> sliders = webDriver.FindElements(OpenQA.Selenium.By.XPath("//div[@id = 'slider-page']/descendant::div[contains(@class, 'qx-slider')]"));
            Assert.Equals(2, sliders.Count);
            IEnumerator<WebElement> itr = sliders.GetEnumerator();
            while (itr.MoveNext())
            {
                WebElement slider = itr.Current;
                long? valueBefore = (long?) exec.ExecuteScript(getValue, slider);
                WebElement knob = slider.FindElement(OpenQA.Selenium.By.XPath("button[contains(@class, 'qx-slider-knob')]"));
                Drag(knob, 150, 0);
                long? valueAfter = (long?) exec.ExecuteScript(getValue, slider);
                Assert.True(valueAfter > valueBefore);
            }
        }
    }

}