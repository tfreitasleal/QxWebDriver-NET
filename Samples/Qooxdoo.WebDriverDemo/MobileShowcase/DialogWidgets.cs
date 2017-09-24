using System.Threading;
using Qooxdoo.WebDriver.UI.mobile;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using Touchable = Qooxdoo.WebDriver.UI.ITouchable;
using WidgetImpl = Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;
using StaleElementReferenceException = OpenQA.Selenium.StaleElementReferenceException;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.MobileShowcase
{
    public class DialogWidgets : Mobileshowcase
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            string title = "Dialog Widgets";
            SelectItem(title);
            VerifyTitle(title);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void popup() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void Popup()
        {
            string popupButtonLocator = "//div[text() = 'Popup']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable popupButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(popupButtonLocator));
            popupButton.Tap();

            string closeButtonLocator = "//div[text() = 'Close Popup']/ancestor::div[contains(@class, 'button')]";
            Touchable closeButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(closeButtonLocator));
            Assert.True(closeButton.Displayed);

            Thread.Sleep(250);
            closeButton.Tap();
            Assert.False(closeButton.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void menu() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void Menu()
        {
            string menuButtonLocator = "//div[text() = 'Menu']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable menuButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(menuButtonLocator));
            menuButton.Tap();

            By listLocator = OpenQA.Selenium.By.XPath("//div[contains(@class, 'menu')]/descendant::ul[contains(@class, 'list')]");
            ISelectable list = (ISelectable) driver.FindWidget(listLocator);
            Assert.True(list.Displayed);

            list.SelectItem(5);
            Thread.Sleep(500);
            Assert.False(list.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void busyIndicator() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void BusyIndicator()
        {
            string busyButtonLocator = "//div[text() = 'Busy Indicator']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable busyButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(busyButtonLocator));
            busyButton.Tap();

            string busyPopupLocator = "//div[contains(text(), 'Please wait')]/ancestor::div[contains(@class, 'popup')]";
            WebElement busyPopup = driver.FindElement(OpenQA.Selenium.By.XPath(busyPopupLocator));
            Assert.True(busyPopup.Displayed);

            Thread.Sleep(4000);
            Assert.False(busyPopup.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void anchorPopup() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void AnchorPopup()
        {
            string anchorPopupButtonLocator = "//div[text() = 'Anchor Popup']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable anchorPopupButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(anchorPopupButtonLocator));
            anchorPopupButton.Tap();

            string yesButtonLocator = "//div[text() = 'Yes']/ancestor::div[contains(@class, 'button')]";
            Touchable yesButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(yesButtonLocator));
            Assert.True(yesButton.Displayed);

            Thread.Sleep(250);
            yesButton.Tap();
            Assert.False(yesButton.Displayed);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void anchorMenu() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void AnchorMenu()
        {
            string anchorMenuButtonLocator = "//div[text() = 'Anchor Menu']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable anchorMenuButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(anchorMenuButtonLocator));
            anchorMenuButton.Tap();

            string greenButtonLocator = "//div[text() = 'Green']/ancestor::li[contains(@class, 'list-item')]";
            WebElement greenButton = driver.FindElement(OpenQA.Selenium.By.XPath(greenButtonLocator));
            Assert.True(greenButton.Displayed);

            Thread.Sleep(250);
            WidgetImpl.Tap(driver.WebDriver, greenButton);

            try
            {
                Assert.False(greenButton.Displayed);
            }
            catch (StaleElementReferenceException)
            {
                // Element is no longer in the DOM
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void picker() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void Picker()
        {
            string pickerButtonLocator = "//div[text() = 'Picker']/ancestor::div[contains(@class, 'button')]";
            Thread.Sleep(250);
            Touchable pickerButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(pickerButtonLocator));
            pickerButton.Tap();

            string chooseButtonLocator = "//div[text() = 'OK']/ancestor::div[contains(@class, 'button')]";
            Touchable chooseButton = (Touchable) driver.FindWidget(OpenQA.Selenium.By.XPath(chooseButtonLocator));
            Assert.True(chooseButton.Displayed);

            Thread.Sleep(250);
            chooseButton.Tap();
            Assert.False(chooseButton.Displayed);
        }
    }

}