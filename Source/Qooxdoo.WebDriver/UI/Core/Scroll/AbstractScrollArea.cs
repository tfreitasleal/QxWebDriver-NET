using System;
using System.Drawing;
using OpenQA.Selenium;
using WebElement = OpenQA.Selenium.IWebElement;

/* ************************************************************************

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

************************************************************************ */

namespace Qooxdoo.WebDriver.UI.Core.Scroll
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.Core.Scroll.AbstractScrollArea">ScrollArea</a>
    /// widget
    /// </summary>
    public class AbstractScrollArea : WidgetImpl, IScrollable
    {
        public AbstractScrollArea(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        protected internal virtual IWidget GetScrollbar(string direction)
        {
            string childControlId = "scrollbar-" + direction;
            try
            {
                IWidget scrollBar = WaitForChildControl(childControlId, 2);
                return scrollBar;
            }
            catch (TimeoutException)
            {
                return null;
            }
        }

        public virtual void ScrollTo(string direction, int? position)
        {
            IWidget scrollBar = GetScrollbar(direction);
            if (scrollBar == null)
            {
                return;
            }
            JsRunner.RunScript("scrollTo", scrollBar.ContentElement, position);
        }

        public virtual long? GetScrollPosition(string direction)
        {
            IWidget scrollBar = GetScrollbar(direction);
            if (scrollBar == null)
            {
                return new long?(0);
            }
            return GetScrollPosition(scrollBar);
        }

        protected internal virtual long? GetScrollPosition(IWidget scrollBar)
        {
            try
            {
                string result = scrollBar.GetPropertyValueAsJson("position");
                return long.Parse(result);
            }
            //catch (com.opera.Core.systems.scope.exceptions.ScopeException)
            catch (Exception ex)
            {
                return null;
            }
        }

        protected internal virtual long? GetScrollStep(IWidget scrollBar)
        {
            string result = scrollBar.GetPropertyValueAsJson("singleStep");
            return long.Parse(result);
        }

        public virtual long? GetScrollStep(string direction)
        {
            IWidget scrollBar = GetScrollbar(direction);
            if (scrollBar == null)
            {
                return new long?(0);
            }
            return GetScrollStep(scrollBar);
        }

        public virtual long? GetMaximum(string direction)
        {
            IWidget scrollBar = GetScrollbar(direction);
            if (scrollBar == null)
            {
                return new long?(0);
            }
            return GetMaximum(scrollBar);
        }

        protected internal virtual long? GetMaximum(IWidget scrollBar)
        {
            string result = scrollBar.GetPropertyValueAsJson("maximum");
            return long.Parse(result);
        }

        public virtual IWidget ScrollToChild(string direction, OpenQA.Selenium.By locator)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
            WebElement target = null;
            try
            {
                target = contentElement.FindElement(locator);
            }
            catch (NoSuchElementException)
            {
            }
            if (target != null && IsChildInView(target).GetValueOrDefault(false))
            {
                return Driver.GetWidgetForElement(target);
            }

            long? singleStep = GetScrollStep(direction);
            long? maximum = GetMaximum(direction);
            long? scrollPosition = GetScrollPosition(direction);

            while (scrollPosition < maximum)
            {
                // Virtual list items are created on demand, so query the DOM again
                try
                {
                    target = contentElement.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                }
                int to;
                if (target != null && IsChildInView(target).GetValueOrDefault(false))
                {
                    // Scroll one more stop after the target item is visible.
                    // Without this, clicking the target in IE9 and Firefox doesn't
                    // work sometimes.
                    to = (int) (scrollPosition + singleStep);
                    ScrollTo(direction, to);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                    return Driver.GetWidgetForElement(target);
                }

                to = (int) (scrollPosition + singleStep);
                ScrollTo(direction, to);
                scrollPosition = GetScrollPosition(direction);
            }

            //TODO: Find out the original timeout and re-Apply it
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            return null;
        }

        public virtual bool? IsChildInView(WebElement child)
        {
            Point paneLocation = contentElement.Location;
            int paneTop = paneLocation.Y;
            int paneLeft = paneLocation.X;
            Size paneSize = contentElement.Size;
            int paneHeight = paneSize.Height;
            int paneBottom = paneTop + paneHeight;
            int paneWidth = paneSize.Width;
            int paneRight = paneLeft + paneWidth;

            Point childLocation = child.Location;
            int childTop = childLocation.Y;
            int childLeft = childLocation.X;

            if (childTop >= paneTop && childTop < paneBottom && childLeft >= paneLeft && childLeft < paneRight)
            {
                return true;
            }

            return false;
        }
    }
}