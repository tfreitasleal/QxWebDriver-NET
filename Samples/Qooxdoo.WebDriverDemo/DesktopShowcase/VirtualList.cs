namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{

    using Assert = NUnit.Framework.Assert;
    //using Before = NUnit.Framework.Before;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using WebElement = OpenQA.Selenium.IWebElement;

    public class VirtualList : DesktopShowcase
    {

        public By RosterLocator = By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/showcase.page.virtuallist.messenger.Roster");

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Before public void setUp() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void SetUp()
        {
            SelectPage("Virtual List");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void virtualList()
        public virtual void virtualList()
        {
            WebElement rosterEl = Root.FindElement(RosterLocator);
            Widget roster = driver.GetWidgetForElement(rosterEl);
            Assert.True(roster.Displayed);
        }
    }

}