using NUnit.Framework;
using OpenQA.Selenium;
using Qooxdoo.WebDriver.UI;

namespace Qooxdoo.WebDriverDemo.DesktopShowcase
{
    [TestFixture]
    public class Form : DesktopShowcase
    {
        public WebDriver.By formLocator = WebDriver.By.Qxh("qx.ui.container.Stack/qx.ui.container.Composite/qxc.application.formdemo.FormItems");

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp() throws Exception
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        [SetUp]
        public virtual void SetUp()
        {
            SelectPage("Form");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
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