/*************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java

   Copyright:
     2012-2013 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

*************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Wisej.WebDriver.Resources;
using Wisej.WebDriver.UI;

namespace Wisej.WebDriver
{
    /// <summary>
    /// A Decorator that wraps a <seealso cref="OpenQA.Selenium.IWebDriver"/> object,
    /// adding qooxdoo-specific features.
    /// Note that the WebDriver used <strong>must</strong> implement the
    /// <seealso cref="OpenQA.Selenium.IJavaScriptExecutor"/> interface.
    /// </summary>
    public class QxWebDriver : IWebDriver, IJavaScriptExecutor
    {
        private readonly IWebDriver _driver;
        private readonly IWidgetFactory _widgetFactory;

        public IJavaScriptExecutor JsExecutor;
        public JavaScriptRunner JsRunner;

        public QxWebDriver(IWebDriver webdriver)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            _widgetFactory = new DefaultWidgetFactory(this);
        }

        public QxWebDriver(IWebDriver webdriver, IWidgetFactory widgetFactory)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        /// <summary>
        /// A condition that waits until the qooxdoo application in the browser is
        /// ready (<code>qx.core.Init.getApplication()</code> returns anything truthy).
        /// </summary>
        public Func<IWebDriver, bool> QxAppIsReady()
        {
            return driver =>
            {
                object result = null;
                string script = JavaScript.Instance.GetValue("isApplicationReady");
                try
                {
                    result = JsExecutor.ExecuteScript(script);
                }
                catch (WebDriverException e)
                {
                }
                var isReady = result != null && (bool) result;
                return isReady;
            };
        }

        /// <summary>
        /// A condition that waits until the qooxdoo application in the browser is
        /// ready (<code>qx.core.Init.getApplication()</code> returns anything truthy).
        /// </summary>
        /*public ExpectedCondition<Boolean> qxAppIsReady() {
            return new ExpectedCondition<Boolean>() {
                @Override
                public Boolean apply(WebDriver driver) {
                    Object result = null;
                    String script = JavaScript.INSTANCE.getValue("isApplicationReady");
                    try {
                        result = jsExecutor.executeScript(script);
                    } catch(org.openqa.selenium.WebDriverException e) {
                    }
                    Boolean isReady = (Boolean) result;
                    return isReady;
                }

                @Override
                public String toString() {
                    return "qooxdoo application is ready.";
                }
            };
        }*/

        /// <summary>
        /// Returns the original WebDriver instance
        /// </summary>
        public virtual IWebDriver WebDriver
        {
            get { return _driver; }
        }

        /// <summary>
        /// Find the first matching <seealso cref="IWidget"/> using the given method.
        /// </summary>
        /// <param name="by"> The locating mechanism </param>
        /// <param name="timeoutInSeconds"> time to wait for the widget </param>
        /// <returns> The first matching element on the current page </returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        protected internal virtual IWidget FindWidget(OpenQA.Selenium.By by, long timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            IWebElement element;
            try
            {
                element = wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (WebDriverTimeoutException e)
            {
                throw new NoSuchElementException("Unable to find element for locator.", e);
            }
            return GetWidgetForElement(element);
        }

        /// <summary>
        /// Find the first matching <seealso cref="IWidget"/> using the given method. Retry for up to 5 seconds before
        /// throwing.
        /// </summary>
        /// <param name="by"> The locating mechanism </param>
        /// <returns> The first matching element on the current page </returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public virtual IWidget FindWidget(OpenQA.Selenium.By by)
        {
            return FindWidget(by, 5);
        }

        /// <summary>
        /// Find the first matching <seealso cref="IWidget"/> using the given method.
        /// </summary>
        /// <param name="by"> The locating mechanism </param>
        /// <param name="timeoutInSeconds"> time to wait for the widget </param>
        /// <returns> The first matching element on the current page </returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public virtual IWidget WaitForWidget(OpenQA.Selenium.By by, long timeoutInSeconds)
        {
            return FindWidget(by, timeoutInSeconds);
        }

        /// <summary>
        /// Returns an instance of <seealso cref="IWidget"/> or one of its subclasses that
        /// represents the qooxdoo widget containing the given element. </summary>
        /// <param name="element"> A IWebElement representing a DOM element that is part of a
        /// qooxdoo widget </param>
        /// <returns> Widget object </returns>
        public virtual IWidget GetWidgetForElement(IWebElement element)
        {
            return _widgetFactory.GetWidgetForElement(element);
        }

        /// <summary>
        /// Registers a new log appender with the AUT's logging system. Entries can be
        /// accessed using getLogEvents()
        /// </summary>
        public virtual void RegisterLogAppender()
        {
            JsRunner.RunScript("registerLogAppender");
        }

        /// <summary>
        /// Retrieves the AUT's qx log entries. registerLogAppender() *must* be called
        /// before this can be used.
        /// </summary>
        public virtual IList<Log.LogEntry> LogEvents
        {
            get
            {
                IList<Log.LogEntry> logEntries = new List<Log.LogEntry>();
                IList<string> jsonEntries = (IList<string>) JsRunner.RunScript("getAllLogEvents");
                using (IEnumerator<string> itr = jsonEntries.GetEnumerator())
                {
                    while (itr.MoveNext())
                    {
                        string json = itr.Current;
                        Log.LogEntry entry = new Log.LogEntry(json);
                        logEntries.Add(entry);
                    }
                }
                return logEntries;
            }
        }

        /// <summary>
        /// Registers a global error handler using qx.event.GlobalError.setErrorHandler
        /// Caught exceptions can be retrieved using getCaughtErrors
        /// </summary>
        public virtual void RegisterGlobalErrorHandler()
        {
            JsRunner.RunScript("registerGlobalErrorHandler");
        }

        /// <summary>
        /// Retrieves any exceptions caught by qooxdoo's global error handling.
        /// RegisterGlobalErrorHandler *must* be called before this can be used.
        /// </summary>
        public virtual IList<string> CaughtErrors
        {
            get { return (IList<string>) JsRunner.RunScript("getCaughtErrors"); }
        }

        /// <summary>
        /// Uses qooxdoo's localization support to get the currently active locale's translation for a string
        /// </summary>
        public virtual string GetTranslation(string @string)
        {
            string js = "return qx.locale.Manager.getInstance().translate('" + @string + "', []).toString();";
            return (string) JsExecutor.ExecuteScript(js, @string);
        }

        /// <summary>
        /// Uses qooxdoo's localization support to get a specific locale's translation for a string
        /// </summary>
        public virtual string GetTranslation(string @string, string locale)
        {
            string js = "return qx.locale.Manager.getInstance().translate('" + @string + "', [], '" + locale + "').toString();";
            return (string) JsExecutor.ExecuteScript(js, @string);
        }

        public void Close()
        {
            _driver.Close();
        }

        public IWebElement FindElement(OpenQA.Selenium.By arg0)
        {
            return _driver.FindElement(arg0);
        }

        public ReadOnlyCollection<IWebElement> FindElements(OpenQA.Selenium.By arg0)
        {
            return _driver.FindElements(arg0);
        }

        public string Url
        {
            get { return _driver.Url; }
            set
            {
                _driver.Url = value;
                WaitForQxApplication();
                Init();
            }
        }

        /// <summary>
        /// Wait until qx.core.Init.getApplication() returns something truthy.
        /// </summary>
        public virtual void WaitForQxApplication()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(QxAppIsReady());
        }

        /// <summary>
        /// Initializes the testing environment.
        /// </summary>
        public virtual void Init()
        {
            JsRunner = new JavaScriptRunner(JsExecutor);
            // make sure getWidgetByElement is defined so other scripts can use it
            JsRunner.DefineFunction("getWidgetByElement");
        }

        public string PageSource
        {
            get { return _driver.PageSource; }
        }

        public string Title
        {
            get { return _driver.Title; }
        }

        public string CurrentWindowHandle
        {
            get { return _driver.CurrentWindowHandle; }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { return _driver.WindowHandles; }
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public object ExecuteAsyncScript(string arg0, params object[] arg1)
        {
            return JsExecutor.ExecuteAsyncScript(arg0, arg1);
        }

        public object ExecuteScript(string arg0, params object[] arg1)
        {
            return JsExecutor.ExecuteScript(arg0, arg1);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}