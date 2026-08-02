using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TrainingProvider providerA = new TrainingProvider("SkillUp Academy");
            TrainingProvider providerB = new TrainingProvider("LearnFast Institute");
            Course csharp = new Course("C# Mastery");
            Course cloud = new Course("Cloud Basics");

            List<TrainingProgram> programs = new List<TrainingProgram>
            {
                new TrainingProgram("TP101", "C# for Beginners", providerA, csharp, "OOP", new DateOnly(2026, 8, 3), "09:00 AM"),
                new TrainingProgram("TP102", "Advanced C#", providerA, csharp, "LINQ", new DateOnly(2026, 8, 4), "11:00 AM"),
                new TrainingProgram("TP201", "Cloud Fundamentals", providerB, cloud, "AWS Intro", new DateOnly(2026, 8, 3), "02:00 PM")
            };

            TrainingCatalog catalog = new TrainingCatalog(programs);

            Console.WriteLine("Case Study 2");
            PrintPrograms("Programs offered by SkillUp Academy", catalog.GetProgramsByProvider("SkillUp Academy"));
            PrintPrograms("Programs scheduled on 2026-08-03", catalog.GetProgramsByDay(new DateOnly(2026, 8, 3)));
            PrintPrograms("Programs scheduled for course C# Mastery", catalog.GetProgramsByCourse("C# Mastery"));
        }

        private static void PrintPrograms(string title, IEnumerable<TrainingProgram> programs)
        {
            Console.WriteLine(title);
            foreach (TrainingProgram program in programs)
            {
                Console.WriteLine("- " + program.Title + " | Topic: " + program.Topic + " | " + program.ScheduleDate.ToString("yyyy-MM-dd") + " " + program.ScheduleTime);
            }
        }
    }

    public class TrainingProvider
    {
        public TrainingProvider(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class Course
    {
        public Course(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class TrainingProgram
    {
        public TrainingProgram(string id, string title, TrainingProvider provider, Course course, string topic, DateOnly scheduleDate, string scheduleTime)
        {
            Id = id;
            Title = title;
            Provider = provider;
            Course = course;
            Topic = topic;
            ScheduleDate = scheduleDate;
            ScheduleTime = scheduleTime;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public TrainingProvider Provider { get; private set; }
        public Course Course { get; private set; }
        public string Topic { get; private set; }
        public DateOnly ScheduleDate { get; private set; }
        public string ScheduleTime { get; private set; }
    }

    public class TrainingCatalog
    {
        private readonly List<TrainingProgram> _programs;

        public TrainingCatalog(List<TrainingProgram> programs)
        {
            _programs = programs;
        }

        public List<TrainingProgram> GetProgramsByProvider(string providerName)
        {
            return _programs.Where(delegate(TrainingProgram program)
            {
                return program.Provider.Name.Equals(providerName, StringComparison.OrdinalIgnoreCase);
            }).ToList();
        }

        public List<TrainingProgram> GetProgramsByDay(DateOnly day)
        {
            return _programs.Where(delegate(TrainingProgram program)
            {
                return program.ScheduleDate == day;
            }).ToList();
        }

        public List<TrainingProgram> GetProgramsByCourse(string courseName)
        {
            return _programs.Where(delegate(TrainingProgram program)
            {
                return program.Course.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase);
            }).ToList();
        }
    }
}