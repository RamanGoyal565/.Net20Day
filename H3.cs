// A logistics company calculates package shipping charges using different pricing strategies 
// based on package type. Incorrect weight and distance values frequently occur.
// Build a Console application using an interface-based design where different package types 
// calculate shipping cost differently. Validate numeric inputs, prevent negative or 
// overflow-prone calculations, and display the final shipping cost.
// Interfaces, multiple classes, arithmetic operators, numeric conversions, validation, 
// TryParse, correctness
// Package Type, Weight, Distance
// Display shipping cost according to package type. Reject invalid or unreasonable input values.
using System;
interface IShipping
{
    double Calculate(double weight, double distance);
}
class StandardPackage : IShipping
{
    public double Calculate(double weight, double distance)
    {
        return weight * distance * 0.50;
    }
}
class ExpressPackage : IShipping
{
    public double Calculate(double weight, double distance)
    {
        return weight * distance * 0.80;
    }
}
class H3
{
    public static void Main()
    {
        Console.Write("Package Type (Standard/Express): ");
        string type = Console.ReadLine();
        double weight, distance;
        Console.Write("Weight: ");
        if (!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
        {
            Console.WriteLine("Invalid Weight");
            return;
        }
        Console.Write("Distance: ");
        if (!double.TryParse(Console.ReadLine(), out distance) || distance <= 0)
        {
            Console.WriteLine("Invalid Distance");
            return;
        }
        IShipping ship;
        if (type.Equals("Standard", StringComparison.OrdinalIgnoreCase))
            ship = new StandardPackage();
        else if (type.Equals("Express", StringComparison.OrdinalIgnoreCase))
            ship = new ExpressPackage();
        else
        {
            Console.WriteLine("Invalid Package Type");
            return;
        }
        Console.WriteLine("Shipping Cost = " + Math.Round(ship.Calculate(weight, distance), 2));
    }
}