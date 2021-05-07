using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    class GuestRepository
    {
        public List<Guest> Guests { get; private set; }

        public GuestRepository()
        {
            Guests = new List<Guest>();
            GetGuestsFromFile();
        }

        private void GetGuestsFromFile()
        {
            List<string> strGuests = new List<string>();

            using (var f = File.Open("Guests.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    strGuests = str.Split("\r\n").ToList();
                }
            }

            strGuests.Remove("");

            List<string[]> GuestsData = new List<string[]>(); // [0] - ID, [1] - полное имя [2] - дата рождения  [3] - адрес  

            foreach (var r in strGuests)
                GuestsData.Add(r.Split("|"));

            for (int i = 0; i < strGuests.Count; i++)
            {
                Guests.Add(new Guest(Convert.ToInt32(GuestsData[i][0]), Convert.ToString(GuestsData[i][1]), Convert.ToDateTime(GuestsData[i][2]), Convert.ToString(GuestsData[i][3])));
            }
        }

        private void SaveChanges()
        {
            List<string> GuestsBefore = new List<string>();

            using (var f = File.Open("Guests.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    GuestsBefore = str.Split("\r\n").ToList();
                }
            }

            using (var f = File.Open("Guests.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamWriter(f))
                {
                    foreach (var l in Guests)
                    {
                        s.WriteLine(l);
                    }
                }
            }
        }

        public void AddGuest(Guest guest)
        {
            Guests.Add(guest);
            SaveChanges();
        }




    }
}
