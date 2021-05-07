using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Data
{
    class RoomRepository
    {
        public List<Room> Rooms { get; private set; }

        public RoomRepository()
        {
            Rooms = new List<Room>();
            GetRoomsFromFile();
        }

        private void GetRoomsFromFile()
        {
            List<string> strRooms = new List<string>();

            using (var f = File.Open("Rooms.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    strRooms = str.Split("\r\n").ToList();
                }
            }

            strRooms.Remove("");

            List<string[]> RoomsData = new List<string[]>(); // [0] - ID, [1] - номер комнаты  [2] - цена  [3] - колличество мест  [4] - категория

            foreach (var r in strRooms)
                RoomsData.Add(r.Split("|"));

            for (int i = 0; i < strRooms.Count; i++)
            {
                Rooms.Add(new Room(Convert.ToInt32(RoomsData[i][0]), Convert.ToInt32(RoomsData[i][1]), Convert.ToDecimal(RoomsData[i][2]), Convert.ToInt32(RoomsData[i][3]), RoomsData[i][4]));
            }
        }

        private void SaveChanges()
        {
            List<string> RoomsBefore = new List<string>();

            using (var f = File.Open("Rooms.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamReader(f))
                {
                    var str = s.ReadToEnd();
                    RoomsBefore = str.Split("\r\n").ToList();
                }
            }

            using (var f = File.Open("Rooms.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var s = new StreamWriter(f))
                {
                    foreach (var l in Rooms)
                    {
                        s.WriteLine(l);
                    }
                }
            }
        }
        public void AddRoom(Room room)
        {
            Rooms.Add(room);
            SaveChanges();
        }

        public void RemoveRoom(int Id)
        {
            Rooms.Remove(Rooms.First(r => r.Id == Id));
            SaveChanges();
        }
    }
}
