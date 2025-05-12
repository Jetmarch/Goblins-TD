using System;
using System.Collections.Generic;


namespace Game.GameEngine.Common
{
    public class PriorityQueue<T>
    {
        private readonly List<(T item, float priority)> _heap = new();
        private readonly Dictionary<T, int> _indexMap = new();
        private IEqualityComparer<T> _comparer;

        public int Count => _heap.Count;

        public PriorityQueue(IEqualityComparer<T> customComparer = null)
        {
            _comparer = customComparer ?? EqualityComparer<T>.Default;
        }

        public void Enqueue(T item, float priority)
        {
            if (_indexMap.ContainsKey(item))
            {
                UpdatePriority(item, priority);
                return;
            }

            _heap.Add((item, priority));
            int i = _heap.Count - 1;
            _indexMap[item] = i;
            SiftUp(i);
        }

        public T Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            T topItem = _heap[0].item;
            Swap(0, _heap.Count - 1);

            _heap.RemoveAt(_heap.Count - 1);
            _indexMap.Remove(topItem);

            if (_heap.Count > 0)
                SiftDown(0);

            return topItem;
        }

        public void UpdatePriority(T item, float newPriority)
        {
            if (!_indexMap.TryGetValue(item, out int i))
                throw new InvalidOperationException("Item not found in queue");

            float oldPriority = _heap[i].priority;
            _heap[i] = (item, newPriority);

            if (newPriority < oldPriority)
                SiftUp(i);
            else
                SiftDown(i);
        }

        public bool Contains(T item) => _indexMap.ContainsKey(item);
        
        public bool TryGetElement(T item, out T foundItem)
        {
            if (_indexMap.TryGetValue(item, out int index))
            {
                foundItem = _heap[index].item;
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
                if (_heap[i].priority >= _heap[parent].priority)
                    break;

                Swap(i, parent);
                i = parent;
            }
        }

        private void SiftDown(int i)
        {
            int last = _heap.Count - 1;
            while (true)
            {
                int left = i * 2 + 1;
                int right = i * 2 + 2;
                int smallest = i;

                if (left <= last && _heap[left].priority < _heap[smallest].priority)
                    smallest = left;
                if (right <= last && _heap[right].priority < _heap[smallest].priority)
                    smallest = right;

                if (smallest == i) break;

                Swap(i, smallest);
                i = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            (_heap[a], _heap[b]) = (_heap[b], _heap[a]);
            _indexMap[_heap[a].item] = a;
            _indexMap[_heap[b].item] = b;
        }
    }

}