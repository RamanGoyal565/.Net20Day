using System;
using System.Collections.Generic;

namespace CaseStudy12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Driver driver = new Driver { Id = 1, Name = "Ravi", Vehicle = new Car("KA01AB1234", 15) };
            Rider rider = new Rider { Id = 1, Name = "Sita" };
            RideMatcher<Driver> matcher = new RideMatcher<Driver>();
            Driver matchedDriver = matcher.Match(new List<Driver> { driver });

            CompletedRide ride = new CompletedRide();
            ride.Driver = matchedDriver;
            ride.Rider = rider;
            ride.Distance = 12;
            ride.BaseFare = 50m;

            Console.WriteLine("Driver: " + matchedDriver.Name);
            Console.WriteLine("Distance: " + RideExtensions.CalculateDistance(ride));
            Console.WriteLine("Fare: " + RideExtensions.CalculateFare(ride));
        }
    }

    public class RideMatcher<T> where T : Driver
    {
        public T Match(List<T> drivers)
        {
            return drivers[0];
        }
    }

    public abstract class Vehicle
    {
        protected Vehicle(string registrationNumber, int mileage)
        {
            RegistrationNumber = registrationNumber;
            Mileage = mileage;
        }

        public string RegistrationNumber { get; private set; }
        public int Mileage { get; private set; }
    }

    public class Car : Vehicle
    {
        public Car(string registrationNumber, int mileage) : base(registrationNumber, mileage) { }
    }

    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public class Rider
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Ride
    {
        private double _distance;
        private decimal _baseFare;

        public Driver Driver { get; set; }
        public Rider Rider { get; set; }

        public double Distance
        {
            get { return _distance; }
            set
            {
                if (value < 0) throw new ArgumentException("Invalid distance");
                _distance = value;
            }
        }

        public decimal BaseFare
        {
            get { return _baseFare; }
            set
            {
                if (value < 0) throw new ArgumentException("Invalid fare");
                _baseFare = value;
            }
        }
    }

    public sealed class CompletedRide : Ride
    {
    }

    public static class RideExtensions
    {
        public static double CalculateDistance(this Ride ride)
        {
            return ride.Distance;
        }

        public static decimal CalculateFare(this Ride ride)
        {
            return ride.BaseFare + (decimal)ride.Distance * 12m;
        }
    }
}