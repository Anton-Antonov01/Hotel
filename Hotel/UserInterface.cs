using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class UserInterface
    {
        Booking booking = new Booking();

        public void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Отель");
            Console.WriteLine("1) Добавить комнату");
            Console.WriteLine("2) Удалить комнату");
            Console.WriteLine("3) Забронировать номер");
            Console.WriteLine("4) Показать свободные номера на дату или диапозон дат");
            Console.WriteLine("5) Регистрация постояльца");
            Console.WriteLine("6) Въезд постояльца");
            Console.WriteLine("7) Выезд постояльца");
            Console.WriteLine("8) Список комнат");
            Console.WriteLine("9) Список постояльцев");
            Console.WriteLine("10) Список забронированных номеров");
            Console.WriteLine("11) Список въездов/выездов постояльцев");


            int key = Convert.ToInt32(Console.ReadLine());
            switch (key)
            {
                case 1:
                    AddRoom();
                    break;
                case 2:
                    RemoveRoom();
                    break;
                case 3:
                    ToBook();
                    break;
                case 4:
                    FreeRoomsForTheDate();
                    break;
                case 5:
                    GuestRegistration();
                    break;
                case 6:
                    CheckIn();
                    break;
                case 7:
                    CheckOut();
                    break;
                default:
                    Console.WriteLine("Неверный номер операции");
                    BackToMainMenu();
                    break;
            }

            BackToMainMenu();
        }


        private void AddRoom()
        {

            Console.Clear();

            int id;
            int number;
            decimal price;
            int numberOfSeats;
            string Category;

            Console.WriteLine("Добавление номера: ");
            Console.WriteLine();
            Console.WriteLine("Введите Id номера");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите Номер комнаты");
            number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите цену за номер");
            price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите колличество мест");
            numberOfSeats = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите категорию");
            Category = Console.ReadLine();

            booking.AddRoom(id, number, price, numberOfSeats, Category);

            BackToMainMenu();
        }

        private void RemoveRoom()
        {
            Console.Clear();

            int id;

            Console.WriteLine("Удаление номера");
            Console.WriteLine("");
            Console.WriteLine("Введите ID номера");
            id = Convert.ToInt32(Console.ReadLine());
            booking.RemoveRoom(id);

            BackToMainMenu();
        }

        private void ToBook()
        {
            Console.Clear();

            string Key;//Поменять

            int id;
            int roomId;
            int guestId;
            DateTime startDate;
            DateTime endDate;

            Console.WriteLine("Бронирование номера");
            Console.WriteLine("");

            Console.WriteLine("Это новый клиент?");
            Console.WriteLine("1) Да");
            Console.WriteLine("0) Нет");
            Key = Console.ReadLine();//Поменять

            if (Key == "1")
                GuestRegistration();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Введите id бронирования");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id комнаты");
            roomId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id постояльца");
            guestId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите дату начала бронирования в формате \"DD.MM.YYYY\" ");
            startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите дату конца бронирования в формате \"DD.MM.YYYY\" (Если бронь на один день - введите дату начала бронирования)");
            endDate = Convert.ToDateTime(Console.ReadLine());


            booking.ToBook(id, roomId, guestId, startDate, endDate);

            BackToMainMenu();
        }

        private void GuestRegistration()
        {
            int id;
            string fullName;
            DateTime birthday;
            string address;

            Console.WriteLine("Регистрация постояльца");
            Console.WriteLine("");
            Console.WriteLine("Введите Id постояльца");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите ФИО постояльца");
            fullName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения постояльца в формате \"DD.MM.YYYY\"");
            birthday = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите адрес постояльца");
            address = Console.ReadLine();

            booking.GuestRegistration(id, fullName, birthday, address);

        }

        private void FreeRoomsForTheDate()
        {
            Console.Clear();

            string key;

            DateTime date1;
            DateTime date2;

            Console.WriteLine("Сделать проверку по дате или диапазону дат?");
            Console.WriteLine("1) По дате");
            Console.WriteLine("2) По диапазону дат");
            key = Console.ReadLine();

            Console.WriteLine("Введите дату в формате \"DD.MM.YYYY\"");
            date1 = Convert.ToDateTime(Console.ReadLine());

            if (key == "2")
            {
                Console.WriteLine("Введите вторую дату в формате \"DD.MM.YYYY\"");
                date2 = Convert.ToDateTime(Console.ReadLine());

                ShowRooms(booking.FreeRoomsForTheDate(date1, date2));
            }
            else
            {
                ShowRooms(booking.FreeRoomsForTheDate(date1));
            }
            Console.WriteLine("");
            Console.WriteLine();

            BackToMainMenu();
        }

        private void ShowRooms(IEnumerable<Room> rooms)
        {
            foreach (var r in rooms)
            {
                Console.WriteLine($"Id:{r.Id} Номер комнты:{r.Number} Цена:{r.Price} Колличество мест:{r.NumberOfSeats} Категория: {r.Category}");
            }
        }

        private void CheckIn()
        {
            Console.Clear();
            string key;

            Console.WriteLine("Была ли соврешена бронь номера?");
            Console.WriteLine("1) Да");
            Console.WriteLine("2) Нет");
            key = Console.ReadLine();

            if (key == "2")
            {
                ToBook();
            }

            int id;
            int roomId;
            int guestId;
            DateTime startDate = DateTime.Now;

            Console.WriteLine("Въезд постояльца");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Введите id Въезда/Выезда");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id комнаты");
            roomId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id постояльца");
            guestId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
            booking.CheckIn(id, roomId, guestId, startDate, default);

            BackToMainMenu();
        }

        private void CheckOut()
        {
            Console.Clear();

            Console.WriteLine("Выезд постояльца");
            Console.WriteLine("Введите id Въезда/Выезда");
            int id = Convert.ToInt32(Console.ReadLine());

            booking.CheckOut(id, DateTime.Now);

            BackToMainMenu();
        }

        private void BackToMainMenu()
        {
            Console.WriteLine("Нажмите любую кнопку, что б вернуться в главное меню");
            Console.ReadLine();
            MainMenu();
        }
    }
}
