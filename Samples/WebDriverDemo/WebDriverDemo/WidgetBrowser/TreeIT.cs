using System.Collections.Generic;
using NUnit.Framework;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Tree.Core;

namespace Wisej.Qooxdoo.WebDriverDemo.WidgetBrowser
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class TreeIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("Tree");
        }

        protected internal virtual void TreeTestCommon(string treeLocator)
        {
            ISelectable tree = (ISelectable) tabPage.FindWidget(By.Qxh(treeLocator));

            //By item1Locator = By.Qxh("*/[@label=" + item1LabelExpected + "]");
            //TreeItem item1 = (TreeItem) tree.FindWidget(item1Locator);

            string item1LabelExpected = "Desktop";
            AbstractItem item1 = (AbstractItem) tree.GetSelectableItem(item1LabelExpected);

            item1.Click();
            if (item1.Open)
            {
                item1.ClickOpenCloseButton();
            }

            string item2LabelExpected = "Inbox";
            AbstractItem item2 = (AbstractItem) tree.GetSelectableItem(item2LabelExpected);
            if (!item2.Open)
            {
                item2.ClickOpenCloseButton();
            }

            string item3LabelExpected = "Trash";
            AbstractItem item3 = (AbstractItem) tree.GetSelectableItem(item3LabelExpected);
            item3.ClickOpenCloseButton();

            string item4LabelExpected = "Junk #12";
            tree.SelectItem(item4LabelExpected);

            if (treeLocator.Contains("VirtualTree"))
            {
                // The VirtualTree's selection is a DataArray of model objects
                IList<string> selection = (IList<string>) tree.GetPropertyValue("selection");
                Assert.Equals(1, selection.Count);
                // The model objects used by the Widget Browser's VirtualTree don't
                // have a readable string representation so we can't verify the
                // selection
            }
            else
            {
                // The regular Tree's selection is an Array of Widgets
                IList<IWidget> selection = (IList<IWidget>) tree.GetWidgetListFromProperty("selection");
                Assert.Equals(1, selection.Count);
                IWidget selected = selection[0];
                string selectedLabel = (string) selected.GetPropertyValue("label");
                Assert.Equals(item4LabelExpected, selectedLabel);
            }
        }

        //ORIGINAL LINE: @Test public void tree()
        [Test]
        public virtual void Tree()
        {
            TreeTestCommon("*/qx.ui.tree.Tree");
        }

        //ORIGINAL LINE: @Test public void virtualTree()
        [Test]
        public virtual void VirtualTree()
        {
            TreeTestCommon("*/qx.ui.tree.VirtualTree");
        }
    }
}