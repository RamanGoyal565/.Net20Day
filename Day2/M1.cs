using System;

public static class FinancialCalculator
{
    // Overload with default compounding frequency (Annually)
    public static double CalculateCompoundInterest(double principal, double rate, int time)
    {
        return CalculateCompoundInterest(principal, rate, time, 1);
    }

    // Overload with compounding frequency
    public static double CalculateCompoundInterest(double principal, double rate, int time, int compoundingFrequency)
    {
        return principal * Math.Pow(1 + rate / compoundingFrequency,
            compoundingFrequency * time);
    }
}

class M1
{
    public static void main()
    {
        double fv1 = FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10);
        double fv2 = FinancialCalculator.CalculateCompoundInterest(
            principal: 10000,
            rate: 0.05,
            time: 10,
            compoundingFrequency: 12);

        Console.WriteLine($"Future Value (Annual): {fv1:F2}");
        Console.WriteLine($"Future Value (Monthly): {fv2:F2}");
    }
}