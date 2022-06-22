using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class HashtableTest
{
    [Test]
    public void TestInsertInt()
    {
        Hashtable<int> hashtable = new Hashtable<int>();
        for (int i = 0; i <= 50; i++)
            hashtable.Insert(i);
        Console.WriteLine(hashtable);
        Assert.AreEqual(0, hashtable.Search(0));
    }
    
    [Test]
    public void TestInsertChar()
    {
        Hashtable<char> hashtable = new Hashtable<char>();
        for (int i = 0; i <= 50; i++)
            hashtable.Insert((char)i);
        Console.WriteLine(hashtable);
        Assert.AreEqual('+', hashtable.Search('+'));
    }
    
    [Test]
    public void TestDelete()
    {
        Hashtable<int> hashtable = new Hashtable<int>();
        Assert.AreEqual(0, hashtable.Delete(5));
        for (int i = 0; i <= 10; i++)
            hashtable.Insert(i);
        Console.WriteLine(hashtable);
        Assert.AreEqual(0, hashtable.Delete(0));
        Assert.AreEqual(1, hashtable.Delete(1));
        Assert.AreEqual(2, hashtable.Delete(2));
        Assert.AreEqual(3, hashtable.Delete(3));
        Assert.AreEqual(4, hashtable.Delete(4));
    }
}