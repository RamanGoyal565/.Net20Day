using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FlightRepository flights = new FlightRepository();
            flights.Add(new Flight { FlightNumber = "AI101", AvailableSeats = 5, BasePrice = 4000m });
            flights.Add(new Flight { FlightNumber = "AI202", AvailableSeats = 0, BasePrice = 6000m });

            Passenger passenger = new Passenger { Id = 1, Name = "Rohan" };
            flights["AI101"].Book(passenger);
            Console.WriteLine("Seats left: " + flights["AI101"].GetAvailableSeats());
            Console.WriteLine("Ticket price: " + flights["AI101"].CalculateTicketPrice());
            Console.WriteLine(flights["AI101"].GenerateBoardingPass(passenger));
        }
    }

    public interface IBookable
    {
        void Book(Passenger passenger);
    }

    public interface ICancelable
    {
        void Cancel(Passenger passenger);
    }

    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Passenger : Person
    {
    }

    public class Flight : IBookable, ICancelable
    {
        private int _availableSeats;
        private decimal _basePrice;
        private readonly List<Passenger> _bookedPassengers = new List<Passenger>();
        private readonly Queue<Passenger> _waitList = new Queue<Passenger>();

        public string FlightNumber { get; set; }

        public int AvailableSeats
        {
            get { return _availableSeats; }
            set
            {
                if (value < 0) throw new ArgumentException("Invalid seats");
                _availableSeats = value;
            }
        }

        public decimal BasePrice
        {
            get { return _basePrice; }
            set
            {
                if (value < 0) throw new ArgumentException("Invalid price");
                _basePrice = value;
            }
        }

        public void Book(Passenger passenger)
        {
            if (AvailableSeats > 0)
            {
                _bookedPassengers.Add(passenger);
                AvailableSeats--;
            }
            else
            {
                _waitList.Enqueue(passenger);
            }
        }

        public void Cancel(Passenger passenger)
        {
            if (_bookedPassengers.Remove(passenger))
            {
                AvailableSeats++;
                if (_waitList.Count > 0)
                {
                    Passenger next = _waitList.Dequeue();
                    Book(next);
                }
            }
        }

        public string GenerateBoardingPass(Passenger passenger)
        {
            return "Boarding Pass: " + passenger.Name + " - " + FlightNumber;
        }
    }

    public class GenericRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        public void Add(T item) { _items.Add(item); }
        public List<T> GetAll() { return _items; }
    }

    public class FlightRepository : GenericRepository<Flight>
    {
        public Flight this[string flightNumber]
        {
            get { return GetAll().First(delegate(Flight flight) { return flight.FlightNumber == flightNumber; }); }
        }
    }

    public static class FlightExtensions
    {
        public static int GetAvailableSeats(this Flight flight)
        {
            return flight.AvailableSeats;
        }

        public static decimal CalculateTicketPrice(this Flight flight)
        {
            return flight.BasePrice + 500m;
        }
    }
}