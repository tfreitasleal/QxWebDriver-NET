using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using WidgetImpl = Wisej.Qooxdoo.WebDriver.UI.Mobile.Core.WidgetImpl;

namespace Wisej.Qooxdoo.WebDriverDemo.MobileShowcase
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Events : Mobileshowcase
    {
        protected internal static string GetEvents = "return [].map.call(qxWeb('.pointers .event'), function(el) { return el.innerHTML })";

        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            Mobileshowcase.SetUpBeforeClass();
            ScrollTo(0, 5000);
            Thread.Sleep(500);
            SelectItem("Events");
            VerifyTitle("Events");
        }

        //ORIGINAL LINE: @Before public void init() throws InterruptedException
        [SetUp]
        public virtual void Init()
        {
            Thread.Sleep(250);
        }

        //ORIGINAL LINE: @Test public void swipe() throws InterruptedException
        [Test]
        public virtual void Swipe()
        {
            if (!(Driver.WebDriver is IHasTouchScreen))
            {
                return;
            }

            WidgetImpl area = (WidgetImpl) Driver.FindWidget(By.XPath("//div[contains(@class, 'container-touch-area')]"));
            area.Track(500, 0, 25);
            Thread.Sleep(500);
            IList<string> eventNames = (IList<string>) Driver.ExecuteScript(GetEvents);
            if (eventNames.Count != 4)
            {
                LogEvents("swipe", eventNames);
            }
            Assert.Equals(4, eventNames.Count);
            Assert.Equals("pointerdown", eventNames[0]);
            Assert.Equals("pointermove", eventNames[1]);
            Assert.Equals("pointerup", eventNames[2]);
            Assert.Equals("swipe", eventNames[3]);
        }

        //ORIGINAL LINE: @Test public void tap() throws InterruptedException
        [Test]
        public virtual void Tap()
        {
            WidgetImpl area = (WidgetImpl) Driver.FindWidget(By.XPath("//div[contains(@class, 'container-touch-area')]"));
            area.Tap();
            Thread.Sleep(500);
            IList<string> eventNames = (IList<string>) Driver.ExecuteScript(GetEvents);
            if (eventNames.Count != 3)
            {
                LogEvents("tap", eventNames);
            }
            Assert.Equals(3, eventNames.Count);
            Assert.Equals("pointerdown", eventNames[0]);
            Assert.Equals("pointerup", eventNames[1]);
            Assert.Equals("tap", eventNames[2]);
        }

        //ORIGINAL LINE: @Test public void longtap() throws InterruptedException
        [Test]
        public virtual void Longtap()
        {
            WidgetImpl area = (WidgetImpl) Driver.FindWidget(By.XPath("//div[contains(@class, 'container-touch-area')]"));
            area.Longtap();
            Thread.Sleep(500);
            IList<string> eventNames = (IList<string>) Driver.ExecuteScript(GetEvents);

            if (eventNames.Count != 4)
            {
                LogEvents("longtap", eventNames);
            }
            Assert.Equals(4, eventNames.Count);
            Assert.Equals("pointerdown", eventNames[0]);
            Assert.Equals("longtap", eventNames[1]);
            Assert.Equals("pointermove", eventNames[2]);
            Assert.Equals("pointerup", eventNames[3]);
        }

        protected internal virtual void LogEvents(string testedEvent, IList<string> eventNames)
        {
            Console.Error.WriteLine(testedEvent + " events:");
            IEnumerator<string> itr = eventNames.GetEnumerator();
            while (itr.MoveNext())
            {
                Console.Error.WriteLine(itr.Current);
            }
        }
    }
}