namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class DragDrop : DesktopShowcase
    {

        public By labelLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/[@value=Shop]");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUp()
        {
            SelectPage("Drag & Drop");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void dragDrop()
        public virtual void dragDrop()
        {
            WebElement labelEl = Root.FindElement(labelLocator);
            Widget label = driver.getWidgetForElement(labelEl);
            Assert.True(label.Displayed);
        }
    }

}