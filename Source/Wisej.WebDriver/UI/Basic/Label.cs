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
using Wisej.WebDriver.UI.Core;

namespace Wisej.WebDriver.UI.Basic
{
    public class Label : WidgetImpl
    {
        public Label(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        /// <summary>
        /// Returns the string representation of a Label's value </summary>
        /// <returns> label string </returns>
        public virtual string Value
        {
            get
            {
                return (string) ExecuteJavascript("return qx.ui.core.Widget.getWidgetByElement(arguments[0]).getValue().toString()");
            }
        }
    }
}