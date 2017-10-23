using System;
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
            var customerEditor = new CustomerEditor();
            customerEditor.ShowDialog(this);
        }
    }
}
