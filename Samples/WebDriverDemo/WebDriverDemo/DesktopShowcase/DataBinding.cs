using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class DataBinding : DesktopShowcase
    {
        public Qooxdoo.WebDriver.By demoLocator = Qooxdoo.WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qxc.application.datademo.Demo");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Data Binding");
        }

        //ORIGINAL LINE: @Test public void dataBinding()
        [Test]
        public virtual void TestDataBinding()
        {
            IWebElement demoEl = Root.FindElement(demoLocator);
            IWidget demo = Driver.GetWidgetForElement(demoEl);
            Assert.True(demo.Displayed);
        }
    }
}