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

namespace Qooxdoo.WebDriver.UI.Tree.Core
{
    public class AbstractItem : UI.Core.WidgetImpl
    {
        public AbstractItem(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public virtual bool Open
        {
            get { return ((bool?) GetPropertyValue("open")).Value; }
        }

        public virtual void ClickOpenCloseButton()
        {
            IWidget button = GetChildControl("open");
            if (button != null)
            {
                button.Click();
            }
        }
    }
}