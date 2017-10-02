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
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Wisej.WebDriver.Resources;

namespace Wisej.WebDriver
{
    public class By : OpenQA.Selenium.By
    {
        public new virtual IWebElement FindElement(ISearchContext context)
        {
            return null;
        }

        public new virtual IList<IWebElement> FindElements(ISearchContext context)
        {
            return null;
        }

        /// <summary>
        /// Searches for elements by traversing the qooxdoo application's widget
        /// hierarchy. See the <a href="TODO">Qxh locator manual page</a> for details.
        ///
        /// This strategy will ignore any widgets that are not currently visible, as
        /// determined by checking the qooxdoo property <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.Core.Widget~isSeeable!method_public">seeable</a>.
        /// </summary>
        /// <param name="locator"> Locator specification </param>
        /// <returns> By.ByQxh </returns>
        public static By Qxh(string locator)
        {
            if (ReferenceEquals(locator, null))
            {
                throw new ArgumentException("Can't find elements without a locator string.");
            }

            return new ByQxh(locator, true);
        }

        /// <summary>
        /// Searches for elements by traversing the qooxdoo application's widget
        /// hierarchy. See the <a href="TODO">Qxh locator manual page</a> for details.
        /// </summary>
        /// <param name="locator"> Locator specification </param>
        /// <param name="onlySeeable"> <code>false</code> if invisible widgets should be
        /// traversed. Note that this can considerably increase execution time. </param>
        /// <returns> configured ByQxh instance </returns>
        public static By Qxh(string locator, bool? onlySeeable)
        {
            if (ReferenceEquals(locator, null))
            {
                throw new ArgumentException("Can't find elements without a locator string.");
            }
            return new ByQxh(locator, onlySeeable);
        }

        /// <summary>
        /// Mechanisms used to locate elements within a qooxdoo Desktop application.
        ///
        /// </summary>
        public class ByQxh : By
        {
            internal readonly string Locator;
            internal bool? OnlySeeable;

            public ByQxh(string locator, bool? onlySeeable)
            {
                Locator = locator;
                OnlySeeable = onlySeeable;
            }

            public override IList<IWebElement> FindElements(ISearchContext context)
            {
                //TODO: findByQxh only returns the first match
                throw new Exception("ByQxh.FindElements is not yet implemented.");
            }

            /// <summary>
            /// Searches for elements by traversing the qooxdoo application's widget
            /// hierarchy using the current ISearchContext as the root node.
            /// See the <a href="TODO">Qxh locator manual page</a> for details.
            /// </summary>
            public override IWebElement FindElement(ISearchContext context)
            {
                IJavaScriptExecutor jsExecutor;

                RemoteWebElement contextElement = null;

                if (context is RemoteWebElement)
                {
                    contextElement = (RemoteWebElement) context;
                    jsExecutor = (IJavaScriptExecutor) contextElement.WrappedDriver;
                }
                else
                {
                    jsExecutor = (IJavaScriptExecutor) context;
                }

                string script = JavaScript.Instance.GetValue("qxh");

                try
                {
                    object result;
                    if (contextElement == null)
                    {
                        // OperaDriver.ExecuteScript won't accept null as an argument
                        result = jsExecutor.ExecuteScript(script, Locator, OnlySeeable);
                    }
                    else
                    {
                        try
                        {
                            result = jsExecutor.ExecuteScript(script, Locator, OnlySeeable,
                                (IWebElement) contextElement);
                        }
                        //todo: catch (com.opera.Core.systems.scope.exceptions.ScopeException)
                        catch (Exception)
                        {
                            // OperaDriver will sometimes throw a ScopeException if ExecuteScript is called
                            // with an OperaWebElement as argument
                            return null;
                        }
                    }
                    return (IWebElement) result;
                }
                catch (WebDriverException e)
                {
                    string msg = e.Message;
                    if (msg.Contains("Error resolving Qxh path") || msg.Contains("JavaScript error"))
                    {
                        // IEDriver doesn't include the original JS exception's message :(
                        return null;
                    }

                    if (msg.Contains("Illegal path step"))
                    {
                        string reason = "Invalid Qxh selector " + Locator;
                        throw new InvalidSelectorException(reason, e);
                    }
                    else
                    {
                        string reason = "Error while processing selector " + Locator;
                        throw new OpenQA.Selenium.WebDriverException(reason, e);
                    }
                }
            }

            public override string ToString()
            {
                return "By.Qxh: " + Locator;
            }
        }
    }
}