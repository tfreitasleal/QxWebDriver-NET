namespace Qooxdoo.WebDriverDemo.DesktopApiViewer
{

    using Assert = NUnit.Framework.Assert;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;
    using AbstractItem = Qooxdoo.WebDriver.UI.Tree.Core.AbstractItem;

    public class Tree : DesktopApiViewer
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void tree()
        public virtual void tree()
        {
            SelectView("Content");

            Selectable tree = (Selectable) driver.FindWidget(By.Qxh("*/apiviewer.ui.PackageTree"));
            Assert.True(tree.Displayed);

            string item1Label = "bom";
            AbstractItem item1 = (AbstractItem) tree.GetSelectableItem(item1Label);
            item1.Click();
            Assert.True(item1.Open);

            string item2Label = "element";
            AbstractItem item2 = (AbstractItem) tree.GetSelectableItem(item2Label);
            item2.Click();
            Assert.True(item2.Open);

            string item3Label = "Dimension";
            AbstractItem item3 = (AbstractItem) tree.GetSelectableItem(item3Label);
            item3.Click();

            Widget tabButton = driver.FindWidget(By.Qxh(tabButtonPath));
            Assert.Equals("qx.bom.element.Dimension", tabButton.GetPropertyValue("label"));

            string hash = (string) driver.ExecuteScript("return location.hash");
            Assert.Equals("#qx.bom.element.Dimension", hash);

        }
    }

}