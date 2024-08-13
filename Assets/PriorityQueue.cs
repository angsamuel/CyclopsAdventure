using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<Tuple<int, T>> elements = new List<Tuple<int, T>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(int priority, T item)
    {
        elements.Add(Tuple.Create(priority, item));
        elements.Sort((x, y) => x.Item1.CompareTo(y.Item1)); // Sort based on priority
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }

        var item = elements[0].Item2;
        elements.RemoveAt(0);
        return item;
    }

    public T Peek()
    {
        if (elements.Count == 0)
        {
            throw new InvalidOperationException("The priority queue is empty.");
        }

        return elements[0].Item2;
    }
}
