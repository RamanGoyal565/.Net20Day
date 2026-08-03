// Multiplication table
// Description
// Return the multiplication table row for a number n from 1..upto.
// Example: n=3, upto=5 -> [3,6,9,12,15]

// Input: n (int), upto (int)
// Output: row (int[])

// Constraints:
// 0 <= upto <= 1e5
// -1e4 <= n <= 1e4

using System;

class Program
{
    static int[] MultiplicationTable(int n, int upto)
    {
        int[] row = new int[upto];

        for (int i = 1; i <= upto; i++)
        {
            row[i - 1] = n * i;
        }

        return row;
    }

    static void Main()
    {
        int n = 3;
        int upto = 5;

        int[] result = MultiplicationTable(n, upto);

        Console.WriteLine(string.Join(" ", result));
    }
}