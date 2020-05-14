using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1429. First Unique Number
    ///     https://leetcode.com/problems/first-unique-number/
    /// </summary>
    [TestFixture]
    public class P01429 {

        [PublicAPI]
        public class FirstUnique : IEnumerable<Int32> {

            private const Int32 Capacity = 100000; // (Problem Constraints: 1 <= nums.length <= 10^5)

            private readonly Queue<Int32> _queue = new Queue<Int32>(FirstUnique.Capacity);
            private readonly Dictionary<Int32, Int32> _queueAppearances = new Dictionary<Int32, Int32>(FirstUnique.Capacity);
            private readonly LinkedList<Int32> _unique = new LinkedList<Int32>();
            private readonly Dictionary<Int32, LinkedListNode<Int32>> _uniqueMap = new Dictionary<Int32, LinkedListNode<Int32>>(FirstUnique.Capacity);

            public FirstUnique(Int32[] nums) {
                foreach (Int32 num in nums) {
                    this.Add(num);
                }
            }

            public Int32 ShowFirstUnique() {
                return this._unique.First?.Value ?? -1;
            }

            public void Add(Int32 value) {
                // if we don't have any appearance of the value
                if (!this._queueAppearances.ContainsKey(value)) {
                    // add the value into the queue with one appearance
                    this._queue.Enqueue(value);
                    this._queueAppearances.Add(value, 1);

                    // add the value at the end of the unique list
                    LinkedListNode<Int32> node = this._unique.AddLast(value);
                    this._uniqueMap.Add(value, node);
                } else {
                    // otherwise
                    // add the value into the queue and increase it's appearances by one
                    this._queue.Enqueue(value);
                    this._queueAppearances[value]++;

                    // remove the value from the unique list, only if already exists
                    LinkedListNode<Int32> node;
                    if (this._uniqueMap.TryGetValue(value, out node)) {
                        this._uniqueMap.Remove(value);
                        this._unique.Remove(node);
                    }
                }
            }

            #region IEnumerable<Int32>

            IEnumerator<Int32> IEnumerable<Int32>.GetEnumerator() {
                return this._getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this._getEnumerator();
            }

            private Queue<Int32>.Enumerator _getEnumerator() {
                return this._queue.GetEnumerator();
            }

            #endregion
        }

        [Test]
        [TestCase("[\"FirstUnique\",\"showFirstUnique\",\"add\",\"showFirstUnique\",\"add\",\"showFirstUnique\",\"add\",\"showFirstUnique\"]", "[[[2,3,5]],[],[5],[],[2],[],[3],[]]", ExpectedResult = "[null,2,null,2,null,3,null,-1]")]
        [TestCase("[\"FirstUnique\",\"showFirstUnique\",\"add\",\"add\",\"add\",\"add\",\"add\",\"showFirstUnique\"]", "[[[7,7,7,7,7,7]],[],[7],[3],[3],[7],[17],[]]", ExpectedResult = "[null,-1,null,null,null,null,null,17]")]
        [TestCase("[\"FirstUnique\",\"showFirstUnique\",\"add\",\"showFirstUnique\"]", "[[[809]],[],[809],[]]", ExpectedResult = "[null,809,null,-1]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<Object[][]>(input2);

            var result = new List<Int32?>();

            FirstUnique firstUnique = null;
            Console.WriteLine("FirstUnique firstUnique = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "FirstUnique":
                        Int32[] nums = ((JArray)parameters[i][0]).Select(o => (Int32)o).ToArray();
                        Console.WriteLine("firstUnique = new FirstUnique([{0}]);", String.Join(",", nums));
                        firstUnique = new FirstUnique(nums);
                        result.Add(null);
                        break;

                    case "showFirstUnique":
                        Console.Write("firstUnique.showFirstUnique();");
                        Int32? value = firstUnique?.ShowFirstUnique();
                        if (value != null) Console.WriteLine("\t\t// returns: {0}{1}", value, value == -1 ? " (not found)" : null);
                        else Console.WriteLine();
                        result.Add(value);
                        break;

                    case "add":
                        Console.Write("firstUnique.add({0});", (Int32)(Int64)parameters[i][0]);
                        firstUnique?.Add(value: (Int32)(Int64)parameters[i][0]);
                        if (firstUnique != null) Console.WriteLine("\t\t// the queue is [{0}]", String.Join(",", firstUnique));
                        else Console.WriteLine();
                        result.Add(null);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
