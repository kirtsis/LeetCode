using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     146. LRU Cache
    ///     https://leetcode.com/problems/lru-cache/
    /// </summary>
    [TestFixture]
    public class P00146 {

        [PublicAPI]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public class LRUCache {

            private readonly Int32 _capacity;
            private readonly Dictionary<Int32, LinkedListNode<KeyValuePair<Int32, Int32>>> _memory;
            private readonly LinkedList<KeyValuePair<Int32, Int32>> _queue;

            public LRUCache(Int32 capacity) {
                this._capacity = capacity;
                this._memory = new Dictionary<Int32, LinkedListNode<KeyValuePair<Int32, Int32>>>(capacity);
                this._queue = new LinkedList<KeyValuePair<Int32, Int32>>();
            }

            public Int32 Get(Int32 key) {
                if (!this._memory.ContainsKey(key)) return -1;

                LinkedListNode<KeyValuePair<Int32, Int32>> node = this._memory[key];
                this._queue.Remove(node);
                this._queue.AddFirst(node);
                return node.Value.Value;
            }

            public KeyValuePair<Int32, Int32>? Put(Int32 key, Int32 value) {
                KeyValuePair<Int32, Int32>? evicted = null;
                LinkedListNode<KeyValuePair<Int32, Int32>> node;

                Boolean found = this._memory.TryGetValue(key, out node);
                if (found) this._queue.Remove(node);
                if (!found && (this._memory.Count == this._capacity)) {
                    evicted = this._queue.Last.Value;
                    this._memory.Remove(this._queue.Last.Value.Key);
                    this._queue.RemoveLast();
                }

                var pair = new KeyValuePair<Int32, Int32>(key, value);
                if (node == null) node = new LinkedListNode<KeyValuePair<Int32, Int32>>(pair);
                else node.Value = pair;

                this._queue.AddFirst(node);
                this._memory[key] = node;
                return evicted;
            }

        }

        [Test]
        [TestCase("[\"LRUCache\",\"put\",\"put\",\"get\",\"put\",\"get\",\"put\",\"get\",\"get\",\"get\"]", "[[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]", ExpectedResult = "[null,null,null,1,null,-1,null,-1,3,4]")]
        [TestCase("[\"LRUCache\",\"put\",\"put\",\"get\",\"put\",\"get\",\"put\",\"get\",\"get\",\"get\"]", "[[2],[1,10],[2,20],[1],[3,30],[2],[4,40],[1],[3],[4]]", ExpectedResult = "[null,null,null,10,null,-1,null,-1,30,40]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<Int32[][]>(input2);

            var result = new List<Int32?>();

            LRUCache cache = null;
            Console.WriteLine("LRUCache cache = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "LRUCache":
                        Console.WriteLine("cache = new LRUCache(capacity: {0})", parameters[i][0]);
                        cache = new LRUCache(capacity: parameters[i][0]);
                        result.Add(null);
                        break;

                    case "put":
                        Console.Write("cache.Put({0}, {1})", parameters[i][0], parameters[i][1]);
                        KeyValuePair<Int32, Int32>? evicted = cache?.Put(key: parameters[i][0], value: parameters[i][1]);
                        if (evicted != null) Console.WriteLine("\t\t// evicts key {0}", evicted.Value.Key);
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "get":
                        Console.Write("cache.Get({0})", parameters[i][0]);
                        Int32? value = cache?.Get(key: parameters[i][0]);
                        if (value != null) Console.WriteLine("\t\t// returns: {0}{1}", value, value == -1 ? " (not found)" : null);
                        else Console.WriteLine();
                        result.Add(value);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
