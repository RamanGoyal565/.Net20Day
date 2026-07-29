// A payroll system imports employee working hours entered manually by HR. Decimal hours, 
// overtime, and invalid numeric entries must be handled without affecting payroll processing.
// Create a Console-based payroll calculator using separate classes for Employee and 
// PayrollCalculator. Validate all inputs using TryParse, calculate regular pay, overtime pay, 
// and gross salary while handling incorrect or extreme values.
// Multiple classes, object-oriented design, primitive types, conversions, arithmetic operators, 
// validation, Math.Round()
// Employee Name, Hours Worked, Hourly Rate
// Correct salary calculation with overtime rules. Invalid values should be rejected without 
// application failure.

using System;
class Employee
{
    public string Name;
    public double HoursWorked;
    public double HourlyRate;
}
class PayrollCalculator
{
    public double Calculate(Employee e)
    {
        if (e.HoursWorked <= 40)
            return e.HoursWorked * e.HourlyRate;
        double regular = 40 * e.HourlyRate;
        double overtime = (e.HoursWorked - 40) * e.HourlyRate * 1.5;
        return regular + overtime;
    }
}

class H2
{
    public static void Main()
    {
        Employee emp = new Employee();
        Console.Write("Employee Name: ");
        emp.Name = Console.ReadLine();
        Console.Write("Hours Worked: ");
        if (!double.TryParse(Console.ReadLine(), out emp.HoursWorked) || emp.HoursWorked < 0)
        {
            Console.WriteLine("Invalid Hours");
            return;
        }
        Console.Write("Hourly Rate: ");
        if (!double.TryParse(Console.ReadLine(), out emp.HourlyRate) || emp.HourlyRate < 0)
        {
            Console.WriteLine("Invalid Rate");
            return;
        }
        PayrollCalculator pay = new PayrollCalculator();
        Console.WriteLine("Gross Salary = " + Math.Round(pay.Calculate(emp), 2));
    }
}