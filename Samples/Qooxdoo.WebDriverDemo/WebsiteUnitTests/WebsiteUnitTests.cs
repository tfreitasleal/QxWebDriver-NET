using System.Collections.Generic;
//using BeforeClass = NUnit.Framework.BeforeClass;
using JavaScript = Qooxdoo.WebDriver.Resources.JavaScript;
using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;
using DesktopUnitTests = Qooxdoo.WebDriverDemo.DesktopUnitTests.DesktopUnitTests;

namespace Qooxdoo.WebDriverDemo.websiteunittests
{
    public class WebsiteUnitTests : DesktopUnitTests
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            driver = Configuration.QxWebDriver;
            driver.get(System.getProperty("org.qooxdoo.demo.auturl"));
            (new WebDriverWait(driver, 30, 250)).Until(testSuiteStateIs("ready"));
            string resPath = "/javascript/getTestClasses.js";
            JavaScript.Instance.AddResource("getTestClasses", resPath);
            testPackages = (IList<string>) driver.jsRunner.runScript("getTestClasses");

            string skipPackages = System.getProperty("org.qooxdoo.demo.unittests.packages.skip", "");
            string[] skipPackagesArray = skipPackages.Split(",", true);
//JAVA TO C# CONVERTER WARNING: Unlike Java's ListIterator, enumerators in .NET do not allow altering the collection:
            for (IEnumerator<string> iter = testPackages.GetEnumerator(); iter.MoveNext();)
            {
                string availablePackage = iter.Current;
                foreach (string skipPackage in skipPackagesArray)
                {
                    if (availablePackage.Equals(skipPackage))
                    {
//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
                        iter.Remove();
                    }
                }
            }
        }
    }

}