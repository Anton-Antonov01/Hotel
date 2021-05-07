using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class BookedRoom
    {
        public BookedRoom(int id, int roomId, int guestId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            RoomId = roomId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;

        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return $"{Id}|{RoomId}|{GuestId}|{StartDate}|{EndDate}";
        }
    }
}
