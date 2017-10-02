using System;
using System.IO;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TakesScreenshot = OpenQA.Selenium.Screenshot;

/*using TestWatcher = NUnit.Framework.Rules.TestWatcher;
using Description = NUnit.Framework.Runner.Description;*/

namespace Qooxdoo.WebDriverDemo
{
    public class OnFailed : ITestListener
    {
        /// <summary>
        /// Takes a screenshot if a test fails.
        /// </summary>
        //private void Failed(Exception e, string description)
        private void Failed(string description)
        {
            string autName = SystemProperties.GetProperty("org.qooxdoo.demo.autname");
            string browserName = SystemProperties.GetProperty("org.qooxdoo.demo.browsername");
            string browserVersion = SystemProperties.GetProperty("org.qooxdoo.demo.browserversion");
            string platformName = Platform.CurrentPlatform.PlatformType.ToString().ToLowerInvariant();
            long now = DateTimeHelperClass.CurrentUnixTimeMillis();
            string fileName = now + " " + autName + " " + browserName + " " + browserVersion + " " + platformName + ".png";
            string tempDir = SystemProperties.GetProperty("java.io.tmpdir");
            string path = tempDir + "/" + fileName;
            ((TakesScreenshot) IntegrationTest.Driver.WebDriver).SaveAsFile(fileName);

            try
            {
                File.Copy(fileName, path, true);
            }
            catch (IOException e1)
            {
                Console.Error.WriteLine("Couldn't save screenshot: " + e1.Message);
                Console.WriteLine(e1.ToString());
                Console.Write(e1.StackTrace);
            }
            Console.WriteLine("Saved screenshot as " + path);
        }

        public void TestStarted(ITest test)
        {
            //throw new NotImplementedException();
        }

        public void TestFinished(ITestResult result)
        {
            if (result.FailCount == 0)
                return;

            var description = string.Format("Test: {0}\r\n{1}\r\n{2}", result.Test.FullName, result.Name, result.Message);
            Failed((description));
        }

        public void TestOutput(TestOutput output)
        {
            //throw new NotImplementedException();
        }
    }
}