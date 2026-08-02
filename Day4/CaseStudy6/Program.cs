using System;
using System.Collections.Generic;

namespace Banking.Common
{
    public interface ITransaction
    {
        decimal Amount { get; }
        void Validate(BankAccount account);
        void Execute(BankAccount account);
        void Rollback(BankAccount account);
    }

    public class BankAccount
    {
        private decimal _balance;

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public List<string> History { get; private set; } = new List<string>();
    }
}

namespace Banking.Deposit
{
    using Banking.Common;

    public sealed class DepositTransaction : ITransaction
    {
        public DepositTransaction(decimal amount) { Amount = amount; }
        public decimal Amount { get; private set; }
        public void Validate(BankAccount account) { if (Amount <= 0) throw new ArgumentException("Invalid deposit"); }
        public void Execute(BankAccount account) { account.Balance += Amount; account.History.Add("Deposit: " + Amount); }
        public void Rollback(BankAccount account) { account.Balance -= Amount; account.History.Add("Rollback Deposit: " + Amount); }
    }
}

namespace Banking.Withdraw
{
    using Banking.Common;

    public sealed class WithdrawTransaction : ITransaction
    {
        public WithdrawTransaction(decimal amount) { Amount = amount; }
        public decimal Amount { get; private set; }
        public void Validate(BankAccount account) { if (Amount <= 0 || account.Balance < Amount) throw new ArgumentException("Invalid withdraw"); }
        public void Execute(BankAccount account) { account.Balance -= Amount; account.History.Add("Withdraw: " + Amount); }
        public void Rollback(BankAccount account) { account.Balance += Amount; account.History.Add("Rollback Withdraw: " + Amount); }
    }
}

namespace Banking.Transfer
{
    using Banking.Common;

    public sealed class TransferTransaction : ITransaction
    {
        public TransferTransaction(decimal amount) { Amount = amount; }
        public decimal Amount { get; private set; }
        public void Validate(BankAccount account) { if (Amount <= 0 || account.Balance < Amount) throw new ArgumentException("Invalid transfer"); }
        public void Execute(BankAccount account) { account.Balance -= Amount; account.History.Add("Transfer: " + Amount); }
        public void Rollback(BankAccount account) { account.Balance += Amount; account.History.Add("Rollback Transfer: " + Amount); }
    }
}

namespace CaseStudy6
{
    using Banking.Common;
    using Banking.Deposit;
    using Banking.Transfer;
    using Banking.Withdraw;

    public class Program
    {
        public static void Main(string[] args)
        {
            BankAccount account = new BankAccount();
            account.Balance = 10000m;
            TransactionProcessor processor = new TransactionProcessor();

            processor.Process(new DepositTransaction(2000m), account);
            processor.Process(new WithdrawTransaction(1500m), account);
            processor.Process(new TransferTransaction(1000m), account);
            processor.UndoLast(account);

            Console.WriteLine("Balance: " + account.Balance);
            foreach (string entry in account.History)
            {
                Console.WriteLine(entry);
            }
        }
    }

    public class TransactionProcessor
    {
        private readonly Stack<ITransaction> _executed = new Stack<ITransaction>();

        public void Process(ITransaction transaction, BankAccount account)
        {
            transaction.Validate(account);
            transaction.Execute(account);
            _executed.Push(transaction);
        }

        public void UndoLast(BankAccount account)
        {
            if (_executed.Count > 0)
            {
                ITransaction transaction = _executed.Pop();
                transaction.Rollback(account);
            }
        }
    }
}