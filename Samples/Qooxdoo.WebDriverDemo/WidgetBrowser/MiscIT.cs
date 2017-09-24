using NUnit.Framework;

namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.IsNotNull;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.IsNull;

    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;

    public class MiscIT : WidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WidgetBrowser.setUpBeforeClass();
            selectTab("Misc");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void dragDrop()
        public virtual void DragDrop()
        {
            By parentLocator = By.Qxh("*/[@classname=widgetbrowser.pages.Misc]/qx.ui.container.Composite/child[9]");
            Widget parentContainer = tabPage.FindWidget(parentLocator);

            By sourceLocator = By.Qxh("child[0]");
            Selectable dragFrom = (Selectable) parentContainer.FindWidget(sourceLocator);

            By targetLocator = By.Qxh("child[1]");
            Selectable dragTo = (Selectable) parentContainer.FindWidget(targetLocator);

            // get an item from the source list
            string label = "Item 4";
            Widget item = dragFrom.GetSelectableItem(label);

            // drag the item to the target list
            item.DragToWidget(dragTo);

            // check if the item was removed from the source
            Assert.IsNull(dragFrom.GetSelectableItem(label));

            // check if the item was added to the target
            Assert.IsNotNull(dragTo.GetSelectableItem(label));
        }
    }

}