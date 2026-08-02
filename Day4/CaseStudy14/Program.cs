using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy14
{
    public partial class Product
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
    }

    public partial class Product
    {
        public DateTime ExpiryDate { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            InventoryRepository<Product> repository = new InventoryRepository<Product>();
            Product product = new Product
            {
                SKU = "P101",
                Quantity = 50,
                ExpiryDate = DateTime.Today.AddDays(20)
            };
            Product lowStock = new Product
            {
                SKU = "P102",
                Quantity = 3,
                ExpiryDate = DateTime.Today.AddDays(-1)
            };

            repository.Add(product);
            repository.Add(lowStock);

            Console.WriteLine(repository["P101"].SKU);
            Console.WriteLine("Low stock count: " + repository.GetAll().GetLowStockItems().Count);
            Console.WriteLine("Expired count: " + repository.GetAll().GetExpiredItems().Count);
        }
    }

    public interface IInventoryItem
    {
        string SKU { get; }
        int Quantity { get; }
        DateTime ExpiryDate { get; }
    }

    public class InventoryRepository<T> where T : Product
    {
        private readonly Dictionary<string, T> _items = new Dictionary<string, T>();
        public void Add(T item) { _items[item.SKU] = item; }
        public T this[string sku] { get { return _items[sku]; } }
        public List<T> GetAll() { return _items.Values.ToList(); }
    }

    public static class InventoryExtensions
    {
        public static List<T> GetLowStockItems<T>(this List<T> items) where T : Product
        {
            return items.Where(delegate(T item) { return item.Quantity < 10; }).ToList();
        }

        public static List<T> GetExpiredItems<T>(this List<T> items) where T : Product
        {
            return items.Where(delegate(T item) { return item.ExpiryDate < DateTime.Today; }).ToList();
        }
    }
}