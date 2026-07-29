// A retail store cashier enters the price of an item, quantity purchased, and discount percentage. 
// Invalid values are sometimes entered due to typing mistakes.
// Develop a Console application that accepts item price, quantity, and discount percentage. 
// Validate all inputs using TryParse, calculate subtotal, discount amount, and final payable amount. 
// Reject negative values and prompt the user appropriately.
// Console input/output, variables, primitive types, arithmetic operators, Math.Round(),
// TryParse, validation
// Price: 249.99 Quantity: 3 Discount: 10
// Display subtotal, discount amount, final payable amount rounded to two decimal places. 
// Display validation errors for invalid or negative input.


using System;
namespace Day1
{
    public class M1
    {
        public static void Main()
        {
            double price, discount;
            int quantity;
            Console.Write("Enter Item Price: ");
            if (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Error: Invalid price. Please enter a numeric value.");
                return;
            }
            if (price < 0)
            {
                Console.WriteLine("Error: Price cannot be negative.");
                return;
            }
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Error: Invalid quantity. Please enter a whole number.");
                return;
            }
            if (quantity < 0)
            {
                Console.WriteLine("Error: Quantity cannot be negative.");
                return;
            }
            Console.Write("Enter Discount Percentage: ");
            if (!double.TryParse(Console.ReadLine(), out discount))
            {
                Console.WriteLine("Error: Invalid discount percentage. Please enter a numeric value.");
                return;
            }
            if (discount < 0)
            {
                Console.WriteLine("Error: Discount percentage cannot be negative.");
                return;
            }
            double subtotal = price * quantity;
            double discountAmount = subtotal * discount / 100;
            double finalAmount = subtotal - discountAmount;
            Console.WriteLine("\n----- Bill Summary -----");
            Console.WriteLine($"Subtotal         : {Math.Round(subtotal, 2):F2}");
            Console.WriteLine($"Discount Amount  : {Math.Round(discountAmount, 2):F2}");
            Console.WriteLine($"Final Payable    : {Math.Round(finalAmount, 2):F2}");
        }
    }
}
