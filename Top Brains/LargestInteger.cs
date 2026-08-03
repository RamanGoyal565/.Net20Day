// Write a method that returns the largest of three integers using conditional logic.

// Input: a (int), b (int), c (int)
// Output: largest (int)

// Constraints:
// -1e9 <= a,b,c <= 1e9

using System;

class Program
{
    static int LargestOfThree(int a, int b, int c)
    {
        int largest = a;

        if (b > largest)
            largest = b;

        if (c > largest)
            largest = c;

        return largest;
    }

    static void Main()
    {
        int a = 10, b = 25, c = 15;

        Console.WriteLine(LargestOfThree(a, b, c));
    }
}