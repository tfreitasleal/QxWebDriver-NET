using System.Threading;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Touchable = Qooxdoo.WebDriver.UI.ITouchable;

    public class Drawer : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Drawer");
            VerifyTitle("Drawer");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void drawer() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public Drawer()
        {
            string[] drawers = new string[] {"top", "right", "bottom", "left"};
            foreach (string drawer in drawers)
            {
                Touchable drawerButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//div[text() = 'Open " + drawer + " drawer']/ancestor::div[contains(@class, 'button')]"));
                drawerButton.Tap();
                Thread.Sleep(1000);
                Touchable closeButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath("//label[text() = 'This is the " + drawer + " drawer.']/parent::div/div[contains(@class, 'button')]"));
                closeButton.Tap();
                Thread.Sleep(1500);
                Assert.False(closeButton.Displayed);
            }
        }
    }

}