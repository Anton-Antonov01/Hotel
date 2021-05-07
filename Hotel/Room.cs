using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Room
    {
        public Room(int id, int number, decimal price, int numberOfSeats, string category)
        {
            Id = id;
            Number = number;
            Price = price;
            NumberOfSeats = numberOfSeats;
            Category = category;
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public int NumberOfSeats { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Id}|{Number}|{Price}|{NumberOfSeats}|{Category}";
        }
    }
}
