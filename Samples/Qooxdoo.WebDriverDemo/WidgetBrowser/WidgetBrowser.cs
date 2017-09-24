namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;

    public abstract class WidgetBrowser : IntegrationTest
    {

        internal static Widget tabPage;

        /// <summary>
        /// Clicks a button in the Widget Browser's main tab bar
        /// </summary>
        public static void selectTab(string title)
        {
            string locator = "qx.ui.container.Composite/qx.ui.container.Scroll/qx.ui.tabview.TabView";
            Selectable tabView = (Selectable) driver.FindWidget(By.Qxh(locator));
            tabView.SelectItem(title);

            string tabPageLocator = "qx.ui.tabview.Page";
            tabPage = tabView.FindWidget(By.Qxh(tabPageLocator));
        }
    }

}