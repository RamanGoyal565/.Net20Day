// A financial analysis tool receives investment details entered manually. Different investment
// types apply different return calculations, and users often enter invalid percentages or 
// investment amounts.
// Develop a Console application with an extensible class design where different investment 
// calculators implement a common interface. Accept validated numeric inputs, calculate projected 
// returns, handle conversion errors, round results appropriately, and ensure the application 
// is easy to extend for future investment types.
// Interfaces, multiple classes, arithmetic operations, Math functions, Parse/TryParse, type 
// conversions, extensibility, validation
// Investment Type, Principal Amount, Annual Rate (%), Duration (Years)
// Display projected investment value with proper rounding. Invalid numeric input, negative 
// values, or impossible percentages must be handled safely without crashing.

using System;
interface IInvestment
{
    double Calculate(double principal, double rate, double years);
}
class SimpleInvestment : IInvestment
{
    public double Calculate(double principal, double rate, double years)
    {
        return principal + (principal * rate * years / 100);
    }
}
class CompoundInvestment : IInvestment
{
    public double Calculate(double principal, double rate, double years)
    {
        return principal * Math.Pow(1 + rate / 100, years);
    }
}
class H5
{
    public static void Main()
    {
        Console.Write("Investment Type (Simple/Compound): ");
        string type = Console.ReadLine();
        double principal, rate, years;
        Console.Write("Principal Amount: ");
        if (!double.TryParse(Console.ReadLine(), out principal) || principal < 0)
        {
            Console.WriteLine("Invalid Principal");
            return;
        }
        Console.Write("Annual Rate (%): ");
        if (!double.TryParse(Console.ReadLine(), out rate) || rate < 0 || rate > 100)
        {
            Console.WriteLine("Invalid Rate");
            return;
        }
        Console.Write("Duration (Years): ");
        if (!double.TryParse(Console.ReadLine(), out years) || years < 0)
        {
            Console.WriteLine("Invalid Duration");
            return;
        }
        IInvestment invest;
        if (type.Equals("Simple", StringComparison.OrdinalIgnoreCase))
            invest = new SimpleInvestment();
        else if (type.Equals("Compound", StringComparison.OrdinalIgnoreCase))
            invest = new CompoundInvestment();
        else
        {
            Console.WriteLine("Invalid Investment Type");
            return;
        }
        Console.WriteLine("Projected Value = " +
            Math.Round(invest.Calculate(principal, rate, years), 2));
    }
}