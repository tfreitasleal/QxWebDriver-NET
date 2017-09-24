using System.Collections.Generic;
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

namespace Qooxdoo.WebDriver.UI
{
    /// <summary>
    /// Represents a qx.Desktop widget. <seealso cref="OpenQA.Selenium.IWebElement"/>
    /// methods are forwarded to the widget's content element. Click() and SendKeys()
    /// will generally workFor simple widgets that contain only one button and/or
    /// text field.
    ///
    /// For more advanced interactions on composite widgets such as qx.ui.formComboBox
    /// or qx.ui.Tree.Tree, see the other interfaces in this namespace.
    /// </summary>
    /// <seealso cref="IScrollable"/>
    /// <seealso cref="ISelectable"/>
    public interface IWidget : WebElement
    {
        /// <summary>
        /// This widget's qooxdoo object registry ID
        /// </summary>
        string QxHash { get; }

        /// <summary>
        /// This widget's qooxdoo class name
        /// </summary>
        string Classname { get; }

        /// <summary>
        /// The WebElement representing this widget's content element
        /// </summary>
        WebElement ContentElement { get; }

        /// <summary>
        /// Returns a <seealso cref="IWidget"/> representing a child control of this widget.
        /// </summary>
        IWidget GetChildControl(string childControlId);

        /// <summary>
        /// Repeatedly checks if the child control with the given id is visible.
        /// Returns the child control if successful.
        /// </summary>
        /// <param name="childControlId">The child control identifier.</param>
        /// <param name="timeoutInSeconds">in seconds</param>
        /// <returns>
        /// The child control widget
        /// </returns>
        IWidget WaitForChildControl(string childControlId, int? timeoutInSeconds);

        /// <summary>
        /// Returns a <seealso cref="IWidget"/> representing the layout parent.
        /// </summary>
        IWidget LayoutParent { get; }

        /// <summary>
        /// Calls IJavaScriptExecutor.ExecuteScript. The first argument is the widget's
        /// content element.
        /// </summary>
        /// <seealso cref="OpenQA.Selenium.IJavaScriptExecutor"/>
        object ExecuteJavascript(string script);

        /// <summary>
        /// Returns the value of a qooxdoo property on this widget, serialized in JSON
        /// format.
        /// <strong>NOTE:</strong> Never use this for property values that are instances
        /// of qx.Core.Object. Circular references in qooxoo's OO system will lead to
        /// JavaScript errors.
        /// </summary>
        string GetPropertyValueAsJson(string propertyName);

        /// <summary>
        /// Returns the value of a qooxdoo property on this widget. See the <seealso cref="OpenQA.Selenium.IJavaScriptExecutor"/>
        /// documentation for details on how JavaScript types are represented.
        /// <strong>NOTE:</strong> Never use this for property values that are instances
        /// of qx.Core.Object. Circular references in qooxoo's OO system will lead to
        /// JavaScript errors.
        /// </summary>
        object GetPropertyValue(string propertyName);

        /// <summary>
        /// Returns a List of <seealso cref="IWidget"/>s representing the value of a widget list property,
        /// e.g. <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.Core.MMultiSelectionHandling~getSelection">MMultiSelectionHandling.getSelection</a>
        /// </summary>
        IList<IWidget> GetWidgetListFromProperty(string propertyName);

        /// <summary>
        /// Returns a <seealso cref="IWidget"/> representing the value of a widget property,
        /// e.g. <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.form.MenuButton~menu!property">the
        /// MenuButton's menu property</a>
        /// </summary>
        IWidget GetWidgetFromProperty(string propertyName);

        /// <summary>
        /// Returns a list of <seealso cref="IWidget"/> objects representing this widget's children
        /// as defined using <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.Core.MChildrenHandling~add!method_public">parent.add(child);</a> in the application code.
        /// </summary>
        IList<IWidget> Children { get; }

        /// <summary>
        /// Finds a widget relative to the current one by traversing the qooxdoo
        /// widget hierarchy.
        /// </summary>
        IWidget FindWidget(OpenQA.Selenium.By by);

        /// <summary>
        /// Drag and drop this widget onto another widget
        /// </summary>
        void DragToWidget(IWidget target);

        /// <summary>
        /// Drag over this widget to another widget
        ///
        /// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void dragOver(Widget target) throws InterruptedException;
        void DragOver(IWidget target);

        /// <summary>
        /// Drag and drop this widget onto another widget
        /// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void drop(Widget target) throws InterruptedException;
        void Drop(IWidget target);
    }
}