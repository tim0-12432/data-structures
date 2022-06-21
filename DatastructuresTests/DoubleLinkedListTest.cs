using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class DoubleLinkedListTest
{
    [Test]
    public void TestInsert()
    {
        DoubleLinkedList<int> list = new DoubleLinkedList<int>();
        for (int i = 6; i <= 10; i++)
            list.Append(i);
        for (int i = 4; i >= 0; i--)
            list.Prepend(i);
        list.InsertAfter(4, 5);
        Console.WriteLine(list);
        Assert.AreEqual(11, list.Length);
        for (int i = 0; i <= 10; i++)
            Assert.AreEqual(1, list.Contains(i), $"Should contain {i} only once!");
    }
    
    [Test]
    public void TestDelete()
    {
        DoubleLinkedList<int> list = new DoubleLinkedList<int>();
        for (int i = 0; i <= 10; i++)
            list.Append(i);
        Console.WriteLine(list);
        Assert.AreEqual(11, list.Length);
        Assert.AreEqual(1, list.Contains(3));
        list.Delete(3);
        Assert.AreEqual(10, list.Length);
        Assert.AreEqual(0, list.Contains(3));
        list.ClearAll();
        Assert.AreEqual(0, list.Length);
        Console.WriteLine(list);
    }

    [Test]
    public void TestSearch()
    {
        DoubleLinkedList<int> list = new DoubleLinkedList<int>();
        for (int i = 0; i <= 10; i++)
            list.Append(i);
        Console.WriteLine(list);
        DoubleLinkedList<int>.ListElement<int> element = list.Search(5);
        Assert.AreEqual(5, element.Data);
        Assert.AreEqual(4, element.Predecessor.Data);
        Assert.AreEqual(6, element.Successor.Data);
        Assert.Throws<ArgumentException>(delegate
        {
            list.Search(12);
        });
    }

    [Test]
    public void TestIterator()
    {
        DoubleLinkedList<int> list = new DoubleLinkedList<int>();
        for (int i = 0; i <= 10; i++)
            list.Append(i);
        Console.WriteLine(list);
        foreach (int number in list)
        {
            Assert.True(number >= 0 & number <= 10);
            Console.Write($"{number} ");
        }
        Console.WriteLine();
    }
}