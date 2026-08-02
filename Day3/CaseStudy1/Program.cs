using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Topic math = new Topic("Algebra");
            Topic science = new Topic("Physics");

            List<Question> questions = new List<Question>
            {
                new MultipleChoiceQuestion(1, "What is 2 + 2?", new Category("Easy"), math, new List<string> { "3", "4", "5" }, "4"),
                new MultipleChoiceQuestion(2, "Solve x if x + 5 = 9.", new Category("Medium"), math, new List<string> { "4", "3", "5" }, "4"),
                new ParagraphQuestion(3, "Explain Newton's first law.", new Category("Theory"), science, 150),
                new MultipleChoiceQuestion(4, "Unit of force?", new Category("Easy"), science, new List<string> { "Newton", "Joule", "Pascal" }, "Newton")
            };

            ExamPortal portal = new ExamPortal(questions);

            Console.WriteLine("Case Study 1");
            Console.WriteLine("Total number of questions: " + portal.GetTotalQuestions());
            PrintQuestions("Questions for topic Algebra", portal.GetQuestionsByTopic("Algebra"));
            PrintQuestions("Questions for topic Physics and category Easy", portal.GetQuestionsByTopicAndCategory("Physics", "Easy"));
        }

        private static void PrintQuestions(string title, IEnumerable<Question> questions)
        {
            Console.WriteLine(title);
            foreach (Question question in questions)
            {
                Console.WriteLine("- [" + question.Type + "] " + question.Text);
            }
        }
    }

    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class Topic
    {
        public Topic(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public abstract class Question
    {
        protected Question(int id, string text, Category category, Topic topic)
        {
            Id = id;
            Text = text;
            Category = category;
            Topic = topic;
        }

        public int Id { get; private set; }
        public string Text { get; private set; }
        public Category Category { get; private set; }
        public Topic Topic { get; private set; }
        public abstract string Type { get; }
    }

    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(int id, string text, Category category, Topic topic, List<string> options, string correctAnswer)
            : base(id, text, category, topic)
        {
            Options = options;
            CorrectAnswer = correctAnswer;
        }

        public List<string> Options { get; private set; }
        public string CorrectAnswer { get; private set; }
        public override string Type
        {
            get { return "MCQ"; }
        }
    }

    public class ParagraphQuestion : Question
    {
        public ParagraphQuestion(int id, string text, Category category, Topic topic, int wordLimit)
            : base(id, text, category, topic)
        {
            WordLimit = wordLimit;
        }

        public int WordLimit { get; private set; }
        public override string Type
        {
            get { return "Paragraph"; }
        }
    }

    public class ExamPortal
    {
        private readonly List<Question> _questions;

        public ExamPortal(List<Question> questions)
        {
            _questions = questions;
        }

        public int GetTotalQuestions()
        {
            return _questions.Count;
        }

        public List<Question> GetQuestionsByTopic(string topicName)
        {
            return _questions.Where(delegate(Question q)
            {
                return q.Topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase);
            }).ToList();
        }

        public List<Question> GetQuestionsByTopicAndCategory(string topicName, string categoryName)
        {
            return _questions.Where(delegate(Question q)
            {
                return q.Topic.Name.Equals(topicName, StringComparison.OrdinalIgnoreCase)
                    && q.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase);
            }).ToList();
        }
    }
}