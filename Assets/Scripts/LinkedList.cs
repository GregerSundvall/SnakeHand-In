


using UnityEngine;

public class LinkedList<T>
{

    public ListNode<T> head;
    private ListNode<T> tail;
    private ListNode<T> current;

    public T Head()
    {
        return head.content;
        
    }
    public int Count { get; private set; }
    public class ListNode<T>
    {
        public T content;
        public ListNode<T> previous;
        public ListNode<T> next;
    }

    
    
    public void Add(T item)
    {
        if (Count == 0)
        {
            head = new ListNode<T>
            {
                content = item
            };
            tail = head;
            Count++;
            return;
        }

        tail.next = new ListNode<T>
        {
            content = item
        };
        tail = tail.next;
        Count++;
    }

    public void Pop()
    {
        if (Count < 2)
        {
            Debug.Log("List only contains 1 item");
            return;
        }
        current = head;
        for (int i = 0; i < Count -1; i++)
        {
            if (i == Count -2)
            {
                current.next = null;
                return;
            }
            current = current.next;
        }
        Count--;
    }
    

    
    
}
