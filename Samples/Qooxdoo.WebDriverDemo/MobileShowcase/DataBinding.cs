using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
    using WebElement = OpenQA.Selenium.IWebElement;
    using HasTouchScreen = OpenQA.Selenium.IHasTouchScreen;

    public class DataBinding : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            ScrollTo(0, 5000);
            Thread.Sleep(500);
            SelectItem("Data Binding");
            VerifyTitle("Data Binding");
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
            Touchable input = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//input"));
            int valueBefore = int.Parse((string) input.GetPropertyValue("value"));

            Touchable slider = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[contains(@class, 'slider')]"));
            slider.track(200, 0, 10);

            int valueAfter = int.Parse((string) input.GetPropertyValue("value"));
            Assert.True(valueAfter > valueBefore);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void time() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void time()
        {
            Touchable button = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Take Time Snapshot']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            WebElement entry = driver.FindElement(OpenQA.Selenium.By.XPath("//div[text() = 'Stop #1']"));
            Assert.True(entry.Displayed);
        }

    }

}