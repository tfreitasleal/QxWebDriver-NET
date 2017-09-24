using System.Threading;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI.mobile;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
using StaleElementReferenceException = OpenQA.Selenium.StaleElementReferenceException;
using WebElement = OpenQA.Selenium.IWebElement;
using HasTouchScreen = OpenQA.Selenium.IHasTouchScreen;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    public class List : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "List";
            SelectItem(title);
            VerifyTitle(title);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void SelectItem()
        public virtual void SelectItem()
        {
            ISelectable list = (ISelectable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'master-detail-detail')]/descendant::ul[contains(@class, 'list')]"));
            list.SelectItem("Item #3");

            WebElement selected = driver.FindElement(OpenQA.Selenium.By.XPath("//div[text() = 'You selected Item #3']"));
            Assert.True(selected.Displayed);
            Touchable ok = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'You selected Item #3']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'dialog-button')]"));
            ok.Tap();
            try
            {
                Assert.False(selected.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void removeItem() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void RemoveItem()
        {
            if (!(driver.WebDriver is HasTouchScreen))
            {
                return;
            }
            WebElement item = driver.FindElement(OpenQA.Selenium.By.XPath("//div[text() = 'Item #6']"));
            WidgetImpl.Track(driver.WebDriver, item, 700, 0, 10);
            Thread.Sleep(1000);
            try
            {
                Assert.False(item.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }
    }

}