using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy8.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Employee
    {
        public string Department { get; set; }
    }

    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
    }
}

namespace CaseStudy8
{
    using CaseStudy8.Entities;

    public class Program
    {
        public static void Main(string[] args)
        {
            MiniOrm db = new MiniOrm();
            Employee employee = new Employee { Id = 1, Name = "Ravi", Department = "IT" };
            Customer customer = new Customer { Id = 1, Name = "Pooja" };
            Order order = new Order { Id = 5, Total = 2500m };

            db.Save<Employee>(employee.Id, employee);
            db.Save<Customer>(customer.Id, customer);
            db.Save<Order>(order.Id, order);

            Console.WriteLine(db.Get<Employee>(1).Name);
            Console.WriteLine(db.GetAll<Customer>().Count);
            db.Delete<Order>(5);
        }
    }

    public class MiniOrm
    {
        private readonly Dictionary<string, Dictionary<int, object>> _store = new Dictionary<string, Dictionary<int, object>>();

        public void Save<T>(int id, T entity)
        {
            string typeName = typeof(T).Name;
            if (!_store.ContainsKey(typeName))
            {
                _store[typeName] = new Dictionary<int, object>();
            }

            _store[typeName][id] = entity;
        }

        public T Get<T>(int id)
        {
            string typeName = typeof(T).Name;
            return (T)_store[typeName][id];
        }

        public void Delete<T>(int id)
        {
            string typeName = typeof(T).Name;
            if (_store.ContainsKey(typeName))
            {
                _store[typeName].Remove(id);
            }
        }

        public List<T> GetAll<T>()
        {
            string typeName = typeof(T).Name;
            if (!_store.ContainsKey(typeName))
            {
                return new List<T>();
            }

            return _store[typeName].Values.Select(delegate(object value) { return (T)value; }).ToList();
        }
    }
}