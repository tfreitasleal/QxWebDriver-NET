using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopApiViewer
{
    [TestFixture]
    public class ClassViewer : DesktopApiViewer
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            DesktopApiViewer.SetUpBeforeClass();
            string className = "qx.ui.core.Widget";
            SelectClass(className);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUpBeforeTest()
        [SetUp]
        public virtual void SetUpBeforeTest()
        {
            string className = "qx.ui.core.Widget";
            SelectClass(className);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void links()
        [Test]
        public virtual void Links()
        {
            string internalTarget = "#capture";
            IWebElement internalLink =
                Driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + internalTarget + "']"));
            internalLink.Click();
            string hashAfter = (string) Driver.ExecuteScript("return location.hash;");
            Assert.Equals("#qx.ui.core.Widget~capture", hashAfter);

            string subClass = "qx.ui.basic.Atom";
            IWebElement subClassLink = Driver.FindElement(OpenQA.Selenium.By.XPath("//a[text()='" + subClass + "']"));
            subClassLink.Click();

            IWidget tabButton = Driver.FindWidget(By.Qxh(TabButtonPath));
            Assert.Equals(subClass, tabButton.GetPropertyValue("label"));
            hashAfter = (string) Driver.ExecuteScript("return location.hash;");
            Assert.Equals("#qx.ui.basic.Atom", hashAfter);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void toggleDetail()
        [Test]
        public virtual void ToggleDetail()
        {
            string detailHeadlinePath =
                "//div[contains(@class, 'info-panel')]/descendant::div[contains(@class, 'item-detail-headline')]";
            try
            {
                Driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
                Assert.True(false, "Constructor details should be hidden initially!");
            }
            catch (NoSuchElementException)
            {
            }

            IWebElement constructorDetailToggle =
                Driver.FindElement(OpenQA.Selenium.By.XPath(
                    "//div[contains(@class, 'info-panel')]/descendant::td[contains(@class, 'toggle')]/img"));
            constructorDetailToggle.Click();
            IWebElement detailHeadline = Driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
            Assert.True(detailHeadline.Displayed);

            constructorDetailToggle.Click();
            try
            {
                detailHeadline = Driver.FindElement(OpenQA.Selenium.By.XPath(detailHeadlinePath));
                Assert.True(false, "Constructor details could not be hidden!");
            }
            catch (NoSuchElementException)
            {
            }
        }
    }
}