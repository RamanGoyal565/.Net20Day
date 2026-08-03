// Given a person's height in centimeters, return the height category:
// - "Short"  : height < 150
// - "Average": 150 <= height < 180
// - "Tall"   : height >= 180

// Input: heightCm (int)
// Output: category (string)

// Constraints:
// 0 <= heightCm <= 300

using System;

class Program
{
    static string GetHeightCategory(int heightCm)
    {
        if (heightCm < 150)
            return "Short";
        else if (heightCm < 180)
            return "Average";
        else
            return "Tall";
    }

    static void Main()
    {
        int heightCm = int.Parse(Console.ReadLine());

        Console.WriteLine(GetHeightCategory(heightCm));
    }
}