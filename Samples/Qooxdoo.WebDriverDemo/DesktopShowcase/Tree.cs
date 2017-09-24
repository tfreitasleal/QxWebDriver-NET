using Assert = NUnit.Framework.Assert;
//using Before = NUnit.Framework.Before;
//using Test = NUnit.Framework.Test;
using By = Qooxdoo.WebDriver.By;
using Widget = Qooxdoo.WebDriver.UI.IWidget;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    public class Tree : DesktopShowcase
    {

        public By treeLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/qx.ui.tree.Tree");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void setUp()
        {
            SelectPage("Tree");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tree()
        public virtual void tree()
        {
            WebElement treeEl = Root.FindElement(treeLocator);
            Widget tree = driver.GetWidgetForElement(treeEl);
            Assert.True(tree.Displayed);
        }
    }

}