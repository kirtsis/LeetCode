using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;
using Node = LeetCodeTests.ListNode;

namespace LeetCodeTests {

    [PublicAPI]
    [DebuggerDisplay("val = {val}")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ListNode {

        public Node next;
        public Int32 val;

        public ListNode(Int32 x) {
            this.val = x;
        }

        public static Node Make([NotNull] IEnumerable<Int32> values, Int32 cyclePosition = -1) {
            if (values == null) throw new ArgumentNullException(nameof(values));

            Int32[] enumerable = values as Int32[] ?? values.ToArray();
            Int32 length = enumerable.Length;

            Node head = null;
            Node tail = null;
            for (Int32 index = length - 1; index >= 0; --index) {
                var current = new Node(enumerable[index]) {
                    next = head
                };
                if (cyclePosition >= 0) {
                    if (tail == null) tail = current;
                    if (index == cyclePosition) tail.next = current;
                }
                head = current;
            }

            return head;
        }

        public static IEnumerable<Int32> Make(Node node) {
            var result = new List<Int32>();

            while (node != null) {
                result.Add(node.val);
                node = node.next;
            }

            return result;
        }

        public static Int32 FindIndex([NotNull] Node head, Node node) {
            if (head == null) throw new ArgumentNullException(nameof(head));

            if (node == null) return -1;

            Int32 result = 0;
            Node current = head;
            while (current != null) {
                if (current == node) return result;

                current = current.next;
                result++;
            }

            return -1;
        }

        public static void Concat([NotNull] Node first, [NotNull] Node second) {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));

            Node tail = first;
            while (tail.next != null) {
                tail = tail.next;
            }

            tail.next = second;
        }

    }

    [TestFixture]
    public class ListNodeTests {

        [Test]
        [TestCase("[]")]
        [TestCase("[2,4,3]")]
        [TestCase("[0,9,1,2,4]")]
        [TestCase("[5,0,1,8,4,5]")]
        [TestCase("[3,2,0,-4]")]
        [TestCase("[-5,0,-1,-8]")]
        [TestCase("[0,-5,0,-1,-8]")]
        public void Test(String input) {
            // ARRANGE
            var valuesIn = JsonConvert.DeserializeObject<Int32[]>(input);

            // ACT
            Node head = Node.Make(valuesIn);
            IEnumerable<Int32> valuesOut = Node.Make(head);

            // ASSERT
            String output = JsonConvert.SerializeObject(valuesOut);
            Assert.That(output, Is.EqualTo(input));
        }

    }

}
