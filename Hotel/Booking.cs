using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Booking
    {
        BookedRoomRepository bookedRoomRepository = new BookedRoomRepository();
        CheckInCheckOutRepository checkInCheckOut = new CheckInCheckOutRepository();
        GuestRepository guestRepository = new GuestRepository();
        RoomRepository roomRepository = new RoomRepository();

        public void AddRoom(int id, int number, decimal price, int numberOfSeats, string category)
        {
            if (roomRepository.Rooms.Any(r => r.Id == id))
                throw new ArgumentException("Номер с таким id уже существует");

            if (roomRepository.Rooms.Any(r => r.Number == number))
                throw new ArgumentException("Комната с таким номером уже существует");

            roomRepository.AddRoom(new Room(id, number, price, numberOfSeats, category));
        }

        public void RemoveRoom(int Id)
        {
            if (!roomRepository.Rooms.Any(r => r.Id == Id))
                throw new ArgumentException("Нет номера с указаным Id");
            roomRepository.RemoveRoom(Id);
        }

        public void ToBook(int id, int RoomId, int GuestId, DateTime startDate, DateTime endDate)
        {
            if (bookedRoomRepository.BookedRooms.Any(r => r.Id == id))
                throw new ArgumentException("Бронирование с указаным Id уже существует");

            if (!roomRepository.Rooms.Any(r => r.Id == RoomId))
                throw new ArgumentException("При бронировании указан несуществующий Id номера");

            if (!guestRepository.Guests.Any(r => r.Id == GuestId))
                throw new ArgumentException("При бронировании указан несуществующий Id постояльца");

            if (endDate < startDate)
                throw new ArgumentException("Первая дата должна быть раньше второй");

            if (!FreeRoomsForTheDate(startDate, endDate).Any(r => r.Id == RoomId))
                throw new Exception("Номер уже забронирован на выбранную дату");

            bookedRoomRepository.AddBookedRoom(new BookedRoom(id, RoomId, GuestId, startDate, endDate));
        }


        public void GuestRegistration(int id, string fullName, DateTime birthDay, string address)
        {
            if (guestRepository.Guests.Any(r => r.Id == id))
                throw new ArgumentException("Постоялец с таким Id уже существует");

            guestRepository.AddGuest(new Guest(id, fullName, birthDay, address));
        }

        public IEnumerable<Room> FreeRoomsForTheDate(DateTime date1, DateTime date2 = default)
        {
            if (date2 == default)
                date2 = date1;

            if (date2 < date1)
                throw new ArgumentException("Первая дата должна быть раньше второй");

            var bookedRooms = bookedRoomRepository.BookedRooms;
            var AllRooms = roomRepository.Rooms;

            List<int> BookedRoomsToDate = new List<int>();

            Interval[] array = new Interval[bookedRooms.Count];


            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Interval();
                array[i].Start = bookedRooms[i].StartDate;
                array[i].End = bookedRooms[i].EndDate;
            }

            Interval New = new Interval()
            {
                Start = date1,
                End = date2
            };

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Includes(New))
                    BookedRoomsToDate.Add(bookedRooms[i].RoomId);
            }

            List<Room> FreeRooms = new List<Room>();

            bool IsBooked = false;

            foreach (Room room in AllRooms)
            {
                IsBooked = false;
                foreach (int NumberBookedRoom in BookedRoomsToDate)
                    if (room.Id == NumberBookedRoom)
                    {
                        IsBooked = true;
                        break;
                    }

                if (!IsBooked)
                    FreeRooms.Add(room);
            }

            return FreeRooms;
        }

        public void CheckIn(int checkInId, int roomId, int guestId, DateTime date1, DateTime date2)
        {
            if (checkInCheckOut.checkInCheckOut.Any(c => c.Id == checkInId))
                throw new ArgumentException("Въезд/Выезд с таким ID уже существует");

            if (!roomRepository.Rooms.Any(r => r.Id == roomId))
                throw new ArgumentException("При бронировании указан несуществующий Id номера");

            if (!guestRepository.Guests.Any(r => r.Id == guestId))
                throw new ArgumentException("При бронировании указан несуществующий Id постояльца");

            if (date2 < date1 && date2 != default)
                throw new ArgumentException("Первая дата должна быть раньше второй");


            checkInCheckOut.AddCheckIn(new CheckInCheckOut(checkInId, roomId, guestId, date1, date2));
        }

        public void CheckOut(int checkInId, DateTime date2)
        {
            if (!checkInCheckOut.checkInCheckOut.Any(c => c.Id == checkInId))
                throw new ArgumentException("Въезд/Выезд с таким ID не существует");

            checkInCheckOut.CheckOut(checkInId, date2);
        }

        class Interval
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public bool Includes(Interval t)
            {
                return t.Start >= Start && t.Start <= End ||
                    t.End <= End && t.End >= Start ||
                    t.Start <= Start && t.End >= End;
            }
        }
    }
}
