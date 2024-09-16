using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatQueue
{ 
    public Queue<ICombatQueueable> queue { get; set; }

    public CombatQueue()
    {
        this.queue = new Queue<ICombatQueueable>();
    }

    public void Clear()
    {
        this.queue.Clear();
    }

    public void Push(ICombatQueueable queueable)
    {
        this.queue.Enqueue(queueable);
    }

    public void PriorityPush(ICombatQueueable queueable)
    {
        ICombatQueueable[] items = this.queue.ToArray();
        this.queue.Clear();
        this.queue.Enqueue(queueable);
        foreach (ICombatQueueable item in items) 
        {
            this.queue.Enqueue(item);
        }
    }

    public ICombatQueueable Pop()
    {
        return this.queue.Dequeue();
    }

    public void Remove(ICombatQueueable queueable)
    {
        ICombatQueueable[] items = this.queue.ToArray();
        this.queue.Clear();
        foreach (ICombatQueueable item in items)
        {
            if (item != queueable)
            {
                this.queue.Enqueue(item);
            }
        }
        CombatUI.Instance.RenderRemove(queueable);
    }

    public bool IsEmpty()
    {  
        return this.queue.Count == 0 ? true : false;
    }
}
