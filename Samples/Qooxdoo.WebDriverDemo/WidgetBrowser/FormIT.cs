using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Qooxdoo.WebDriverDemo.widgetbrowser
{

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.Equals;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.False;
//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//    import static NUnit.Framework.Assert.True;

    using Assert = NUnit.Framework.Assert;
    //using BeforeClass = NUnit.Framework.BeforeClass;
    //using Test = NUnit.Framework.Test;
    using By = Qooxdoo.WebDriver.By;
    using Selectable = Qooxdoo.WebDriver.UI.ISelectable;
    using Widget = Qooxdoo.WebDriver.UI.IWidget;

    public class FormIT : WidgetBrowser
    {

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @BeforeClass public static void setUpBeforeClass() throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public static void SetUpBeforeClass()
        {
            WidgetBrowser.setUpBeforeClass();
            selectTab("Form");
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void textField()
        public virtual void TextField()
        {
            string text = "HelloTextField";
            By textFieldLocator = By.Qxh("*/qx.ui.form.TextField");
            Widget textField = tabPage.FindWidget(textFieldLocator);
            textField.SendKeys(text);
            string value = (string) textField.GetPropertyValue("value");
            Assert.Equals(text, value);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void passwordField()
        public virtual void PasswordField()
        {
            string text = "HelloPasswordField";
            By passwordFieldLocator = By.Qxh("*/qx.ui.form.PasswordField");
            Widget passwordField = tabPage.FindWidget(passwordFieldLocator);
            passwordField.SendKeys(text);
            string value = (string) passwordField.GetPropertyValue("value");
            Assert.Equals(text, value);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void textArea()
        public virtual void TextArea()
        {
            string text = "Hello\nTextArea";
            By textAreaLocator = By.Qxh("*/qx.ui.form.TextArea");
            Widget textArea = tabPage.FindWidget(textAreaLocator);
            textArea.SendKeys(text);
            string value = (string) textArea.GetPropertyValue("value");

            Pattern regex = Pattern.compile("Hello.*?TextArea", Pattern.DOTALL);
            Matcher regexMatcher = regex.matcher(value);
            Assert.True(regexMatcher.matches());
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void comboBox()
        public virtual void ComboBox()
        {
            string item = "Item 4";
            By comboBoxLocator = By.Qxh("*/qx.ui.form.ComboBox");
            // The SelectItem clicks the ComboBox, then clicks an item in the popup list
            Selectable comboBox = (Selectable) tabPage.FindWidget(comboBoxLocator);
            comboBox.SelectItem(item);
            string value = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(item, value);

            string text = "Hello ComboBox";
            // clear is delegated to the TextField child control when using the
            // ISelectable interface
            comboBox.Clear();
            comboBox.SendKeys(text);
            value = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(text, value);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void virtualComboBox() throws InterruptedException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual void VirtualComboBox()
        {
            string item = "Item 14";
            By comboBoxLocator = By.Qxh("*/qx.ui.form.VirtualComboBox");
            // The SelectItem clicks the ComboBox, then clicks an item in the popup list
            Selectable comboBox = (Selectable) tabPage.FindWidget(comboBoxLocator);
            comboBox.SelectItem(item);
            string value = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(item, value);

            string text = "HelloVirtualComboBox";
            // clear is delegated to the TextField child control when using the
            // ISelectable interface
            comboBox.Clear();
            comboBox.SendKeys(text);
            // The value won't be updated until the box loses focus
            Widget repeatButton = tabPage.FindWidget(By.Qxh("*/qx.ui.form.RepeatButton"));
            repeatButton.Click();
            Thread.Sleep(500);
            string typedValue = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(text, typedValue);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void dateField()
        public virtual void DateField()
        {
            Widget dateField = tabPage.FindWidget(By.Qxh("*/qx.ui.form.DateField"));
            // No ISelectable implementation for DateField yet, so we use the childControls directly
            dateField.GetChildControl("button").Click();
            Widget list = dateField.WaitForChildControl("list", 2);
            list.GetChildControl("last-year-button").Click();
            list.GetChildControl("next-month-button").Click();
            list.FindWidget(By.Qxh("*/[@value=^12$]")).Click();

            string value = (string) dateField.GetPropertyValueAsJson("value");
            Assert.True(value.matches("\"?\\d{4}-\\d{2}-\\d{2}.*"));
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void selectBox()
        public virtual void SelectBox()
        {
            string item = "Item 4";
            Selectable selectBox = (Selectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.SelectBox"));
            selectBox.SelectItem(item);
            IList<Widget> selection = selectBox.GetWidgetListFromProperty("selection");
            Assert.Equals(1, selection.Count);
            Widget selected = selection[0];
            string selectedLabel = (string) selected.GetPropertyValue("label");
            Assert.Equals(item, selectedLabel);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void virtualSelectBox()
        public virtual void VirtualSelectBox()
        {
            string item = "Item 19";
            Selectable vSelectBox = (Selectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.VirtualSelectBox"));
            vSelectBox.SelectItem(item);
            IList<string> selection = (IList<string>) vSelectBox.GetPropertyValue("selection");
            Assert.Equals(1, selection.Count);
            string selected = selection[0];
            Assert.Equals(item, selected);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void list()
        public virtual void List()
        {
            string item = "Item 5";
            Selectable list = (Selectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.List"));
            list.SelectItem(item);
            IList<Widget> selection = list.GetWidgetListFromProperty("selection");
            Assert.Equals(1, selection.Count);
            Widget selected = selection[0];
            string selectedLabel = (string) selected.GetPropertyValue("label");
            Assert.Equals(item, selectedLabel);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void radioButtonGroup()
        public virtual void RadioButtonGroup()
        {
            By by = By.Qxh("*/[@label=RadioButton 2]");
            Widget radioButton = tabPage.FindWidget(by);
            radioButton.Click();
            Assert.True(radioButton.Selected);
        }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Test public void buttons()
        public virtual void Buttons()
        {
            Widget toggleButton = tabPage.FindWidget(By.Qxh("*/qx.ui.form.ToggleButton"));
            Assert.False(toggleButton.Selected);
            toggleButton.Click();
            Assert.True(toggleButton.Selected);

            Selectable menuButton = (Selectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.MenuButton"));
            menuButton.SelectItem("Button2");
        }
    }

}