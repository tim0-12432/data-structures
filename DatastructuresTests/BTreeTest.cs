using Datastructures;

namespace DatastructuresTests;

[TestFixture]
public class BTreeTest
{
    [Test]
    public void TestInsert()
    {
        int[] arr = new int[] {234, 23, 245, 675, 43, -24, -578, -3, 6, 0, 2342};
        BTree<int> tree = new BTree<int>(2);
        Assert.True(tree.IsEmpty());
        foreach (int i in arr)
            tree.Insert(i);
        Assert.False(tree.IsEmpty());
        Assert.AreEqual(arr.Length, tree.Size);
        Assert.AreEqual(4, tree.Height);
    }
}