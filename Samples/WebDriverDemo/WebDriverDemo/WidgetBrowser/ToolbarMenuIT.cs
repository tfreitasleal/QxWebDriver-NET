using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver;
using Wisej.Qooxdoo.WebDriver.UI;

namespace Wisej.Qooxdoo.WebDriverDemo.WidgetBrowser
{
    //JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
    //    import static NUnit.Framework.Assert.False;

    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class ToolbarMenuIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("Toolbar.*");
        }

        //ORIGINAL LINE: @Test public void menu()
        [Test]
        public virtual void Menu()
        {
            By menuLocator = By.Qxh("*/qx.ui.menu.Menu");
            ISelectable menu = (ISelectable) tabPage.FindWidget(menuLocator);
            IWidget menuRadioButton = menu.GetSelectableItem("Menu RadioButton");
            bool selectedBefore = ((bool?) menuRadioButton.GetPropertyValue("value")).Value;
            menu.SelectItem("Menu RadioButton");
            bool selectedAfter = ((bool?) menuRadioButton.GetPropertyValue("value")).Value;
            Assert.False(selectedBefore == selectedAfter);
        }

        //ORIGINAL LINE: @Test public void splitButton()
        [Test]
        public virtual void SplitButton()
        {
            By splitButtonLocator = By.Qxh("*/qx.ui.toolbar.SplitButton");
            IWidget tbarSplit = tabPage.FindWidget(splitButtonLocator);
            tbarSplit.GetChildControl("arrow").Click();
            ISelectable splitMenu = (ISelectable) tbarSplit.GetWidgetFromProperty("menu");
            splitMenu.SelectItem(0);
        }

        //ORIGINAL LINE: @Test public void menuButton()
        [Test]
        public virtual void MenuButton()
        {
            By menuButtonLocator = By.Qxh("*/[@label=MenuButton]");
            ISelectable tbarMenuButton = (ISelectable) tabPage.FindWidget(menuButtonLocator);

            // Must Click the button so the menu is rendered before we can check if
            // the MenuRadioButton is selected
            tbarMenuButton.Click();
            ISelectable buttonMenu = (ISelectable) tbarMenuButton.GetWidgetFromProperty("menu");
            IWidget buttonMenuRadioButton = buttonMenu.GetSelectableItem("Menu RadioButton");
            bool selectedBefore = ((bool?) buttonMenuRadioButton.GetPropertyValue("value")).Value;
            tbarMenuButton.Click();
            tbarMenuButton.SelectItem("Menu RadioButton");
            bool selectedAfter = ((bool?) buttonMenuRadioButton.GetPropertyValue("value")).Value;
            Assert.False(selectedBefore == selectedAfter);
        }
    }
}