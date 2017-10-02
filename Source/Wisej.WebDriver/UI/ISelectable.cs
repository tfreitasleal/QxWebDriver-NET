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

namespace Wisej.WebDriver.UI
{
    /// <summary>
    /// Represents a widget that allows the user to select one or more out of
    /// several items that are displayed as widgets. Only works with qx.Desktop widgets,
    /// for qx.Mobile please use Wisej.WebDriver.UI.Mobile.ISelectable instead.
    /// </summary>
    public interface ISelectable : IWidget
    {
        /// <summary>
        /// Finds a selectable child widget by index and returns it
        /// </summary>
        IWidget GetSelectableItem(int? index);

        /// <summary>
        /// Finds a selectable child widget by index and selects it
        /// </summary>
        void SelectItem(int? index);

        /// <summary>
        /// Finds the first selectable child widget with a label matching the regular
        /// expression and returns it
        /// </summary>
        IWidget GetSelectableItem(string regex);

        /// <summary>
        /// Finds the first selectable child widget with a label matching the regular
        /// expression and selects it
        /// </summary>
        void SelectItem(string regex);
    }
}