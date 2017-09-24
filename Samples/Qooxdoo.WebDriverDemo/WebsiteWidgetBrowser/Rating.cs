using System.Collections.Generic;
using Qooxdoo.WebDriver;
using Assert = NUnit.Framework.Assert;
//using BeforeClass = NUnit.Framework.BeforeClass;
//using Test = NUnit.Framework.Test;
using IJavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using WebElement = OpenQA.Selenium.IWebElement;

namespace Qooxdoo.WebDriverDemo.websitewidgetbrowser
{
    public class Rating : WebsiteWidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            WebsiteWidgetBrowser.setUpBeforeClass();
            selectTab("Rating");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void ratingDefault()
        public virtual void ratingDefault()
        {
            WebElement rating = webDriver.FindElement(By.Id("rating-default"));
            rating(rating);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void ratingLength()
        public virtual void ratingLength()
        {
            WebElement rating = webDriver.FindElement(By.Id("rating-length"));
            rating(rating);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void ratingNote()
        public virtual void ratingNote()
        {
            WebElement rating = webDriver.FindElement(By.Id("rating-note"));
            rating(rating);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void ratingHeart()
        public virtual void ratingHeart()
        {
            WebElement rating = webDriver.FindElement(OpenQA.Selenium.By.XPath("//div[contains(@class, 'qx-rating-heart')]"));
            rating(rating);
        }

        public virtual void rating(WebElement rating)
        {
            IList<WebElement> items = rating.FindElements(OpenQA.Selenium.By.XPath("descendant::*[contains(@class, 'qx-rating-item')]"));
            WebElement lastItem = items[items.Count - 1];
            lastItem.Click();

            string getValue = "return qxWeb(arguments[0]).getValue();";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            long? valueMax = (long?) exec.ExecuteScript(getValue, rating);

            Assert.Equals(new long?(items.Count), valueMax);

            WebElement firstItem = items[0];
            firstItem.Click();

            long? valueMin = (long?) exec.ExecuteScript(getValue, rating);
            Assert.Equals(new long?(1), valueMin);
        }
    }

}