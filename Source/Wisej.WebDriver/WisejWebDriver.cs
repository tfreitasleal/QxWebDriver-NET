using OpenQA.Selenium;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;

namespace Wisej.WebDriver
{
    public class WisejWebDriver : QxWebDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver"/> class.
        /// </summary>
        /// <param name="browser">The browser of the webdriver to wrap.</param>
        public WisejWebDriver(Browser browser)
            : base(browser)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver" /> class.
        /// </summary>
        /// <param name="browser">The browser of the webdriver to wrap.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public WisejWebDriver(Browser browser, int implicitWaitSeconds)
            : base(browser, implicitWaitSeconds)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver"/> class.
        /// </summary>
        /// <param name="browser">The browser of the webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        public WisejWebDriver(Browser browser, IWidgetFactory widgetFactory)
            : base(browser, widgetFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver" /> class.
        /// </summary>
        /// <param name="browser">The browser of the webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public WisejWebDriver(Browser browser, IWidgetFactory widgetFactory, int implicitWaitSeconds)
            : base(browser, widgetFactory, implicitWaitSeconds)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver"/> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        public WisejWebDriver(IWebDriver webdriver)
            : base(webdriver)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver" /> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public WisejWebDriver(IWebDriver webdriver, int implicitWaitSeconds)
            : base(webdriver, implicitWaitSeconds)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver"/> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        public WisejWebDriver(IWebDriver webdriver, IWidgetFactory widgetFactory)
            : base(webdriver, widgetFactory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WisejWebDriver" /> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public WisejWebDriver(IWebDriver webdriver, IWidgetFactory widgetFactory, int implicitWaitSeconds)
            : base(webdriver, widgetFactory, implicitWaitSeconds)
        {
        }
    }
}