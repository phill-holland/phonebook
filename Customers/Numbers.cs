using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Phonebook
{
    namespace Customers
    {
        public class Number : IComparable<Number>, IEqualityComparer<Number>
        {
            public int ID { get; set; }

            public int CustomerID { get; set; }

            public string Value { get; set; }

            public bool Active { get; set; }

            public Number()
            {
                ID = 0;
                CustomerID = 0;
                Value = "";
                Active = false;
            }

            public int CompareTo(Number obj)
            {
                return Equals(this, obj) == true ? 0 : -1;
            }

            public bool Equals(Number a, Number b)
            {
                if (a.CustomerID != b.CustomerID) return false;
                if (b.ID != b.ID) return false;
                if (a.Value.CompareTo(b.Value) != 0) return false;
                if (a.Active != b.Active) return false;
                
                return true;
            }

            public int GetHashCode(Number obj)
            {
                return HashCode.Combine<int, int, string, bool>(CustomerID, ID, Value, Active);
            }
        };
    };
};
