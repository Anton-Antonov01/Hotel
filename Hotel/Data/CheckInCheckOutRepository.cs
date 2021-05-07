using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    class CheckInCheckOutRepository
    {
        public List<CheckInCheckOut> checkInCheckOut { get; private set; }

        public CheckInCheckOutRepository()
        {
            checkInCheckOut = new List<CheckInCheckOut>();
            GetCheckInCheckOutFromFile();
        }

        private void GetCheckInCheckOutFromFile()
        {
            List<string> strCheckInCheckOut = new List<string>();

            using (var f = File.Open("CheckInCheckOut.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    strCheckInCheckOut = str.Split("\r\n").ToList();
                }
            }

            strCheckInCheckOut.Remove("");

            List<string[]> BookedRoomsData = new List<string[]>(); // [0] - ID, [1] - ID комнаты  [2] - ID постояльца  [3] Дата начала бронирования  [4] - дата конца бронирования

            foreach (var r in strCheckInCheckOut)
                BookedRoomsData.Add(r.Split("|"));

            for (int i = 0; i < strCheckInCheckOut.Count; i++)
            {
                checkInCheckOut.Add(new CheckInCheckOut(Convert.ToInt32(BookedRoomsData[i][0]), Convert.ToInt32(BookedRoomsData[i][1]), Convert.ToInt32(BookedRoomsData[i][2]), Convert.ToDateTime(BookedRoomsData[i][3]), Convert.ToDateTime(BookedRoomsData[i][4])));
            }

        }

        private void SaveChanges()
        {
            List<string> CheckInCheckOutBefore = new List<string>();

            using (var f = File.Open("CheckInCheckOut.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    CheckInCheckOutBefore = str.Split("\r\n").ToList();
                }
            }

            using (var f = File.Open("CheckInCheckOut.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamWriter(f))
                {
                    foreach (var l in checkInCheckOut)
                    {
                        s.WriteLine(l);
                    }
                }
            }
        }

        public void AddCheckIn(CheckInCheckOut CheckInCheckOut)
        {
            checkInCheckOut.Add(CheckInCheckOut);
            SaveChanges();
        }

        public void CheckOut(int CheckInId, DateTime CheckOutDate)
        {
            checkInCheckOut.Find(x => x.Id == CheckInId).CheckOutDate = CheckOutDate;
            SaveChanges();
        }
    }
}
