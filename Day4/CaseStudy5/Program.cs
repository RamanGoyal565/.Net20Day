using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy5
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
    }

    public partial class Book
    {
        public string Author { get; set; }
    }

    public class Magazine
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Library library = new Library();
            library.AddBook(new Book { Id = 1, Title = "Clean Code", Author = "Robert Martin", IsAvailable = true });
            library.AddBook(new Book { Id = 2, Title = "CLR via C#", Author = "Richter", IsAvailable = false });

            Console.WriteLine("Found: " + library["Clean Code"].Title);
            library.Borrow("Clean Code");
            library.Return("Clean Code");

            List<Book> available = library.Books.GetAvailableBooks();
            foreach (Book book in available)
            {
                Console.WriteLine("Available book: " + book.Title);
            }
        }
    }

    public interface IRepository<T>
    {
        void Add(T item);
        List<T> GetAll();
    }

    internal class GenericRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        public void Add(T item) { _items.Add(item); }
        public List<T> GetAll() { return _items; }
    }

    public class Library
    {
        private readonly GenericRepository<Book> _bookRepository = new GenericRepository<Book>();
        public List<Book> Books { get { return _bookRepository.GetAll(); } }

        public Book this[string title]
        {
            get { return Books.First(delegate(Book book) { return book.Title.Equals(title, StringComparison.OrdinalIgnoreCase); }); }
        }

        public void AddBook(Book book) { _bookRepository.Add(book); }
        public void Borrow(string title) { this[title].IsAvailable = false; }
        public void Return(string title) { this[title].IsAvailable = true; }
        public List<Book> Search(string text) { return Books.Where(delegate(Book book) { return book.Title.Contains(text); }).ToList(); }
    }

    public static class BookExtensions
    {
        public static List<Book> GetAvailableBooks(this List<Book> books)
        {
            return books.Where(delegate(Book book) { return book.IsAvailable; }).ToList();
        }
    }
}