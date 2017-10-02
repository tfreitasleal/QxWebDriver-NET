using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using By = OpenQA.Selenium.By;

namespace Qooxdoo.WebDriverDemo.WebsiteWidgetBrowser
{
    [TestFixture]
    public class Rating : WebsiteWidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WebsiteWidgetBrowser.SetUpBeforeClass();
            SelectTab("Rating");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void ratingDefault()
        [Test]
        public virtual void RatingDefault()
        {
            IWebElement rating = webDriver.FindElement(By.Id("rating-default"));
            TesteRating(rating);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void ratingLength()
        [Test]
        public virtual void RatingLength()
        {
            IWebElement rating = webDriver.FindElement(By.Id("rating-length"));
            TesteRating(rating);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void ratingNote()
        [Test]
        public virtual void RatingNote()
        {
            IWebElement rating = webDriver.FindElement(By.Id("rating-note"));
            TesteRating(rating);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void ratingHeart()
        [Test]
        public virtual void RatingHeart()
        {
            IWebElement rating = webDriver.FindElement(By.XPath("//div[contains(@class, 'qx-rating-heart')]"));
            TesteRating(rating);
        }

        public virtual void TesteRating(IWebElement rating)
        {
            IList<IWebElement> items =
                rating.FindElements(By.XPath("descendant::*[contains(@class, 'qx-rating-item')]"));
            IWebElement lastItem = items[items.Count - 1];
            lastItem.Click();

            string getValue = "return qxWeb(arguments[0]).getValue();";
            IJavaScriptExecutor exec = (IJavaScriptExecutor) webDriver;
            long? valueMax = (long?) exec.ExecuteScript(getValue, rating);

            Assert.Equals(items.Count, valueMax);

            IWebElement firstItem = items[0];
            firstItem.Click();

            long? valueMin = (long?) exec.ExecuteScript(getValue, rating);
            Assert.Equals(1, valueMin);
        }
    }
}