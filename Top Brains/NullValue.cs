// Compute the average of non-null values in a nullable double array.
// - Ignore nulls.
// - If there are no non-null values, return null.
// Round to 2 decimals (AwayFromZero).

// Input: values (double?[])
// Output: average (double?)

// Constraints:
// 0 <= values.Length <= 2*10^5

using System;

class Program
{
    static double? Average(double?[] values)
    {
        double sum = 0;
        int count = 0;

        foreach (double? value in values)
        {
            if (value.HasValue)
            {
                sum += value.Value;
                count++;
            }
        }

        if (count == 0)
            return null;

        return Math.Round(sum / count, 2, MidpointRounding.AwayFromZero);
    }

    static void Main()
    {
        double?[] values = { 1.5, null, 2.5, 3.0, null };

        double? result = Average(values);

        Console.WriteLine(result.HasValue ? result.Value.ToString("F2") : "null");
    }
