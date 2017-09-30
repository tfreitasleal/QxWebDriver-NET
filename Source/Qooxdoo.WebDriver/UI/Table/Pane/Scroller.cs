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
using OpenQA.Selenium;

namespace Qooxdoo.WebDriver.UI.Table.Pane
{
    public class Scroller : Core.Scroll.AbstractScrollArea
    {
        public Scroller(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public override void ScrollTo(string direction, int? position)
        {
            string propertyName = "scroll" + direction.ToUpper();
            JsRunner.RunScript("setPropertyValue", contentElement, propertyName, position);
        }

        public override long? GetMaximum(string direction)
        {
            if (string.ReferenceEquals(direction, "y"))
            {
                return (long?) JsRunner.RunScript("getTableScrollerMaximum", contentElement);
            }

            // TODO
            return new long?(0);
        }

        public override long? GetScrollPosition(string direction)
        {
            string propertyName = "scroll" + direction.ToUpper();
            return (long?) JsRunner.RunScript("getPropertyValue", contentElement, propertyName);
        }

        public override long? GetScrollStep(string direction)
        {
            if (string.ReferenceEquals(direction, "y"))
            {
                return (long?) JsRunner.RunScript("getTableRowHeight", contentElement);
            }

            // TODO
            return new long?(0);
        }

        public virtual IWebElement GetVisibleRow(int? index)
        {
            IWidget pane = GetChildControl("pane");
            IList<IWebElement> rows = pane.ContentElement.FindElements(OpenQA.Selenium.By.XPath("div/div"));
            if (index <= rows.Count)
            {
                return rows[index.Value];
            }
            return null;
        }

        public virtual IWebElement ScrollToRow(int? rowIndex)
        {
            long? firstVisibleRow = FirstVisibleRow;
            long? visibleRowCount = VisibleRowCount;
            long? lastVisibleRow = firstVisibleRow + visibleRowCount - 1;

            if (rowIndex.Value >= firstVisibleRow && rowIndex.Value <= lastVisibleRow)
            {
                int? visibleIndex = (int) (rowIndex.Value - firstVisibleRow);
                return GetVisibleRow(visibleIndex);
            }

            string direction = "y";
            long? singleStep = GetScrollStep(direction);
            long? scrollPosition = GetScrollPosition(direction);
            long? maximum = GetMaximum(direction);

            if (rowIndex.Value > firstVisibleRow && scrollPosition < maximum)
            {
                int to = (int) (scrollPosition + singleStep);
                ScrollTo(direction, to);
                return ScrollToRow(rowIndex);
            }
            else if (rowIndex.Value < lastVisibleRow && scrollPosition > 0)
            {
                int to = (int) (scrollPosition - singleStep);
                ScrollTo(direction, to);
                return ScrollToRow(rowIndex);
            }

            return null;
        }

        public virtual long? FirstVisibleRow
        {
            get { return (long?) JsRunner.RunScript("getFirstVisibleTableRow", contentElement); }
        }

        public virtual long? VisibleRowCount
        {
            get { return (long?) JsRunner.RunScript("getVisibleTableRowCount", contentElement); }
        }
    }
}