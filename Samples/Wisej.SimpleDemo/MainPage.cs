using System;
using Wisej.Web;

namespace Wisej.SimpleDemo
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void openWindow_Click(object sender, EventArgs e)
        {
            var firstWindow = new FirstWindow();
            firstWindow.ShowDialog();
        }
    }
}