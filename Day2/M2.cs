using System;
using System.Collections.Generic;

public static class LibraryProcessor
{
    public static bool TryParseISBN(string isbn, out string cleanedISBN)
    {
        cleanedISBN = isbn.Replace("-", "").Trim();

        if (cleanedISBN.Length == 13)
            return true;

        cleanedISBN = "";
        return false;
    }

    public static bool TryProcessOrder(out List<string> validISBNs, params string[] orders)
    {
        validISBNs = new List<string>();

        foreach (string order in orders)
        {
            string[] books = order.Split(',');

            foreach (string book in books)
            {
                if (TryParseISBN(book, out string cleaned))
                {
                    validISBNs.Add(cleaned);
                }
            }
        }

        return validISBNs.Count > 0;
    }
}

class M2
{
    public static void main()
    {
        bool result = LibraryProcessor.TryProcessOrder(
            out List<string> validBooks,
            "978-3-16-148410-0,1234567890123,invalid-isbn,978-1-4028-9462-6");

        Console.WriteLine(result);

        foreach (string book in validBooks)
            Console.WriteLine(book);
    }
}