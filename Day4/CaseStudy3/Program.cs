using System;
using System.Collections.Generic;

namespace CaseStudy3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NotificationManager manager = new NotificationManager();
            List<INotificationChannel> channels = new List<INotificationChannel>
            {
                new Email(),
                new WhatsApp(),
                new Sms(),
                new PushNotification()
            };

            manager.Send("Project update", channels);

            foreach (NotificationStatus status in manager.History)
            {
                Console.WriteLine(status.Channel + " => " + status.IsSent);
            }
        }
    }

    public interface INotificationChannel
    {
        string Name { get; }
        bool Send(string message);
    }

    public class NotificationStatus
    {
        public string Channel { get; set; }
        public bool IsSent { get; set; }
    }

    public class NotificationManager
    {
        private readonly List<NotificationStatus> _history = new List<NotificationStatus>();

        public List<NotificationStatus> History
        {
            get { return _history; }
        }

        public void Send(string message, IEnumerable<INotificationChannel> channels)
        {
            foreach (INotificationChannel channel in channels)
            {
                bool result = channel.Send(message);
                _history.Add(new NotificationStatus { Channel = channel.Name, IsSent = result });
            }
        }
    }

    public class Email : INotificationChannel
    {
        public string Name { get { return "Email"; } }
        public bool Send(string message)
        {
            Console.WriteLine("Email sent: " + message);
            return true;
        }
    }

    public class Sms : INotificationChannel
    {
        public string Name { get { return "SMS"; } }
        public bool Send(string message)
        {
            Console.WriteLine("SMS sent: " + message);
            return true;
        }
    }

    public class WhatsApp : INotificationChannel
    {
        public string Name { get { return "WhatsApp"; } }
        public bool Send(string message)
        {
            Console.WriteLine("WhatsApp sent: " + message);
            return true;
        }
    }

    public class PushNotification : INotificationChannel
    {
        public string Name { get { return "Push"; } }
        public bool Send(string message)
        {
            Console.WriteLine("Push notification sent: " + message);
            return true;
        }
    }
}