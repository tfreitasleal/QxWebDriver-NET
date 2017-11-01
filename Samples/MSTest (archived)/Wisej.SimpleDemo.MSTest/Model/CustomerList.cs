using System.ComponentModel;

namespace Wisej.SimpleDemo.Model
{
    public class CustomerList : BindingList<Customer>
    {
        private static CustomerList _instance;

        public static CustomerList GetCustomerList()
        {
            if (_instance == null)
                BuildInstance();

            return _instance;
        }

        public static bool Contains(int customerId)
        {
            foreach (var customer in _instance)
            {
                if (customer.CustomerId == customerId)
                    return true;
            }

            return false;
        }

        public static Customer GetCustomer(int customerId)
        {
            foreach (var customer in _instance)
            {
                if (customer.CustomerId == customerId)
                    return customer;
            }

            return null;
        }

        private static void BuildInstance()
        {
            _instance = new CustomerList
            {
                new Customer
                {
                    FirstName = "Muddy",
                    LastName = "Waters",
                    State = States.MS
                },
                new Customer
                {
                    FirstName = "Louis",
                    LastName = "Armstrong",
                    State = States.LA
                }
            };
        }
    }
}