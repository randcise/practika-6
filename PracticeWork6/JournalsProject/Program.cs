#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;

// Клас журналу + IComparable (порівняння за ціною)
public class Journal : IComparable<Journal>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Pages { get; set; }
    public int Rating { get; set; } // 1-10

    public Journal(string name, double price, int pages, int rating)
    {
        Name = name;
        Price = price;
        Pages = pages;
        Rating = rating;
    }

    public int CompareTo(Journal other)
    {
        return Price.CompareTo(other.Price);
    }

    public override string ToString()
    {
        return $"{Name}: Ціна={Price}, Сторінок={Pages}, Рейтинг={Rating}";
    }
}

// Порівняння за сторінками
public class PagesComparer : IComparer<Journal>
{
    public int Compare(Journal x, Journal y)
    {
        return x.Pages.CompareTo(y.Pages);
    }
}

// Порівняння за рейтингом
public class RatingComparer : IComparer<Journal>
{
    public int Compare(Journal x, Journal y)
    {
        return x.Rating.CompareTo(y.Rating);
    }
}

// IEnumerable — колекція журналів
public class JournalCollection : IEnumerable<Journal>
{
    private List<Journal> list = new List<Journal>();

    public void Add(Journal j)
    {
        list.Add(j);
    }

    public IEnumerator<Journal> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        // Масив журналів
        Journal[] arr =
        {
            new Journal("Forbes", 120, 80, 9),
            new Journal("Times", 90, 70, 7),
            new Journal("EcoLife", 60, 60, 6)
        };

        Console.WriteLine("Сортування масиву за ціною (IComparable):");
        Array.Sort(arr);
        foreach (var j in arr) Console.WriteLine(j);

        Console.WriteLine("\nСортування за сторінками (IComparer):");
        Array.Sort(arr, new PagesComparer());
        foreach (var j in arr) Console.WriteLine(j);

        Console.WriteLine("\nСортування за рейтингом (IComparer):");
        Array.Sort(arr, new RatingComparer());
        foreach (var j in arr) Console.WriteLine(j);

        // IEnumerable колекція
        JournalCollection collection = new JournalCollection();
        collection.Add(new Journal("Forbes", 120, 80, 9));
        collection.Add(new Journal("Times", 90, 70, 7));
        collection.Add(new Journal("EcoLife", 60, 60, 6));

        Console.WriteLine("\nСписок журналів (IEnumerable), впорядкований за рейтингом:");
        List<Journal> sorted = new List<Journal>(collection);
        sorted.Sort(new RatingComparer());
        foreach (var j in sorted) Console.WriteLine(j);
    }
}
