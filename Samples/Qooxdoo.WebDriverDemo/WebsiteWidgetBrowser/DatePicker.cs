using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class DatePicker : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Date Picker");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void openClose()
        public virtual void OpenClose()
        {
            string calendarPath = "//div[contains(@class, 'qx-calendar')]";
            WebElement picker = webDriver.FindElement(By.Id("datepicker-default"));
            string valueBefore = picker.GetAttribute("value");

            WebElement calendar = webDriver.FindElement(OpenQA.Selenium.By.XPath(calendarPath));
            Assert.False(calendar.Displayed);
            picker.Click();

            Assert.True(calendar.Displayed);

            WebElement header = webDriver.FindElement(OpenQA.Selenium.By.XPath("//h1"));
            header.Click();
            Assert.False(calendar.Displayed);

            picker.Click();
            WebElement nextMonth = calendar.FindElement(OpenQA.Selenium.By.XPath("descendant::button[@class='qx-calendar-next']"));
            nextMonth.Click();
            Assert.True(calendar.Displayed);

            WebElement day = calendar.FindElement(OpenQA.Selenium.By.XPath("descendant::button[text()='4']"));
            day.Click();
            Assert.False(calendar.Displayed);

            string valueAfter = picker.GetAttribute("value");
            Assert.AreNotEqual(valueBefore, valueAfter);
        }
    }

}