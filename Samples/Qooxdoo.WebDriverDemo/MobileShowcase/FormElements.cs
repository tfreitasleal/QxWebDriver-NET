using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;
using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    public class FormElements : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            SelectItem("Form Elements");
            VerifyTitle("Form");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void textInput()
        [Test]
        public virtual void TextInput()
        {
            ScrollTo(0, 0);
            IWidget input = Driver.FindWidget(By.XPath("//input[@type='text']"));
            input.SendKeys("Affe");
            Assert.Equals("Affe", input.GetPropertyValue("value"));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void checkBox()
        [Test]
        public virtual void CheckBox()
        {
            ScrollTo(0, 0);
            ITouchable checkBox =
                (ITouchable) Driver.FindWidget(By.XPath("//span[contains(@class, 'checkbox')]"));
            // value is an empty string until the checkbox has been selected/deselected
            Assert.Equals("", checkBox.GetPropertyValue("value"));
            checkBox.Tap();
            Assert.True((bool?) checkBox.GetPropertyValue("value"));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void radioButton() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void RadioButton()
        {
            ScrollTo(0, 0);
            Thread.Sleep(500);
            ITouchable radioButton =
                (ITouchable) Driver.FindWidget(By.XPath("//span[contains(@class, 'radio')]"));
            // value is an empty string until the radio button has been selected/deselected
            Assert.Equals("", radioButton.GetPropertyValue("value"));
            radioButton.Tap();
            Assert.True((bool?) radioButton.GetPropertyValue("value"));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void selectBox() throws InterruptedException
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [Test]
        public virtual void SelectBox()
        {
            ScrollTo(0, 1500);
            Thread.Sleep(500);
            ITouchable selectBox =
                (ITouchable) Driver.FindWidget(By.XPath("//input[contains(@class, 'selectbox')]"));
            Assert.Equals("", (string) selectBox.GetPropertyValue("value"));
            selectBox.Tap();
            IWebElement twitter =
                Driver.FindElement(By.XPath("//div[text() = 'Twitter']/ancestor::li[contains(@class, 'list-item')]"));
            WidgetImpl.Tap(Driver.WebDriver, twitter);
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
        [Test]
        public virtual void Slider()
        {
            if (!(Driver.WebDriver is IHasTouchScreen))
            {
                return;
            }
            ScrollTo(0, 1500);
            Thread.Sleep(500);
            ITouchable slider =
                (ITouchable) Driver.FindWidget(By.XPath("//div[contains(@class, 'slider')]"));
            long? valueBefore = (long?) slider.GetPropertyValue("value");

            slider.Track(200, 0, 10);
            Thread.Sleep(5000);
            long? valueAfter = (long?) slider.GetPropertyValue("value");
            Assert.True(valueBefore < valueAfter);
        }
    }
}