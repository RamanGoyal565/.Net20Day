using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plugins.Tax
{
    public class TaxPlugin : CaseStudy10.IPlugin
    {
        public void Execute() { Console.WriteLine("Tax plugin executed"); }
    }
}

namespace Plugins.Payment
{
    public class PaymentPlugin : CaseStudy10.IPlugin
    {
        public void Execute() { Console.WriteLine("Payment plugin executed"); }
    }
}

namespace Plugins.Logging
{
    public class LoggingPlugin : CaseStudy10.IPlugin
    {
        public void Execute() { Console.WriteLine("Logging plugin executed"); }
    }
}

namespace CaseStudy10
{
    public interface IPlugin
    {
        void Execute();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<IPlugin> plugins = PluginLoader<IPlugin>.Load();
            foreach (IPlugin plugin in plugins)
            {
                plugin.Execute();
            }
        }
    }

    public class PluginLoader<T> where T : class
    {
        public static List<T> Load()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<T> items = new List<T>();
            IEnumerable<Type> types = assembly.GetTypes().Where(delegate(Type type)
            {
                return typeof(T).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;
            });

            foreach (Type type in types)
            {
                T instance = Activator.CreateInstance(type) as T;
                if (instance != null)
                {
                    items.Add(instance);
                }
            }

            return items;
        }
    }
}