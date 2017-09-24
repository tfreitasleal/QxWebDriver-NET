using System.Collections.Generic;
using NUnit.Framework;
using Qooxdoo.WebDriver;

namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.Equals;

    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using AbstractItem = Qooxdoo.WebDriver.UI.Tree.Core.AbstractItem;

    public class TreeIT : WidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WidgetBrowser.setUpBeforeClass();
            selectTab("Tree");
        }

        protected internal virtual void TreeTestCommon(string treeLocator)
        {
            Selectable tree = (Selectable) tabPage.FindWidget(By.Qxh(treeLocator));

            //By item1Locator = By.Qxh("*/[@label=" + item1LabelExpected + "]");
            //TreeItem item1 = (TreeItem) tree.FindWidget(item1Locator);

            string item1LabelExpected = "Desktop";
            AbstractItem item1 = (AbstractItem) tree.GetSelectableItem(item1LabelExpected);

            item1.Click();
            if (item1.Open)
            {
                item1.clickOpenCloseButton();
            }

            string item2LabelExpected = "Inbox";
            AbstractItem item2 = (AbstractItem) tree.GetSelectableItem(item2LabelExpected);
            if (!item2.Open)
            {
                item2.clickOpenCloseButton();
            }

            string item3LabelExpected = "Trash";
            AbstractItem item3 = (AbstractItem) tree.GetSelectableItem(item3LabelExpected);
            item3.clickOpenCloseButton();

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
                IList<Widget> selection = (IList<Widget>) tree.GetWidgetListFromProperty("selection");
                Assert.Equals(1, selection.Count);
                Widget selected = selection[0];
                string selectedLabel = (string) selected.GetPropertyValue("label");
                Assert.Equals(item4LabelExpected, selectedLabel);
            }
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tree()
        public virtual void Tree()
        {
            TreeTestCommon("*/qx.ui.tree.Tree");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void virtualTree()
        public virtual void VirtualTree()
        {
            TreeTestCommon("*/qx.ui.tree.VirtualTree");
        }

    }

}