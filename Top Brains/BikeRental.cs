using System;
using System.Collections.Generic;

public class Bike
{
    public string Model { get; set; }
    public int PricePerDay { get; set; }
    public string Brand { get; set; }
}

public class BikeUtility
{
    public void AddBikeDetails(string model, string brand, int pricePerDay)
    {
        int key = Program.bikeDetails.Count + 1;

        Bike bike = new Bike();
        bike.Model = model;
        bike.Brand = brand;
        bike.PricePerDay = pricePerDay;

        Program.bikeDetails.Add(key, bike);
    }

    public SortedDictionary<string, List<Bike>> GroupBikesByBrand()
    {
        SortedDictionary<string, List<Bike>> grouped =
            new SortedDictionary<string, List<Bike>>();

        foreach (KeyValuePair<int, Bike> item in Program.bikeDetails)
        {
            Bike bike = item.Value;

            if (!grouped.ContainsKey(bike.Brand))
            {
                grouped[bike.Brand] = new List<Bike>();
            }

            grouped[bike.Brand].Add(bike);
        }

        return grouped;
    }
}

public class Program
{
    public static SortedDictionary<int, Bike> bikeDetails =
        new SortedDictionary<int, Bike>();

    public static void Main(string[] args)
    {
        BikeUtility utility = new BikeUtility();

        while (true)
        {
            Console.WriteLine("1. Add Bike Details");
            Console.WriteLine("2. Group Bikes By Brand");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the model: ");
                    string model = Console.ReadLine();

                    Console.Write("Enter the brand: ");
                    string brand = Console.ReadLine();

                    Console.Write("Enter the price per day: ");
                    int price = Convert.ToInt32(Console.ReadLine());

                    utility.AddBikeDetails(model, brand, price);

                    Console.WriteLine("Bike details added successfully");
                    break;

                case 2:
                    SortedDictionary<string, List<Bike>> result =
                        utility.GroupBikesByBrand();

                    foreach (KeyValuePair<string, List<Bike>> item in result)
                    {
                        foreach (Bike bike in item.Value)
                        {
                            Console.WriteLine(item.Key + " " + bike.Model);
                        }
                    }
                    break;

                case 3:
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

            Console.WriteLine();
        }
    }
}