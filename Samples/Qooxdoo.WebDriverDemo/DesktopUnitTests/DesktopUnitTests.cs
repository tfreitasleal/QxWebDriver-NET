using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Qooxdoo.WebDriver.Resources;
using By = Qooxdoo.WebDriver.By;

/*using JSONArray = org.json.simple.JSONArray;
using JSONObject = org.json.simple.JSONObject;
using JSONParser = org.json.simple.parser.JSONParser;
using ParseException = org.json.simple.parser.ParseException;*/

namespace Qooxdoo.WebDriverDemo.DesktopUnitTests
{
    [TestFixture]
    public class DesktopUnitTests : IntegrationTest
    {
        public static string getTestSuiteState = "return qx.core.Init.getApplication().runner.getTestSuiteState();";

        public static string getTestResults =
            "return JSON.stringify(qx.core.Init.getApplication().runner.view.getTestResults());";

        public static IList<string> testPackages;

        protected internal int? failCount = 0;

        public static Func<IWebDriver, bool?> TestSuiteStateIs(string state)
        {
            return driver =>
            {
                string result = null;
                try
                {
                    result = SuiteState;
                    return result.Equals(state);
                }
                catch (WebDriverException e)
                {
                    Console.Error.WriteLine("Couldn't get test suite state: " + e.ToString());
                    return false;
                }
            };
        }

        /*public static ExpectedCondition<Boolean> testSuiteStateIs(final String state) {
            return new ExpectedCondition<Boolean>() {
                @Override
                public Boolean apply(WebDriver driver) {
                    String result = null;
                    try {
                        result = getSuiteState();
                        return result.equals(state);
                    } catch(org.openqa.selenium.WebDriverException e) {
                        System.err.println("Couldn't get test suite state: " + e.toString());
                        return false;
                    }
                }

                @Override
                public String toString() {
                    return "Test suite state is '" + state + "'.";
                }
            };
        }*/

        public static string SuiteState
        {
            get
            {
                string suiteState = (string) Driver.JsExecutor.ExecuteScript(getTestSuiteState);
                return suiteState;
            }
        }

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Driver = Configuration.QxWebDriver;
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until(TestSuiteStateIs("ready"));
            string resPath = "/javascript/getTestPackages.js";
            JavaScript.Instance.AddResource("getTestPackages", resPath);

            // comma-separated list of test packages to be split up into sub-packages
            string splitPackages = SystemProperties.GetProperty("org.qooxdoo.demo.unittests.packages.split", "");
            // comma-separated list of test packages to skip
            string skipPackages = SystemProperties.GetProperty("org.qooxdoo.demo.unittests.packages.skip", "");

            testPackages = (IList<string>) Driver.JsRunner.RunScript("getTestPackages", splitPackages, skipPackages);
            Console.WriteLine("Test packages: " + testPackages);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void unitTests()
        [Test]
        public virtual void UnitTests()
        {
            long totalTime = 0;
            IEnumerator<string> itr = testPackages.GetEnumerator();
            while (itr.MoveNext())
            {
                string nextPackage = itr.Current;
                DateTime packageStart = DateTime.Now;
                RunPackage(nextPackage);

                DateTime packageEnd = DateTime.Now;
                long diff = packageEnd.Ticks - packageStart.Ticks;
                totalTime = totalTime + diff;
                //long seconds = TimeUnit.MILLISECONDS.toSeconds(diff);
                var seconds = TimeSpan.FromMilliseconds(diff).TotalSeconds;
                if (seconds == 1)
                {
                    Console.WriteLine("Finished in " + diff + " ms.");
                }
                else
                {
                    Console.WriteLine("Finished in ~" + seconds + " s.");
                }

                GetResults();
                LogAutExceptions();
            }
            long seconds2 = (totalTime / 1000) % 60;
            long minutes = (totalTime / (1000 * 60)) % 60;
            Console.WriteLine("All test packages completed in " + minutes + "m " + seconds2 + "s.");
            Assert.AreEqual(Convert.ToDouble(0), Convert.ToDouble(failCount), failCount + " test(s) failed.");
        }

        public virtual void RunPackage(string packageName)
        {
            string packageUrl = SystemProperties.GetProperty("org.qooxdoo.demo.auturl") + "?testclass=" + packageName;
            Console.WriteLine("Executing test package " + packageName);
            Driver.Url = packageUrl;
            Driver.RegisterGlobalErrorHandler();
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until(TestSuiteStateIs("ready"));

            try
            {
                IWebElement runButton = Driver.FindElement(By.Id("run"));
                Console.WriteLine("Clicking run button");
                runButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Calling run()");
                Driver.ExecuteScript("qx.core.Init.getApplication().runner.view.run()");
            }

            new WebDriverWait(Driver, TimeSpan.FromSeconds(600)).Until(TestSuiteStateIs("finished"));
        }

        public virtual void GetResults()
        {
            Console.WriteLine("Retrieving package results.");
            string results = (string) Driver.ExecuteScript(getTestResults);
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

        public virtual void LogAutExceptions()
        {
            // Print AUT exceptions
            IList<string> caughtErrors = Driver.CaughtErrors;
            IEnumerator<string> exItr = caughtErrors.GetEnumerator();
            while (exItr.MoveNext())
            {
                Console.Error.WriteLine(exItr.Current);
            }
        }

        [OneTimeTearDown]
        public new static void TearDownAfterClass()
        {
            Driver.Quit();
        }
    }
}