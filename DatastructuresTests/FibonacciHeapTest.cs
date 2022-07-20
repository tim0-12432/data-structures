using Datastructures;
using NUnit.Framework;

namespace DatastructuresTests;

[TestFixture]
public class FibonacciHeapTest
{    
    [Test]
    public void TestInsert()
    {
        FibonacciHeap<IntegerDec> heap = new FibonacciHeap<IntegerDec>();
        int[] arr = new[] {23, 1312, -34, -87, 27, 324, 97, 0, -6};
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size);
        Console.WriteLine(heap);
    }
    
    [Test]
    public void TestExtract()
    {
        FibonacciHeap<IntegerDec> heap = new FibonacciHeap<IntegerDec>();
        int[] arr = new[] {23, 1312, -34, -87, 27, 324, 97, 0, -6};
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(-87, (int)heap.Minimum());
        Assert.AreEqual(-87, (int)heap.ExtractMin());
        Assert.AreEqual(arr.Length - 1, heap.Size);
    }
    
    [Test]
    public void TestDelete()
    {
        FibonacciHeap<IntegerDec> heap = new FibonacciHeap<IntegerDec>();
        int[] arr = new[] {1312, -46, 23};
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(-46, (int)heap.Minimum());
        Assert.AreEqual(arr.Length, heap.Size);
        FibonacciHeap<IntegerDec>.Node elem = heap.Head.RightSibling.RightSibling;
        Assert.AreEqual(1312, (int)elem.Data);
        Assert.AreEqual(1312, (int)heap.Delete(elem));
        Assert.AreEqual(-46, (int)heap.Minimum());
        Console.WriteLine(heap);
    }
    
    [Test]
    public void TestUnion()
    {
        FibonacciHeap<IntegerDec> heap1 = new FibonacciHeap<IntegerDec>();
        int[] arr1 = new[] {23, 1312, -34, -87};
        foreach (int item in arr1)
            heap1.Insert(item);
        Console.WriteLine(heap1);
        FibonacciHeap<IntegerDec> heap2 = new FibonacciHeap<IntegerDec>();
        int[] arr2 = new[] {27, 324, 97, 0, -603};
        foreach (int item in arr2)
            heap2.Insert(item);
        Console.WriteLine(heap2);
        Assert.AreEqual(-87, (int)heap1.Minimum());
        Assert.AreEqual(arr1.Length, heap1.Size);
        Assert.AreEqual(-603, (int)heap2.Minimum());
        Assert.AreEqual(arr2.Length, heap2.Size);
        heap1.Union(heap2);
        Assert.AreEqual(arr1.Length + arr2.Length, heap1.Size);
        Assert.AreEqual(-603, (int)heap1.Minimum());
        Console.WriteLine(heap1);
    }
}