using System;
using System.Drawing;
using Wisej.Web;

namespace Wisej.SimpleDemo
{
    public partial class ButtonsWindow : Form
    {
        public ButtonsWindow()
        {
            InitializeComponent();
        }

        private void customerEditor_Click(object sender, EventArgs e)
        {
            var customerEditorWindow = new CustomerEditor();
            customerEditorWindow.ShowDialog(this);
        }

        private void supplierEditor_Click(object sender, EventArgs e)
        {
            AlertBox.Show("Supplier Editor isn't implemented", MessageBoxIcon.Error, true, ContentAlignment.BottomRight, 10000);
        }
    }
}
