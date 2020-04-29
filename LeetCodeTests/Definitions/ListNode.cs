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

        public static ListNode Make([NotNull] IEnumerable<Int32> values) {
            if (values == null) throw new ArgumentNullException(nameof(values));

            ListNode first = null;
            foreach (Int32 value in values.Reverse()) {
                var current = new ListNode(value) {
                    next = first
                };
                first = current;
            }

            return first;
        }

        public static IEnumerable<Int32> Make(ListNode node) {
            var result = new List<Int32>();

            while (node != null) {
                result.Add(node.val);
                node = node.next;
            }

            return result;
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
