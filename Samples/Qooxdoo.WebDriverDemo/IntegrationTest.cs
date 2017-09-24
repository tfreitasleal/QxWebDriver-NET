using System;
using System.Collections;
using System.Collections.Generic;
using QxWebDriver = Qooxdoo.WebDriver.QxWebDriver;
using LogEntry = Qooxdoo.WebDriver.Log.LogEntry;

namespace Qooxdoo.WebDriverDemo
{
    public abstract class IntegrationTest
    {

        public static QxWebDriver driver;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Rule public OnFailed ruleExample = new OnFailed();
        public OnFailed ruleExample = new OnFailed();

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            driver = Configuration.QxWebDriver;
            driver.Manage().Window().Maximize();
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            driver.RegisterLogAppender();
            driver.RegisterGlobalErrorHandler();
        }

        /// <summary>
        /// Prints the AUT's log messages
        /// </summary>
        public static void printQxLog(QxWebDriver driver)
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
        public static void printQxErrors(QxWebDriver driver)
        {
            IList<string> caughtErrors = (IList<string>) driver.CaughtErrors;
            IEnumerator exItr = caughtErrors.GetEnumerator();
            while (exItr.MoveNext())
            {
                Console.Error.WriteLine(exItr.Current);
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @AfterClass public static void tearDownAfterClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void tearDownAfterClass()
        {
            printQxLog(driver);
            printQxErrors(driver);
            driver.quit();
        }
    }

}