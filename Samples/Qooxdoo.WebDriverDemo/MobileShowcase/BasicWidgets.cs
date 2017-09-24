using System.Threading;
using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    public class BasicWidgets : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "Basic Widgets";
            SelectItem(title);
            VerifyTitle(title);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void basicWidgets() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void basicWidgets()
        {
            // toggle button
            Touchable toggleButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'togglebutton') and @data-label-checked='ON']"));
            bool? valueBefore = (bool?) toggleButton.GetPropertyValue("value");
            Assert.True(valueBefore);
            Thread.Sleep(250);
            toggleButton.Tap();
            bool? valueAfter = (bool?) toggleButton.GetPropertyValue("value");
            Assert.False(valueAfter);
            Thread.Sleep(500);
            toggleButton.Tap();

            ScrollTo(0, 500);
            Thread.Sleep(500);

            // collapsible
            Touchable collapsibleHeader = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'collapsible-header')]"));
            WebElement collapsibleContent = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'collapsible-content')]"));
            Assert.False(collapsibleContent.Displayed);
            collapsibleHeader.Tap();
            Assert.True(collapsibleContent.Displayed);
        }
    }

}