using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
    using StaleElementReferenceException = OpenQA.Selenium.StaleElementReferenceException;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class Toolbar : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Toolbar");
            VerifyTitle("Toolbar");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void search()
        public virtual void Search()
        {
            Touchable button = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Search']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            Touchable popupButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Search']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            Assert.False(popupButton.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void back()
        public virtual void Back()
        {
            Touchable button = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//img[contains(@src, 'arrowleft')]/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            Touchable popupButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Are you sure?']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            Assert.False(popupButton.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void camera() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void Camera()
        {
            Touchable button = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//img[contains(@src, 'camera')]/ancestor::div[contains(@class, 'button')]"));
            button.Tap();

            WebElement popup = driver.FindElement(OpenQA.Selenium.By.XPath("//div[text() = 'Data connection...']/ancestor::div[contains(@class, 'popup-content')]"));
            Assert.True(popup.Displayed);
            Thread.Sleep(5000);
            Assert.False(popup.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void delete()
        public virtual void Delete()
        {
            Touchable button = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Delete']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            Touchable popupButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Are you sure?']/ancestor::div[contains(@class, 'popup-content')]/descendant::div[contains(@class, 'button')]"));
            popupButton.Tap();
            try
            {
                Assert.False(popupButton.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }
    }

}