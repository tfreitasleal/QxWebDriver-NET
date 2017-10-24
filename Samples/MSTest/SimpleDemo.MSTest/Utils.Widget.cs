using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace SimpleDemo.MSTest
{
    public static partial class Utils
    {
        #region Widget

        public static IWidget WidgetGet(this QxWebDriver driver, string widgetPath, string widgetType,
            long timeoutInSeconds = 5)
        {
            OpenQA.Selenium.By widgetBy = QxhByString(By.Namespace(widgetPath));
            IWidget widget = driver.WaitForWidget(widgetBy, timeoutInSeconds);
            Assert.IsNotNull(widget, string.Format("{0} {1} not found.", widgetType, widgetPath));
            return widget;
        }

        public static IWidget WidgetGet(this IWidget rootWidget, string widgetPath, string widgetType,
            long timeoutInSeconds = 5)
        {
            OpenQA.Selenium.By widgetBy = QxhByString(By.Namespace(widgetPath));
            IWidget widget = rootWidget.WaitForWidget(widgetBy, timeoutInSeconds);
            Assert.IsNotNull(widget, string.Format("{0} {1} not found.", widgetType, widgetPath));
            return widget;
        }

        public static void AssertIsEnabled(this IWidget widget, string widgetType = "")
        {
            Assert.IsTrue(widget.Enabled, string.Format("{0} is not Enabled.", widgetType));
        }

        public static void AssertIsNotEnabled(this IWidget widget, string widgetType = "")
        {
            Assert.IsFalse(widget.Enabled, string.Format("{0} is Enabled.", widgetType));
        }

        public static void AssertIsSelected(this IWidget widget, string widgetType = "")
        {
            Assert.IsTrue(widget.Selected, string.Format("{0} is not Selected.", widgetType));
        }

        public static void AssertIsNotSelected(this IWidget widget, string widgetType = "")
        {
            Assert.IsFalse(widget.Selected, string.Format("{0} is Selected.", widgetType));
        }

        public static void AssertIsDisplayed(this IWidget widget, string widgetType = "")
        {
            Assert.IsTrue(widget.Displayed, string.Format("{0} is not Displayed.", widgetType));
        }

        public static void AssertIsNotDisplayed(this IWidget widget, string widgetType = "")
        {
            Assert.IsFalse(widget.Displayed, string.Format("{0} is Displayed.", widgetType));
        }

        public static void AssertClassname(this IWidget widget, string classname, string widgetType = "")
        {
            Assert.AreEqual(widget.Classname, classname,
                string.Format("{0}: expected {1} and actual is {2}.", widgetType, classname, widget.Classname));
        }

        public static void AssertLocation(this IWidget widget, Point location, string widgetType = "")
        {
            Assert.AreEqual(widget.Location.X, location.X,
                string.Format("{0}.X: expected {1} and actual is {2}.", widgetType, location.X, widget.Location.X));
            Assert.AreEqual(widget.Location.Y, location.Y,
                string.Format("{0}.Y: expected {1} and actual is {2}.", widgetType, location.Y, widget.Location.Y));
        }

        public static void AssertSize(this IWidget widget, Size size, string widgetType = "")
        {
            Assert.AreEqual(widget.Size.Width, size.Width,
                string.Format("{0}.Width: expected {1} and actual is {2}.", widgetType, size.Width, widget.Size.Width));
            Assert.AreEqual(widget.Size.Height, size.Height,
                string.Format("{0}.Height: expected {1} and actual is {2}.", widgetType, size.Height,
                    widget.Size.Height));
        }

        #endregion
    }
}