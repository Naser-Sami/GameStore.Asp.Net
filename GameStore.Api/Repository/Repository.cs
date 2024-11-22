using System;
using System.Collections.Generic;

namespace GameStore.Api.Repository;

public class Repository<T>
{
    private static readonly List<T> items = new List<T>();

    public static void Add(T item)
    {
        items.Add(item);
    }

    public static void Remove(T item)
    {
        items.Remove(item);
    }



    public static IEnumerable<T> GetAll()
    {
        return items;
    }

    public static IEnumerable<T> GetAll(Func<T, bool> predicate)
    {
        return items.Where(predicate);
    }

    public static void DisplayAll()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    // Swap values of any type.
    public static void Swap(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    // Check if a list is null or empty.
    public static bool IsNullOrEmpty(IEnumerable<T> collection)
    {
        return collection == null || !collection.Any();
    }

    // Compare two values of any comparable type.
    public static T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    // Find the smaller of two values.
    public static T Min<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) < 0 ? a : b;
    }

    // Map elements from one type to another.
    public static List<TResult> ConvertAll<TSource, TResult>(List<TSource> source, Func<TSource, TResult> converter)
    {
        return source.Select(converter).ToList();
    }

    // Return the default value for a type.
    public static T? GetDefaultValue()
    {
        return default;
    }

    // Create a shallow copy of an object.
    public static T Clone<T>(T source) where T : ICloneable
    {
        return (T)source.Clone();
    }

    // Filter distinct items in a collection.
    public static IEnumerable<T> GetDistinct(IEnumerable<T> items)
    {
        return items.Distinct();
    }

    // Filter items based on a condition.
    public static IEnumerable<T> Filter(IEnumerable<T> source, Func<T, bool> predicate)
    {
        return source.Where(predicate);
    }

    // Execute an action on each item in a collection.
    public static void ForEach(IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }

    // Get the index of an item that matches a condition.
    public static int FindIndex(IList<T> list, Predicate<T> match)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (match(list[i]))
            {
                return i;
            }
        }
        return -1; // Not found
    }

    // Sort a list based on a property of the items.
    public static List<T> SortBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector)
    {
        return items.OrderBy(keySelector).ToList();
    }

    // Check if two collections are equal.
    public static bool AreCollectionsEqual<T>(IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.SequenceEqual(second);
    }


    // Paginate a Collection
    // Split a collection into pages.
    public static IEnumerable<T> GetPage<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    // Find the first value in a collection that is not the default for its type.
    public static T? FirstNonDefault<T>(IEnumerable<T> items)
    {
        return items.FirstOrDefault(item => !EqualityComparer<T>.Default.Equals(item, default));
    }


    // Flatten a List of Lists 
    // Flatten nested collections into a single list.
    public static IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> source)
    {
        return source.SelectMany(item => item);
    }


    // Check if All Items Meet a Condition
    // Verify that all items satisfy a condition.
    public static bool All<T>(IEnumerable<T> source, Func<T, bool> predicate)
    {
        return source.All(predicate);
    }


    // Create a dictionary from a collection.
    public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(
    IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TValue> valueSelector)
    {
        return source.ToDictionary(keySelector, valueSelector);
    }


    // Combine 'Merge', two dictionaries.
    public static Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(
    Dictionary<TKey, TValue> first,
    Dictionary<TKey, TValue> second)
    {
        return first.Concat(second).ToDictionary(kv => kv.Key, kv => kv.Value);
    }


    // Retrieve a random item from a collection.
    public static T GetRandomItem<T>(IList<T> items)
    {
        Random random = new Random();
        return items[random.Next(items.Count)];
    }


    // Retry a function if it fails.
    public static T Retry<T>(Func<T> operation, int maxAttempts)
    {
        int attempts = 0;
        while (true)
        {
            try
            {
                return operation();
            }
            catch
            {
                attempts++;
                if (attempts >= maxAttempts)
                    throw;
            }
        }
    }

}
