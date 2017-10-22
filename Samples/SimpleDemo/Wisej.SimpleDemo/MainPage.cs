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
            var listWindow = new ListWindow();
            listWindow.ShowDialog();
        }

        private void sayGoodBye_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Do you want to say good-bye now?", "Polite Question", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
                Application.Exit();
        }
    }
}