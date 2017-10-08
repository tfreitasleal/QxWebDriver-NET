using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class Languages : DesktopShowcase
    {
        public WebDriver.By buttonGroupLocator =
                WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/qx.ui.form.RadioButtonGroup");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Languages");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void languages()
        [Test]
        public virtual void TestLanguages()
        {
            IWebElement buttonGroupEl = Root.FindElement(buttonGroupLocator);
            IWidget buttonGroup = Driver.GetWidgetForElement(buttonGroupEl);
            Assert.True(buttonGroup.Displayed);
        }
    }
}