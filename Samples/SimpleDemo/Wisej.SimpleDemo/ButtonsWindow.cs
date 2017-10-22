using System;
using Wisej.Web;

namespace Wisej.SimpleDemo
{
    public partial class ListWindow : Form
    {
        public ListWindow()
        {
            InitializeComponent();
        }

        private void openWindow_Click(object sender, EventArgs e)
        {
            var secondWindow = new SecondWindow();
            secondWindow.ShowDialog(this);
        }
    }
}
