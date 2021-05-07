using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class CheckInCheckOut
    {
        public CheckInCheckOut(int id, int roomId, int guestId, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            RoomId = roomId;
            GuestId = guestId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;

        }

        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public override string ToString()
        {
            return $"{Id}|{RoomId}|{GuestId}|{CheckInDate}|{CheckOutDate}";
        }
    }
}
