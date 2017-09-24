using System.Collections.Generic;
using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
    using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
    using WebElement = OpenQA.Selenium.IWebElement;
    using HasTouchScreen = OpenQA.Selenium.IHasTouchScreen;

    public class Carousel : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void SetUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Carousel");
            VerifyTitle("Carousel");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void carousel() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public Carousel()
        {
            if (!(driver.WebDriver is HasTouchScreen))
            {
                return;
            }
            IList<WebElement> pages = driver.FindElements(OpenQA.Selenium.By.XPath("//div[contains(@class, 'carousel-page')]"));

            WebElement label0 = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(text(), 'This is a carousel')]"));
            Assert.True(label0.Displayed);

            WidgetImpl.Track(driver.WebDriver, pages[0], -350, 0, 50);
            Thread.Sleep(1000);

            WebElement label1 = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(text(), 'It contains multiple carousel pages')]"));
            Assert.True(label1.Displayed);

            WidgetImpl.Track(driver.WebDriver, pages[1], -350, 0, 50);
            Thread.Sleep(1000);

            WebElement label2 = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(text(), 'Carousel pages may contain')]"));
            Assert.True(label2.Displayed);

            Touchable nextPage = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Next Page']/ancestor::div[contains(@class, 'button')]"));
            nextPage.Tap();
            Thread.Sleep(1000);

            WebElement label3 = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(text(), 'The carousel snaps')]"));
            Assert.True(label3.Displayed);

            WidgetImpl.Track(driver.WebDriver, pages[3], -350, 0, 50);
            Thread.Sleep(1000);

            WebElement label4 = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(text(), 'You can add as many')]"));
            Assert.True(label4.Displayed);

            IList<WebElement> paginationLabels = driver.FindElements(OpenQA.Selenium.By.XPath("//div[contains(@class, 'carousel-pagination-label')]"));
            Assert.Equals(6, paginationLabels.Count);

            Touchable addButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Add more pages']/ancestor::div[contains(@class, 'button')]"));
            addButton.Tap();

            paginationLabels = driver.FindElements(OpenQA.Selenium.By.XPath("//div[contains(@class, 'carousel-pagination-label')]"));
            Assert.Equals(56, paginationLabels.Count);
        }
    }

}