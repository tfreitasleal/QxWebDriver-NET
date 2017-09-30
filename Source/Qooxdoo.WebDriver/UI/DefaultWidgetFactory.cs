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
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;

namespace Qooxdoo.WebDriver.UI
{
    public class DefaultWidgetFactory : IWidgetFactory
    {
        public DefaultWidgetFactory(QxWebDriver qxWebDriver)
        {
            Driver = qxWebDriver;
            _packageName = GetType().Assembly.FullName;
        }

        protected internal QxWebDriver Driver;
        private readonly string _packageName;
        private readonly Dictionary<IWebElement, IWidget> _elements = new Dictionary<IWebElement, IWidget>();

        /// <summary>
        /// Returns a list of qooxdoo interfaces implemented by the widget containing
        /// the given element.
        /// </summary>
        public virtual IList<string> GetWidgetInterfaces(IWebElement element)
        {
            return (IList<string>) Driver.JsRunner.RunScript("getInterfaces", element);
        }

        /// <summary>
        /// Returns the inheritance hierarchy of the widget containing the given
        /// element.
        /// </summary>
        public virtual IList<string> GetWidgetInheritance(IWebElement element)
        {
            return (IList<string>) Driver.JsRunner.RunScript("getInheritance", element);
        }

        /// <summary>
        /// Returns an instance of <seealso cref="IWidget"/> or one of its subclasses that
        /// represents the qooxdoo widget containing the given element. </summary>
        /// <param name="element"> A IWebElement representing a DOM element that is part of a
        /// qooxdoo widget </param>
        /// <returns> Widget object </returns>
        public virtual IWidget GetWidgetForElement(IWebElement element)
        {
            if (_elements.ContainsKey(element))
            {
                return _elements[element];
            }

            IList<string> interfaces = GetWidgetInterfaces(element);
            IList<string> classes = GetWidgetInheritance(element);

            ((List<string>) classes).AddRange(interfaces);

            if (classes.Remove("qx.ui.core.Widget"))
            {
                classes.Add("qx.ui.core.WidgetImpl");
            }

            if (classes.Remove("qx.ui.mobile.core.Widget"))
            {
                classes.Add("qx.ui.mobile.core.WidgetImpl");
            }

            IWidget widget = null;

            using (IEnumerator<string> classIter = classes.GetEnumerator())
            {
                while (classIter.MoveNext())
                {
                    string className = classIter.Current;
                    string widgetClassName = GetWidgetClassName(className);
                    if (!ReferenceEquals(widgetClassName, null))
                    {
                        ConstructorInfo constr = GetConstructorByClassName(widgetClassName);
                        if (constr != null)
                        {
                            try
                            {
                                //widget = (IWidget) constr.newInstance(element, driver);
                                widget = (IWidget) constr.Invoke(element, new object[] {Driver});
                                _elements[element] = widget;
                                //return widget;
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine("Could not instantiate '" + widgetClassName + "': " + e.Message);
                                Console.WriteLine(e.ToString());
                                Console.Write(e.StackTrace);
                            }
                        }
                    }
                }
            }

            return widget;
        }

        private string GetWidgetClassName(string qxClassName)
        {
            if (qxClassName.StartsWith("qx.ui.", StringComparison.Ordinal))
            {
                var convertedClassName = ConvertClassName(qxClassName.Substring(5));
                return _packageName + convertedClassName;
            }

            return null;
        }

        private string ConvertClassName(string className)
        {
            var classNameParts = className.Split('.');
            for (var index = 0; index < classNameParts.Length; index++)
            {
                classNameParts[index] =
                    char.ToUpper(classNameParts[index][0]) + classNameParts[index].Substring(1, classNameParts[index].Length - 1);
            }
            className = string.Join(".", classNameParts);

            return className;
        }

        private ConstructorInfo GetConstructorByClassName(string widgetClassName)
        {
            try
            {
                Type widgetClass = Type.GetType(widgetClassName);
                if (widgetClass != null)
                {
                    var cnst = widgetClass.GetConstructors();
                    if (cnst.Length > 0)
                    {
                        return cnst[0];
                    }
                }
            }
            //catch (ClassNotFoundException)
            catch (Exception)
            {
                //System.out.println("No class for " + widgetClassName);
            }
            return null;
        }
    }
}