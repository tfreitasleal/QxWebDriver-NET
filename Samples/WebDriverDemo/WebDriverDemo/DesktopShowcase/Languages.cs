using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Languages : DesktopShowcase
    {
        public WebDriver.By buttonGroupLocator =
                WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/qx.ui.form.RadioButtonGroup");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Languages");
        }

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