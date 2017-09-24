namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using Keys = OpenQA.Selenium.Keys;
    using NoSuchElementException = OpenQA.Selenium.NoSuchElementException;
    using WebElement = OpenQA.Selenium.IWebElement;
    using Actions = OpenQA.Selenium.Interactions.Actions;

    public class Tabs : DesktopApiViewer
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            DesktopApiViewer.setUpBeforeClass();
            string className = "qx.ui.form.Button";
            SelectClass(className);
        }

        protected internal virtual bool Firefox
        {
            get
            {
                if (System.getProperty("org.qooxdoo.demo.platform").equalsIgnoreCase("windows") && System.getProperty("org.qooxdoo.demo.browsername").equalsIgnoreCase("firefox") && System.getProperty("org.qooxdoo.demo.browserversion").equalsIgnoreCase("stable"))
                {
                    return true;
                }
                return false;
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tabs()
        public virtual void tabs()
        {
            if (Firefox)
            {
                return;
            }
            string newTabClass = "qx.ui.form.MenuButton";
            WebElement link = driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + newTabClass + "']"));
            Actions action = new Actions(driver.WebDriver);
            action.keyDown(Keys.SHIFT);
            action.Click(link);
            action.keyUp(Keys.SHIFT);
            action.perform();

            string newTabPath = "*/apiviewer.DetailFrameTabView/*/[@label=" + newTabClass + "]";
            Widget newTabButton = driver.FindWidget(By.Qxh(newTabPath));
            Assert.True(newTabButton.Displayed);

            string closeButtonPath = newTabPath + "/qx.ui.form.Button";
            Widget closeButton = driver.FindWidget(By.Qxh(closeButtonPath));
            Assert.True(closeButton.Displayed);
            closeButton.Click();

            try
            {
                driver.FindWidget(By.Qxh(newTabPath));
                Assert.True("New tab was not closed!", false);
            }
            catch (NoSuchElementException)
            {
            }
        }

    }

}