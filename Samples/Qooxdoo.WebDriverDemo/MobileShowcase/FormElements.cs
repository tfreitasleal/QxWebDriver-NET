using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
    using StaleElementReferenceException = OpenQA.Selenium.StaleElementReferenceException;
    using WebElement = OpenQA.Selenium.IWebElement;
    using HasTouchScreen = OpenQA.Selenium.IHasTouchScreen;

    public class FormElements : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Form Elements");
            VerifyTitle("Form");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void textInput()
        public virtual void textInput()
        {
            ScrollTo(0, 0);
            Widget input = driver.FindWidget(OpenQA.Selenium.By.XPath("//input[@type='text']"));
            input.SendKeys("Affe");
            Assert.Equals("Affe", input.GetPropertyValue("value"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void checkBox()
        public virtual void checkBox()
        {
            ScrollTo(0, 0);
            Touchable checkBox = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//span[contains(@class, 'checkbox')]"));
            // value is an empty string until the checkbox has been selected/deselected
            Assert.Equals("", checkBox.GetPropertyValue("value"));
            checkBox.Tap();
            Assert.True((bool?) checkBox.GetPropertyValue("value"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void radioButton() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void radioButton()
        {
            ScrollTo(0, 0);
            Thread.Sleep(500);
            Touchable radioButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//span[contains(@class, 'radio')]"));
            // value is an empty string until the radio button has been selected/deselected
            Assert.Equals("", radioButton.GetPropertyValue("value"));
            radioButton.Tap();
            Assert.True((bool?) radioButton.GetPropertyValue("value"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void selectBox() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void selectBox()
        {
            ScrollTo(0, 1500);
            Thread.Sleep(500);
            Touchable selectBox = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//input[contains(@class, 'selectbox')]"));
            Assert.Equals("", (string) selectBox.GetPropertyValue("value"));
            selectBox.Tap();
            WebElement twitter = driver.FindElement(OpenQA.Selenium.By.XPath("//div[text() = 'Twitter']/ancestor::li[contains(@class, 'list-item')]"));
            WidgetImpl.Tap(driver.WebDriver, twitter);
            try
            {
                Assert.False(twitter.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
            Assert.Equals("Twitter", selectBox.GetPropertyValue("value"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void slider() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void slider()
        {
            if (!(driver.WebDriver is HasTouchScreen))
            {
                return;
            }
            ScrollTo(0, 1500);
            Thread.Sleep(500);
            Touchable slider = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'slider')]"));
            long? valueBefore = (long?) slider.GetPropertyValue("value");

            slider.track(200, 0, 10);
            Thread.Sleep(5000);
            long? valueAfter = (long?) slider.GetPropertyValue("value");
            Assert.True(valueBefore < valueAfter);
        }
    }

}