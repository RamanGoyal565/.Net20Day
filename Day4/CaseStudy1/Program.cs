using System;
using System.Collections.Generic;

namespace CaseStudy1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Approver teamLead = new TeamLead();
            Approver manager = new Manager();
            Approver director = new Director();

            teamLead.SetNext(manager);
            manager.SetNext(director);

            List<ExpenseRequest> requests = new List<ExpenseRequest>
            {
                new ExpenseRequest(1, "Stationery", 4500m),
                new ExpenseRequest(2, "Laptop", 42000m),
                new ExpenseRequest(3, "Office Renovation", 125000m)
            };

            foreach (ExpenseRequest request in requests)
            {
                teamLead.Approve(request);
            }
        }
    }

    public class ExpenseRequest
    {
        public ExpenseRequest(int id, string description, decimal amount)
        {
            Id = id;
            Description = description;
            Amount = amount;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
    }

    public interface IApprover
    {
        void SetNext(Approver approver);
        void Approve(ExpenseRequest request);
    }

    public abstract class Approver : IApprover
    {
        private Approver _next;

        public void SetNext(Approver approver)
        {
            _next = approver;
        }

        public void Approve(ExpenseRequest request)
        {
            if (CanApprove(request.Amount))
            {
                Console.WriteLine(GetType().Name + " approved request " + request.Id + " for " + request.Amount.ToString("C"));
                return;
            }

            if (_next != null)
            {
                _next.Approve(request);
                return;
            }

            Console.WriteLine("No approver found for request " + request.Id);
        }

        protected abstract bool CanApprove(decimal amount);
    }

    public class TeamLead : Approver
    {
        protected override bool CanApprove(decimal amount)
        {
            return amount <= 10000m;
        }
    }

    public class Manager : Approver
    {
        protected override bool CanApprove(decimal amount)
        {
            return amount <= 50000m;
        }
    }

    public class Director : Approver
    {
        protected override bool CanApprove(decimal amount)
        {
            return true;
        }
    }
}