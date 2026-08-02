// A utility billing department wants a reusable billing calculator where different customer 
// types (Residential and Commercial) calculate bills differently, and input files frequently 
// contain invalid values.
// Design a Console application using multiple classes and interfaces to calculate electricity 
// bills. Accept validated user inputs, apply customer-specific calculation rules, and ensure 
// invalid numeric values do not terminate the application.
// Multiple classes, interfaces, TryParse, arithmetic operators, type conversions, validation, 
// extensibility
// Customer Type, Units Consumed, Rate, Fixed Charges
// Correct bill calculation based on customer type. Invalid inputs should be handled gracefully 
// with meaningful messages.

using System;
interface IBillCalculator
{
    double CalculateBill(double units, double rate, double fixedCharges);
}
class ResidentialCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        return (units * rate) + fixedCharges;
    }
}
class CommercialCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        return (units * rate * 1.20) + fixedCharges;
    }
}
class H1
{
    public static void Main()
    {
        Console.Write("Customer Type (Residential/Commercial): ");
        string type = Console.ReadLine();
        double units, rate, fixedCharges;
        Console.Write("Units Consumed: ");
        if (!double.TryParse(Console.ReadLine(), out units) || units < 0)
        {
            Console.WriteLine("Invalid Units");
            return;
        }
        Console.Write("Rate: ");
        if (!double.TryParse(Console.ReadLine(), out rate) || rate < 0)
        {
            Console.WriteLine("Invalid Rate");
            return;
        }
        Console.Write("Fixed Charges: ");
        if (!double.TryParse(Console.ReadLine(), out fixedCharges) || fixedCharges < 0)
        {
            Console.WriteLine("Invalid Fixed Charges");
            return;
        }
        IBillCalculator bill;
        if (type.Equals("Residential", StringComparison.OrdinalIgnoreCase))
            bill = new ResidentialCustomer();
        else if (type.Equals("Commercial", StringComparison.OrdinalIgnoreCase))
            bill = new CommercialCustomer();
        else
        {
            Console.WriteLine("Invalid Customer Type");
            return;
        }
        Console.WriteLine("Total Bill = " + Math.Round(bill.CalculateBill(units, rate, fixedCharges), 2));
    }
}