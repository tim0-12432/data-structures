using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class BinaryTreeTest
{
    [Test]
    public void TestInsertInt()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        for (int i = 0; i <= 50; i++)
            tree.Insert(new Random().Next(0, 51));
        tree.Insert(5);
        Console.WriteLine(tree);
        Assert.AreEqual(5, tree.Search(5).Data);
    }
    
    [Test]
    public void TestInsertChar()
    {
        BinaryTree<char> tree = new BinaryTree<char>();
        for (int i = 0; i <= 50; i++)
            tree.Insert((char)new Random().Next(0, 51));
        tree.Insert('+');
        Console.WriteLine(tree);
        Assert.AreEqual('+', tree.Search('+').Data);
    }
    
    [Test]
    public void TestDelete()
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        Assert.AreEqual(5, tree.Delete(new BinaryTree<int>.Node(5)));
        for (int i = 0; i <= 10; i++)
            tree.Insert(i);
        Console.WriteLine(tree);
        Assert.AreEqual(11, tree.Height);
        BinaryTree<int>.Node node = tree.Search(5);
        Assert.AreEqual(5, tree.Delete(node));
        Assert.AreEqual(0, tree.Search(12).Data);
    }
}