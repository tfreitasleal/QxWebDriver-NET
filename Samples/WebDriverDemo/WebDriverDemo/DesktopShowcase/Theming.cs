using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Theming : DesktopShowcase
    {
        public WebDriver.By calculatorLocator =
                WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/showcase.page.theme.calc.view.Calculator");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Theming");
        }

        //ORIGINAL LINE: @Test public void theming()
        [Test]
        public virtual void TesTheming()
        {
            IWebElement calcEl = Root.FindElement(calculatorLocator);
            IWidget calc = Driver.GetWidgetForElement(calcEl);
            Assert.True(calc.Displayed);
        }
    }
}