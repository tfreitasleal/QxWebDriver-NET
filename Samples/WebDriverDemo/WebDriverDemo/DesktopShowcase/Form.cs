using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Form : DesktopShowcase
    {
        public WebDriver.By formLocator = WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qxc.application.formdemo.FormItems");

        //ORIGINAL LINE: @Before public void setUp() throws Exception
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Form");
        }

        //ORIGINAL LINE: @Test public void form()
        [Test]
        public virtual void TextForm()
        {
            IWebElement formEl = Root.FindElement(formLocator);
            IWidget form = Driver.GetWidgetForElement(formEl);
            Assert.True(form.Displayed);
        }
    }
}