using System.Threading;
using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    public class Drawer : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Drawer");
            VerifyTitle("Drawer");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void drawer() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public void TestDrawer()
        {
            string[] drawers = {"top", "right", "bottom", "left"};
            foreach (string drawer in drawers)
            {
                ITouchable drawerButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(
                    "//div[text() = 'Open " + drawer + " drawer']/ancestor::div[contains(@class, 'button')]"));
                drawerButton.Tap();
                Thread.Sleep(1000);
                ITouchable closeButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(
                    "//label[text() = 'This is the " + drawer + " drawer.']/parent::div/div[contains(@class, 'button')]"));
                closeButton.Tap();
                Thread.Sleep(1500);
                Assert.False(closeButton.Displayed);
            }
        }
    }
}