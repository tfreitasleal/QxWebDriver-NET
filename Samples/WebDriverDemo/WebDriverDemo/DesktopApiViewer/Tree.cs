using NUnit.Framework;
using Qooxdoo.WebDriver;
using Qooxdoo.WebDriver.UI;
using Qooxdoo.WebDriver.UI.Tree.Core;

namespace Wisej.Qooxdoo.WebDriverDemo.DesktopApiViewer
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Tree : DesktopApiViewer
    {
        //ORIGINAL LINE: @Test public void tree()
        [Test]
        public virtual void TestTree()
        {
            SelectView("Content");

            ISelectable tree = (ISelectable) Driver.FindWidget(By.Qxh("*/apiviewer.ui.PackageTree"));
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

            IWidget tabButton = Driver.FindWidget(By.Qxh(TabButtonPath));
            Assert.Equals("qx.bom.element.Dimension", tabButton.GetPropertyValue("label"));

            string hash = (string) Driver.ExecuteScript("return location.hash");
            Assert.Equals("#qx.bom.element.Dimension", hash);
        }
    }
}