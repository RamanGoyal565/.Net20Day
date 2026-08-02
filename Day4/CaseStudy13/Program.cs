using System;
using System.Collections.Generic;

namespace CaseStudy13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EventManager<BaseEvent> manager = new EventManager<BaseEvent>();
            Conference conference = new Conference { Id = 101, Title = "DotNet Conf", Date = DateTime.Today.AddDays(7) };
            Workshop workshop = new Workshop { Id = 102, Title = "C# Workshop", Date = DateTime.Today.AddDays(3) };

            manager.Add(conference);
            manager.Add(workshop);
            conference.Register("Anu");
            workshop.NotifyUsers();
            manager.SendReminders();

            Console.WriteLine(manager[101].Title);
            foreach (object summary in manager.GetSummaries())
            {
                Console.WriteLine(summary);
            }
        }
    }

    public interface IRegistrable
    {
        void Register(string userName);
    }

    public interface INotifiable
    {
        void NotifyUsers();
    }

    public abstract class BaseEvent : IRegistrable, INotifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public List<string> Participants { get; private set; } = new List<string>();
        public void Register(string userName) { Participants.Add(userName); }
        public void NotifyUsers() { Console.WriteLine("Notifying users for " + Title); }
    }

    public class Conference : BaseEvent { }
    public class Workshop : BaseEvent { }
    public class Webinar : BaseEvent { }

    public class EventManager<T> where T : BaseEvent
    {
        private readonly Dictionary<int, T> _events = new Dictionary<int, T>();
        public void Add(T item) { _events[item.Id] = item; }
        public T this[int id] { get { return _events[id]; } }
        public void SendReminders() { foreach (T item in _events.Values) { item.SendReminder(); } }
        public List<object> GetSummaries()
        {
            List<object> summaries = new List<object>();
            foreach (T item in _events.Values)
            {
                summaries.Add(new { item.Id, item.Title, ParticipantCount = item.Participants.Count });
            }
            return summaries;
        }
    }

    public static class EventExtensions
    {
        public static void SendReminder(this BaseEvent item)
        {
            Console.WriteLine("Reminder sent for " + item.Title + " on " + item.Date.ToShortDateString());
        }
    }
}