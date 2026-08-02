// A school administrator enters marks obtained in five subjects to calculate student 
// performance. Some marks are accidentally entered as text or outside the valid range.
// Build a Console application that accepts marks for five subjects, validates that each mark 
// is between 0 and 100, calculates total, average, percentage, and displays the rounded percentage.
// Arrays not required, variables, arithmetic operators, Parse/TryParse, Math.Round(), validation
// Five marks such as: 78, 82, 91, 65, 88
// Display total, average, percentage, and reject invalid marks or non-numeric values.


using System;
namespace Day1{
    class M5
    {
        public static void Main()
        {
            double m1, m2, m3, m4, m5;
            Console.Write("Enter Marks for Subject 1: ");
            if (!double.TryParse(Console.ReadLine(), out m1) || m1 < 0 || m1 > 100)
            {
                Console.WriteLine("Error: Invalid marks for Subject 1.");
                return;
            }
            Console.Write("Enter Marks for Subject 2: ");
            if (!double.TryParse(Console.ReadLine(), out m2) || m2 < 0 || m2 > 100)
            {
                Console.WriteLine("Error: Invalid marks for Subject 2.");
                return;
            }
            Console.Write("Enter Marks for Subject 3: ");
            if (!double.TryParse(Console.ReadLine(), out m3) || m3 < 0 || m3 > 100)
            {
                Console.WriteLine("Error: Invalid marks for Subject 3.");
                return;
            }
            Console.Write("Enter Marks for Subject 4: ");
            if (!double.TryParse(Console.ReadLine(), out m4) || m4 < 0 || m4 > 100)
            {
                Console.WriteLine("Error: Invalid marks for Subject 4.");
                return;
            }
            Console.Write("Enter Marks for Subject 5: ");
            if (!double.TryParse(Console.ReadLine(), out m5) || m5 < 0 || m5 > 100)
            {
                Console.WriteLine("Error: Invalid marks for Subject 5.");
                return;
            }
            double total = m1 + m2 + m3 + m4 + m5;
            double average = total / 5;
            double percentage = (total / 500) * 100;
            Console.WriteLine("\n----- Student Result -----");
            Console.WriteLine("Total Marks : " + total);
            Console.WriteLine("Average     : " + average);
            Console.WriteLine("Percentage  : " + Math.Round(percentage, 2) + "%");
        }
    }
}