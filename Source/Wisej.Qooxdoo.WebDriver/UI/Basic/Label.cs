/*************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java

   Copyright:
     2014 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

*************************************************************************/

using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI.Core;

namespace Wisej.Qooxdoo.WebDriver.UI.Basic
{
    /// <summary>
    /// Label widget
    /// </summary>
    /// <seealso cref="Wisej.Qooxdoo.WebDriver.UI.Core.WidgetImpl" />
    public class Label : WidgetImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="webDriver">The web driver.</param>
        public Label(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        /// <summary>
        /// Gets the string representation of a Label's value </summary>
        /// <returns>The label string.</returns>
        public virtual string Value
        {
            get
            {
                return (string) ExecuteJavascript(
                    "return qx.ui.core.Widget.getWidgetByElement(arguments[0]).getValue().toString()");
            }
        }
    }
}