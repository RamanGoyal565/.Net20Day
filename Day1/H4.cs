// A hospital registration system records patient age, weight, height, and body temperature. 
// Since data is entered manually, the system must remain reliable even when multiple invalid 
// values are entered consecutively.
// Design a Console application with separate validation and patient classes. Continuously 
// prompt until all values are valid, perform necessary numeric conversions, calculate BMI, 
// and display a formatted patient summary.
// Classes, validation layer, Parse/TryParse, variables, primitive types, conversions, Math, 
// user input handling
// Age, Weight, Height, Temperature
// Application should never terminate due to invalid input and should display a validated 
// patient summary with calculated BMI.

using System;
class Patient
{
    public int Age;
    public double Weight;
    public double Height;
    public double Temperature;
}
class Validator
{
    public static double ReadPositiveDouble(string msg)
    {
        double value;
        while (true)
        {
            Console.Write(msg);
            if (double.TryParse(Console.ReadLine(), out value) && value > 0)
                return value;
            Console.WriteLine("Invalid Input");
        }
    }
    public static int ReadPositiveInt(string msg)
    {
        int value;
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                return value;
            Console.WriteLine("Invalid Input");
        }
    }
}
class H4
{
    public static void Main()
    {
        Patient p = new Patient();
        p.Age = Validator.ReadPositiveInt("Age: ");
        p.Weight = Validator.ReadPositiveDouble("Weight (kg): ");
        p.Height = Validator.ReadPositiveDouble("Height (m): ");
        p.Temperature = Validator.ReadPositiveDouble("Temperature: ");
        double bmi = p.Weight / (p.Height * p.Height);
        Console.WriteLine("\nPatient Summary");
        Console.WriteLine("Age : " + p.Age);
        Console.WriteLine("Weight : " + p.Weight);
        Console.WriteLine("Height : " + p.Height);
        Console.WriteLine("Temperature : " + p.Temperature);
        Console.WriteLine("BMI : " + Math.Round(bmi, 2));
    }
}