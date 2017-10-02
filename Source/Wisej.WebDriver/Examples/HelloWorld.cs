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
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Wisej.WebDriver.UI;

namespace Wisej.WebDriver.Examples
{
    public class HelloWorld
    {
        /// <summary>
        /// A simple demo test for a qx.Desktop skeleton application.
        /// </summary>
        public static void Main(string[] args)
        {
            QxWebDriver driver = new QxWebDriver(new FirefoxDriver());
            // get waits until the qooxdoo application is ready
            driver.Url = "http://localhost/custom/source/index.html";

            // QxWebDriver.FindWidget searches for widgets from the qooxdoo
            // application root downwards. This locator specifies a Button widget
            // that is a direct child of the root node
            By by = By.Qxh("qx.ui.form.Button");
            IWidget button = driver.FindWidget(by);
            button.Click();

            IAlert alert = driver.SwitchTo().Alert();
            Console.WriteLine("qooxdoo says: " + alert.Text);
            alert.Accept();

            driver.Close();
        }
    }
}