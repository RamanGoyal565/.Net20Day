using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PermanentEmployee emp1 = new PermanentEmployee
            {
                Id = 1,
                Name = "Pankaj",
                BasicSalary = 50000m
            };

            ContractEmployee emp2 = new ContractEmployee
            {
                Id = 2,
                Name = "Neha",
                BasicSalary = 30000m
            };

            Intern emp3 = new Intern
            {
                Id = 3,
                Name = "Amit",
                BasicSalary = 12000m
            };

            List<Employee> employees = new List<Employee> { emp1, emp2, emp3 };
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee.Name + " Salary: " + employee.CalculateSalary() + ", Bonus: " + employee.CalculateBonus());
            }

            IEnumerable<object> report = employees.Select(delegate(Employee employee)
            {
                return new
                {
                    employee.Id,
                    employee.Name,
                    Salary = employee.CalculateSalary(),
                    Bonus = employee.CalculateBonus()
                };
            });

            foreach (object item in report)
            {
                Console.WriteLine(item);
            }
        }
    }

    public abstract class Employee
    {
        private int _id;
        private string _name;
        private decimal _basicSalary;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0) throw new ArgumentException("Invalid Id");
                _id = value;
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Invalid Name");
                _name = value;
            }
        }

        public decimal BasicSalary
        {
            get { return _basicSalary; }
            set
            {
                if (value < 0) throw new ArgumentException("Invalid Salary");
                _basicSalary = value;
            }
        }

        public abstract decimal CalculateSalary();
        public abstract decimal CalculateBonus();
    }

    public class PermanentEmployee : Employee
    {
        public override decimal CalculateSalary() { return BasicSalary + 10000m; }
        public override decimal CalculateBonus() { return BasicSalary * 0.20m; }
    }

    public class ContractEmployee : Employee
    {
        public override decimal CalculateSalary() { return BasicSalary; }
        public override decimal CalculateBonus() { return BasicSalary * 0.10m; }
    }

    public class Intern : Employee
    {
        public override decimal CalculateSalary() { return BasicSalary; }
        public override decimal CalculateBonus() { return BasicSalary * 0.05m; }
    }
}