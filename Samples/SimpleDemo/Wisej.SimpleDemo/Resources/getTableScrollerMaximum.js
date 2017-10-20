/* ************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java
   http://qooxdoo.org

   Copyright:
     2014 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

************************************************************************ */

qxwebdriver.getTableScrollerMaximum = function() {
  var scroller = qx.ui.core.Widget.getWidgetByElement(arguments[0]);
  var model = scroller.getTable().getTableModel();
  var rowCount = model.getRowCount();
  var rowHeight = scroller.getTable().getRowHeight();
  var scrollSize = rowCount * rowHeight;
  var paneSize = scroller.getPaneClipper().getInnerSize();

  if (paneSize.height < scrollSize) {
    return Math.max(0, scrollSize - paneSize.height);
  } else {
    return 0;
  }
};