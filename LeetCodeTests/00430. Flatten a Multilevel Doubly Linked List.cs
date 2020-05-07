using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;
using Node = LeetCodeTests.MultilevelDoublyLinkedListNode;

namespace LeetCodeTests {

    /// <summary>
    ///     430. Flatten a Multilevel Doubly Linked List
    ///     https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/
    /// </summary>
    [TestFixture]
    public class P00430 {

        [PublicAPI]
        public Node Flatten(Node head) {
            Node current = head;
            var next = new Stack<Node>();
            while (current != null) {
                if (current.child != null) {
                    if (current.next != null) next.Push(current.next);
                    current.next = current.child;
                    current.child = null;
                    current.next.prev = current;
                    current = current.next;
                    continue;
                }

                if ((current.next == null) && (next.Count > 0)) {
                    current.next = next.Pop();
                    current.next.prev = current;
                    current = current.next;
                    continue;
                }

                current = current.next;
            }

            return head;
        }

        [Test]
        [TestCase("[1,2,3,4,5,6,null,null,null,7,8,9,10,null,null,11,12]", ExpectedResult = "[1,2,3,7,8,11,12,9,10,4,5,6]")]
        [TestCase("[1,2,null,3]", ExpectedResult = "[1,3,2]")]
        [TestCase("[1,null,2,null,3]", ExpectedResult = "[1,2,3]")]
        [TestCase("[]", ExpectedResult = "[]")]
        public String Test(String input) {
            Node head = Node.Make(JsonConvert.DeserializeObject<Int32?[]>(input));
            Node result = this.Flatten(head);
            return JsonConvert.SerializeObject(Node.Make(result));
        }

    }

}
