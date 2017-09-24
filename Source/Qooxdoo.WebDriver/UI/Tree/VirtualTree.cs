﻿using System.Collections;
using List = Qooxdoo.WebDriver.UI.List.List;
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

namespace Qooxdoo.WebDriver.UI.Tree
{
    public class VirtualTree : List.List
    {
        public VirtualTree(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }
    }
}