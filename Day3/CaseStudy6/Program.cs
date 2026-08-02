using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Room room101 = new Room("101", RoomType.Single, 2000m);
            Room room102 = new Room("102", RoomType.Single, 2000m);
            Room room201 = new Room("201", RoomType.Double, 3200m);
            Room room202 = new Room("202", RoomType.Double, 3200m);

            List<Booking> bookings = new List<Booking>
            {
                new Booking("B1", new Customer("C1", "Asha", "9999988888"), room101, BookingType.Advance, new DateOnly(2026, 8, 2), 1800m),
                new Booking("B2", new Customer("C2", "Vikram", "9999977777"), room201, BookingType.OnTime, new DateOnly(2026, 8, 2), 3200m),
                new Booking("B3", new Customer("C3", "Neha", "9999966666"), room202, BookingType.Advance, new DateOnly(2026, 8, 3), 2880m)
            };

            Hotel hotel = new Hotel("Blue Orchid", new List<Room> { room101, room102, room201, room202 }, bookings);

            Console.WriteLine("Case Study 6");
            Console.WriteLine("Total revenue generated for 2026-08-02: " + hotel.GetRevenueForDay(new DateOnly(2026, 8, 2)).ToString("C"));
            PrintVacancy(hotel.GetVacantRoomsByType());
            PrintCustomers("Customer details in room 201", hotel.GetCustomersByRoom("201"));
        }

        private static void PrintVacancy(Dictionary<RoomType, int> vacancies)
        {
            Console.WriteLine("Vacant rooms for each room type");
            foreach (KeyValuePair<RoomType, int> vacancy in vacancies)
            {
                Console.WriteLine("- " + vacancy.Key + ": " + vacancy.Value);
            }
        }

        private static void PrintCustomers(string title, IEnumerable<Customer> customers)
        {
            Console.WriteLine(title);
            foreach (Customer customer in customers)
            {
                Console.WriteLine("- " + customer.Name + " | Contact: " + customer.ContactNumber);
            }
        }
    }

    public enum RoomType
    {
        Single,
        Double
    }

    public enum BookingType
    {
        Advance,
        OnTime
    }

    public class Customer
    {
        public Customer(string id, string name, string contactNumber)
        {
            Id = id;
            Name = name;
            ContactNumber = contactNumber;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string ContactNumber { get; private set; }
    }

    public class Room
    {
        public Room(string number, RoomType type, decimal baseRent)
        {
            Number = number;
            Type = type;
            BaseRent = baseRent;
        }

        public string Number { get; private set; }
        public RoomType Type { get; private set; }
        public decimal BaseRent { get; private set; }
    }

    public class Booking
    {
        public Booking(string id, Customer customer, Room room, BookingType type, DateOnly bookingDate, decimal finalRent)
        {
            Id = id;
            Customer = customer;
            Room = room;
            Type = type;
            BookingDate = bookingDate;
            FinalRent = finalRent;
        }

        public string Id { get; private set; }
        public Customer Customer { get; private set; }
        public Room Room { get; private set; }
        public BookingType Type { get; private set; }
        public DateOnly BookingDate { get; private set; }
        public decimal FinalRent { get; private set; }
    }

    public class Hotel
    {
        private readonly List<Room> _rooms;
        private readonly List<Booking> _bookings;

        public Hotel(string name, List<Room> rooms, List<Booking> bookings)
        {
            Name = name;
            _rooms = rooms;
            _bookings = bookings;
        }

        public string Name { get; private set; }

        public decimal GetRevenueForDay(DateOnly day)
        {
            return _bookings.Where(delegate(Booking booking) { return booking.BookingDate == day; })
                .Sum(delegate(Booking booking) { return booking.FinalRent; });
        }

        public Dictionary<RoomType, int> GetVacantRoomsByType()
        {
            HashSet<string> occupiedRoomNumbers = new HashSet<string>(_bookings.Select(delegate(Booking booking) { return booking.Room.Number; }));
            Dictionary<RoomType, int> vacancies = new Dictionary<RoomType, int>();
            IEnumerable<IGrouping<RoomType, Room>> roomGroups = _rooms.GroupBy(delegate(Room room) { return room.Type; });

            foreach (IGrouping<RoomType, Room> roomGroup in roomGroups)
            {
                int vacantCount = roomGroup.Count(delegate(Room room) { return !occupiedRoomNumbers.Contains(room.Number); });
                vacancies.Add(roomGroup.Key, vacantCount);
            }

            return vacancies;
        }

        public List<Customer> GetCustomersByRoom(string roomNumber)
        {
            return _bookings.Where(delegate(Booking booking) { return booking.Room.Number.Equals(roomNumber, StringComparison.OrdinalIgnoreCase); })
                .Select(delegate(Booking booking) { return booking.Customer; })
                .ToList();
        }
    }
}