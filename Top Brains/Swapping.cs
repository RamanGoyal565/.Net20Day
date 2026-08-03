// Swap two numbers using:

// ·        Method 1: ref

// ·        Method 2: out

using System;

class Program
{
    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void Main()
    {
        int x = 10;
        int y = 20;

        Console.WriteLine("Before Swapping:");
        Console.WriteLine("x = " + x + ", y = " + y);

        Swap(ref x, ref y);

        Console.WriteLine("After Swapping:");
        Console.WriteLine("x = " + x + ", y = " + y);
    }
}