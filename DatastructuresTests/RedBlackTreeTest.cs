using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class RedBlackTreeTest
{
    [Test]
    public void TestIntInsert()
    {
        RedBlackTree<int> tree = new RedBlackTree<int>();
        for (int i = 0; i <= 10; i++)
            tree.Insert((int)(i * 3 * 0.5));
        tree.Insert(62);
        Console.WriteLine(tree);
        Assert.True(tree.Validate());
        Assert.That(tree.Search(62).Data, Is.EqualTo(62));
    }
    
    [Test]
    public void TestCharInsert()
    {
        RedBlackTree<char> tree = new RedBlackTree<char>();
        for (int i = 0; i <= 10; i++)
            tree.Insert((char)(i * 3 * 0.5));
        tree.Insert('$');
        Console.WriteLine(tree);
        Assert.True(tree.Validate());
        Assert.That(tree.Search('$').Data, Is.EqualTo('$'));
    }
    
    [Test]
    public void TestDelete()
    {
        RedBlackTree<int> tree = new RedBlackTree<int>();
        Assert.That(tree.Delete(new RedBlackTree<int>.RBNode(5)), Is.EqualTo(5));
        for (int i = 0; i <= 10; i++)
            tree.Insert(i);
        Assert.True(tree.Validate());
        Console.WriteLine(tree);
        Assert.That(tree.Height, Is.EqualTo(11));
        RedBlackTree<int>.RBNode node = tree.Search(5);
        Assert.That(tree.Delete(node), Is.EqualTo(5));
        Assert.That(tree.Search(12).Data, Is.EqualTo(0));
        Assert.That(tree.Validate(), Is.True);
    }
}