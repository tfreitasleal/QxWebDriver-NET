using MenuButton = Qooxdoo.WebDriver.UI.Form.MenuButton;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriver.UI.Table.ColumnMenu
{
    public class Button : Form.MenuButton
    {
        public Button(WebElement element, QxWebDriver driver) : base(element, driver)
        {
        }
    }
}