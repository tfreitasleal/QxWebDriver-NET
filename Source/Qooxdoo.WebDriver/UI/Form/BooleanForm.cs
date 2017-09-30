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

using OpenQA.Selenium;

namespace Qooxdoo.WebDriver.UI.Form
{
    public class BooleanForm : Core.WidgetImpl
    {
        public BooleanForm(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public new virtual bool Selected
        {
            get { return ((bool?) GetPropertyValue("value")).Value; }
        }
    }
}