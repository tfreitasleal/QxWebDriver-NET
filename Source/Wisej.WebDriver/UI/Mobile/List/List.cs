using OpenQA.Selenium;
using WidgetImpl = Wisej.WebDriver.UI.Mobile.Core.WidgetImpl;

namespace Wisej.WebDriver.UI.Mobile.List
{
    public class List : WidgetImpl, ISelectable
    {
        public List(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public IWidget GetSelectableItem(int? index)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SelectItem(int? index)
        {
            index++; //xpath's position() is 1-based
            var locator =
                OpenQA.Selenium.By.XPath("descendant::li[contains(@class, 'list-item') and position() = " + index + "]");
            IWebElement item = contentElement.FindElement(locator);
            WidgetImpl.Tap(Driver.WebDriver, item);
        }

        public IWidget GetSelectableItem(string regex)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SelectItem(string title)
        {
            var locator = OpenQA.Selenium.By.XPath(
                "descendant::div[contains(@class, 'list-item-title') and text() = '" + title + "']/ancestor::li");
            IWebElement item = contentElement.FindElement(locator);
            WidgetImpl.Tap(Driver.WebDriver, item);
        }
    }
}