using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class MinHeapTest
{
    [Test]
    public void TestInsert()
    {
        int[] arr = new[] {-34, 213, 34, -123, -45, 0, 32, 36, 90, 2, 4, -5};
        MinHeap<int> heap = new MinHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Assert.AreEqual(-123, heap.Minimum());
        Console.WriteLine(heap);
        foreach (int _ in arr)
            Assert.True(arr.Contains(heap.ExtractRoot()));
    }
    
    [Test]
    public void TestExtractRoot()
    {
        int smallest = -742;
        int[] arr = new[] {-278, 242, 43, smallest, -4, 0, 3};
        MinHeap<int> heap = new MinHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Assert.AreEqual(smallest, heap.Minimum());
        Console.WriteLine(heap);
        Assert.AreEqual(smallest, heap.ExtractRoot());
        foreach (int _ in arr[0..^1])
            Assert.False(heap.ExtractRoot() == smallest);
    }
    
    [Test]
    public void TestSorting()
    {
        int[] arr = new[] {-234, -324, 324, 23, 344, -343, -4673, -86, 1431, 32};
        MinHeap<int> heap = new MinHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Console.WriteLine(heap);
        int lastOne = -10000;
        foreach (int _ in arr)
        {
            int temp = heap.Minimum();
            Assert.True(heap.ExtractRoot() >= lastOne);
            lastOne = temp;
        }
    }
}