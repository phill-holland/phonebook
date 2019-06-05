using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phonebook.Customers;
using General;

namespace Phonebook
{
    namespace Services
    {
        public interface ICustomerService
        {
            IEnumerable<Customer> All();
            IEnumerable<Customers.Number> Get(int id);
            bool Activate(int customer_id, int number_id);
        };

        public class CustomerService : ICustomerService
        {
            protected List<Customers.Customer> customers = new List<Customer>();

            public CustomerService()
            {
                customers = new List<Customer>();

                Unique keys = new Unique();
                customers = Customers.Customers.Generate(keys).ToList();
            }

            public IEnumerable<Customer> All()
            {
                return customers;
            }

            public IEnumerable<Customers.Number> Get(int id)
            {
                var a = (from t1 in customers
                         where t1.CustomerID == id
                         select t1).FirstOrDefault();

                return a.Numbers;
            }

            public bool Activate(int customer_id, int number_id)
            {
                try
                {
                    var a = (from t1 in customers
                             where t1.CustomerID == customer_id                             
                             select t1).SingleOrDefault();

                    if (a != null)
                    {
                        var b = (from t1 in a.Numbers
                                 where t1.ID == number_id
                                 select t1).SingleOrDefault();

                        if (!b.Active)
                        {
                            b.Active = true;

                            return true;
                        }
                    }
                }
                catch { }

                return false;
            }
        };
    };
};
