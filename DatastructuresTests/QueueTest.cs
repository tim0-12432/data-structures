namespace DatastructuresTests;

[TestFixture]
public class QueueTest
{
    [Test]
    public void TestInsert()
    {
        Datastructures.Queue<int> queue = new Datastructures.Queue<int>();
        Assert.True(queue.IsEmpty());
        for (int i = 0; i <= 10; i++)
            queue.Enqueue(i);
        Console.WriteLine(queue);
        Assert.False(queue.IsEmpty());
        Assert.AreEqual(11, queue.Length());
        Assert.AreEqual(0, queue.Peek());
    }
    
    [Test]
    public void TestDelete()
    {
        Datastructures.Queue<int> queue = new Datastructures.Queue<int>();
        Assert.Throws<InvalidOperationException>(delegate { queue.Dequeue(); });
        for (int i = 0; i <= 10; i++)
            queue.Enqueue(i);
        Console.WriteLine(queue);
        Assert.AreEqual(0, queue.Dequeue());
        Assert.AreEqual(1, queue.Dequeue());
        Assert.AreEqual(2, queue.Dequeue());
        Assert.AreEqual(3, queue.Dequeue());
        Assert.AreEqual(4, queue.Dequeue());
    }
    
    [Test]
    public void TestIterator()
    {
        Datastructures.Queue<int> queue = new Datastructures.Queue<int>();
        for (int i = 0; i <= 10; i++)
            queue.Enqueue(i);
        Console.WriteLine(queue);
        foreach (int number in queue)
        {
            Assert.True(number >= 0 & number <= 10);
            Console.Write($"{number} ");
        }
        Console.WriteLine();
    }
}