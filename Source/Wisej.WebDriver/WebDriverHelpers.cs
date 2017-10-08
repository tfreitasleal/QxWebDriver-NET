using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Wisej.Qooxdoo.WebDriver
{
    /// <summary>
    /// WebDriver Extensions methods
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Find the first matching <seealso cref="IWebElement"/> using the given method.
        /// </summary>
        /// <param name="driver">The <seealso cref="IWebDriver"/> parameter.</param>
        /// <param name="by">The locating mechanism to use.</param>
        /// <param name="timeoutInSeconds"> time to wait for the element </param>
        /// <returns>The first matching element on the current page.</returns>
        /// <exception cref="NoSuchElementException"> If no matching element was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public static IWebElement FindElement(this IWebDriver driver, OpenQA.Selenium.By by, long timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            IWebElement element;
            try
            {
                element = wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new NoSuchElementException("Unable to find element for locator.", e);
            }
            return element;
        }
    }

    public partial class By
    {
        /// <summary>
        /// Convert a widget namespace (including the widget's name) to a <see cref="ByQxh"/> locator string.
        /// </summary>
        /// <param name="namespace">The locator string.</param>
        /// <returns>The converted locator string.</returns>
        public static string Namespace(string @namespace)
        {
            var parts = @namespace.Split('.');

            var result = string.Join(@"/", parts.Select(part => string.Format("[@name={0}]", part)).ToArray());

            return result;
        }
    }
}