using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class LRUCache
    {
        public int Size;
        Dictionary<int, LinkedListNode<LRUCacheItem>> processToAddress = new Dictionary<int, LinkedListNode<LRUCacheItem>>();
        LinkedList<LRUCacheItem> cache = new LinkedList<LRUCacheItem>();
        public LRUCache(int size)
        {
            Size = size;
        }

        public object Get(int pID)
        {
            if (!cache.Any())
                return null;

            LinkedListNode<LRUCacheItem> node;
            if (processToAddress.TryGetValue(pID, out node))
            {
                // it's in cache, remove it and add to end
                cache.Remove(node);
                cache.AddLast(node);
            }

            return node.Value;
        }

        public void Add(int pID, object value)
        {
            LinkedListNode<LRUCacheItem> node;
            if (processToAddress.TryGetValue(pID, out node))
            {
                cache.Remove(node);
                cache.AddLast(node);
                return;
            }

            if (Size == cache.Count)
            {
                node = cache.First;
                cache.Remove(node);
                processToAddress.Remove(node.Value.Key);
            }

            node = new LinkedListNode<LRUCacheItem>(new LRUCacheItem(pID, value));
            processToAddress.Add(pID, node);
            cache.AddLast(node);
        }

        class LRUCacheItem
        {
            public int Key;
            public object Value;
            public LRUCacheItem(int key, object value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
