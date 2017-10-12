using OpenQA.Selenium;

namespace SimpleDemo.MSTest
{
    public enum TestUri
    {
        None,
        ApiViewer,
        Wisej
    }

    public static class HandleTestUri
    {
        public static void SetUri(IWebDriver driver, TestUri testUri)
        {
            switch (testUri)
            {
                case TestUri.ApiViewer:
                    driver.Url = "http://www.qooxdoo.org/current/api/index.html";
                    break;
                case TestUri.Wisej:
                    driver.Url = "http://localhost:16461/";
                    break;
            }
        }
    }
}