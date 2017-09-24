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

namespace Qooxdoo.WebDriver.UI.Form
{
    /// <summary>
    /// Represents a <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.ui.form.ComboBox">ComboBox</a>
    /// widget
    /// </summary>
    public class ComboBox : SelectBox
    {
        public ComboBox(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        protected internal override IWidget Button
        {
            get
            {
                if (button == null)
                {
                    button = GetChildControl("button");
                }
                return button;
            }
        }

        /*public virtual void SendKeys(params CharSequence[] keysToSend)
        {
            GetChildControl("textfield").ContentElement.SendKeys(keysToSend);
        }*/

        public new virtual void Clear()
        {
            GetChildControl("textfield").ContentElement.Clear();
        }
    }
}