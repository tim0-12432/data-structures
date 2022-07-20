using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class MaxHeapTest
{
    [Test]
    public void TestInsert()
    {
        int[] arr = new[] {-34, 213, 34, -123, -45, 0, 32, 36, 90, 2, 4, -5};
        MaxHeap<int> heap = new MaxHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Assert.AreEqual(213, heap.Maximum());
        Console.WriteLine(heap);
        foreach (int _ in arr)
            Assert.True(arr.Contains(heap.ExtractRoot()));
    }
    
    [Test]
    public void TestExtractRoot()
    {
        int greatest = 645;
        int[] arr = new[] {-278, 242, 43, greatest, -4, 0, 3};
        MaxHeap<int> heap = new MaxHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Assert.AreEqual(greatest, heap.Maximum());
        Console.WriteLine(heap);
        Assert.AreEqual(greatest, heap.ExtractRoot());
        foreach (int _ in arr[0..^1])
            Assert.False(heap.ExtractRoot() == greatest);
    }
    
    [Test]
    public void TestSorting()
    {
        int[] arr = new[] {-234, -324, 324, 23, 344, -343, -467, -86, 1431, 32};
        MaxHeap<int> heap = new MaxHeap<int>();
        foreach (int item in arr)
            heap.Insert(item);
        Assert.AreEqual(arr.Length, heap.Size());
        Console.WriteLine(heap);
        int lastOne = 10000;
        foreach (int _ in arr)
        {
            int temp = heap.Maximum();
            Assert.True(heap.ExtractRoot() <= lastOne);
            lastOne = temp;
        }
    }
}