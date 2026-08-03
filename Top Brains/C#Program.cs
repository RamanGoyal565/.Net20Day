// <!-- C# program
// Description
// Mahirl and Alphabets and Vowels
// Mahirl's uncle Sam has just taught her about vowels.
// To test her understanding, he gave her the following assignment.

// He gave her two words.
// Mahirl needs to:

// Task 1: Remove Common Consonants
// Remove all consonants from the first word that also appear in the second word.

// While comparing characters, case should not be considered.
// (Example: 'A' and 'a' are considered the same.)

// Task 2: Remove Consecutive Duplicate Characters
// After deleting the common consonants:
// If there are two or more consecutive identical characters, only the first occurrence must be kept and all others deleted.

// Your job is to help Mahirl complete this assignment.

//  Input Format
// Input consists of two strings:
// The first word and the second word.

// Maximum string length: 50 characters.

// Strings contain only uppercase and lowercase English letters.

// Comparisons are case-insensitive.

//  Output Format
// Output the final processed string after applying both rules. -->

using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static bool IsVowel(char ch)
    {
        ch = char.ToLower(ch);
        return ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';
    }

    static void Main()
    {
        string first = Console.ReadLine();
        string second = Console.ReadLine();

        // Store all characters of second word (case-insensitive)
        HashSet<char> charsInSecond = new HashSet<char>();
        foreach (char ch in second)
        {
            charsInSecond.Add(char.ToLower(ch));
        }

        // Task 1: Remove common consonants
        StringBuilder temp = new StringBuilder();

        foreach (char ch in first)
        {
            char lower = char.ToLower(ch);

            if (!IsVowel(ch) && charsInSecond.Contains(lower))
                continue;

            temp.Append(ch);
        }

        // Task 2: Remove consecutive duplicate characters
        StringBuilder result = new StringBuilder();

        foreach (char ch in temp.ToString())
        {
            if (result.Length == 0 || result[result.Length - 1] != ch)
            {
                result.Append(ch);
            }
        }

        Console.WriteLine(result.ToString());
    }
}