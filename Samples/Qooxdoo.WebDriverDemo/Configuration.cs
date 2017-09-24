using System;

namespace Qooxdoo.WebDriverDemo
{
    using SelendroidDriver = io.selendroid.SelendroidDriver;

    using QxWebDriver = Qooxdoo.WebDriver.QxWebDriver;
    using Platform = OpenQA.Selenium.Platform;
    using WebDriver = OpenQA.Selenium.IWebDriver;
    using ChromeDriver = OpenQA.Selenium.Chrome.ChromeDriver;
    using FirefoxDriver = OpenQA.Selenium.Firefox.FirefoxDriver;
    using DesiredCapabilities = OpenQA.Selenium.Remote.DesiredCapabilities;
    using RemoteWebDriver = OpenQA.Selenium.Remote.RemoteWebDriver;

    public class Configuration
    {

        protected internal static DesiredCapabilities getCapabilities(string browserName)
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
            /*else if (browserName.Equals("ipad"))
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
                capabilities = DesiredCapabilities.Firefox()();
            }*/

            return capabilities;
        }

        protected internal static Platform getPlatform(string platformName)
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
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static OpenQA.Selenium.IWebDriver getWebDriver() throws Exception
        public static WebDriver WebDriver
        {
            get
            {
                WebDriver webDriver;
                string hubUrl = System.getProperty("org.qooxdoo.demo.huburl");
                string browserName = System.getProperty("org.qooxdoo.demo.browsername", "firefox");
                string browserVersion = System.getProperty("org.qooxdoo.demo.browserversion");
                string platformName = System.getProperty("org.qooxdoo.demo.platform", "any");

                if (string.ReferenceEquals(hubUrl, null))
                {
                    if (browserName.Equals("chrome"))
                    {
                        webDriver = new ChromeDriver();
                    }
                    else if (browserName.Equals("android"))
                    {
                        DesiredCapabilities caps = SelendroidCapabilities.android();
                        webDriver = new SelendroidDriver(caps);
                    }
                    else
                    {
                        webDriver = new FirefoxDriver();
                    }
                }
                else
                {
                    DesiredCapabilities browser = getCapabilities(browserName);
                    if (!string.ReferenceEquals(browserVersion, null))
                    {
                        browser.Version = browserVersion;
                    }
                    browser.Platform = getPlatform(platformName);
                    webDriver = new RemoteWebDriver(new URL(hubUrl), browser);
                }
                return webDriver;
            }
        }

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static QxWebDriver.QxWebDriver getQxWebDriver() throws Exception
        public static QxWebDriver QxWebDriver
        {
            get
            {
                WebDriver webDriver = WebDriver;
                QxWebDriver qxWebDriver = new QxWebDriver(webDriver);
                return qxWebDriver;
            }
        }
    }

}