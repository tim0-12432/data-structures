﻿namespace DatastructuresTests;

[TestFixture]
public class StackTest
{
    [Test]
    public void TestInsert()
    {
        Datastructures.Stack<int> stack = new Datastructures.Stack<int>();
        Assert.True(stack.IsEmpty());
        for (int i = 0; i <= 10; i++)
            stack.Push(i);
        Console.WriteLine(stack);
        Assert.False(stack.IsEmpty());
        Assert.AreEqual(11, stack.Height);
        Assert.AreEqual(10, stack.Peek());
        stack.Push(11, 12, 13, 14, 15);
        Assert.AreEqual(16, stack.Height);
        Assert.AreEqual(15, stack.Peek());
    }
    
    [Test]
    public void TestDelete()
    {
        Datastructures.Stack<int> stack = new Datastructures.Stack<int>();
        Assert.Throws<InvalidOperationException>(delegate { stack.Pop(); });
        for (int i = 0; i <= 10; i++)
            stack.Push(i);
        Console.WriteLine(stack);
        Assert.AreEqual(10, stack.Pop());
        Assert.AreEqual(9, stack.Pop());
        Assert.AreEqual(8, stack.Pop());
        Assert.AreEqual(7, stack.Pop());
        Assert.AreEqual(6, stack.Pop());
        stack.Pop(3);
        Assert.AreEqual(2, stack.Peek());
    }
    
    [Test]
    public void TestIterator()
    {
        Datastructures.Stack<int> stack = new Datastructures.Stack<int>();
        for (int i = 0; i <= 10; i++)
            stack.Push(i);
        Console.WriteLine(stack);
        foreach (int number in stack)
        {
            Assert.True(number >= 0 & number <= 10);
            Console.Write($"{number} ");
        }
        Console.WriteLine();
    }
    
    [Test]
    public void TestReverse()
    {
        Datastructures.Stack<int> stack = new Datastructures.Stack<int>();
        for (int i = 0; i <= 10; i++)
            stack.Push(i);
        Console.WriteLine(stack);
        stack.Reverse();
        Console.WriteLine(stack);
        for (int i = 0; i <= 10; i++)
            Assert.AreEqual(i, stack.Pop());
    }
}