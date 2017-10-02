using System.Collections.Generic;
using NUnit.Framework;
using Wisej.WebDriver.UI;
using By = Wisej.WebDriver.By;

namespace Wisej.WebDriverDemo.WidgetBrowser
{
    [TestFixture]
    public class ListIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("List");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void list()
        [Test]
        public virtual void List()
        {
            By listLocator = By.Qxh("*/qx.ui.form.List");
            ISelectable list = (ISelectable) tabPage.FindWidget(listLocator);

            string label1 = "Engert, Othmar";
            // use the ISelectable interface to scroll the list until
            // the desired item is visible before clicking it
            list.SelectItem(label1);

            IList<IWidget> selection = list.GetWidgetListFromProperty("selection");
            // check if an item was selected
            Assert.Equals(1, selection.Count);
            // compare labels to check if the correct item was selected
            IWidget selected = selection[0];
            string selectedLabel = (string) selected.GetPropertyValue("label");
            Assert.Equals(label1, selectedLabel);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void virtualList()
        [Test]
        public virtual void VirtualList()
        {
            // For the virtual list, we must use the ISelectable interface. It will
            // scroll the tree until it finds an item with a matching label, then
            // Click it.
            string label1 = "Pauls, Gernfried";
            By virtualListLocator = By.Qxh("*/qx.ui.List.List");
            ISelectable vlist = (ISelectable) tabPage.FindWidget(virtualListLocator);
            vlist.SelectItem(label1);

            IList<string> selection = (IList<string>) vlist.GetPropertyValue("selection");
            Assert.Equals(1, selection.Count);
            string selected = selection[0];
            //the Virtual List's model items don't override toString, so selected is
            //something like qx.data.model.firstname"lastname[2314-0] :(
            //Assert.Equals(label1, selected);

            By groupedVirtualListLocator = By.Qxh("widgetbrowser.pages.List/qx.ui.container.Composite/child[5]");
            ISelectable gvlist = (ISelectable) tabPage.FindWidget(groupedVirtualListLocator);
            gvlist.SelectItem("Fritz, Katrein");

            selection = (IList<string>) vlist.GetPropertyValue("selection");
            Assert.Equals(1, selection.Count);
            selected = selection[0];
        }
    }
}