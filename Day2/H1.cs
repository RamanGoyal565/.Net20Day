using System;
using System.Collections.Generic;

public class Configuration
{
    public Dictionary<string, string> Settings { get; set; } = new();
}

public interface IConfigurationSource
{
    bool TryLoad(out Configuration config);
}

public class EnvironmentVariableSource : IConfigurationSource
{
    public bool TryLoad(out Configuration config)
    {
        config = null;
        return false;      // No data found
    }
}

public class JsonFileSource : IConfigurationSource
{
    public bool TryLoad(out Configuration config)
    {
        config = null;
        return false;      // File missing
    }
}

public class DatabaseSource : IConfigurationSource
{
    public bool TryLoad(out Configuration config)
    {
        config = new Configuration();
        config.Settings["ConnectionString"] = "Server=SQL;";
        return true;
    }
}

public static class ConfigurationLoader
{
    public static Configuration Load(params IConfigurationSource[] sources)
    {
        foreach (IConfigurationSource source in sources)
        {
            if (source.TryLoad(out Configuration config))
            {
                Console.WriteLine($"Loaded from {source.GetType().Name}");
                return config;
            }
        }

        Console.WriteLine("No configuration found.");
        return null;
    }
}

class H1
{
    public static void main()
    {
        Configuration config = ConfigurationLoader.Load(
            new EnvironmentVariableSource(),
            new JsonFileSource(),
            new DatabaseSource());
    }
}