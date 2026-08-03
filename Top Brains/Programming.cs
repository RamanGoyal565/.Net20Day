// In a lucky draw, contestants with ticket numbers having a special property are chosen as Lucky Winners.

// Let S(n) be the sum of the digits of n. Example:

// S(484) = 4 + 8 + 4 = 16

// S(22) = 2 + 2 = 4

// A non-prime positive integer x is called a Lucky Number if:

// S(x × x) = S(x) × S(x)

// Example: 22 is a Lucky Number because:

// S(22) = 4

// S(484) = 16

// And 4 × 4 = 16 

// You must find how many Lucky Numbers lie between m and n (inclusive).

using System;

class Program
{
    // Returns sum of digits
    static int SumOfDigits(long num)
    {
        int sum = 0;
        while (num > 0)
        {
            sum += (int)(num % 10);
            num /= 10;
        }
        return sum;
    }

    // Checks if a number is prime
    static bool IsPrime(int num)
    {
        if (num < 2)
            return false;

        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0)
                return false;
        }

        return true;
    }

    // Checks whether a number is Lucky
    static bool IsLucky(int x)
    {
        if (x <= 1 || IsPrime(x))
            return false;

        int sum = SumOfDigits(x);
        int squareSum = SumOfDigits((long)x * x);

        return squareSum == sum * sum;
    }

    static void Main()
    {
        int m = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        int count = 0;

        for (int i = m; i <= n; i++)
        {
            if (IsLucky(i))
                count++;
        }

        Console.WriteLine(count);
    }
}