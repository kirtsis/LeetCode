using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;
using Node = LeetCodeTests.MultilevelDoublyLinkedListNode;

namespace LeetCodeTests {

    [PublicAPI]
    [DebuggerDisplay("val = {val}")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class MultilevelDoublyLinkedListNode {

        public Node child;
        public Node next;
        public Node prev;
        public Int32 val;

        public MultilevelDoublyLinkedListNode(Int32 x) {
            this.val = x;
        }

        public static Node Make([NotNull] IEnumerable<Int32?> values) {
            if (values == null) throw new ArgumentNullException(nameof(values));

            Int32?[] enumerable = values as Int32?[] ?? values.ToArray();
            Int32 length = enumerable.Length;
            if (length == 0) return null;

            if (enumerable[0] == null) throw new ArgumentException("The value of index zero cannot be null.", nameof(values));

            Int32 currentLevel = 0;
            Boolean newLevel = false;
            var levels = new List<IList<Int32?>>();
            foreach (Int32? value in enumerable) {
                if (levels.Count <= currentLevel) levels.Add(new List<Int32?>());
                levels[currentLevel].Add(value);

                if ((value == null) && !newLevel) {
                    currentLevel++;
                    newLevel = true;
                    continue;
                }

                if ((value != null) && newLevel) newLevel = false;
            }

            Node head = null;
            Node levelHead = null;
            foreach (IList<Int32?> level in levels) {
                Node parent = levelHead;
                levelHead = null;
                Node previous = null;
                foreach (Int32? value in level) {
                    if (value == null) {
                        parent = parent?.next;
                        continue;
                    }

                    var current = new Node(value.Value) {
                        prev = previous
                    };
                    if (previous != null) previous.next = current;
                    previous = current;

                    if (parent != null) {
                        parent.child = current;
                        parent = null;
                    }

                    if (head == null) head = current;
                    if (levelHead == null) levelHead = current;
                }
            }

            return head;
        }

        public static IEnumerable<Int32?> Make(Node node) {
            var levels = new List<IList<Int32?>>();

            Int32 indentation = 0;
            var currentLevel = new List<Int32?>();
            while (node != null) {
                currentLevel.Add(node.val);
                if (node.child != null) {
                    List<Int32?> childLevel = MultilevelDoublyLinkedListNode.Make(node.child).ToList();
                    childLevel.InsertRange(0, Enumerable.Repeat<Int32?>(null, indentation));
                    levels.Insert(0, childLevel);
                }
                node = node.next;
                indentation++;
            }

            levels.Insert(0, currentLevel);

            var result = new List<Int32?>();
            foreach (IList<Int32?> level in levels) {
                result.AddRange(level);
                result.Add(null);
            }

            return result.Take(1 + result.FindLastIndex(value => value != null));
        }

    }

    [TestFixture]
    public class MultilevelDoublyLinkedListNodeTests {

        [Test]
        [TestCase("[1,2,3,4,5,6,null,null,null,7,8,9,10,null,null,11,12]")]
        [TestCase("[1,2,null,3]")]
        [TestCase("[]")]
        public void Test(String input) {
            // ARRANGE
            var valuesIn = JsonConvert.DeserializeObject<Int32?[]>(input);

            // ACT
            Node head = Node.Make(valuesIn);
            IEnumerable<Int32?> valuesOut = Node.Make(head);

            // ASSERT
            String output = JsonConvert.SerializeObject(valuesOut);
            Assert.That(output, Is.EqualTo(input));
        }

    }

}
