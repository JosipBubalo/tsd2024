using System;
using System.Collections.Generic;

public class RandomList<T>
{
    private List<T> items;
    private Random random;

    public RandomList()
    {
        items = new List<T>();
        random = new Random();
    }

    public void Add(T element)
    {
        if (random.Next(2) == 0)
        {
            items.Insert(0, element);
            Console.WriteLine($"Added {element} at the beginning.");
        }
        else
        {
            items.Add(element);
            Console.WriteLine($"Added {element} at the end.");
        }
    }

    public T Get(int index)
    {
        if (items.Count == 0 || index < 0)
            throw new InvalidOperationException("List is empty or invalid index.");

        int randomIndex = random.Next(Math.Min(index + 1, items.Count));
        Console.WriteLine($"Returning element from index {randomIndex}.");
        return items[randomIndex];
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
}
