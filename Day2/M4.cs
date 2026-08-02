using System;

public static class Geometry
{
    // Circle
    public static double CalculateArea(double radius, int decimals = 2)
    {
        return Math.Round(Math.PI * radius * radius, decimals);
    }

    // Rectangle
    public static double CalculateArea(int length, int width)
    {
        return length * width;
    }

    // Triangle
    public static double CalculateArea(double baseValue, double height)
    {
        return 0.5 * baseValue * height;
    }
}

class M4
{
    public static void main()
    {
        Console.WriteLine("Circle: " + Geometry.CalculateArea(5));

        Console.WriteLine("Rectangle: " +
            Geometry.CalculateArea(4, 6));

        Console.WriteLine("Triangle: " +
            Geometry.CalculateArea(3.0, 7.0));

        Console.WriteLine("Circle (4 decimals): " +
            Geometry.CalculateArea(radius: 5, decimals: 4));
    }
}