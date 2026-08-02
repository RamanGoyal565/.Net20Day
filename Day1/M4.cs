// A bank employee manually enters customer deposit and withdrawal amounts to determine the 
// remaining balance. Incorrect inputs are common.
// Design a Console application that accepts opening balance, total deposits, and total 
// withdrawals. Validate all values and calculate the final balance. Prevent withdrawals 
// from exceeding the available balance.
// Variables, arithmetic operators, numeric conversions, TryParse, validation, business rules
// Opening Balance: 5000 Deposits: 2500 Withdrawals: 3200
// Display updated balance. Show an error if withdrawals exceed available funds or inputs are invalid.

using System;
namespace Day1{
    class M4
    {
        public static void Main()
        {
            double openingBalance, deposits, withdrawals;
            Console.Write("Enter Opening Balance: ");
            if (!double.TryParse(Console.ReadLine(), out openingBalance))
            {
                Console.WriteLine("Error: Invalid opening balance.");
                return;
            }
            if (openingBalance < 0)
            {
                Console.WriteLine("Error: Opening balance cannot be negative.");
                return;
            }
            Console.Write("Enter Total Deposits: ");
            if (!double.TryParse(Console.ReadLine(), out deposits))
            {
                Console.WriteLine("Error: Invalid deposit amount.");
                return;
            }
            if (deposits < 0)
            {
                Console.WriteLine("Error: Deposits cannot be negative.");
                return;
            }
            Console.Write("Enter Total Withdrawals: ");
            if (!double.TryParse(Console.ReadLine(), out withdrawals))
            {
                Console.WriteLine("Error: Invalid withdrawal amount.");
                return;
            }
            if (withdrawals < 0)
            {
                Console.WriteLine("Error: Withdrawals cannot be negative.");
                return;
            }
            double availableBalance = openingBalance + deposits;
            if (withdrawals > availableBalance)
            {
                Console.WriteLine("Error: Withdrawals exceed available balance.");
                return;
            }
            double finalBalance = availableBalance - withdrawals;
            Console.WriteLine($"\nUpdated Balance: {finalBalance:F2}");
        }
    }
}