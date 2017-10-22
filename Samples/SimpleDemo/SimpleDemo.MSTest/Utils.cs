using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;
using Wisej.Web;

namespace SimpleDemo.MSTest
{
    public static class Utils
    {
        public static void AssertMessageBox(this QxWebDriver driver, DialogResult result)
        {
            var messageBox = driver.WaitForWidget(By.Qxh("wisej.web.MessageBox"), 10);
            Assert.IsNotNull(messageBox);
            var buttonsPane = messageBox.FindWidget(By.Qxh("qx.ui.container.Composite"));
            Assert.IsNotNull(buttonsPane);

            IWidget button = null;
            foreach (var child in buttonsPane.Children)
            {
                if (child.Text == result.ToString())
                {
                    button = child;
                    break;
                }
            }

            Assert.IsNotNull(button);
            button.Click();
        }
    }
}