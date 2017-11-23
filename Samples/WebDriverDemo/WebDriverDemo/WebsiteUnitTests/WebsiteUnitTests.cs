using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using Qooxdoo.WebDriver.Resources;

namespace Qooxdoo.WebDriverDemo.WebsiteUnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class WebsiteUnitTests : DesktopUnitTests.DesktopUnitTests
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Driver = Configuration.QxWebDriver;
            Driver.Url = SystemProperties.GetProperty("org.qooxdoo.demo.auturl");
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until(TestSuiteStateIs("ready"));
            string resPath = "/javascript/getTestClasses.js";
            JavaScript.Instance.AddResource("getTestClasses", resPath);
            testPackages = (IList<string>) Driver.JsRunner.RunScript("getTestClasses");

            string skipPackages = SystemProperties.GetProperty("org.qooxdoo.demo.unittests.packages.skip", "");
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
                        //iter.Remove();

                        // todo solve itr.Remove()
                    }
                }
            }
        }
    }
}