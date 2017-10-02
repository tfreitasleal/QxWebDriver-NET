using NUnit.Framework;
using OpenQA.Selenium;
using Wisej.WebDriver.UI;

namespace Wisej.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class DataBinding : DesktopShowcase
    {
        public Wisej.WebDriver.By demoLocator = Wisej.WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qxc.application.datademo.Demo");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Data Binding");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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