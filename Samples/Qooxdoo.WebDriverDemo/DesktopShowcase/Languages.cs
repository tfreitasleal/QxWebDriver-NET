namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class Languages : DesktopShowcase
    {

        public By buttonGroupLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/qx.ui.form.RadioButtonGroup");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUp()
        {
            SelectPage("Languages");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void languages()
        public virtual void languages()
        {
            WebElement buttonGroupEl = Root.FindElement(buttonGroupLocator);
            Widget buttonGroup = driver.getWidgetForElement(buttonGroupEl);
            Assert.True(buttonGroup.Displayed);
        }
    }

}