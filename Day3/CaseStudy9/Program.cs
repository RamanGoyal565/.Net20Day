using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Event music = new Event("Battle of Bands", 500m);
            Event dance = new Event("Dance Clash", 300m);
            Event quiz = new Event("Tech Quiz", 200m);

            List<Registration> registrations = new List<Registration>
            {
                new Registration(new Participant("P1", "Riya", ParticipantType.Individual), music, 500m),
                new Registration(new Participant("P2", "Team Rhythm", ParticipantType.Team), dance, 300m),
                new Registration(new Participant("P3", "Arjun", ParticipantType.Individual), quiz, 200m),
                new Registration(new Participant("P4", "Code Crew", ParticipantType.Team), quiz, 200m)
            };

            Fest fest = new Fest("Campus Fest", registrations);

            Console.WriteLine("Case Study 9");
            Console.WriteLine("Total amount collected: " + fest.GetTotalAmountCollected().ToString("C"));
            PrintCounts("Number of participants for each event", fest.GetParticipantCountByEvent());
            PrintAmounts("Amount collected for each event", fest.GetAmountCollectedByEvent());
        }

        private static void PrintCounts(string title, Dictionary<string, int> counts)
        {
            Console.WriteLine(title);
            foreach (KeyValuePair<string, int> count in counts)
            {
                Console.WriteLine("- " + count.Key + ": " + count.Value);
            }
        }

        private static void PrintAmounts(string title, Dictionary<string, decimal> amounts)
        {
            Console.WriteLine(title);
            foreach (KeyValuePair<string, decimal> amount in amounts)
            {
                Console.WriteLine("- " + amount.Key + ": " + amount.Value.ToString("C"));
            }
        }
    }

    public enum ParticipantType
    {
        Individual,
        Team
    }

    public class Event
    {
        public Event(string name, decimal fee)
        {
            Name = name;
            Fee = fee;
        }

        public string Name { get; private set; }
        public decimal Fee { get; private set; }
    }

    public class Participant
    {
        public Participant(string id, string name, ParticipantType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public ParticipantType Type { get; private set; }
    }

    public class Registration
    {
        public Registration(Participant participant, Event eventInfo, decimal amountPaid)
        {
            Participant = participant;
            EventInfo = eventInfo;
            AmountPaid = amountPaid;
        }

        public Participant Participant { get; private set; }
        public Event EventInfo { get; private set; }
        public decimal AmountPaid { get; private set; }
    }

    public class Fest
    {
        private readonly List<Registration> _registrations;

        public Fest(string name, List<Registration> registrations)
        {
            Name = name;
            _registrations = registrations;
        }

        public string Name { get; private set; }

        public decimal GetTotalAmountCollected()
        {
            return _registrations.Sum(delegate(Registration registration) { return registration.AmountPaid; });
        }

        public Dictionary<string, int> GetParticipantCountByEvent()
        {
            return _registrations.GroupBy(delegate(Registration registration) { return registration.EventInfo.Name; })
                .ToDictionary(delegate(IGrouping<string, Registration> group) { return group.Key; }, delegate(IGrouping<string, Registration> group) { return group.Count(); });
        }

        public Dictionary<string, decimal> GetAmountCollectedByEvent()
        {
            return _registrations.GroupBy(delegate(Registration registration) { return registration.EventInfo.Name; })
                .ToDictionary(delegate(IGrouping<string, Registration> group) { return group.Key; }, delegate(IGrouping<string, Registration> group) { return group.Sum(delegate(Registration registration) { return registration.AmountPaid; }); });
        }
    }
}