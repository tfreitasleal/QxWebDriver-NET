using OpenQA.Selenium;

namespace Qooxdoo.WebDriver.UI.Mobile.Form
{
    public class Slider : Core.WidgetImpl
    {
        public Slider(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public override void Track(int x, int y, int step)
        {
            IWebElement element = contentElement.FindElement(OpenQA.Selenium.By.XPath("div"));
            Track(Driver.WebDriver, element, x, y, step);
        }
    }
}