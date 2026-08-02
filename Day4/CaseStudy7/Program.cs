using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ShoppingCart<Product> cart = new ShoppingCart<Product>();
            cart.AddItem(new Product(1, "Laptop", 50000m));
            cart.AddItem(new Product(2, "Mouse", 1000m));
            cart.AddItem(new Product(3, "Keyboard", 2000m));

            decimal discount = cart.ApplyFlatDiscount(3000m);
            object invoice = new
            {
                ItemCount = cart.Count,
                Total = cart.TotalPrice(),
                Discount = discount
            };

            Console.WriteLine(cart[0].Name);
            Console.WriteLine(invoice);
        }
    }

    public class Product
    {
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }

    public class ShoppingCart<T> where T : Product
    {
        private readonly List<T> _items = new List<T>();
        public int Count { get { return _items.Count; } }
        public void AddItem(T item) { _items.Add(item); }
        public void RemoveItem(T item) { _items.Remove(item); }
        public decimal TotalPrice() { return _items.Sum(delegate(T item) { return item.Price; }); }
        public T this[int index] { get { return _items[index]; } }
    }

    public static class CartExtensions
    {
        public static decimal ApplyFlatDiscount<T>(this ShoppingCart<T> cart, decimal amount) where T : Product
        {
            decimal total = cart.TotalPrice();
            if (amount > total) return total;
            return amount;
        }
    }
}