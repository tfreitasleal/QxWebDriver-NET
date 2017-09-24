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

    public class ScrollPane : AbstractScrollArea
    {

        public ScrollPane(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public override void ScrollTo(string direction, int? position)
        {
            JsRunner.RunScript("scrollTo", contentElement, position, direction);
        }

        public override long? GetMaximum(string direction)
        {
            return (long?) JsRunner.RunScript("getScrollMax", contentElement, direction);
        }

        public override long? GetScrollPosition(string direction)
        {
            string propertyName = "scroll" + direction.ToUpper();
            return (long?) GetPropertyValue(propertyName);
        }

        public override long? GetScrollStep(string direction)
        {
            return (long) 10;
        }
    }

}