using System;

public enum LogLevel
{
    Info,
    Warning,
    Error
}

public static class LogParser
{
    public static bool ParseLogLine(
        in string logLine,
        out DateTime timestamp,
        out LogLevel level,
        ref int counter)
    {
        timestamp = DateTime.MinValue;
        level = LogLevel.Info;

        string[] parts = logLine.Split(' ');

        if (DateTime.TryParse(parts[0] + " " + parts[1], out timestamp))
        {
            if (logLine.Contains("ERROR"))
                level = LogLevel.Error;
            else if (logLine.Contains("WARNING"))
                level = LogLevel.Warning;
            else
                level = LogLevel.Info;

            counter++;
            return true;
        }

        return false;
    }
}

class M3
{
    public static void main()
    {
        int count = 0;

        string line = "2023-10-27 14:30:00 ERROR: Disk full";

        LogParser.ParseLogLine(in line,
            out DateTime time,
            out LogLevel level,
            ref count);

        Console.WriteLine(time);
        Console.WriteLine(level);
        Console.WriteLine(count);
    }
}