using Qooxdoo.WebDriver.UI.Mobile.Core;
using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriver.UI.Mobile.List
{
    public class List : Core.WidgetImpl, ISelectable
    {
        public List(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public IWidget GetSelectableItem(int? index)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SelectItem(int? index)
        {
            index++; //xpath's position() is 1-based
            var locator = OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'list-item') and position() = " + index + "]");
            WebElement item = contentElement.FindElement(locator);
            WidgetImpl.Tap(Driver.WebDriver, item);
        }

        public IWidget GetSelectableItem(string regex)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SelectItem(string title)
        {
            var locator = OpenQA.Selenium.By.XPath("descendant::div[contains(@class, 'list-item-title') and text() = '" + title + "']/ancestor::li");
            WebElement item = contentElement.FindElement(locator);
            WidgetImpl.Tap(Driver.WebDriver, item);
        }
    }
}