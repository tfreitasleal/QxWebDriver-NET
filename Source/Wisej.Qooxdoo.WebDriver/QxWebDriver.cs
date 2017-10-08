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
using Wisej.Qooxdoo.WebDriver.Resources;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriver
{
    /// <summary>
    /// A Decorator that wraps a <see cref="IWebDriver"/> object,  adding qooxdoo-specific features.
    /// Note that the WebDriver used <strong>must</strong> implement the <see cref="IJavaScriptExecutor"/> interface.
    /// </summary>
    public class QxWebDriver : IWebDriver, IJavaScriptExecutor
    {
        private readonly IWebDriver _driver;
        private readonly IWidgetFactory _widgetFactory;
        private TimeSpan? _implictWait;

        /// <summary>
        /// Gets the javascritp executor.
        /// </summary>
        /// <value>
        /// The javascritp executor.
        /// </value>
        public IJavaScriptExecutor JsExecutor { get; }

        /// <summary>
        /// Gets the javascritp runner.
        /// </summary>
        /// <value>
        /// The javascritp runner.
        /// </value>
        public JavaScriptRunner JsRunner { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QxWebDriver"/> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        public QxWebDriver(IWebDriver webdriver)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            SetImplicitWait(4);
            _widgetFactory = new DefaultWidgetFactory(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QxWebDriver" /> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public QxWebDriver(IWebDriver webdriver, int implicitWaitSeconds)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            SetImplicitWait(implicitWaitSeconds);
            _widgetFactory = new DefaultWidgetFactory(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QxWebDriver"/> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        public QxWebDriver(IWebDriver webdriver, IWidgetFactory widgetFactory)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            SetImplicitWait(4);
            _widgetFactory = widgetFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QxWebDriver" /> class.
        /// </summary>
        /// <param name="webdriver">The webdriver to wrap.</param>
        /// <param name="widgetFactory">The widget factory to use.</param>
        /// <param name="implicitWaitSeconds">The implicit wait duration in seconds.</param>
        public QxWebDriver(IWebDriver webdriver, IWidgetFactory widgetFactory, int implicitWaitSeconds)
        {
            _driver = webdriver;
            JsExecutor = (IJavaScriptExecutor) _driver;
            SetImplicitWait(implicitWaitSeconds);
            _widgetFactory = widgetFactory;
        }

        private void SetImplicitWait(int implicitWaitSeconds)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitSeconds);
            try
            {
                var seconds = _driver.Manage().Timeouts().ImplicitWait;
            }
            catch (Exception)
            {
                _implictWait = TimeSpan.FromSeconds(implicitWaitSeconds);
            }
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
                catch (WebDriverException)
                {
                }
                var isReady = result != null && (bool) result;
                return isReady;
            };
        }

        #region Converted from Java

        /*/// <summary>
        /// A condition that waits until the qooxdoo application in the browser is
        /// ready (<code>qx.core.Init.getApplication()</code> returns anything truthy).
        /// </summary>
        public ExpectedCondition<Boolean> qxAppIsReady() {
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

        #endregion

        /// <summary>
        /// Gets the original WebDriver instance
        /// </summary>
        public virtual IWebDriver WebDriver
        {
            get { return _driver; }
        }

        /// <summary>
        /// Find the first matching <see cref="IWidget"/> using the given method.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <param name="timeoutInSeconds"> Time to wait for the widget in seconds</param>
        /// <returns>The first matching widget on the current page.</returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public virtual IWidget FindWidget(OpenQA.Selenium.By by, long timeoutInSeconds)
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
        /// Find the first matching <see cref="IWidget"/> using the given method. Retry for up to <see cref="ITimeouts.ImplicitWait"/> seconds 
        /// before throwing.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>The first matching element on the current page.</returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public virtual IWidget FindWidget(OpenQA.Selenium.By by)
        {
            if (_implictWait.HasValue)
                return FindWidget(by, _implictWait.Value.Seconds);

            return FindWidget(@by, _driver.Manage().Timeouts().ImplicitWait.Seconds);
        }

        /// <summary>
        /// Find the first matching <see cref="IWidget"/> using the given method.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <param name="timeoutInSeconds"> time to wait for the widget </param>
        /// <returns>The first matching element on the current page.</returns>
        /// <exception cref="NoSuchElementException"> If no matching widget was found before the timeout elapsed </exception>
        /// <seealso cref="By"/>
        public virtual IWidget WaitForWidget(OpenQA.Selenium.By by, long timeoutInSeconds)
        {
            return FindWidget(by, timeoutInSeconds);
        }

        /// <summary>
        /// Gets an instance of <see cref="IWidget"/> or one of its subclasses that
        /// represents the qooxdoo widget containing the given element. </summary>
        /// <param name="element"> A <see cref="IWebElement"/> representing a DOM element that is part of a
        /// qooxdoo widget </param>
        /// <returns>Widget object.</returns>
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
            string js = string.Format("return qx.locale.Manager.getInstance().translate('{0}', []).toString();", 
                @string);
            return (string) JsExecutor.ExecuteScript(js, @string);
        }

        /// <summary>
        /// Uses qooxdoo's localization support to get a specific locale's translation for a string
        /// </summary>
        public virtual string GetTranslation(string @string, string locale)
        {
            string js = string.Format("return qx.locale.Manager.getInstance().translate('{0}', [], '{1}').toString();",
                @string, locale);
            return (string) JsExecutor.ExecuteScript(js, @string);
        }

        /// <summary>
        /// Close the current window, quitting the browser if it is the last window currently open.
        /// </summary>
        public void Close()
        {
            _driver.Close();
        }

        /// <summary>
        /// Finds the first <see cref="T:OpenQA.Selenium.IWebElement" /> using the given method.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="T:OpenQA.Selenium.IWebElement" /> on the current context.</returns>
        /// <exception cref="T:OpenQA.Selenium.NoSuchElementException">If no element matches the criteria.</exception>
        public IWebElement FindElement(OpenQA.Selenium.By by)
        {
            return _driver.FindElement(by);
        }

        /// <summary>
        /// Finds all <see cref="T:OpenQA.Selenium.IWebElement">IWebElements</see> within the current context
        /// using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of all <see cref="T:OpenQA.Selenium.IWebElement">WebElements</see>
        /// matching the current criteria, or an empty list if nothing matches.</returns>
        public ReadOnlyCollection<IWebElement> FindElements(OpenQA.Selenium.By by)
        {
            return _driver.FindElements(by);
        }

        /// <summary>
        /// Gets or sets the URL the browser is currently displaying.
        /// </summary>
        /// <remarks>
        /// Setting the <see cref="P:OpenQA.Selenium.IWebDriver.Url" /> property will load a new web page in the current browser window.
        /// This is done using an HTTP GET operation, and the method will block until the
        /// load is complete. This will follow redirects issued either by the server or
        /// as a meta-redirect from within the returned HTML. Should a meta-redirect "rest"
        /// for any duration of time, it is best to wait until this timeout is over, since
        /// should the underlying page change while your test is executing the results of
        /// future calls against this interface will be against the freshly loaded page.
        /// </remarks>
        /// <seealso cref="M:OpenQA.Selenium.INavigation.GoToUrl(System.String)" />
        /// <seealso cref="M:OpenQA.Selenium.INavigation.GoToUrl(System.Uri)" />
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

        /// <summary>
        /// Gets the source of the page last loaded by the browser.
        /// </summary>
        /// <remarks>
        /// If the page has been modified after loading (for example, by JavaScript)
        /// there is no guarantee that the returned text is that of the modified page.
        /// Please consult the documentation of the particular driver being used to
        /// determine whether the returned text reflects the current state of the page
        /// or the text last sent by the web server. The page source returned is a
        /// representation of the underlying DOM: do not expect it to be formatted
        /// or escaped in the same way as the response sent from the web server.
        /// </remarks>
        public string PageSource
        {
            get { return _driver.PageSource; }
        }

        /// <summary>Gets the title of the current browser window.</summary>
        public string Title
        {
            get { return _driver.Title; }
        }

        /// <summary>
        /// Gets the current window handle, which is an opaque handle to this
        /// window that uniquely identifies it within this driver instance.
        /// </summary>
        public string CurrentWindowHandle
        {
            get { return _driver.CurrentWindowHandle; }
        }

        /// <summary>Gets the window handles of open browser windows.</summary>
        public ReadOnlyCollection<string> WindowHandles
        {
            get { return _driver.WindowHandles; }
        }

        /// <summary>Instructs the driver to change its settings.</summary>
        /// <returns>An <see cref="T:OpenQA.Selenium.IOptions" /> object allowing the user to change
        /// the settings of the driver.</returns>
        public IOptions Manage()
        {
            return _driver.Manage();
        }

        /// <summary>
        /// Instructs the driver to navigate the browser to another location.
        /// </summary>
        /// <returns>An <see cref="T:OpenQA.Selenium.INavigation" /> object allowing the user to access
        /// the browser's history and to navigate to a given URL.</returns>
        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        /// <summary>Quits this driver, closing every associated window.</summary>
        public void Quit()
        {
            _driver.Quit();
        }

        /// <summary>
        /// Instructs the driver to send future commands to a different frame or window.
        /// </summary>
        /// <returns>An <see cref="T:OpenQA.Selenium.ITargetLocator" /> object which can be used to select
        /// a frame or window.</returns>
        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        /// <summary>
        /// Executes JavaScript asynchronously in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        public object ExecuteAsyncScript(string script, params object[] args)
        {
            return JsExecutor.ExecuteAsyncScript(script, args);
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        /// <remarks>
        ///     <para>
        /// The <see cref="M:OpenQA.Selenium.IJavaScriptExecutor.ExecuteScript(System.String,System.Object[])" />method executes JavaScript in the context of
        /// the currently selected frame or window. This means that "document" will refer
        /// to the current document. If the script has a return value, then the following
        /// steps will be taken:
        /// </para>
        ///     <para>
        ///         <list type="bullet">
        ///             <item>
        ///                 <description>For an HTML element, this method returns a <see cref="T:OpenQA.Selenium.IWebElement" /></description>
        ///             </item>
        ///             <item>
        ///                 <description>For a number, a <see cref="T:System.Int64" /> is returned</description>
        ///             </item>
        ///             <item>
        ///                 <description>For a boolean, a <see cref="T:System.Boolean" /> is returned</description>
        ///             </item>
        ///             <item>
        ///                 <description>For all other cases a <see cref="T:System.String" /> is returned.</description>
        ///             </item>
        ///             <item>
        ///                 <description>For an array,we check the first element, and attempt to return a
        /// <see cref="T:System.Collections.Generic.List`1" /> of that type, following the rules above. Nested lists are not
        /// supported.</description>
        ///             </item>
        ///             <item>
        ///                 <description>If the value is null or there is no return value,
        /// <see langword="null" /> is returned.</description>
        ///             </item>
        ///         </list>
        ///     </para>
        ///     <para>
        /// Arguments must be a number (which will be converted to a <see cref="T:System.Int64" />),
        /// a <see cref="T:System.Boolean" />, a <see cref="T:System.String" /> or a <see cref="T:OpenQA.Selenium.IWebElement" />.
        /// An exception will be thrown if the arguments do not meet these criteria.
        /// The arguments will be made available to the JavaScript via the "arguments" magic
        /// variable, as if the function were called via "Function.apply"
        /// </para>
        /// </remarks>
        public object ExecuteScript(string script, params object[] args)
        {
            return JsExecutor.ExecuteScript(script, args);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}