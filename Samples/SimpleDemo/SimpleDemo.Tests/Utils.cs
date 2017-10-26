using NUnit.Framework;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using By = Qooxdoo.WebDriver.By;
using Label = Qooxdoo.WebDriver.UI.Basic.Label;

namespace SimpleDemo.Tests
{
    public static partial class Utils
    {
        #region MessageBox

        public static void MessageBoxClick(this QxWebDriver driver, DialogResult result, long timeoutInSeconds = 5)
        {
            var messageBoxBy = QxhByString("wisej.web.MessageBox");
            var messageBox = driver.WaitForWidget(messageBoxBy, timeoutInSeconds);
            Assert.IsNotNull(messageBox, "MessageBox not found.");

            var buttonsPaneBy = QxhByString("qx.ui.container.Composite");
            var buttonsPane = messageBox.FindWidget(buttonsPaneBy);
            Assert.IsNotNull(buttonsPane, "MessageBox button's panel not found.");

            IWidget button = null;
            foreach (var child in buttonsPane.Children)
            {
                if (child.Text == result.ToString())
                {
                    button = child;
                    break;
                }
            }

            Assert.IsNotNull(button, string.Format("MessageBox Button {0} not found.", result.ToString()));
            Assert.IsTrue(button.Enabled, string.Format("MessageBox  {0} isn't enabled.", result.ToString()));
            button.Click();
        }

        // TODO: MessageBoxGetCaption
        // TODO: MessageBoxGetMessage

        #endregion

        #region Button

        public static IWidget ButtonGet(this QxWebDriver driver, string buttonPath, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(buttonPath, "Button", timeoutInSeconds);
        }

        public static IWidget ButtonGet(this IWidget rootWidget, string buttonPath, long timeoutInSeconds = 5)
        {
            return rootWidget.WidgetGet(buttonPath, "Button", timeoutInSeconds);
        }

        public static void ButtonClick(this QxWebDriver driver, string buttonPath, long timeoutInSeconds = 5)
        {
            var button = ButtonGet(driver, buttonPath, timeoutInSeconds);
            Assert.IsTrue(button.Enabled, string.Format("Button {0} isn't enabled.", buttonPath));
            button.Click();
        }

        public static void ButtonClick(this IWidget rootWidget, string buttonPath, long timeoutInSeconds = 5)
        {
            var button = ButtonGet(rootWidget, buttonPath, timeoutInSeconds);
            Assert.IsTrue(button.Enabled, string.Format("Button {0} isn't enabled.", buttonPath));
            button.Click();
        }

        // TODO: ButtonAssertContent

        #endregion

        #region Window

        public static IWidget WindowGet(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(windowName, "Window", timeoutInSeconds);
        }

        public static void WindowClose(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("close-button");
        }

        public static void WindowClose(this IWidget window, long timeoutInSeconds = 5)
        {
            window.ButtonAction("close-button");
        }

        public static void WindowMaximize(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("maximize-button");
        }

        public static void WindowMaximize(this IWidget window, long timeoutInSeconds = 5)
        {
            window.ButtonAction("maximize-button");
        }

        public static void WindowMinimize(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("minimize-button");
        }

        public static void WindowMinimize(this IWidget window, long timeoutInSeconds = 5)
        {
            window.ButtonAction("minimize-button");
        }

        public static void WindowRestore(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("restore-button");
        }

        public static void WindowRestore(this IWidget window, long timeoutInSeconds = 5)
        {
            window.ButtonAction("restore-button");
        }

        private static void ButtonAction(this IWidget window, string actionTag)
        {
            var name = window.GetPropertyValue("name");
            IWidget button = window.GetChildControl(actionTag);
            Assert.IsNotNull(button, string.Format("Window {0} button for {1} not found.", name, actionTag));
            Assert.IsTrue(button.Enabled, string.Format("Window {0} button for {1} isn't enabled.", name, actionTag));
            button.Click();
        }

        #endregion

        #region Label

        public static IWidget LabelGet(this QxWebDriver driver, string labelPath, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(labelPath, "Label", timeoutInSeconds);
        }

        public static IWidget LabelGet(this IWidget rootWidget, string labelPath, long timeoutInSeconds = 5)
        {
            return rootWidget.WidgetGet(labelPath, "Label", timeoutInSeconds);
        }

        public static void LabelAssertValue(this QxWebDriver driver, string labelPath, string value,
            long timeoutInSeconds = 5)
        {
            var labelWidget = driver.LabelGet(labelPath, timeoutInSeconds);
            LabelAssertValueCore(labelWidget, labelPath, value);
        }

        public static void LabelAssertValue(this IWidget rootWidget, string labelPath, string value,
            long timeoutInSeconds = 5)
        {
            var labelWidget = rootWidget.LabelGet(labelPath, timeoutInSeconds);
            LabelAssertValueCore(labelWidget, labelPath, value);
        }

        private static void LabelAssertValueCore(this IWidget labelWidget, string labelPath, string value)
        {
            Label label = (Label) labelWidget;
            Assert.IsNotNull(label, string.Format("Could not cast {0} to Label.", labelPath));

            var labelValue = label.Value;
            Assert.AreEqual(value, labelValue, string.Format("Expected {0} and actual is {1}.", value, labelValue));
        }

        public static string LabelGetValue(this QxWebDriver driver, string labelPath, long timeoutInSeconds = 5)
        {
            var labelWidget = driver.LabelGet(labelPath, timeoutInSeconds);
            return LabelGetValueCore(labelWidget, labelPath);
        }

        public static string LabelGetValue(this IWidget rootWidget, string labelPath, long timeoutInSeconds = 5)
        {
            var labelWidget = rootWidget.LabelGet(labelPath, timeoutInSeconds);
            return LabelGetValueCore(labelWidget, labelPath);
        }

        public static string LabelGetValueCore(this IWidget labelWidget, string labelPath)
        {
            Label label = (Label) labelWidget;
            Assert.IsNotNull(label, string.Format("Could not cast {0} to Label.", labelPath));
            return label.Value;
        }

        #endregion

        #region By

        public static OpenQA.Selenium.By QxhByString(string expression)
        {
            OpenQA.Selenium.By by = By.Qxh(expression);
            Assert.IsNotNull(by, string.Format("Could not cast {0} to OpenQA.Selenium.By.", expression));
            return by;
        }

        #endregion
    }
}