using System;
using System.ComponentModel;
using Wisej.SimpleDemo.Model;
using Wisej.Web;

namespace Wisej.SimpleDemo.View
{
    public partial class CustomerEditor : Form
    {
        private Customer _customer;

        public CustomerEditor()
        {
            InitializeComponent();
        }

        private void DataGridBinding_Load(object sender, System.EventArgs e)
        {
            // Bind ComboBox list datasources first
            statesbindingSource.EnumToDataSource(typeof(States));
            state.DataSource = statesbindingSource;
            state.DisplayMember = "Description";
            state.ValueMember = "Key";

            // AutoCompleteCustomSource isn't supported on Wisej
            //state.AutoCompleteCustomSource.AddRange(EnumExtension.EnumToArray(typeof(States)));

            customerListBindingSource.DataSource = CustomerList.GetCustomerList();
            dataGridView.Rows[0].Selected = true;
        }

        private void newButton_Click(object sender, System.EventArgs e)
        {
            GetNewCustomer();
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            _customer.Save();
            dataGridView.Rows[dataGridView.Rows.Count - 1].Selected = true;
        }

        private void removeButton_Click(object sender, System.EventArgs e)
        {
            _customer.Delete();
        }

        private void GetNewCustomer()
        {
            _customer = new Customer();
            customerBindingSource.DataSource = _customer;
        }

        private void dataGridView_SelectionChanged(object sender, System.EventArgs e)
        {
            BindLine();
        }

        private void state_Validated(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                customerBindingSource.ResetBindings(false);
        }


        private void BindLine()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var id = (int) dataGridView.SelectedRows[0].Cells[0].Value;
                _customer = CustomerList.GetCustomer(id);
                customerBindingSource.DataSource = _customer;
            }
        }
    }
}