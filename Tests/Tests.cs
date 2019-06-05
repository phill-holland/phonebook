using NUnit.Framework;
using Phonebook.Customers;
using General;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        Phonebook.Services.CustomerService service = null;
        Phonebook.Controllers.CustomerController controller = null;
        List<Customer> customers = null;

        [SetUp]
        public void Setup()
        {
            service = new Phonebook.Services.CustomerService();
            controller = new Phonebook.Controllers.CustomerController(service);
            customers = new List<Customer>();

            Unique keys = new Unique();
            customers = Customers.Generate(keys).ToList();
        }

        [Test]
        public void TestAPIGetAllFunction()
        {
            List<Customer> values = controller.Get().ToList();

            Assert.AreEqual(customers.Count(), values.Count());

            for (int i = 0; i < customers.Count(); ++i)
            {
                Customer a = customers[i];
                Customer b = values[i];

                Assert.That(a, Is.EqualTo(b).Using(new Customer()));
            }

            Assert.Pass();
        }

        [Test]
        public void TestAPIGetCustomerPhoneNumbers()
        {
            Customer a = customers.FirstOrDefault();

            List<Number> numbers = controller.Get(a.CustomerID).ToList();

            for (int i = 0; i < a.Numbers.Count(); ++i)
            {
                Number n1 = a.Numbers[i];
                Number n2 = numbers[i];

                Assert.That(n1, Is.EqualTo(n2).Using(new Number()));
            }

            Assert.Pass();
        }

        [Test]
        public void TestAPIChangePhoneNumberToActive()
        {
            Number n = FindInactiveNumber();

            Assert.AreEqual(controller.Activate(n.CustomerID, n.ID), true);

            List<Number> numbers = controller.Get(n.CustomerID).ToList();

            bool found = false;

            foreach (Number i in numbers)
            {
                if (i.ID == n.ID)
                {
                    Assert.AreEqual(i.Active, true);
                    found = true;
                }
            }

            Assert.AreEqual(found, true);

            Assert.Pass();
        }

        public Number FindInactiveNumber()
        {
            foreach (Customer c in customers)
            {
                foreach (Number n in c.Numbers)
                {
                    if (n.Active == false) return n;
                }
            }

            return null;
        }
    };
};