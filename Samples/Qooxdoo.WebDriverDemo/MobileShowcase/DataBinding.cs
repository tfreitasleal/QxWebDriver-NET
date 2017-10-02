using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    public class DataBinding : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
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
        [Test]
        public virtual void Slider()
        {
            if (!(Driver.WebDriver is IHasTouchScreen))
            {
                return;
            }
            ITouchable input = (ITouchable) Driver.FindWidget(By.XPath("//input"));
            int valueBefore = int.Parse((string) input.GetPropertyValue("value"));

            ITouchable slider =
                (ITouchable) Driver.FindWidget(By.XPath("//div[contains(@class, 'slider')]"));
            slider.Track(200, 0, 10);

            int valueAfter = int.Parse((string) input.GetPropertyValue("value"));
            Assert.True(valueAfter > valueBefore);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void time() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void Time()
        {
            ITouchable button = (ITouchable) Driver.FindWidget(By.XPath(
                "//div[text() = 'Take Time Snapshot']/ancestor::div[contains(@class, 'button')]"));
            button.Tap();
            IWebElement entry = Driver.FindElement(By.XPath("//div[text() = 'Stop #1']"));
            Assert.True(entry.Displayed);
        }
    }
}