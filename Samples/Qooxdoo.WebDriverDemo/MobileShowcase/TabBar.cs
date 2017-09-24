using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class TabBar : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Tab Bar");
            VerifyTitle("Tabs");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tabBar() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public TabBar()
        {
            string[] tabs = new string[] {"Desktop", "Server", "Mobile", "Website"};
            foreach (string tab in tabs)
            {
                Touchable tabButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = '" + tab + "']/ancestor::div[contains(@class, 'tabButton')]"));
                tabButton.Tap();
                Thread.Sleep(500);
                WebElement tabContent = driver.FindElement(OpenQA.Selenium.By.XPath("//b[text() = 'qx." + tab + "']/ancestor::div[contains(@class, 'content')]"));
                Assert.True(tabContent.Displayed);
            }
        }
    }

}