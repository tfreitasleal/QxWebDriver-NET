using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Wisej.WebDriver.UI;
using By = Wisej.WebDriver.By;

namespace Wisej.WebDriverDemo.DesktopApiViewer
{
    [TestFixture]
    public class Tabs : DesktopApiViewer
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            DesktopApiViewer.SetUpBeforeClass();
            string className = "qx.ui.form.Button";
            SelectClass(className);
        }

        protected internal virtual bool Firefox
        {
            get
            {
                if (Platform.CurrentPlatform.PlatformType == PlatformType.Windows &&
                    SystemProperties.GetProperty("org.qooxdoo.demo.browsername")
                        .Equals("firefox", StringComparison.OrdinalIgnoreCase) &&
                    SystemProperties.GetProperty("org.qooxdoo.demo.browserversion")
                        .Equals("stable", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void tabs()
        [Test]
        public virtual void TestTabs()
        {
            if (Firefox)
            {
                return;
            }
            string newTabClass = "qx.ui.form.MenuButton";
            IWebElement link = Driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + newTabClass + "']"));
            Actions action = new Actions(Driver.WebDriver);
            action.KeyDown(Keys.Shift);
            action.Click(link);
            action.KeyUp(Keys.Shift);
            action.Perform();

            string newTabPath = "*/apiviewer.DetailFrameTabView/*/[@label=" + newTabClass + "]";
            IWidget newTabButton = Driver.FindWidget(By.Qxh(newTabPath));
            Assert.True(newTabButton.Displayed);

            string closeButtonPath = newTabPath + "/qx.ui.form.Button";
            IWidget closeButton = Driver.FindWidget(By.Qxh(closeButtonPath));
            Assert.True(closeButton.Displayed);
            closeButton.Click();

            try
            {
                Driver.FindWidget(By.Qxh(newTabPath));
                Assert.True(false, "New tab was not closed!");
            }
            catch (NoSuchElementException)
            {
            }
        }
    }
}