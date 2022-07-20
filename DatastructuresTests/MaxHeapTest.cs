using Datastructures;
using NUnit.Framework;

namespace DatastructuresTests;

[TestFixture]
public class MaxHeapTest
{
    [Test]
    public void TestConvert()
    {
        int[] arr = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        MaxHeap<int> heap = (MaxHeap<int>) arr;
        Console.WriteLine(heap);
        int[] res = (int[]) heap;
        Assert.AreEqual(arr, res);
    }
    
    [Test]
    public void TestInsert()
    {
        int[] arr = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        MaxHeap<int> heap = (MaxHeap<int>) arr;
        Console.WriteLine(heap);
        int[] res = (int[]) heap;
        Assert.AreEqual(arr, res);
    }
}