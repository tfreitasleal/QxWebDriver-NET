using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriver.UI.Mobile.Form
{
    public class Slider : Core.WidgetImpl
    {
        public Slider(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public override void Track(int x, int y, int step)
        {
            WebElement element = contentElement.FindElement(OpenQA.Selenium.By.XPath("div"));
            Track(Driver.WebDriver, element, x, y, step);
        }
    }
}