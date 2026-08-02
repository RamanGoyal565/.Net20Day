using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CruiseShip cruise = new CruiseShip("S101", "Ocean Bliss", "Mumbai", "Goa");
            CargoShip cargo = new CargoShip("S205", "Cargo King", "Chennai", "Dubai");

            List<ShipBooking> bookings = new List<ShipBooking>
            {
                new ShipBooking(new Customer("C1", "Nitin", "9876500011"), cruise, 12000m),
                new ShipBooking(new Customer("C2", "Sara", "9876500022"), cruise, 12500m),
                new ShipBooking(new Customer("C3", "Global Traders", "9876500033"), cargo, 45000m)
            };

            MarineCompany company = new MarineCompany("BlueWave Marine", new List<Ship> { cruise, cargo }, bookings);

            Console.WriteLine("Case Study 10");
            Console.WriteLine("Total amount collected: " + company.GetTotalAmountCollected().ToString("C"));
            PrintShipAmounts("Total amount for every ship", company.GetAmountCollectedByShip());
            PrintCustomers("Customer details for cruise ship Ocean Bliss", company.GetCustomersByCruiseShip("Ocean Bliss"));
        }

        private static void PrintShipAmounts(string title, Dictionary<string, decimal> amounts)
        {
            Console.WriteLine(title);
            foreach (KeyValuePair<string, decimal> amount in amounts)
            {
                Console.WriteLine("- " + amount.Key + ": " + amount.Value.ToString("C"));
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

    public abstract class Ship
    {
        protected Ship(string id, string name, string source, string destination)
        {
            Id = id;
            Name = name;
            Source = source;
            Destination = destination;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public string Destination { get; private set; }
        public abstract string Type { get; }
    }

    public class CruiseShip : Ship
    {
        public CruiseShip(string id, string name, string source, string destination)
            : base(id, name, source, destination)
        {
        }

        public override string Type
        {
            get { return "Cruise"; }
        }
    }

    public class CargoShip : Ship
    {
        public CargoShip(string id, string name, string source, string destination)
            : base(id, name, source, destination)
        {
        }

        public override string Type
        {
            get { return "Cargo"; }
        }
    }

    public class ShipBooking
    {
        public ShipBooking(Customer customer, Ship ship, decimal amountPaid)
        {
            Customer = customer;
            Ship = ship;
            AmountPaid = amountPaid;
        }

        public Customer Customer { get; private set; }
        public Ship Ship { get; private set; }
        public decimal AmountPaid { get; private set; }
    }

    public class MarineCompany
    {
        private readonly List<Ship> _ships;
        private readonly List<ShipBooking> _bookings;

        public MarineCompany(string name, List<Ship> ships, List<ShipBooking> bookings)
        {
            Name = name;
            _ships = ships;
            _bookings = bookings;
        }

        public string Name { get; private set; }

        public decimal GetTotalAmountCollected()
        {
            return _bookings.Sum(delegate(ShipBooking booking) { return booking.AmountPaid; });
        }

        public Dictionary<string, decimal> GetAmountCollectedByShip()
        {
            Dictionary<string, decimal> amountByShip = new Dictionary<string, decimal>();
            foreach (Ship ship in _ships)
            {
                decimal amount = _bookings.Where(delegate(ShipBooking booking) { return booking.Ship.Id == ship.Id; })
                    .Sum(delegate(ShipBooking booking) { return booking.AmountPaid; });
                amountByShip.Add(ship.Name, amount);
            }

            return amountByShip;
        }

        public List<Customer> GetCustomersByCruiseShip(string shipName)
        {
            return _bookings.Where(delegate(ShipBooking booking)
            {
                return booking.Ship is CruiseShip && booking.Ship.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase);
            }).Select(delegate(ShipBooking booking) { return booking.Customer; }).ToList();
        }
    }
}