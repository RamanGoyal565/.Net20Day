// Evaluate a simple arithmetic expression of the form "a op b" (spaces required),
// where op is one of: +, -, *, /.

// Return:
// - the integer result as a string for valid expressions
// - "Error:DivideByZero"   if b==0 for division
// - "Error:InvalidNumber"  if a or b is not an int
// - "Error:UnknownOperator" if op is not one of + - * /
// - "Error:InvalidExpression" if format is invalid

// Input: expression (string)
// Output: result (string)

// Constraints:
// 1 <= expression.Length <= 100

using System;

class Program
{
    static string EvaluateExpression(string expression)
    {
        string[] parts = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
            return "Error:InvalidExpression";

        if (!int.TryParse(parts[0], out int a) ||
            !int.TryParse(parts[2], out int b))
            return "Error:InvalidNumber";

        switch (parts[1])
        {
            case "+":
                return (a + b).ToString();

            case "-":
                return (a - b).ToString();

            case "*":
                return (a * b).ToString();

            case "/":
                if (b == 0)
                    return "Error:DivideByZero";
                return (a / b).ToString();

            default:
                return "Error:UnknownOperator";
        }
    }

    static void Main()
    {
        Console.WriteLine(EvaluateExpression("10 + 5"));   // 15
        Console.WriteLine(EvaluateExpression("10 / 0"));   // Error:DivideByZero
        Console.WriteLine(EvaluateExpression("a + 5"));    // Error:InvalidNumber
        Console.WriteLine(EvaluateExpression("10 ^ 5"));   // Error:UnknownOperator
        Console.WriteLine(EvaluateExpression("10+5"));     // Error:InvalidExpression
    }
}