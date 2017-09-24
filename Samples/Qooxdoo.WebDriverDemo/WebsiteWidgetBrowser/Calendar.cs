using System;
using System.Threading;
using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using IJavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Calendar : WebsiteWidgetBrowser
    {
        protected internal string[] monthNamesDefault = new string[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
        protected internal string[] monthNamesCustom = new string[] {"Jan", "Feb", "Mär", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dez"};
        protected internal static int month;
        protected internal static int year;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Calendar");

            DateTime date = DateTime.Now;
            DateTime cal = new DateTime();
            cal = new DateTime(date);
            year = cal.Year;
            month = cal.Month;
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void calendarDefault() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void CalendarDefault()
        {
            TestCalendar("calendar-default");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void calendarCustom() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void CalendarCustom()
        {
            TestCalendar("calendar-custom");
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCalendar(String id) throws InterruptedException
        public virtual void TestCalendar(string id)
        {
            By calHeaderLoc = OpenQA.Selenium.By.XPath("descendant::td[contains(@class, 'qx-calendar-month')]");
            By prevMonthLoc = OpenQA.Selenium.By.XPath("descendant::button[contains(@class, 'qx-calendar-prev')]");
            By nextMonthLoc = OpenQA.Selenium.By.XPath("descendant::button[contains(@class, 'qx-calendar-next')]");

            string[] monthNames;
            if (id.Contains("custom"))
            {
                monthNames = monthNamesCustom;
            }
            else
            {
                monthNames = monthNamesDefault;
            }

            WebElement calendar = webDriver.FindElement(By.Id(id));
            WebElement calHeader = calendar.FindElement(calHeaderLoc);
            Assert.Equals(monthNames[month] + " " + year, calHeader.Text);

            string getValue = "return qxWeb('#" + id + "').getValue()";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            string valueBefore = (string) exec.ExecuteScript(getValue);

            WebElement prevMonth = calendar.FindElement(prevMonthLoc);
            prevMonth.Click();
            // refresh the elements because the calendar re-renders itself if the displayed month changes
            calendar = webDriver.FindElement(By.Id(id));
            calHeader = calendar.FindElement(calHeaderLoc);

            int prevMonthIdx;
            int prevYear = year;
            if (month == 0)
            {
                prevMonthIdx = 11;
                prevYear = prevYear - 1;
            }
            else
            {
                prevMonthIdx = month - 1;
            }
            string prevMonthName = monthNames[prevMonthIdx];
            Assert.Equals(prevMonthName + " " + prevYear, calHeader.Text);

            WebElement nextMonth = calendar.FindElement(nextMonthLoc);
            nextMonth.Click();
            nextMonth = calendar.FindElement(nextMonthLoc);
            nextMonth.Click();
            // refresh the elements because the calendar re-renders itself if the displayed month changes
            calendar = webDriver.FindElement(By.Id(id));
            calHeader = calendar.FindElement(calHeaderLoc);

            int nextMonthIdx;
            int nextYear = year;
            if (month == 11)
            {
                nextMonthIdx = 0;
                nextYear = nextYear + 1;
            }
            else
            {
                nextMonthIdx = month + 1;
            }
            string nextMonthName = monthNames[nextMonthIdx];
            Assert.Equals(nextMonthName + " " + nextYear, calHeader.Text);

            WebElement day = calendar.FindElement(OpenQA.Selenium.By.XPath("descendant::button[contains(@class, 'qx-calendar-day') and text() = '17']"));
            day.Click();
            Thread.Sleep(250);
            string getDateString = getValue + ".toString()";
            string valueAfter = (string) exec.ExecuteScript(getDateString);
            Assert.AreNotEqual(valueBefore, valueAfter);
            string nextMonthNameEn = monthNamesDefault[nextMonthIdx];
            nextMonthNameEn = nextMonthNameEn.Substring(0, 3);
            Console.WriteLine("valueAfter " + valueAfter);
            Console.WriteLine(" " + nextMonthNameEn + " 17 " + nextYear);
            Assert.True(valueAfter.Contains(" " + nextMonthNameEn + " 17 " + nextYear));
        }
    }

}