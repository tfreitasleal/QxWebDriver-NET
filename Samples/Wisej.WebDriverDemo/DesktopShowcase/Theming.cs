using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.WebDriver.UI;

namespace Wisej.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class Theming : DesktopShowcase
    {
        public WebDriver.By calculatorLocator =
                WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/showcase.page.theme.calc.view.Calculator");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Theming");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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