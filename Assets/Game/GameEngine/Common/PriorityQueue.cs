using System;
using System.Collections.Generic;


namespace Game.GameEngine.Common
{


    public class PriorityQueue<T>
    {
        private List<(T item, float priority)> heap = new();
        private Dictionary<T, int> indexMap = new();
        private IEqualityComparer<T> comparer;

        public int Count => heap.Count;

        public PriorityQueue(IEqualityComparer<T> customComparer = null)
        {
            comparer = customComparer ?? EqualityComparer<T>.Default;
        }

        public void Enqueue(T item, float priority)
        {
            if (indexMap.ContainsKey(item))
            {
                UpdatePriority(item, priority);
                return;
            }

            heap.Add((item, priority));
            int i = heap.Count - 1;
            indexMap[item] = i;
            SiftUp(i);
        }

        public T Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T topItem = heap[0].item;
            Swap(0, heap.Count - 1);

            heap.RemoveAt(heap.Count - 1);
            indexMap.Remove(topItem);

            if (heap.Count > 0)
                SiftDown(0);

            return topItem;
        }

        public void UpdatePriority(T item, float newPriority)
        {
            if (!indexMap.TryGetValue(item, out int i))
                throw new InvalidOperationException("Item not found in queue");

            float oldPriority = heap[i].priority;
            heap[i] = (item, newPriority);

            if (newPriority < oldPriority)
                SiftUp(i);
            else
                SiftDown(i);
        }

        public bool Contains(T item) => indexMap.ContainsKey(item);
        
        public bool TryGetElement(T item, out T foundItem)
        {
            if (indexMap.TryGetValue(item, out int index))
            {
                foundItem = heap[index].item;
                return true;
            }

            foundItem = default;
            return false;
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (heap[i].priority >= heap[parent].priority)
                    break;

                Swap(i, parent);
                i = parent;
            }
        }

        private void SiftDown(int i)
        {
            int last = heap.Count - 1;
            while (true)
            {
                int left = i * 2 + 1;
                int right = i * 2 + 2;
                int smallest = i;

                if (left <= last && heap[left].priority < heap[smallest].priority)
                    smallest = left;
                if (right <= last && heap[right].priority < heap[smallest].priority)
                    smallest = right;

                if (smallest == i) break;

                Swap(i, smallest);
                i = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            (heap[a], heap[b]) = (heap[b], heap[a]);
            indexMap[heap[a].item] = a;
            indexMap[heap[b].item] = b;
        }
    }

}