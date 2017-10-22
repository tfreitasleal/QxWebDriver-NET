using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class MiscIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("Misc");
        }

        //ORIGINAL LINE: @Test public void dragDrop()
        [Test]
        public virtual void DragDrop()
        {
            By parentLocator = By.Qxh("*/[@classname=widgetbrowser.pages.Misc]/qx.ui.container.Composite/child[9]");
            IWidget parentContainer = tabPage.FindWidget(parentLocator);

            By sourceLocator = By.Qxh("child[0]");
            ISelectable dragFrom = (ISelectable) parentContainer.FindWidget(sourceLocator);

            By targetLocator = By.Qxh("child[1]");
            ISelectable dragTo = (ISelectable) parentContainer.FindWidget(targetLocator);

            // get an item from the source list
            string label = "Item 4";
            IWidget item = dragFrom.GetSelectableItem(label);

            // drag the item to the target list
            item.DragToWidget(dragTo);

            // check if the item was removed from the source
            Assert.IsNull(dragFrom.GetSelectableItem(label));

            // check if the item was added to the target
            Assert.IsNotNull(dragTo.GetSelectableItem(label));
        }
    }
}