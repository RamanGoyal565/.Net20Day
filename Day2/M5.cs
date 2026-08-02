using System;

public static class MathOperations
{
    // Add two numbers
    public static int Add(int a, int b)
    {
        return a + b;
    }

    // Add multiple numbers
    public static int Add(params int[] numbers)
    {
        int sum = 0;

        foreach (int n in numbers)
            sum += n;

        return sum;
    }

    // Multiply two numbers
    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    // Multiply multiple numbers
    public static int Multiply(params int[] numbers)
    {
        int product = 1;

        foreach (int n in numbers)
            product *= n;

        return product;
    }
}

class M5
{
    public static void main()
    {
        Console.WriteLine(MathOperations.Add(5, 10));

        Console.WriteLine(MathOperations.Add(1, 2, 3, 4, 5));

        Console.WriteLine(MathOperations.Multiply(2, 3));

        Console.WriteLine(MathOperations.Multiply(2, 3, 4, 5));
    }
}