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
using Qooxdoo.WebDriver.UI.Core.Scroll;

namespace Qooxdoo.WebDriver.UI.Form
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.form.List">List</a>
    /// widget
    /// </summary>
    public class List : AbstractScrollArea, ISelectable
    {
        public List(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public virtual IWidget GetSelectableItem(int? index)
        {
            object result = JsRunner.RunScript("getItemFromSelectables", contentElement, index);
            IWebElement element = (IWebElement) result;
            return Driver.GetWidgetForElement(element);
        }

        public virtual void SelectItem(int? index)
        {
            //TODO: scroll
            GetSelectableItem(index).Click();
        }

        public virtual IWidget GetSelectableItem(string label)
        {
            ScrollTo("y", 0);
            By itemLocator = By.Qxh("*/[@label=" + label + "]");
            return ScrollToChild("y", itemLocator);
        }

        public virtual void SelectItem(string regex)
        {
            IWidget item = GetSelectableItem(regex);
            item.Click();
        }
    }
}