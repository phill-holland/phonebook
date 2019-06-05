using System;
using System.Collections;
using System.Collections.Generic;
using General;

namespace Phonebook
{
    namespace Customers
    {
        public class Customer : IComparable<Customer>, IEqualityComparer<Customer>
        {
            public int CustomerID { get; set; }

            public string Name { get; set; }

            public List<Number> Numbers { get; set; }

            public Customer()
            {
                Clear();
            }

            public Customer(IUnique key)
            {
                Clear();
                Generate(key);
            }

            public void Clear()
            {
                CustomerID = 0;
                Name = "";
                Numbers = new List<Number>();
            }

            public void Generate(IUnique key)
            {
                string[] titles = { "Mr", "Miss", "Mrs", "Sir", "Lord", "Esquire", "King", "God", "Chief", "Master" };
                string[] first = { "Badger", "Squirrel", "Narwhal", "Frog", "Horse", "Cow", "Monkey", "Hedgehog", "Platypus", "Pig", "Termite" };
                string[] last = { "Banana", "Cheese", "Crackers", "Hummus", "Marmite", "Cucumber", "Crisps", "Chicken", "Pork", "Beef"};

                CustomerID = key.Next();
                Random rand = new Random(CustomerID);

                Name = titles[rand.Next(0, titles.Length)] + " ";
                Name += first[rand.Next(0, first.Length)] + " ";
                Name += last[rand.Next(0, last.Length)];

                int total = rand.Next(1, 3);

                for (int i = 0; i < total; ++i)
                {
                    Number temp = new Number();

                    temp.ID = i + 1;
                    temp.CustomerID = CustomerID;
                    temp.Active = rand.Next(0, 2) == 0 ? true : false;

                    temp.Value = "";

                    for (int j = 0; j < 9; ++j)
                    {
                        temp.Value += rand.Next(0, 9).ToString();
                    }

                    Numbers.Add(temp);
                }
            }

            public int CompareTo(Customer obj)
            {
                return Equals(this, obj) == true ? 0 : -1;
            }

            public bool Equals(Customer a, Customer b)
            {
                if (a.CustomerID != b.CustomerID) return false;
                if (a.Name.CompareTo(b.Name) != 0) return false;

                for (int i = 0; i < Numbers.Count; ++i)
                {
                    Number n1 = a.Numbers[i];
                    Number n2 = b.Numbers[i];
                }

                return true;
            }

            public int GetHashCode(Customer obj)
            {
                int result = HashCode.Combine<int, string>(CustomerID, Name);

                foreach (Number n in Numbers)
                {
                    result = HashCode.Combine<int, int>(result, n.GetHashCode());
                }

                return result;
            }
        };

        public class Customers
        {
            public static IEnumerable<Customer> Generate(IUnique key)
            {
                for (int i = 0; i < 20; ++i)
                {
                    yield return new Customer(key);
                }
            }
        };
    };
};
