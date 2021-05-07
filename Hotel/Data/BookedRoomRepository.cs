using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    class BookedRoomRepository
    {
        public List<BookedRoom> BookedRooms { get; private set; }

        public BookedRoomRepository()
        {
            BookedRooms = new List<BookedRoom>();
            GetBookedRoomsFromFile();
        }

        private void GetBookedRoomsFromFile()
        {
            List<string> strBookedRooms = new List<string>();

            using (var f = File.Open("BookedRooms.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    strBookedRooms = str.Split("\r\n").ToList();
                }
            }


            strBookedRooms.Remove("");

            List<string[]> BookedRoomsData = new List<string[]>(); // [0] - ID, [1] - ID комнаты [2] - ID постояльца  [3] Дата начала бронирования  [4] - дата конца бронирования

            foreach (var r in strBookedRooms)
                BookedRoomsData.Add(r.Split("|"));

            for (int i = 0; i < strBookedRooms.Count; i++)
            {
                BookedRooms.Add(new BookedRoom(Convert.ToInt32(BookedRoomsData[i][0]), Convert.ToInt32(BookedRoomsData[i][1]), Convert.ToInt32(BookedRoomsData[i][2]), Convert.ToDateTime(BookedRoomsData[i][3]), Convert.ToDateTime(BookedRoomsData[i][4])));
            }
        }

        private void SaveChanges()
        {
            List<string> BookedRoomsBefore = new List<string>();

            using (var f = File.Open("BookedRooms.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    BookedRoomsBefore = str.Split("\r\n").ToList();
                }
            }

            using (var f = File.Open("BookedRooms.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamWriter(f))
                {
                    foreach (var l in BookedRooms)
                    {
                        s.WriteLine(l);
                    }
                }
            }
        }

        public void AddBookedRoom(BookedRoom bookedRoom)
        {
            BookedRooms.Add(bookedRoom);
            SaveChanges();
        }

    }
}
