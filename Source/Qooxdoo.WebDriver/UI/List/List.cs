using System;
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

namespace Qooxdoo.WebDriver.UI.List
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.List.List">List</a>
    /// widget
    /// </summary>
    public class List : Form.List, IScrollable, ISelectable
    {

        public List(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public new virtual IWidget GetSelectableItem(int? index)
        {
            throw new Exception("GetSelectableItem(Integer index) is not implemented for qx.ui.List.List, use GetSelectableItem(String label) instead.");
        }

    }

}