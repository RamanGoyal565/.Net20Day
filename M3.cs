// A warehouse manager records package dimensions to calculate shipping volume. 
// Employees occasionally enter decimal values incorrectly.
// Create a Console application that accepts length, width, and height. 
// Validate inputs, calculate volume, and display the result. Reject invalid or 
// non-positive dimensions and prevent calculation until valid data is entered.
// Console application, double, variables, operators, Parse/TryParse, validation
// Length: 25.5 Width: 18.2 Height: 12
// Display calculated volume. Handle invalid numeric entries gracefully without crashing.

using System;
namespace Day1
{
    public class M3
    {
        public static void Main()
        { 
            double length, width, height;
            while (true)
            {
                Console.Write("Enter Length: ");
                if (double.TryParse(Console.ReadLine(), out length) && length > 0)
                    break;
                Console.WriteLine("Error: Please enter a valid positive number.");
            }
            while (true)
            {
                Console.Write("Enter Width: ");
                if (double.TryParse(Console.ReadLine(), out width) && width > 0)
                    break;
                Console.WriteLine("Error: Please enter a valid positive number.");
            }
            while (true)
            {
                Console.Write("Enter Height: ");
                if (double.TryParse(Console.ReadLine(), out height) && height > 0)
                    break;
                Console.WriteLine("Error: Please enter a valid positive number.");
            }
            double volume = length * width * height;
            Console.WriteLine($"\nVolume = {volume}");
        }
    }  
}