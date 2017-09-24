namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class Theming : DesktopShowcase
    {

        public By calculatorLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/showcase.page.theme.calc.view.Calculator");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUp()
        {
            SelectPage("Theming");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void theming()
        public virtual void theming()
        {
            WebElement calcEl = Root.FindElement(calculatorLocator);
            Widget calc = driver.GetWidgetForElement(calcEl);
            Assert.True(calc.Displayed);
        }
    }

}