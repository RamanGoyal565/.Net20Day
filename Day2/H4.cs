using System;
using System.Collections.Generic;

public class Transaction
{
    public string Id { get; set; }

    public List<Transaction> Dependencies { get; set; } = new();
}

public static class RiskCalculator
{
    public static int CalculateRiskScore(string transactionId, Transaction transaction)
    {
        if (!TryParseTransactionId(transactionId, out string id))
        {
            Console.WriteLine("Invalid Transaction ID");
            return -1;
        }

        int depth = 0;

        HashSet<string> visited = new();

        return Calculate(transaction, visited, ref depth);
    }

    static bool TryParseTransactionId(string id, out string parsed)
    {
        parsed = id;

        return id.StartsWith("TX");
    }

    static int Calculate(Transaction node,
                         HashSet<string> visited,
                         ref int depth)
    {
        if (depth > 1000)
        {
            Console.WriteLine("Maximum recursion depth exceeded.");
            return -1;
        }

        if (visited.Contains(node.Id))
            return 0;

        visited.Add(node.Id);

        depth++;

        int score = 1;

        foreach (Transaction child in node.Dependencies)
        {
            int value = Calculate(child, visited, ref depth);

            if (value == -1)
                return -1;

            score += value;
        }

        depth--;

        return score;
    }
}

class H4
{
    public static void main()
    {
        Transaction t1 = new() { Id = "T1" };
        Transaction t2 = new() { Id = "T2" };

        t1.Dependencies.Add(t2);
        t2.Dependencies.Add(t1);

        int score = RiskCalculator.CalculateRiskScore("TX001", t1);

        Console.WriteLine(score);
    }
}