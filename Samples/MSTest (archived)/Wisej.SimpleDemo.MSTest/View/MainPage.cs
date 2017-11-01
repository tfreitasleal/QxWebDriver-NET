using System;
using System.Drawing;
using Wisej.Web;

namespace Wisej.SimpleDemo.View
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            AlertBox.Show("Place holder to show the application is present in the browser.", MessageBoxIcon.None, true,
                ContentAlignment.BottomRight, 120000);
        }

        private void buttonsWindow_Click(object sender, EventArgs e)
        {
            var window = new ButtonsWindow();
            window.ShowDialog();
        }

        private void sayGoodBye_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Do you want to say good-bye now?", "Polite Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
                Application.Exit();
        }
    }
}