namespace Datastructures;

/// <summary>
/// Binary min-heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public class MinHeap<T> : Heap<T> where T : IComparable, new()
{
    protected override bool Compare(T a, T b) => a.CompareTo(b) < 0;

    /// <summary>
    /// Get the smallest element in the heap.
    /// </summary>
    /// <returns>The minimum</returns>
    public T Minimum() => Items[0];
}