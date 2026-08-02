using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Passenger raj = new Passenger("P001", "Raj Kumar");
            Passenger anita = new Passenger("P002", "Anita Shah");
            Train trainA = new Train("T101", "Chennai Express");
            Train trainB = new Train("T205", "Deccan Queen");

            List<Ticket> tickets = new List<Ticket>
            {
                new Ticket("TK1", raj, trainA, 850m, new Payment("PAY1", 850m, "UPI")),
                new Ticket("TK2", raj, trainB, 540m, new Payment("PAY2", 540m, "Card")),
                new Ticket("TK3", anita, trainA, 850m, new Payment("PAY3", 850m, "NetBanking"))
            };

            TicketingSystem system = new TicketingSystem(tickets);

            Console.WriteLine("Case Study 4");
            Console.WriteLine("Total amount collected: " + system.GetTotalAmountCollected().ToString("C"));
            PrintTickets("Tickets for Raj Kumar", system.GetTicketsByPassenger("Raj Kumar"));
            PrintPassengers("Passengers for Chennai Express", system.GetPassengersByTrain("Chennai Express"));
        }

        private static void PrintTickets(string title, IEnumerable<Ticket> tickets)
        {
            Console.WriteLine(title);
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine("- " + ticket.Id + " | Train: " + ticket.Train.Name + " | Fare: " + ticket.Fare.ToString("C"));
            }
        }

        private static void PrintPassengers(string title, IEnumerable<Passenger> passengers)
        {
            Console.WriteLine(title);
            foreach (Passenger passenger in passengers.GroupBy(delegate(Passenger p) { return p.Id; }).Select(delegate(IGrouping<string, Passenger> group) { return group.First(); }))
            {
                Console.WriteLine("- " + passenger.Name);
            }
        }
    }

    public class Passenger
    {
        public Passenger(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Train
    {
        public Train(string number, string name)
        {
            Number = number;
            Name = name;
        }

        public string Number { get; private set; }
        public string Name { get; private set; }
    }

    public class Payment
    {
        public Payment(string id, decimal amount, string mode)
        {
            Id = id;
            Amount = amount;
            Mode = mode;
        }

        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Mode { get; private set; }
    }

    public class Ticket
    {
        public Ticket(string id, Passenger passenger, Train train, decimal fare, Payment payment)
        {
            Id = id;
            Passenger = passenger;
            Train = train;
            Fare = fare;
            Payment = payment;
        }

        public string Id { get; private set; }
        public Passenger Passenger { get; private set; }
        public Train Train { get; private set; }
        public decimal Fare { get; private set; }
        public Payment Payment { get; private set; }
    }

    public class TicketingSystem
    {
        private readonly List<Ticket> _tickets;

        public TicketingSystem(List<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public decimal GetTotalAmountCollected()
        {
            return _tickets.Sum(delegate(Ticket ticket) { return ticket.Payment.Amount; });
        }

        public List<Ticket> GetTicketsByPassenger(string passengerName)
        {
            return _tickets.Where(delegate(Ticket ticket) { return ticket.Passenger.Name.Equals(passengerName, StringComparison.OrdinalIgnoreCase); }).ToList();
        }

        public List<Passenger> GetPassengersByTrain(string trainName)
        {
            return _tickets.Where(delegate(Ticket ticket) { return ticket.Train.Name.Equals(trainName, StringComparison.OrdinalIgnoreCase); })
                .Select(delegate(Ticket ticket) { return ticket.Passenger; })
                .ToList();
        }
    }
}