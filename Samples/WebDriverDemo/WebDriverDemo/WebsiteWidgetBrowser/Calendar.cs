using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using By = OpenQA.Selenium.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Calendar : WebsiteWidgetBrowser
    {
        protected internal string[] monthNamesDefault =
        {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
            "November", "December"
        };

        protected internal string[] monthNamesCustom =
            {"Jan", "Feb", "Mär", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dez"};

        protected internal static int month;
        protected internal static int year;

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Calendar");

            DateTime date = DateTime.Now;
            year = date.Year;
            month = date.Month;
        }

        //ORIGINAL LINE: @Test public void calendarDefault() throws InterruptedException
        [Test]
        public virtual void CalendarDefault()
        {
            TestCalendar("calendar-default");
        }

        //ORIGINAL LINE: @Test public void calendarCustom() throws InterruptedException
        [Test]
        public virtual void CalendarCustom()
        {
            TestCalendar("calendar-custom");
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void testCalendar(String id) throws InterruptedException
        public virtual void TestCalendar(string id)
        {
            By calHeaderLoc = By.XPath("descendant::td[contains(@class, 'qx-calendar-month')]");
            By prevMonthLoc = By.XPath("descendant::button[contains(@class, 'qx-calendar-prev')]");
            By nextMonthLoc = By.XPath("descendant::button[contains(@class, 'qx-calendar-next')]");

            string[] monthNames;
            if (id.Contains("custom"))
            {
                monthNames = monthNamesCustom;
            }
            else
            {
                monthNames = monthNamesDefault;
            }

            IWebElement calendar = webDriver.FindElement(By.Id(id));
            IWebElement calHeader = calendar.FindElement(calHeaderLoc);
            Assert.Equals(monthNames[month] + " " + year, calHeader.Text);

            string getValue = "return qxWeb('#" + id + "').getValue()";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            string valueBefore = (string) exec.ExecuteScript(getValue);

            IWebElement prevMonth = calendar.FindElement(prevMonthLoc);
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

            IWebElement nextMonth = calendar.FindElement(nextMonthLoc);
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

            IWebElement day =
                calendar.FindElement(
                    By.XPath("descendant::button[contains(@class, 'qx-calendar-day') and text() = '17']"));
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