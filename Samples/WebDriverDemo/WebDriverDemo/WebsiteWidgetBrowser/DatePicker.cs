using NUnit.Framework;
using OpenQA.Selenium;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class DatePicker : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Date Picker");
        }

        //ORIGINAL LINE: @Test public void openClose()
        [Test]
        public virtual void OpenClose()
        {
            string calendarPath = "//div[contains(@class, 'qx-calendar')]";
            IWebElement picker = webDriver.FindElement(By.Id("datepicker-default"));
            string valueBefore = picker.GetAttribute("value");

            IWebElement calendar = webDriver.FindElement(OpenQA.Selenium.By.XPath(calendarPath));
            Assert.False(calendar.Displayed);
            picker.Click();

            Assert.True(calendar.Displayed);

            IWebElement header = webDriver.FindElement(OpenQA.Selenium.By.XPath("//h1"));
            header.Click();
            Assert.False(calendar.Displayed);

            picker.Click();
            IWebElement nextMonth = calendar.FindElement(OpenQA.Selenium.By.XPath("descendant::button[@class='qx-calendar-next']"));
            nextMonth.Click();
            Assert.True(calendar.Displayed);

            IWebElement day = calendar.FindElement(OpenQA.Selenium.By.XPath("descendant::button[text()='4']"));
            day.Click();
            Assert.False(calendar.Displayed);

            string valueAfter = picker.GetAttribute("value");
            Assert.AreNotEqual(valueBefore, valueAfter);
        }
    }
}