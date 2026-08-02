using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CacheManager<int> numberCache = new CacheManager<int>();
            numberCache.Add("one", 1, DateTime.Now.AddMinutes(10));
            numberCache.Add("two", 2, DateTime.Now.AddMinutes(-5));

            CacheManager<Customer> customerCache = new CacheManager<Customer>();
            customerCache.Add("c1", new Customer(1, "Anu"), DateTime.Now.AddHours(1));

            Console.WriteLine("Case Study 2");
            Console.WriteLine("numberCache[one] = " + numberCache["one"]);
            Console.WriteLine("Customer name = " + customerCache.GetByKey("c1").Name);
            Console.WriteLine("All Keys: " + string.Join(", ", numberCache.GetAllKeys()));
            Console.WriteLine("Expired Count: " + numberCache.CountExpiredItems());
        }
    }

    public class Customer
    {
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Order
    {
        public Order(int id, decimal total)
        {
            Id = id;
            Total = total;
        }

        public int Id { get; private set; }
        public decimal Total { get; private set; }
    }

    public class CacheEntry<T>
    {
        public CacheEntry(string key, T value, DateTime expiresAt)
        {
            Key = key;
            Value = value;
            ExpiresAt = expiresAt;
        }

        public string Key { get; private set; }
        public T Value { get; private set; }
        public DateTime ExpiresAt { get; private set; }
    }

    public class InvalidCacheKeyException : Exception
    {
        public InvalidCacheKeyException(string key) : base("Invalid key: " + key)
        {
        }
    }

    public class CacheManager<T>
    {
        private readonly Dictionary<string, CacheEntry<T>> _items = new Dictionary<string, CacheEntry<T>>();

        public void Add(string key, T value, DateTime expiresAt)
        {
            _items[key] = new CacheEntry<T>(key, value, expiresAt);
        }

        public void Remove(string key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new InvalidCacheKeyException(key);
            }

            _items.Remove(key);
        }

        public T GetByKey(string key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new InvalidCacheKeyException(key);
            }

            return _items[key].Value;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public CacheEntry<T> GetEntry(string key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new InvalidCacheKeyException(key);
            }

            return _items[key];
        }

        public IEnumerable<CacheEntry<T>> GetEntries()
        {
            return _items.Values;
        }

        public T this[string key]
        {
            get { return GetByKey(key); }
        }
    }

    public static class CacheExtensions
    {
        public static List<string> GetAllKeys<T>(this CacheManager<T> cacheManager)
        {
            return cacheManager.GetEntries().Select(delegate(CacheEntry<T> entry) { return entry.Key; }).ToList();
        }

        public static int CountExpiredItems<T>(this CacheManager<T> cacheManager)
        {
            return cacheManager.GetEntries().Count(delegate(CacheEntry<T> entry) { return entry.ExpiresAt < DateTime.Now; });
        }
    }
}