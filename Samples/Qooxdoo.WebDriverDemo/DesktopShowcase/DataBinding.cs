namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class DataBinding : DesktopShowcase
    {

        public By demoLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qxc.application.datademo.Demo");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUp()
        {
            SelectPage("Data Binding");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void dataBinding()
        public virtual void dataBinding()
        {
            WebElement demoEl = Root.FindElement(demoLocator);
            Widget demo = driver.getWidgetForElement(demoEl);
            Assert.True(demo.Displayed);
        }
    }

}