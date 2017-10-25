using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using LogEntry = Qooxdoo.WebDriver.Log.LogEntry;
using QxWebDriver = Qooxdoo.WebDriver.QxWebDriver;

namespace Wisej.Qooxdoo.WebDriverDemo
{
    public abstract class IntegrationTest
    {
        public static QxWebDriver Driver;

        //ORIGINAL LINE: @Rule public OnFailed ruleExample = new OnFailed();
        //[Rule]
        public OnFailed ruleExample = new OnFailed();

        public static void SetUpBeforeClass()
        {
            Driver = Configuration.QxWebDriver;
            Driver.Manage().Window.Maximize();
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            Driver.RegisterLogAppender();
            Driver.RegisterGlobalErrorHandler();
        }

        /// <summary>
        /// Prints the AUT's log messages
        /// </summary>
        public static void PrintQxLog(QxWebDriver driver)
        {
            IList<LogEntry> logEntries = driver.LogEvents;
            IEnumerator<LogEntry> logItr = logEntries.GetEnumerator();
            while (logItr.MoveNext())
            {
                Console.WriteLine(logItr.Current);
            }
        }

        /// <summary>
        /// Prints AUT exceptions
        /// </summary>
        public static void PrintQxErrors(QxWebDriver driver)
        {
            IList<string> caughtErrors = driver.CaughtErrors;
            IEnumerator exItr = caughtErrors.GetEnumerator();
            while (exItr.MoveNext())
            {
                Console.Error.WriteLine(exItr.Current);
            }
        }

        public static void TearDownAfterClass()
        {
            PrintQxLog(Driver);
            PrintQxErrors(Driver);
            Driver.Quit();
        }
    }

}