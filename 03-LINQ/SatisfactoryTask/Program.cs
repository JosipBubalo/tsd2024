using System;

class Program
{
    static void Main()
    {
        RandomList<int> intList = new RandomList<int>();

        Console.WriteLine("Is int list empty? " + intList.IsEmpty());

        intList.Add(10);
        intList.Add(20);
        intList.Add(30);
        intList.Add(40);
        intList.Add(50);

        Console.WriteLine("Is int list empty? " + intList.IsEmpty());

        Console.WriteLine("Random element (max index 2): " + intList.Get(2));
        Console.WriteLine("Random element (max index 4): " + intList.Get(4));

        Console.WriteLine("\n----- String List Test -----\n");

        // Using RandomList with strings
        RandomList<string> strList = new RandomList<string>();
        strList.Add("Apple");
        strList.Add("Banana");
        strList.Add("Cherry");
        strList.Add("Date");
        strList.Add("Elderberry");

        Console.WriteLine("Random string (max index 2): " + strList.Get(2));
        Console.WriteLine("Random string (max index 4): " + strList.Get(4));
    }
}
