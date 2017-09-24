using System;

namespace Qooxdoo.WebDriverDemo
{
    using TestWatcher = NUnit.Framework.rules.TestWatcher;
    using Description = NUnit.Framework.runner.Description;
    using TakesScreenshot = OpenQA.Selenium.TakesScreenshot;

    public class OnFailed : TestWatcher
    {

        /// <summary>
        /// Takes a screenshot if a test fails.
        /// </summary>
        protected internal override void failed(Exception e, Description description)
        {
            string autName = System.getProperty("org.qooxdoo.demo.autname");
            string browserName = System.getProperty("org.qooxdoo.demo.browsername");
            string browserVersion = System.getProperty("org.qooxdoo.demo.browserversion");
            string platformName = System.getProperty("org.qooxdoo.demo.platform");
            long now = DateTimeHelperClass.CurrentUnixTimeMillis();
            string fileName = now.ToString() + " " + autName + " " + browserName + " " + browserVersion + " " + platformName + ".png";
            string tempDir = System.getProperty("java.io.tmpdir");
            string path = tempDir + "/" + fileName;

            File scrFile = ((TakesScreenshot)IntegrationTest.driver.WebDriver).getScreenshotAs(OutputType.FILE);

            try
            {
                FileUtils.copyFile(scrFile, new File(path));
            }
            catch (IOException e1)
            {
                Console.Error.WriteLine("Couldn't save screenshot: " + e1.Message);
                Console.WriteLine(e1.ToString());
                Console.Write(e1.StackTrace);
            }
            Console.WriteLine("Saved screenshot as " + path);
        }
    }
}