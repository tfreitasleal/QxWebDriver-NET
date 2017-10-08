using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class VirtualList : DesktopShowcase
    {
        public By RosterLocator =
                By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/showcase.page.virtuallist.messenger.Roster");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Virtual List");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void virtualList()
        [Test]
        public virtual void TestVirtualList()
        {
            IWebElement rosterEl = Root.FindElement(RosterLocator);
            IWidget roster = Driver.GetWidgetForElement(rosterEl);
            Assert.True(roster.Displayed);
        }
    }
}