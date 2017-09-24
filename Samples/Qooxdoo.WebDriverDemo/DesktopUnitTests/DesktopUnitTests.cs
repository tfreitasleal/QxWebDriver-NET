using System;
using System.Collections;
using System.Collections.Generic;
using Qooxdoo.WebDriver;
using JSONArray = org.json.simple.JSONArray;
using JSONObject = org.json.simple.JSONObject;
using JSONParser = org.json.simple.parser.JSONParser;
using ParseException = org.json.simple.parser.ParseException;
//using AfterClass = NUnit.Framework.AfterClass;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using JavaScript = Qooxdoo.WebDriver.Resources.JavaScript;
using NoSuchElementException = OpenQA.Selenium.NoSuchElementException;
using WebDriver = OpenQA.Selenium.IWebDriver;
using WebElement = OpenQA.Selenium.IWebElement;
using ExpectedCondition = OpenQA.Selenium.Support.UI.ExpectedCondition;
using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;

namespace Qooxdoo.WebDriverDemo.DesktopUnitTests
{
    public class DesktopUnitTests : IntegrationTest
    {

        public static string getTestSuiteState = "return qx.core.Init.getApplication().runner.getTestSuiteState();";
        public static string getTestResults = "return JSON.stringify(qx.core.Init.getApplication().runner.view.getTestResults());";

        public static IList<string> testPackages;

        protected internal int? failCount = 0;

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public static OpenQA.Selenium.Support.UI.ExpectedCondition<Nullable<bool>> testSuiteStateIs(final String state)
        public static ExpectedCondition<bool?> testSuiteStateIs(string state)
        {
            return new ExpectedConditionAnonymousInnerClass(state);
        }

        private class ExpectedConditionAnonymousInnerClass : ExpectedCondition<bool?>
        {
            private string state;

            public ExpectedConditionAnonymousInnerClass(string state)
            {
                this.state = state;
            }

            public override bool? Apply(WebDriver driver)
            {
                string result = null;
                try
                {
                    result = SuiteState;
                    return result.Equals(state);
                }
                catch (OpenQA.Selenium.WebDriverException e)
                {
                    Console.Error.WriteLine("Couldn't get test suite state: " + e.ToString());
                    return false;
                }
            }

            public override string ToString()
            {
                return "Test suite state is '" + state + "'.";
            }
        }

        public static string SuiteState
        {
            get
            {
                string suiteState = (string) driver.JsExecutor.ExecuteScript(getTestSuiteState);
                return suiteState;
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            driver = Configuration.QxWebDriver;
            driver.Url = System.getProperty("org.qooxdoo.demo.auturl");
            (new WebDriverWait(driver, 30, 250)).Until(testSuiteStateIs("ready"));
            string resPath = "/javascript/getTestPackages.js";
            JavaScript.Instance.AddResource("getTestPackages", resPath);

            // comma-separated list of test packages to be split up into sub-packages
            string splitPackages = System.getProperty("org.qooxdoo.demo.unittests.packages.split", "");
            // comma-separated list of test packages to skip
            string skipPackages = System.getProperty("org.qooxdoo.demo.unittests.packages.skip", "");

            testPackages = (IList<string>) driver.JsRunner.RunScript("getTestPackages", splitPackages, skipPackages);
            Console.WriteLine("Test packages: " + testPackages);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void unitTests()
        public virtual void unitTests()
        {
            long totalTime = 0;
            IEnumerator<string> itr = testPackages.GetEnumerator();
            while (itr.MoveNext())
            {
                string nextPackage = itr.Current;
                DateTime packageStart = DateTime.Now;
                runPackage(nextPackage);

                DateTime packageEnd = DateTime.Now;
                long diff = packageEnd.Ticks - packageStart.Ticks;
                totalTime = totalTime + diff;
                long seconds = TimeUnit.MILLISECONDS.toSeconds(diff);
                if (seconds == 1)
                {
                    Console.WriteLine("Finished in " + diff + " ms.");
                }
                else
                {
                    Console.WriteLine("Finished in ~" + seconds + " s.");
                }

                Results;
                logAutExceptions();
            }
            long seconds = (totalTime / 1000) % 60;
            long minutes = (totalTime / (1000 * 60)) % 60;
            Console.WriteLine("All test packages completed in " + minutes + "m " + seconds + "s.");
            Assert.Equals(failCount + " test(s) failed.", Convert.ToDouble(0), Convert.ToDouble(failCount));
        }

        public virtual void runPackage(string packageName)
        {
            string packageUrl = System.getProperty("org.qooxdoo.demo.auturl") + "?testclass=" + packageName;
            Console.WriteLine("Executing test package " + packageName);
            driver.get(packageUrl);
            driver.registerGlobalErrorHandler();
            (new WebDriverWait(driver, 30, 250)).Until(testSuiteStateIs("ready"));

            try
            {
                WebElement runButton = driver.FindElement(By.Id("run"));
                Console.WriteLine("Clicking run button");
                runButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Calling run()");
                driver.ExecuteScript("qx.core.Init.getApplication().runner.view.run()");
            }

            (new WebDriverWait(driver, 600, 250)).Until(testSuiteStateIs("finished"));
        }

        public virtual void getResults()
        {
            Console.WriteLine("Retrieving package results.");
            string results = (string) driver.ExecuteScript(getTestResults);
            JSONParser parser = new JSONParser();
            object obj;
            try
            {
                obj = parser.parse(results);
                JSONObject jsonEntry = (JSONObject) obj;
                ISet set = jsonEntry.Keys;
                IEnumerator itr = set.GetEnumerator();
                while (itr.MoveNext())
                {
                    string testName = (string) itr.Current;
                    JSONObject testResult = (JSONObject) jsonEntry.get(testName);
                    string state = (string) testResult.get("state");
                    if (state.Equals("error") || state.Equals("failure"))
                    {
                        failCount++;
                        Console.Error.WriteLine(state.ToUpper() + " " + testName);
                        JSONArray messages = (JSONArray) testResult.get("messages");
                        IEnumerator<string> mItr = messages.GetEnumerator();
                        while (mItr.MoveNext())
                        {
                            string message = mItr.Current;
                            Console.Error.WriteLine(message);
                        }
                        Console.Error.WriteLine();
                    }
                }
            }
            catch (ParseException e)
            {
                Console.Error.WriteLine("Unable to parse JSON test results " + results);
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        public virtual void logAutExceptions()
        {
            // Print AUT exceptions
            IList<string> caughtErrors = (IList<string>) driver.CaughtErrors;
            IEnumerator<string> exItr = caughtErrors.GetEnumerator();
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
            driver.quit();
        }

    }

}