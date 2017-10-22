using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class DragDrop : DesktopShowcase
    {
        public By labelLocator = WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/[@value=Shop]");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Drag & Drop");
        }

        //ORIGINAL LINE: @Test public void dragDrop()
        [Test]
        public virtual void TestDragDrop()
        {
            IWebElement labelEl = Root.FindElement(labelLocator);
            IWidget label = Driver.GetWidgetForElement(labelEl);
            Assert.True(label.Displayed);
        }
    }
}