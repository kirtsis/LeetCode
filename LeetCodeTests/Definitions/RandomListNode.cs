using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;
using Node = LeetCodeTests.RandomListNode;

namespace LeetCodeTests {

    [PublicAPI]
    [DebuggerDisplay("val = {val}")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RandomListNode {

        public Node next;
        public Node random;
        public Int32 val;

        public RandomListNode(Int32 x) {
            this.val = x;
        }

        public static Node Make([NotNull] IEnumerable<IEnumerable<Int32?>> pairs) {
            if (pairs == null) throw new ArgumentNullException(nameof(pairs));

            IEnumerable<Int32?>[] nodes = pairs as IEnumerable<Int32?>[] ?? pairs.ToArray();
            Int32 length = nodes.Length;
            if (length == 0) return null;

            var dictionary = new Dictionary<Int32, Tuple<Node, Int32?>>();
            for (Int32 index = 0; index < length; ++index) {
                if (nodes[index] == null) throw new ArgumentException("Node representation cannot be null.", nameof(pairs));

                Int32?[] values = nodes[index] as Int32?[] ?? nodes[index].ToArray();
                if (values.Length != 2) throw new ArgumentException("Node representation cannot have length other than 2.", nameof(pairs));
                if (values[0] == null) throw new ArgumentException("The first value in the Node representation cannot be null.", nameof(pairs));

                dictionary.Add(index, Tuple.Create(new Node(values[0].Value), values[1]));
            }

            Node previous = null;
            for (Int32 index = 0; index < length; ++index) {
                Tuple<Node, Int32?> tuple = dictionary[index];

                Node node = tuple.Item1;
                if (previous != null) previous.next = node;
                previous = node;

                Int32? randomIndex = tuple.Item2;
                if (randomIndex != null) node.random = dictionary[randomIndex.Value].Item1;
            }

            return dictionary[0].Item1;
        }

        public static IEnumerable<IEnumerable<Int32?>> Make(Node node) {
            var dictionary = new Dictionary<Node, Int32>();

            Int32 index = 0;
            while (node != null) {
                dictionary.Add(node, index++);
                node = node.next;
            }

            return dictionary.Select(item => new List<Int32?> {
                                 item.Key.val,
                                 item.Key.random != null ? dictionary[item.Key.random] : (Int32?)null
                             }).ToList();
        }

    }

    [TestFixture]
    public class RandomListNodeTests {

        [Test]
        [TestCase("[]")]
        [TestCase("[[1,1],[2,1]]")]
        [TestCase("[[3,null],[3,0],[3,null]]")]
        [TestCase("[[7,null],[13,0],[11,4],[10,2],[1,0]]")]
        public void Test(String input) {
            // ARRANGE
            var values = JsonConvert.DeserializeObject<Int32?[][]>(input);

            // ACT
            Node head = Node.Make(values);
            IEnumerable<IEnumerable<Int32?>> valuesOut = Node.Make(head);

            // ASSERT
            String output = JsonConvert.SerializeObject(valuesOut);
            Assert.That(output, Is.EqualTo(input));
        }

    }

}
