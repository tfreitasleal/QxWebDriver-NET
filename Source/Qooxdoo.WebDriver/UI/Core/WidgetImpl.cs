using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Qooxdoo.WebDriver.Resources;
using Coordinates = OpenQA.Selenium.Interactions.Internal.ICoordinates;
using HasInputDevices = OpenQA.Selenium.IHasInputDevices;
using JavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using Locatable = OpenQA.Selenium.ILocatable;
using Mouse = OpenQA.Selenium.IMouse;
//using OutputType = OpenQA.Selenium.OutputType;
using WebDriver = OpenQA.Selenium.IWebDriver;
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

namespace Qooxdoo.WebDriver.UI.Core
{
    public class WidgetImpl : IWidget
    {
        public WidgetImpl(WebElement element, QxWebDriver webDriver)
        {
            Driver = webDriver;

            JsExecutor = Driver.JsExecutor;
            JsRunner = Driver.JsRunner;

            contentElement = (WebElement) JsRunner.RunScript("getContentElement", element);
        }

        private string _qxHash = null;

        private string _classname = null;

        protected internal WebElement contentElement;

        protected internal QxWebDriver Driver;

        protected internal JavaScriptExecutor JsExecutor;

        protected internal JavaScriptRunner JsRunner;

        public virtual string QxHash
        {
            get
            {
                if (ReferenceEquals(_qxHash, null))
                {
                    _qxHash = (string) JsRunner.RunScript("getObjectHash", contentElement);
                }
                return _qxHash;
            }
        }

        public virtual string Classname
        {
            get
            {
                if (ReferenceEquals(_classname, null))
                {
                    _classname = (string) JsRunner.RunScript("getClassname", contentElement);
                }
                return _classname;
            }
        }

        public virtual WebElement ContentElement
        {
            get { return contentElement; }
        }

        public virtual void DragToWidget(IWidget target)
        {
            Actions actions = new Actions(Driver.WebDriver);
            actions.DragAndDrop(ContentElement, target.ContentElement);
            actions.Perform();
        }

        public virtual void DragOver(IWidget target)
        {
            Mouse mouse = ((HasInputDevices) Driver.WebDriver).Mouse;
            Locatable root = (Locatable) Driver.FindElement(By.TagName("body"));
            //cast WebElement to Locatable
            Locatable sourceL = (Locatable) contentElement;
            Locatable targetL = (Locatable) target.ContentElement;

            Coordinates coord = root.Coordinates;
            mouse.MouseDown(sourceL.Coordinates);

            //get source position (center,center)
            int sourceX = sourceL.Coordinates.LocationInDom.X + contentElement.Size.Width / 2;
            int sourceY = sourceL.Coordinates.LocationInDom.Y + contentElement.Size.Height / 2;

            // get target position (center, center)
            int targetX = targetL.Coordinates.LocationInDom.X + target.ContentElement.Size.Width / 2;
            int targetY = targetL.Coordinates.LocationInDom.Y + target.ContentElement.Size.Height / 2;

            //compute deltas between source and target position
            //delta must be positive, however
            //also we have to define the direction
            int directionX = 1; //move direction is right

            int directionY = 1; //move direction is bottom

            var deltaX = targetX - sourceX;
            if (deltaX < 0)
            {
                deltaX *= -1;
                directionX = -1; // move direction is left
            }

            var deltaY = targetY - sourceY;
            if (deltaY < 0)
            {
                deltaY *= -1;
                directionY = -1; // move direction is top
            }

            //define base delta, which must be the higher one

            int baseDelta = deltaX;
            if (deltaY > deltaX)
            {
                baseDelta = deltaY;
            }

            // iterate base delta, set mouse cursor in relation to delta x & delta y
            int x = 0;
            int y = 0;

            for (int i = 1; i <= baseDelta; i += 4)
            {
                if (i > baseDelta)
                {
                    i = baseDelta;
                }
                x = sourceX + deltaX * i / baseDelta * directionX;
                y = sourceY + deltaY * i / baseDelta * directionY;

                mouse.MouseMove(coord, x, y);
                //System.out.println(x +", "+ y);
                Thread.Sleep(1);
            }
            // source has the same coordinates as target
            if (sourceX == targetX && sourceY == targetY)
            {
                mouse.MouseMove(targetL.Coordinates, x++, y);
                Thread.Sleep(20);
            }
        }

        public virtual void Drop(IWidget target)
        {
            Mouse mouse = ((HasInputDevices) Driver.WebDriver).Mouse;
            DragOver(target);

            Locatable targetL = (Locatable) target.ContentElement;
            mouse.MouseUp(targetL.Coordinates);
        }

        public virtual void Click()
        {
            Actions actions = new Actions(Driver.WebDriver);
            actions.MoveToElement(ContentElement);
            actions.Click();
            actions.Perform();
        }

        public virtual void SendKeys(string keysToSend)
        {
            contentElement.SendKeys(keysToSend);
        }

        public IWidget WaitForChildControl(String childControlId, int? timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds.GetValueOrDefault(5)));
            //return wait.Until(ChildControlIsVisible(childControlId));

            // todo: fix it properly

            return null;
        }

        /*/// <summary>
        /// A condition that waits until a child control has been rendered, then returns it.
        /// </summary>
//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public OpenQA.Selenium.Support.UI.ExpectedCondition<Qooxdoo.WebDriver.UI.IWidget> ChildControlIsVisible(final String childControlId)
        public virtual ExpectedCondition<IWidget> ChildControlIsVisible(string childControlId)
        {
            return new ExpectedConditionAnonymousInnerClass(this, childControlId);
        }

        private class ExpectedConditionAnonymousInnerClass : ExpectedCondition<IWidget>
        {
            private readonly WidgetImpl _outerInstance;

            private string _childControlId;

            public ExpectedConditionAnonymousInnerClass(WidgetImpl outerInstance, string childControlId)
            {
                this._outerInstance = outerInstance;
                this._childControlId = childControlId;
            }

            public IWidget Apply(WebDriver webDriver)
            {
                IWidget childControl = _outerInstance.GetChildControl(_childControlId);
                if (childControl != null && childControl.Displayed)
                {
                    return childControl;
                }
                return null;
            }

            public string ToString()
            {
                return "Child control is visible.";
            }
        }*/

        /// <summary>
        /// A condition that waits until a child control has been rendered, then returns it.
        /// </summary>
        /*public ExpectedCondition<IWidget> ChildControlIsVisible(string childControlId)
        {
            return new ExpectedCondition<IWidget>() {
                @Override

                public IWidget Apply(WebDriver webDriver)
                {
                IWidget childControl = GetChildControl(childControlId);
                if (childControl != null && childControl.IsDisplayed())
                {
                return childControl;
            }
            return null;
            }

            @Override
            public String toString()
            {
                return "Child control is visible.";
            }
            };
        }*/

        public virtual IWidget GetChildControl(string childControlId)
        {
            object result = JsRunner.RunScript("getChildControl", contentElement, childControlId);
            WebElement element = (WebElement) result;
            if (element == null)
            {
                return null;
            }
            return Driver.GetWidgetForElement(element);
        }

        public virtual bool? HasChildControl(string childControlId)
        {
            object result = JsRunner.RunScript("hasChildControl", contentElement, childControlId);
            return (bool?) result;
        }

        public virtual IWidget LayoutParent
        {
            get
            {
                object result = JsRunner.RunScript("getLayoutParent", contentElement);
                WebElement element = (WebElement) result;
                if (element == null)
                {
                    return null;
                }
                return Driver.GetWidgetForElement(element);
            }
        }

        public virtual object ExecuteJavascript(string script)
        {
            return JsExecutor.ExecuteScript(script, contentElement);
        }

        public virtual string GetPropertyValueAsJson(string propertyName)
        {
            object result = JsRunner.RunScript("getPropertyValueAsJson", contentElement, propertyName);
            return (string) result;
        }

        public virtual object GetPropertyValue(string propertyName)
        {
            object result = JsRunner.RunScript("getPropertyValue", contentElement, propertyName);
            return result;
        }

        private WebElement GetElementFromProperty(string propertyName)
        {
            object result = JsRunner.RunScript("getElementFromProperty", contentElement, propertyName);
            return (WebElement) result;
        }

        /// <summary>
        /// Returns a <seealso cref="WidgetImpl"/> representing the value of a widget property,
        /// e.g. <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.UI.form.MenuButton~menu!property">the
        /// MenuButton's menu property</a>
        /// </summary>
        public virtual IWidget GetWidgetFromProperty(string propertyName)
        {
            return Driver.GetWidgetForElement(GetElementFromProperty(propertyName));
        }

        /// <summary>
        /// Returns a <seealso cref="WidgetImpl"/> representing the value of a widget property,
        /// e.g. <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.UI.form.MenuButton~menu!property">the
        /// MenuButton's menu property</a>
        /// </summary>
        public virtual IList<IWidget> GetWidgetListFromProperty(string propertyName)
        {
            IList<WebElement> elements =
                (IList<WebElement>) JsRunner.RunScript("getElementsFromProperty", contentElement, propertyName);
            IList<IWidget> widgets = new List<IWidget>();

            using (IEnumerator<WebElement> elemIter = elements.GetEnumerator())
            {
                while (elemIter.MoveNext())
                {
                    WebElement element = elemIter.Current;
                    IWidget widget = Driver.GetWidgetForElement(element);
                    widgets.Add(widget);
                }
            }

            return widgets;
        }

        private IList<WebElement> ChildrenElements
        {
            get
            {
                object result = JsRunner.RunScript("getChildrenElements", contentElement);
                IList<WebElement> children = (IList<WebElement>) result;
                return children;
            }
        }

        public virtual IList<IWidget> Children
        {
            get
            {
                IList<WebElement> childrenElements = ChildrenElements;
                IList<IWidget> children = new List<IWidget>();

                using (IEnumerator<WebElement> iter = childrenElements.GetEnumerator())
                {
                    while (iter.MoveNext())
                    {
                        WebElement child = iter.Current;
                        children.Add(Driver.GetWidgetForElement(child));
                    }
                }

                return children;
            }
        }

        /// <summary>
        /// A condition that checks if an element is rendered.
        /// </summary>
        //JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
        //ORIGINAL LINE: public OpenQA.Selenium.Support.UI.ExpectedCondition<OpenQA.Selenium.IWebElement> IsRendered(final OpenQA.Selenium.IWebElement contentElement, final OpenQA.Selenium.By by)
        /*public virtual ExpectedCondition<WebElement> IsRendered(WebElement contentElement, OpenQA.Selenium.By by)
        {
            return new ExpectedConditionAnonymousInnerClass2(this, contentElement, by);
        }

        private class ExpectedConditionAnonymousInnerClass2 : ExpectedCondition<WebElement>
        {
            private readonly WidgetImpl outerInstance;

            private WebElement contentElement;
            private OpenQA.Selenium.By by;

            public ExpectedConditionAnonymousInnerClass2(WidgetImpl outerInstance, WebElement contentElement,
                OpenQA.Selenium.By by)
            {
                this.outerInstance = outerInstance;
                this.contentElement = contentElement;
                this.by = by;
            }

            public WebElement Apply(WebDriver driver)
            {
                return contentElement.FindElement(by);
            }

            public string ToString()
            {
                return "element is rendered.";
            }
        }*/

        /// <summary>
        /// A condition that checks if an element is rendered.
        /// </summary>
        /*public ExpectedCondition<WebElement> IsRendered(WebElement contentElement, OpenQA.Selenium.By by)
        {
            return new ExpectedCondition<WebElement>() {
                @Override

                public WebElement apply(WebDriver driver)
                {
                return contentElement.findElement(by);
            }

            @Override
            public String toString()
            {
                return "element is rendered.";
            }
            };
        }*/

        public virtual WebElement FindElement(OpenQA.Selenium.By by)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            //return wait.Until(IsRendered(contentElement, by));

            // todo: fix it properly

            return null;
        }

        /// <summary>
        /// Finds a widget relative to the current one by traversing the qooxdoo
        /// widget hierarchy.
        /// </summary>
        public virtual IWidget FindWidget(OpenQA.Selenium.By by)
        {
            WebElement element = FindElement(by);
            return Driver.GetWidgetForElement(element);
        }

        public override string ToString()
        {
            return "QxWidget " + Classname + "[" + QxHash + "]";
        }

        /// <summary>
        /// Not implemented for qooxdoo widgets.
        /// </summary>
        public virtual void Submit()
        {
            throw new Exception("Not implemented for qooxdoo widgets.");
        }

        public void Clear()
        {
            contentElement.Clear();
        }

        public string TagName
        {
            get { return contentElement.TagName; }
        }

        public string GetAttribute(string name)
        {
            return contentElement.GetAttribute(name);
        }

        public string GetProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool Selected
        {
            get { return contentElement.Selected; }
        }

        public bool Enabled
        {
            get { return contentElement.Enabled; }
        }

        public string Text
        {
            get { return contentElement.Text; }
        }

        public ReadOnlyCollection<WebElement> FindElements(OpenQA.Selenium.By by)
        {
            return contentElement.FindElements(by);
        }

        /// <summary>
        /// Determines if the widget is visible by querying the qooxdoo property
        /// <a href="http://demo.qooxdoo.org/current/apiviewer/#qx.UI.core.IWidget~isSeeable!method_public">seeable</a>.
        /// </summary>
        public virtual bool Displayed
        {
            get
            {
                return ((bool?) ExecuteJavascript("return qx.ui.Core.Widget.getWidgetByElement(arguments[0]).isSeeable()")).Value;
            }
        }

        public Point Location
        {
            get { return contentElement.Location; }
        }

        public Size Size
        {
            get { return contentElement.Size; }
        }

        public string GetCssValue(string propertyName)
        {
            return contentElement.GetCssValue(propertyName);
        }
    }
}