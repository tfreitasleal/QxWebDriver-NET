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
using Qooxdoo.WebDriver.UI.Core.Scroll;

namespace Qooxdoo.WebDriver.UI.Menu
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.menu.Menu">Menu</a>
    /// widget
    /// </summary>
    public class Menu : Core.WidgetImpl, ISelectable, IScrollable
    {
        //TODO: Nested menus

        public Menu(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public virtual void SelectItem(int? index)
        {
            GetSelectableItem(index).Click();
        }

        public void SelectItem(string regex)
        {
            GetSelectableItem(regex).Click();
        }

        public IWidget GetSelectableItem(int? index)
        {
            bool? hasSlideBar = HasChildControl("slidebar");
            if (hasSlideBar.Value)
            {
                Console.Error.WriteLine("Menu item selection by index is currently only supported for non-scrolling menus!");
                return null;
            }

            IList<IWidget> children = Children;
            return children[index.Value];
        }

        public IWidget GetSelectableItem(string label)
        {
            By itemLocator = By.Qxh("*/[@label=" + label + "]");
            bool? hasSlideBar = HasChildControl("slidebar");
            if (hasSlideBar.Value)
            {
                ScrollTo("y", 0);
                return ScrollToChild("y", itemLocator);
            }

            return FindWidget(itemLocator);
        }

        public virtual ScrollPane ScrollPane
        {
            get
            {
                IWidget slideBar = GetChildControl("slidebar");
                return (ScrollPane) slideBar.GetChildControl("scrollpane");
            }
        }

        public void ScrollTo(string direction, int? position)
        {
            ScrollPane scrollPane = ScrollPane;
            scrollPane.ScrollTo(direction, position);
        }

        public IWidget ScrollToChild(string direction, OpenQA.Selenium.By locator)
        {
            ScrollPane scrollPane = ScrollPane;
            return scrollPane.ScrollToChild(direction, locator);
        }

        public long? GetMaximum(string direction)
        {
            ScrollPane scrollPane = ScrollPane;
            return scrollPane.GetMaximum(direction);
        }

        public long? GetScrollPosition(string direction)
        {
            ScrollPane scrollPane = ScrollPane;
            return scrollPane.GetScrollPosition(direction);
        }
    }
}