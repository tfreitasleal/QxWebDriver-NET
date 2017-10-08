using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Wisej.Qooxdoo.WebDriver.UI;
using By = Wisej.Qooxdoo.WebDriver.By;

namespace Wisej.Qooxdoo.WebDriverDemo.WidgetBrowser
{
    [TestFixture]
    public class FormIT : WidgetBrowser
    {
        [OneTimeSetUp]
        public new static void SetUpBeforeClass()
        {
            WidgetBrowser.SetUpBeforeClass();
            SelectTab("Form");
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void textField()
        [Test]
        public virtual void TextField()
        {
            string text = "HelloTextField";
            By textFieldLocator = By.Qxh("*/qx.ui.form.TextField");
            IWidget textField = tabPage.FindWidget(textFieldLocator);
            textField.SendKeys(text);
            string value = (string) textField.GetPropertyValue("value");
            Assert.Equals(text, value);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void passwordField()
        [Test]
        public virtual void PasswordField()
        {
            string text = "HelloPasswordField";
            By passwordFieldLocator = By.Qxh("*/qx.ui.form.PasswordField");
            IWidget passwordField = tabPage.FindWidget(passwordFieldLocator);
            passwordField.SendKeys(text);
            string value = (string) passwordField.GetPropertyValue("value");
            Assert.Equals(text, value);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void textArea()
        [Test]
        public virtual void TextArea()
        {
            string text = "Hello\nTextArea";
            By textAreaLocator = By.Qxh("*/qx.ui.form.TextArea");
            IWidget textArea = tabPage.FindWidget(textAreaLocator);
            textArea.SendKeys(text);
            string value = (string) textArea.GetPropertyValue("value");

            // Java converted code
            // Pattern regex = Pattern.compile("Hello.*?TextArea", Pattern.DOTALL);
            // Matcher regexMatcher = regex.matcher(value);
            // Assert.True(regexMatcher.matches());

            Regex pattern = new Regex("Hello.*?TextArea", RegexOptions.Compiled | RegexOptions.Singleline);
            Assert.True(pattern.IsMatch(value));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void comboBox()
        [Test]
        public virtual void ComboBox()
        {
            string item = "Item 4";
            By comboBoxLocator = By.Qxh("*/qx.ui.form.ComboBox");
            // The SelectItem clicks the ComboBox, then clicks an item in the popup list
            ISelectable comboBox = (ISelectable) tabPage.FindWidget(comboBoxLocator);
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
        [Test]
        public virtual void VirtualComboBox()
        {
            string item = "Item 14";
            By comboBoxLocator = By.Qxh("*/qx.ui.form.VirtualComboBox");
            // The SelectItem clicks the ComboBox, then clicks an item in the popup list
            ISelectable comboBox = (ISelectable) tabPage.FindWidget(comboBoxLocator);
            comboBox.SelectItem(item);
            string value = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(item, value);

            string text = "HelloVirtualComboBox";
            // clear is delegated to the TextField child control when using the
            // ISelectable interface
            comboBox.Clear();
            comboBox.SendKeys(text);
            // The value won't be updated until the box loses focus
            IWidget repeatButton = tabPage.FindWidget(By.Qxh("*/qx.ui.form.RepeatButton"));
            repeatButton.Click();
            Thread.Sleep(500);
            string typedValue = (string) comboBox.GetPropertyValue("value");
            Assert.Equals(text, typedValue);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void dateField()
        [Test]
        public virtual void DateField()
        {
            IWidget dateField = tabPage.FindWidget(By.Qxh("*/qx.ui.form.DateField"));
            // No ISelectable implementation for DateField yet, so we use the childControls directly
            dateField.GetChildControl("button").Click();
            IWidget list = dateField.WaitForChildControl("list", 2);
            list.GetChildControl("last-year-button").Click();
            list.GetChildControl("next-month-button").Click();
            list.FindWidget(By.Qxh("*/[@value=^12$]")).Click();

            string value = dateField.GetPropertyValueAsJson("value");

            // Java converted code
            //Assert.True(value.matches("\"?\\d{4}-\\d{2}-\\d{2}.*"));

            Regex pattern = new Regex("\"?\\d{4}-\\d{2}-\\d{2}.*", RegexOptions.Compiled);
            Assert.True(pattern.IsMatch(value));
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void selectBox()
        [Test]
        public virtual void SelectBox()
        {
            string item = "Item 4";
            ISelectable selectBox = (ISelectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.SelectBox"));
            selectBox.SelectItem(item);
            IList<IWidget> selection = selectBox.GetWidgetListFromProperty("selection");
            Assert.Equals(1, selection.Count);
            IWidget selected = selection[0];
            string selectedLabel = (string) selected.GetPropertyValue("label");
            Assert.Equals(item, selectedLabel);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void virtualSelectBox()
        [Test]
        public virtual void VirtualSelectBox()
        {
            string item = "Item 19";
            ISelectable vSelectBox = (ISelectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.VirtualSelectBox"));
            vSelectBox.SelectItem(item);
            IList<string> selection = (IList<string>) vSelectBox.GetPropertyValue("selection");
            Assert.Equals(1, selection.Count);
            string selected = selection[0];
            Assert.Equals(item, selected);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void list()
        [Test]
        public virtual void List()
        {
            string item = "Item 5";
            ISelectable list = (ISelectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.List"));
            list.SelectItem(item);
            IList<IWidget> selection = list.GetWidgetListFromProperty("selection");
            Assert.Equals(1, selection.Count);
            IWidget selected = selection[0];
            string selectedLabel = (string) selected.GetPropertyValue("label");
            Assert.Equals(item, selectedLabel);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void radioButtonGroup()
        [Test]
        public virtual void RadioButtonGroup()
        {
            By by = By.Qxh("*/[@label=RadioButton 2]");
            IWidget radioButton = tabPage.FindWidget(by);
            radioButton.Click();
            Assert.True(radioButton.Selected);
        }

        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Test public void buttons()
        [Test]
        public virtual void Buttons()
        {
            IWidget toggleButton = tabPage.FindWidget(By.Qxh("*/qx.ui.form.ToggleButton"));
            Assert.False(toggleButton.Selected);
            toggleButton.Click();
            Assert.True(toggleButton.Selected);

            ISelectable menuButton = (ISelectable) tabPage.FindWidget(By.Qxh("*/qx.ui.form.MenuButton"));
            menuButton.SelectItem("Button2");
        }
    }
}