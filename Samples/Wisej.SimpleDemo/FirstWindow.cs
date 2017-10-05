using System;
using Wisej.Web;

namespace Wisej.SimpleDemo
{
    public partial class FirstWindow : Form
    {
        public FirstWindow()
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
