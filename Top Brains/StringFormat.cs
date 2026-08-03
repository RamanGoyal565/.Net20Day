// Given strings formatted as "Name:Score", build a list of Student objects.
// Filter students with Score >= minScore, sort by Score descending then Name ascending,
// and serialize the result to a JSON array using System.Text.Json.

// Use a C# record for Student.

// Input: items (string[]), minScore (int)
// Output: json (string)

// Constraints:
// 0 <= items.Length <= 2*10^5

using System;
using System.Linq;
using System.Text.Json;

public record Student(string Name, int Score);

public class Program
{
    static string GetStudentsJson(string[] items, int minScore)
    {
        var students = items
            .Select(item =>
            {
                var parts = item.Split(':');
                return new Student(parts[0], int.Parse(parts[1]));
            })
            .Where(s => s.Score >= minScore)
            .OrderByDescending(s => s.Score)
            .ThenBy(s => s.Name)
            .ToList();

        return JsonSerializer.Serialize(students);
    }

    static void Main()
    {
        string[] items = { "Alice:90", "Bob:75", "Charlie:90", "David:60" };
        int minScore = 70;

        string json = GetStudentsJson(items, minScore);
        Console.WriteLine(json);
    }
}