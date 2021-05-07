using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Guest
    {
        public Guest(int id, string fullName, DateTime birthday, string address)
        {
            Id = id;
            FullName = fullName;
            Birthday = birthday;
            Address = address;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{Id}|{FullName}|{Birthday}|{Address}";
        }

    }
}
