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

using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Wisej.WebDriver.UI.Core;

namespace Wisej.WebDriver.UI.TabView
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.tabview.TabView">TabView</a>
    /// widget
    /// </summary>
    public class TabView : WidgetImpl, ISelectable
    {
        public TabView(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public IWidget GetSelectableItem(int? index)
        {
            IWidget bar = GetChildControl("bar");
            IList<IWidget> buttons = bar.Children;
            return buttons[index.Value];
        }

        public void SelectItem(int? index)
        {
            GetSelectableItem(index).Click();
        }

        public IWidget GetSelectableItem(string regex)
        {
            IWidget bar = GetChildControl("bar");
            IList<IWidget> buttons = bar.Children;
            IWidget button = null;

            using (IEnumerator<IWidget> iter = buttons.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    button = iter.Current;
                    string buttonLabel = (string) button.GetPropertyValue("label");
                    if (Regex.IsMatch(buttonLabel, regex))
                    {
                        break;
                    }
                }
            }

            return button;
        }

        public void SelectItem(string regex)
        {
            GetSelectableItem(regex).Click();
        }
    }
}