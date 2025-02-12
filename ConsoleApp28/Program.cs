using System;
using System.Collections.Generic;
public interface IList<T>
{
    void Add(T item);
    void Remove(T item);
    T Get(int index);
    void Set(int index, T item);
    int Count { get; }
}

public class ArrayList<T> : IList<T>
{
    private T[] items;
    private int count;

    public ArrayList()
    {
        items = new T[10];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }
        items[count++] = item;
    }

    public void Remove(T item)
    {
        int index = Array.IndexOf(items, item, 0, count);
        if (index >= 0)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
        }
    }

    public T Get(int index) => items[index];
    public void Set(int index, T item) => items[index] = item;
    public int Count => count;
}
public class LinkedList<T> : IList<T>
{
    private class Node
    {
        public T Data;
        public Node Next;
        public Node(T data) => Data = data;
    }

    private Node head;
    private int count;

    public void Add(T item)
    {
        if (head == null)
        {
            head = new Node(item);
        }
        else
        {
            Node current = head;
            while (current.Next != null) current = current.Next;
            current.Next = new Node(item);
        }
        count++;
    }

    public void Remove(T item)
    {
        if (head == null) return;
        if (head.Data.Equals(item)) { head = head.Next; count--; return; }

        Node current = head;
        while (current.Next != null && !current.Next.Data.Equals(item))
        {
            current = current.Next;
        }
        if (current.Next != null)
        {
            current.Next = current.Next.Next;
            count--;
        }
    }

    public T Get(int index)
    {
        Node current = head;
        for (int i = 0; i < index; i++) current = current.Next;
        return current.Data;
    }

    public void Set(int index, T item)
    {
        Node current = head;
        for (int i = 0; i < index; i++) current = current.Next;
        current.Data = item;
    }

    public int Count => count;
}

public interface IComparer<T>
{
    int Compare(T x, T y);
}


public class StringComparer : IComparer<string>
{
    public int Compare(string x, string y) => x.Length.CompareTo(y.Length);
}


public class Book
{
    public string Title { get; set; }
    public double Price { get; set; }
}


public class BookComparer : IComparer<Book>
{
    public int Compare(Book x, Book y) => x.Price.CompareTo(y.Price);
}

public static class Sorter
{
    public static void Sort<T>(T[] array, IComparer<T> comparer)
    {
        Array.Sort(array, comparer.Compare);
    }
}

public interface IFactory<T>
{
    T Create();
}

public class RandomNumberFactory : IFactory<int>
{
    private Random random = new Random();
    public int Create() => random.Next(1, 100);
}


public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public override string ToString() => $"{Name}, {Age} years old";
}

public class PersonFactory : IFactory<Person>
{
    public Person Create()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите возраст: ");
        int age = int.Parse(Console.ReadLine());
        return new Person { Name = name, Age = age };
    }
}

public static class FactoryUtil
{
    public static T[] CreateArray<T>(IFactory<T> factory, int n)
    {
        T[] array = new T[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = factory.Create();
        }
        return array;
    }
}


public class Program
{
    public static void Main()
    {

        IList<int> arrayList = new ArrayList<int>();
        arrayList.Add(10);
        arrayList.Add(20);
        arrayList.Remove(10);
        Console.WriteLine("ArrayList count: " + arrayList.Count);

        string[] words = { "apple", "banana", "kiwi", "grape" };
        Sorter.Sort(words, new StringComparer());
        Console.WriteLine("Sorted strings: " + string.Join(", ", words));

        IFactory<int> numberFactory = new RandomNumberFactory();
        int[] numbers = FactoryUtil.CreateArray(numberFactory, 5);
        Console.WriteLine("Random numbers: " + string.Join(", ", numbers));
    }
}
