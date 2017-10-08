using OpenQA.Selenium;

namespace Wisej.Qooxdoo.WebDriver.UI.Table.ColumnMenu
{
    /// <summary>
    /// Button widget
    /// </summary>
    /// <seealso cref="Wisej.Qooxdoo.WebDriver.UI.Form.MenuButton" />
    public class Button : Form.MenuButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="driver">The driver.</param>
        public Button(IWebElement element, QxWebDriver driver) : base(element, driver)
        {
        }
    }
}