namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.False;

    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using Assert = NUnit.Framework.Assert;

    public class ToolbarMenuIT : WidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void setUpBeforeClass()
        {
            WidgetBrowser.setUpBeforeClass();
            selectTab("Toolbar.*");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void menu()
        public virtual void menu()
        {
            By menuLocator = By.Qxh("*/qx.ui.menu.Menu");
            Selectable menu = (Selectable) tabPage.FindWidget(menuLocator);
            Widget menuRadioButton = menu.GetSelectableItem("Menu RadioButton");
            bool selectedBefore = (bool?) menuRadioButton.GetPropertyValue("value").Value;
            menu.SelectItem("Menu RadioButton");
            bool selectedAfter = (bool?) menuRadioButton.GetPropertyValue("value").Value;
            Assert.False(selectedBefore == selectedAfter);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void splitButton()
        public virtual void splitButton()
        {
            By splitButtonLocator = By.Qxh("*/qx.ui.toolbar.SplitButton");
            Widget tbarSplit = tabPage.FindWidget(splitButtonLocator);
            tbarSplit.GetChildControl("arrow").Click();
            Selectable splitMenu = (Selectable) tbarSplit.GetWidgetFromProperty("menu");
            splitMenu.SelectItem(0);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void menuButton()
        public virtual void menuButton()
        {
            By menuButtonLocator = By.Qxh("*/[@label=MenuButton]");
            Selectable tbarMenuButton = (Selectable) tabPage.FindWidget(menuButtonLocator);

            // Must Click the button so the menu is rendered before we can check if
            // the MenuRadioButton is selected
            tbarMenuButton.Click();
            Selectable buttonMenu = (Selectable) tbarMenuButton.GetWidgetFromProperty("menu");
            Widget buttonMenuRadioButton = buttonMenu.GetSelectableItem("Menu RadioButton");
            bool selectedBefore = (bool?) buttonMenuRadioButton.GetPropertyValue("value").Value;
            tbarMenuButton.Click();
            tbarMenuButton.SelectItem("Menu RadioButton");
            bool selectedAfter = (bool?) buttonMenuRadioButton.GetPropertyValue("value").Value;
            Assert.False(selectedBefore == selectedAfter);
        }

    }

}