using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Mobile.Core;
using ISelectable = Qooxdoo.WebDriver.UI.Mobile.ISelectable;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class DialogWidgets : Mobileshowcase
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "Dialog Widgets";
            SelectItem(title);
            VerifyTitle(title);
        }

        //ORIGINAL LINE: @Test public void popup() throws InterruptedException
        [Test]
        public virtual void Popup()
        {
            string popupButtonLocator = "//div[text() = 'Popup']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable popupButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(popupButtonLocator));
            popupButton.Tap();

            string closeButtonLocator = "//div[text() = 'Close Popup']/ancestor::div[contains(@class, 'button')]";
            ITouchable closeButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(closeButtonLocator));
            Assert.True(closeButton.Displayed);

            Thread.Sleep(250);
            closeButton.Tap();
            Assert.False(closeButton.Displayed);
        }

        //ORIGINAL LINE: @Test public void menu() throws InterruptedException
        [Test]
        public virtual void Menu()
        {
            string menuButtonLocator = "//div[text() = 'Menu']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable menuButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(menuButtonLocator));
            menuButton.Tap();

            By listLocator = By.XPath("//div[contains(@class, 'menu')]/descendant::ul[contains(@class, 'list')]");
            ISelectable list = (ISelectable) Driver.FindWidget(listLocator);
            Assert.True(list.Displayed);

            list.SelectItem(5);
            Thread.Sleep(500);
            Assert.False(list.Displayed);
        }

        //ORIGINAL LINE: @Test public void busyIndicator() throws InterruptedException
        [Test]
        public virtual void BusyIndicator()
        {
            string busyButtonLocator = "//div[text() = 'Busy Indicator']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable busyButton = (ITouchable) Driver.FindWidget(By.XPath(busyButtonLocator));
            busyButton.Tap();

            string busyPopupLocator = "//div[contains(text(), 'Please wait')]/ancestor::div[contains(@class, 'popup')]";
            IWebElement busyPopup = Driver.FindElement(By.XPath(busyPopupLocator));
            Assert.True(busyPopup.Displayed);

            Thread.Sleep(4000);
            Assert.False(busyPopup.Displayed);
        }

        //ORIGINAL LINE: @Test public void anchorPopup() throws InterruptedException
        [Test]
        public virtual void AnchorPopup()
        {
            string anchorPopupButtonLocator = "//div[text() = 'Anchor Popup']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable anchorPopupButton = (ITouchable) Driver.FindWidget(By.XPath(anchorPopupButtonLocator));
            anchorPopupButton.Tap();

            string yesButtonLocator = "//div[text() = 'Yes']/ancestor::div[contains(@class, 'button')]";
            ITouchable yesButton = (ITouchable) Driver.FindWidget(By.XPath(yesButtonLocator));
            Assert.True(yesButton.Displayed);

            Thread.Sleep(250);
            yesButton.Tap();
            Assert.False(yesButton.Displayed);
        }

        //ORIGINAL LINE: @Test public void anchorMenu() throws InterruptedException
        [Test]
        public virtual void AnchorMenu()
        {
            string anchorMenuButtonLocator = "//div[text() = 'Anchor Menu']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable anchorMenuButton = (ITouchable) Driver.FindWidget(By.XPath(anchorMenuButtonLocator));
            anchorMenuButton.Tap();

            string greenButtonLocator = "//div[text() = 'Green']/ancestor::li[contains(@class, 'list-item')]";
            IWebElement greenButton = Driver.FindElement(By.XPath(greenButtonLocator));
            Assert.True(greenButton.Displayed);

            Thread.Sleep(250);
            WidgetImpl.Tap(Driver.WebDriver, greenButton);

            try
            {
                Assert.False(greenButton.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }

        //ORIGINAL LINE: @Test public void picker() throws InterruptedException
        [Test]
        public virtual void Picker()
        {
            string pickerButtonLocator = "//div[text() = 'Picker']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            ITouchable pickerButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(pickerButtonLocator));
            pickerButton.Tap();

            string chooseButtonLocator = "//div[text() = 'OK']/ancestor::div[contains(@class, 'button')]";
            ITouchable chooseButton = (ITouchable) Driver.FindWidget(OpenQA.Selenium.By.XPath(chooseButtonLocator));
            Assert.True(chooseButton.Displayed);

            Thread.Sleep(250);
            chooseButton.Tap();
            Assert.False(chooseButton.Displayed);
        }
    }
}