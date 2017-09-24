using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using Actions = OpenQA.Selenium.Interactions.Actions;
using Coordinates = OpenQA.Selenium.Interactions.Internal.ICoordinates;
using HasInputDevices = OpenQA.Selenium.IHasInputDevices;
using HasTouchScreen = OpenQA.Selenium.IHasTouchScreen;
using InterruptedException = System.Threading.ThreadInterruptedException;
using Locatable = OpenQA.Selenium.ILocatable;
using Mouse = OpenQA.Selenium.IMouse;
using Point = System.Drawing.Point;
using TouchActions = OpenQA.Selenium.Interactions.TouchActions;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;

/* ************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java

   Copyright:
     2014 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

************************************************************************ */

namespace Qooxdoo.WebDriver.UI.Mobile.Core
{
    public class WidgetImpl : UI.Core.WidgetImpl, ITouchable
    {
        public WidgetImpl(WebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
            // workaround for https://github.com/selendroid/selendroid/issues/337
            contentElement = element;
        }

        public override bool Displayed
        {
            get
            {
                if (contentElement.Displayed)
                {
                    string script = "return arguments[0].offsetWidth > 0 || arguments[0].offsetHeight > 0";
                    return ((bool?) JsExecutor.ExecuteScript(script, contentElement)).Value;
                }
                else
                {
                    return false;
                }
            }
        }

        public virtual void Tap()
        {
            Tap(Driver.WebDriver, contentElement);
        }

        public static void Tap(IWebDriver driver, WebElement element)
        {
            if (driver is HasTouchScreen)
            {
                TouchActions tap = (new TouchActions(driver)).SingleTap(element);
                tap.Perform();
            }
            else
            {
                element.Click();
            }
        }

        public virtual void Longtap()
        {
            Longtap(Driver.WebDriver, contentElement);
        }

        public static void Longtap(IWebDriver driver, WebElement element)
        {
            if (driver is HasTouchScreen)
            {
                TouchActions longtap = new TouchActions(driver);
                Point center = GetCenter(element);
                longtap.Down(center.X, center.Y);
                longtap.Perform();
                try
                {
                    Thread.Sleep(750);
                }
                catch (InterruptedException)
                {
                }
                longtap.Up(center.X, center.Y);
                longtap.Perform();
            }
            else
            {
                Locatable locatable = (Locatable) element;
                Coordinates coords = locatable.Coordinates;
                Mouse mouse = ((HasInputDevices) driver).Mouse;
                mouse.MouseDown(coords);
                try
                {
                    Thread.Sleep(750);
                }
                catch (InterruptedException)
                {
                }
                mouse.MouseUp(coords);
            }
        }

        protected internal static Point GetCenter(WebElement element)
        {
            Size size = element.Size;
            int halfWidth = size.Width / 2;
            int halfHeight = size.Height / 2;

            Point loc = element.Location;
            int posX = loc.X + halfWidth;
            int posY = loc.Y + halfHeight;

            Point point = new Point(posX, posY);
            return point;
        }

        public virtual void Track(int x, int y, int step)
        {
            Track(Driver.WebDriver, contentElement, x, y, step);
        }

        public static void Track(IWebDriver driver, WebElement element, int x, int y, int step)
        {
            if (driver is HasTouchScreen)
            {
                if (step == 0)
                {
                    step = 1;
                    // TODO: no move if step == 0
                }

                Point center = GetCenter(element);

                int posX = center.X;
                int posY = center.Y;

                int endX = posX + x;
                int endY = posY + y;

                TouchActions touchAction = new TouchActions(driver);
                touchAction.Down(posX, posY);

                while ((x < 0 && posX > endX || x > 0 && posX < endX) || (y < 0 && posY > endY || y > 0 && posY < endY))
                {
                    if (x > 0 && posX < endX)
                    {
                        if (posX + step > endX)
                        {
                            posX += endX - (posX + step);
                        }
                        else
                        {
                            posX += step;
                        }
                    }

                    else if (x < 0 && posX > endX)
                    {
                        if (posX - step < endX)
                        {
                            posX -= endX + (posX - step);
                        }
                        else
                        {
                            posX -= step;
                        }
                    }

                    if (y > 0 && posY < endY)
                    {
                        if (posY + step > endY)
                        {
                            posY += endY - (posY + step);
                        }
                        else
                        {
                            posY += step;
                        }
                    }

                    else if (y < 0 && posY > endY)
                    {
                        if (posY - step < endY)
                        {
                            posY -= endY + (posY - step);
                        }
                        else
                        {
                            posY -= step;
                        }
                    }

                    touchAction.Move(posX, posY);
                }

                touchAction.Up(posX, posY).Perform();
            }
            else
            {
                Actions mouseAction = new Actions(driver);
                mouseAction.DragAndDropToOffset(element, x, y);
            }
        }

        public virtual void ScrollTo(int x, int y)
        {
            string script = "qx.ui.mobile.Core.Widget.getWidgetById(arguments[0].id).ScrollTo(" + x + ", " + y + ")";
            IList<WebElement> scrollContainers = Driver.FindElements(By.CssSelector(".Scroll"));

            using (var itr = scrollContainers.GetEnumerator())
            {
                while (itr.MoveNext())
                {
                    var scroller = itr.Current;
                    if (scroller != null && scroller.Displayed)
                    {
                        Driver.ExecuteScript(script, scroller);
                    }
                }
            }
        }
    }
}