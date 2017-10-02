using Wisej.WebDriver.UI;

namespace Wisej.WebDriverDemo.WidgetBrowser
{
    public abstract class WidgetBrowser : IntegrationTest
    {
        internal static IWidget tabPage;

        /// <summary>
        /// Clicks a button in the Widget Browser's main tab bar
        /// </summary>
        public static void SelectTab(string title)
        {
            string locator = "qx.ui.container.Composite/qx.ui.container.Scroll/qx.ui.tabview.TabView";
            ISelectable tabView = (ISelectable) Driver.FindWidget(WebDriver.By.Qxh(locator));
            tabView.SelectItem(title);

            string tabPageLocator = "qx.ui.tabview.Page";
            tabPage = tabView.FindWidget(WebDriver.By.Qxh(tabPageLocator));
        }
    }
}