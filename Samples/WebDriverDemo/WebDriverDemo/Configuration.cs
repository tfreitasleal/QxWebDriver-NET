using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Qooxdoo.WebDriver;

//using SelendroidDriver = io.selendroid.SelendroidDriver;

namespace Wisej.Qooxdoo.WebDriverDemo
{
    public class Configuration
    {
        /*protected internal static DesiredCapabilities GetCapabilities(string browserName)
        {
            DesiredCapabilities capabilities = null;

            if (browserName.Equals("chrome"))
            {
                capabilities = DesiredCapabilities.Chrome();
            }
            else if (browserName.Equals("ie") || browserName.Contains("explorer"))
            {
                capabilities = DesiredCapabilities.InternetExplorer();
            }
            else if (browserName.Equals("opera"))
            {
                capabilities = DesiredCapabilities.Opera();
            }
            else if (browserName.Equals("safari"))
            {
                capabilities = DesiredCapabilities.Safari();
            }
            else if (browserName.Equals("android"))
            {
                capabilities = DesiredCapabilities.Android();
            }
            else if (browserName.Equals("ipad"))
            {
                capabilities = DesiredCapabilities.IPad()();
            }
            else if (browserName.Equals("iphone"))
            {
                capabilities = DesiredCapabilities.IPhone()();
            }
            else if (browserName.Equals("phantomjs"))
            {
                capabilities = DesiredCapabilities.PhantomJS()();
            }
            else
            {
                capabilities = DesiredCapabilities.Firefox();
            }

            return capabilities;
        }

        protected internal static Platform GetPlatform(string platformName)
        {
            Platform platform = null;
            if (platformName.Equals("linux"))
            {
                platform = Platform.LINUX;
            }
            else if (platformName.Equals("mac"))
            {
                platform = Platform.MAC;
            }
            else if (platformName.Equals("xp"))
            {
                platform = Platform.XP;
            }
            else if (platformName.Equals("win7"))
            {
                platform = Platform.VISTA;
            }
            else if (platformName.Equals("win8"))
            {
                platform = Platform.WIN8;
            }
            else if (platformName.StartsWith("win", StringComparison.Ordinal))
            {
                platform = Platform.WINDOWS;
            }
            else if (platformName.Equals("android"))
            {
                platform = Platform.ANDROID;
            }
            else
            {
                platform = Platform.ANY;
            }

            return platform;
        }*/

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static OpenQA.Selenium.IWebDriver getWebDriver() throws Exception
        public static IWebDriver WebDriver
        {
            get
            {
                /*IWebDriver webDriver;
                string hubUrl = SystemProperties.GetProperty("org.qooxdoo.demo.huburl");
                string browserName = SystemProperties.GetProperty("org.qooxdoo.demo.browsername", "firefox");
                string browserVersion = SystemProperties.GetProperty("org.qooxdoo.demo.browserversion");
                //string platformName = SystemProperties.GetProperty("org.qooxdoo.demo.platform", "any");

                if (string.ReferenceEquals(hubUrl, null))
                {
                    if (browserName.Equals("chrome"))
                    {
                        webDriver = new ChromeDriver();
                    }
                    /*else if (browserName.Equals("android"))
                    {
                        DesiredCapabilities caps = SelendroidCapabilities.android();
                        webDriver = new SelendroidDriver(caps);
                    }#1#
                    else
                    {
                        webDriver = new FirefoxDriver();
                    }
                }
                else
                {
                    DesiredCapabilities browser = GetCapabilities(browserName);
                    if (!string.IsNullOrWhiteSpace(browserVersion))
                    {
                        browser.SetCapability(CapabilityType.Version, browserVersion);
                        //browser.Version = browserVersion;
                    }
                    browser.Platform = Platform.CurrentPlatform;
                    //browser.Platform = GetPlatform(platformName);
                    webDriver = new RemoteWebDriver(new Uri(hubUrl), browser);
                }*/
                return new ChromeDriver();
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static QxWebDriver.QxWebDriver getQxWebDriver() throws Exception
        public static QxWebDriver QxWebDriver
        {
            get
            {
                IWebDriver webDriver = WebDriver;
                QxWebDriver qxWebDriver = new QxWebDriver(webDriver);
                return qxWebDriver;
            }
        }
    }
}