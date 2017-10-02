using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.WebDriver.UI;

namespace Wisej.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class DragDrop : DesktopShowcase
    {
        public By labelLocator = WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qx.ui.container.Composite/[@value=Shop]");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Drag & Drop");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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