using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Web;
using Label = Wisej.Qooxdoo.WebDriver.UI.Basic.Label;

namespace SimpleDemo.NUnit
{
    public static class Utils
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
            Assert.True(button.Enabled, string.Format("MessageBox  {0} isn't enabled.", result.ToString()));
            button.Click();
        }

        // TODO: MessageBoxGetCaption
        // TODO: MessageBoxGetMessage

        #endregion

        #region Button

        public static IWidget ButtonGet(this QxWebDriver driver, string buttonPath, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(buttonPath, "Button");
        }

        public static void ButtonClick(this QxWebDriver driver, string buttonPath, long timeoutInSeconds = 5)
        {
            var button = ButtonGet(driver, buttonPath, timeoutInSeconds);
            Assert.True(button.Enabled, string.Format("Button {0} isn't enabled.", buttonPath));
            button.Click();
        }

        // TODO: ButtonAssertContent
        // TODO: ButtonAssertNotEnabled

        #endregion

        #region Window

        public static IWidget WindowGet(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(windowName, "Window");
        }

        public static void WindowClose(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("close-button");
        }

        public static void WindowMaximize(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("maximize-button");
        }

        public static void WindowMinimize(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("minimize-button");
        }

        public static void WindowRestore(this QxWebDriver driver, string windowName, long timeoutInSeconds = 5)
        {
            var window = WindowGet(driver, windowName, timeoutInSeconds);
            window.ButtonAction("restore-button");
        }

        private static void ButtonAction(this IWidget window, string actionTag)
        {
            var name = window.GetPropertyValue("name");
            IWidget button = window.GetChildControl(actionTag);
            Assert.IsNotNull(button, string.Format("Window {0} button for {1} not found.", name, actionTag));
            Assert.True(button.Enabled, string.Format("Window {0} button for {1} isn't enabled.", name, actionTag));
            button.Click();
        }

        #endregion

        #region Widget

        public static IWidget WidgetGet(this QxWebDriver driver, string widgetPath, string widgetType,
            long timeoutInSeconds = 5)
        {
            OpenQA.Selenium.By widgetBy = QxhByString(By.Namespace(widgetPath));
            IWidget widget = driver.WaitForWidget(widgetBy, timeoutInSeconds);
            Assert.IsNotNull(widget, string.Format("{0} {1} not found.", widgetType, widgetPath));
            return widget;
        }

        #endregion

        #region Label

        public static IWidget LabelGet(this QxWebDriver driver, string labelPath, long timeoutInSeconds = 5)
        {
            return driver.WidgetGet(labelPath, "Label");
        }

        public static void LabelAssertValue(this QxWebDriver driver, string labelPath, string value,
            long timeoutInSeconds = 5)
        {
            var labelWidget = driver.LabelGet(labelPath);
            Label label = (Label) labelWidget;
            Assert.IsNotNull(label, string.Format("Could not cast {0} to Label.", labelPath));

            var labelValue = label.Value;
            Assert.AreEqual(value, labelValue, string.Format("Expected {0} and found {1}.", value, labelValue));
        }

        public static string LabelGetValue(this QxWebDriver driver, string labelPath, long timeoutInSeconds = 5)
        {
            var labelWidget = driver.LabelGet(labelPath);
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