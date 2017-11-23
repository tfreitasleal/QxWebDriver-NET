using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;
using By = Qooxdoo.WebDriver.By;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class VirtualList : DesktopShowcase
    {
        public By RosterLocator =
                By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/showcase.page.virtuallist.messenger.Roster");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Virtual List");
        }

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