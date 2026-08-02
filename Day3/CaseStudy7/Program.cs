using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Shoe runner = new Shoe("SH1", "AirFlex", "Nike", ShoeKind.Running);
            Shoe loafer = new Shoe("SH2", "Classic Walk", "Bata", ShoeKind.Casual);
            Shoe spike = new Shoe("SH3", "Power Spike", "Adidas", ShoeKind.Sports);

            ShoeCollectionHouse house = new ShoeCollectionHouse("FootWorld", new List<InventoryItem>
            {
                new InventoryItem(runner, 12),
                new InventoryItem(loafer, 8),
                new InventoryItem(spike, 6)
            });

            Customer customer1 = new Customer("C1", "Karan");
            Customer customer2 = new Customer("C2", "Pooja");

            house.AddTransaction(new Transaction("TR1", customer1, runner, TransactionType.Buy, new DateOnly(2026, 7, 30)));
            house.AddTransaction(new Transaction("TR2", customer1, loafer, TransactionType.Replace, new DateOnly(2026, 8, 1)));
            house.AddTransaction(new Transaction("TR3", customer2, runner, TransactionType.Buy, new DateOnly(2026, 8, 2)));

            Console.WriteLine("Case Study 7");
            PrintKindCounts(house.GetShoeCountByKind());
            PrintTransactions("Transaction history for Karan", house.GetTransactionsByCustomer("Karan"));
            PrintCustomers("Customers who bought AirFlex", house.GetCustomersWhoBoughtShoe("AirFlex"));
        }

        private static void PrintKindCounts(Dictionary<ShoeKind, int> counts)
        {
            Console.WriteLine("Number of shoes for each kind");
            foreach (KeyValuePair<ShoeKind, int> count in counts)
            {
                Console.WriteLine("- " + count.Key + ": " + count.Value);
            }
        }

        private static void PrintTransactions(string title, IEnumerable<Transaction> transactions)
        {
            Console.WriteLine(title);
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine("- " + transaction.Date.ToString("yyyy-MM-dd") + " | " + transaction.Type + " | " + transaction.Shoe.ModelName);
            }
        }

        private static void PrintCustomers(string title, IEnumerable<Customer> customers)
        {
            Console.WriteLine(title);
            foreach (Customer customer in customers.GroupBy(delegate(Customer c) { return c.Id; }).Select(delegate(IGrouping<string, Customer> group) { return group.First(); }))
            {
                Console.WriteLine("- " + customer.Name);
            }
        }
    }

    public enum ShoeKind
    {
        Running,
        Casual,
        Sports
    }

    public enum TransactionType
    {
        Buy,
        Replace
    }

    public class Shoe
    {
        public Shoe(string id, string modelName, string brand, ShoeKind kind)
        {
            Id = id;
            ModelName = modelName;
            Brand = brand;
            Kind = kind;
        }

        public string Id { get; private set; }
        public string ModelName { get; private set; }
        public string Brand { get; private set; }
        public ShoeKind Kind { get; private set; }
    }

    public class InventoryItem
    {
        public InventoryItem(Shoe shoe, int quantity)
        {
            Shoe = shoe;
            Quantity = quantity;
        }

        public Shoe Shoe { get; private set; }
        public int Quantity { get; private set; }
    }

    public class Customer
    {
        public Customer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Transaction
    {
        public Transaction(string id, Customer customer, Shoe shoe, TransactionType type, DateOnly date)
        {
            Id = id;
            Customer = customer;
            Shoe = shoe;
            Type = type;
            Date = date;
        }

        public string Id { get; private set; }
        public Customer Customer { get; private set; }
        public Shoe Shoe { get; private set; }
        public TransactionType Type { get; private set; }
        public DateOnly Date { get; private set; }
    }

    public class ShoeCollectionHouse
    {
        private readonly List<InventoryItem> _inventory;
        private readonly List<Transaction> _transactions;

        public ShoeCollectionHouse(string name, List<InventoryItem> inventory)
        {
            Name = name;
            _inventory = inventory;
            _transactions = new List<Transaction>();
        }

        public string Name { get; private set; }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public Dictionary<ShoeKind, int> GetShoeCountByKind()
        {
            return _inventory.GroupBy(delegate(InventoryItem item) { return item.Shoe.Kind; })
                .ToDictionary(delegate(IGrouping<ShoeKind, InventoryItem> group) { return group.Key; }, delegate(IGrouping<ShoeKind, InventoryItem> group) { return group.Sum(delegate(InventoryItem item) { return item.Quantity; }); });
        }

        public List<Transaction> GetTransactionsByCustomer(string customerName)
        {
            return _transactions.Where(delegate(Transaction transaction) { return transaction.Customer.Name.Equals(customerName, StringComparison.OrdinalIgnoreCase); }).ToList();
        }

        public List<Customer> GetCustomersWhoBoughtShoe(string modelName)
        {
            return _transactions.Where(delegate(Transaction transaction)
            {
                return transaction.Type == TransactionType.Buy && transaction.Shoe.ModelName.Equals(modelName, StringComparison.OrdinalIgnoreCase);
            }).Select(delegate(Transaction transaction) { return transaction.Customer; }).ToList();
        }
    }
}