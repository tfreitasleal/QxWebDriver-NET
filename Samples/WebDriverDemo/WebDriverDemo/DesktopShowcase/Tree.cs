using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Tree : DesktopShowcase
    {
        public By treeLocator =
                By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.window.Desktop/qx.ui.window.Window/qx.ui.tree.Tree");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Tree");
        }

        //ORIGINAL LINE: @Test public void tree()
        [Test]
        public virtual void TestTree()
        {
            IWebElement treeEl = Root.FindElement(treeLocator);
            IWidget tree = Driver.GetWidgetForElement(treeEl);
            Assert.True(tree.Displayed);
        }
    }
}