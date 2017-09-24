namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using NoSuchElementException = OpenQA.Selenium.NoSuchElementException;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class ClassViewer : DesktopApiViewer
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            DesktopApiViewer.setUpBeforeClass();
            string className = "qx.ui.core.Widget";
            SelectClass(className);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUpBeforeTest()
        public virtual void SetUpBeforeTest()
        {
            string className = "qx.ui.core.Widget";
            SelectClass(className);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void links()
        public virtual void Links()
        {
            string internalTarget = "#capture";
            WebElement internalLink = driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + internalTarget + "']"));
            internalLink.Click();
            string hashAfter = (string) driver.ExecuteScript("return location.hash;");
            Assert.Equals("#qx.ui.core.Widget~capture", hashAfter);

            string subClass = "qx.ui.Basic.Atom";
            WebElement subClassLink = driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + subClass + "']"));
            subClassLink.Click();

            Widget tabButton = driver.FindWidget(By.Qxh(tabButtonPath));
            Assert.Equals(subClass, tabButton.GetPropertyValue("label"));
            hashAfter = (string) driver.ExecuteScript("return location.hash;");
            Assert.Equals("#qx.ui.Basic.Atom", hashAfter);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void toggleDetail()
        public virtual void toggleDetail()
        {
            string detailHeadlinePath = "//div[contains(@class, 'info-panel')]/descendant::div[contains(@class, 'item-detail-headline')]";
            try
            {
                driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
                Assert.True("Constructor details should be hidden initially!", false);
            }
            catch (NoSuchElementException)
            {
            }

            WebElement constructorDetailToggle = driver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'info-panel')]/descendant::td[contains(@class, 'toggle')]/img"));
            constructorDetailToggle.Click();
            WebElement detailHeadline = driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
            Assert.True(detailHeadline.Displayed);

            constructorDetailToggle.Click();
            try
            {
                detailHeadline = driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
                Assert.True("Constructor details could not be hidden!", false);
            }
            catch (NoSuchElementException)
            {
            }
        }

    }

}