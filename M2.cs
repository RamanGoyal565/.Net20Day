// A fitness trainer wants a console utility that calculates Body Mass Index (BMI) for clients,
// but users frequently enter height in different formats.
// Build a Console application that accepts weight (kg) and height (meters). Validate numeric 
// input, calculate BMI, round to two decimals, and classify the user into BMI categories. 
// Handle invalid and zero values safely.
// Variables, primitive types, Math operators, Parse/TryParse, conditional logic, Math.Round()
// Weight: 72.5 Height: 1.72
// Display BMI and appropriate category. Reject zero or negative height and invalid 
// numeric values.

using System;
namespace Day1
{
    public class M2
    {
        public static void Main()
        {
            double weight,height;
            Console.Write("Enter Weight: ");
            if (!double.TryParse(Console.ReadLine(), out weight))
            {
                Console.WriteLine("Error: Invalid weight. Please enter a numeric value.");
                return;
            }
            if (weight <= 0)
            {
                Console.WriteLine("Error: Weight cannot be zero or negative.");
                return;
            }
            Console.Write("Enter Height: ");
            if (!double.TryParse(Console.ReadLine(), out height))
            {
                Console.WriteLine("Error: Invalid height. Please enter a numeric value.");
                return;
            }
            if (height <= 0)
            {
                Console.WriteLine("Error: Height cannot be zero or negative.");
                return;
            }
            double BMI=Math.Round(weight/Math.Pow(height,2),2);
            Console.WriteLine($"BMI : {BMI}");
        }
    }
}