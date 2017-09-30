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
using Qooxdoo.WebDriver.UI.Core;

namespace Qooxdoo.WebDriver.UI.Form
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.form.SelectBox">SelectBox</a>
    /// widget
    /// </summary>
    public class SelectBox : WidgetImpl, ISelectable
    {
        protected internal IWidget button = null;
        protected internal ISelectable list = null;

        public SelectBox(IWebElement element, QxWebDriver driver) : base(element, driver)
        {
        }

        public virtual IWidget GetSelectableItem(int? index)
        {
            return List.GetSelectableItem(index);
        }

        public virtual void SelectItem(int? index)
        {
            Button.Click();
            GetSelectableItem(index).Click();
        }

        public virtual IWidget GetSelectableItem(string regex)
        {
            return List.GetSelectableItem(regex);
        }

        public virtual void SelectItem(string regex)
        {
            Button.Click();
            GetSelectableItem(regex).Click();
        }

        protected internal virtual IWidget Button
        {
            get
            {
                if (button == null)
                {
                    button = Driver.GetWidgetForElement(contentElement);
                }
                return button;
            }
        }

        protected internal virtual ISelectable List
        {
            get
            {
                if (list == null)
                {
                    list = (ISelectable) WaitForChildControl("list", 3);
                }
                return list;
            }
        }
    }
}