using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ListNode {

        public ListNode next;
        public Int32 val;

        public ListNode(Int32 x) {
            this.val = x;
        }

        public static ListNode Make([NotNull] IEnumerable<Int32> values, Int32 cyclePosition = -1) {
            if (values == null) throw new ArgumentNullException(nameof(values));

            Int32[] enumerable = values as Int32[] ?? values.ToArray();
            Int32 length = enumerable.Length;

            ListNode head = null;
            ListNode tail = null;
            for (Int32 index = length - 1; index >= 0; --index) {
                var current = new ListNode(enumerable[index]) {
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

        public static IEnumerable<Int32> Make(ListNode node) {
            var result = new List<Int32>();

            while (node != null) {
                result.Add(node.val);
                node = node.next;
            }

            return result;
        }

        public static Int32 FindIndex([NotNull] ListNode head, ListNode cycleStart) {
            if (head == null) throw new ArgumentNullException(nameof(head));

            if (cycleStart == null) return -1;

            Int32 result = 0;
            ListNode node = head;
            while (node != null) {
                if (node == cycleStart) return result;

                node = node.next;
                result++;
            }

            return -1;
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
            var values = JsonConvert.DeserializeObject<Int32[]>(input);

            // ACT
            ListNode head = ListNode.Make(values);

            // ASSERT
            String output = JsonConvert.SerializeObject(ListNode.Make(head));
            Assert.That(output, Is.EqualTo(input));
        }

    }

}
