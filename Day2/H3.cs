using System;

public static class Logger
{
    public static string FormatLogMessage(string template, params object[] args)
    {
        string result = template;

        void ReplacePlaceholders()
        {
            ReadOnlySpan<char> span = result.AsSpan();

            for (int i = 0; i < args.Length; i++)
            {
                string placeholder = "{" + i + "}";

                string value;

                if (args[i] is string s)
                {
                    value = s;
                }
                else if (args[i] is DateTime dt)
                {
                    value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    value = args[i].ToString();
                }

                result = result.Replace(placeholder, value);
            }

            // Example TryParse usage
            if (int.TryParse("123", out int number))
            {
                Console.WriteLine($"Parsed integer: {number}");
            }
        }

        ReplacePlaceholders();

        return result;
    }
}

class H3
{
    public static void main()
    {
        string msg = Logger.FormatLogMessage(
            "User {0} logged in from {1} at {2}",
            "JohnDoe",
            "192.168.1.1",
            DateTime.Now);

        Console.WriteLine(msg);
    }
}