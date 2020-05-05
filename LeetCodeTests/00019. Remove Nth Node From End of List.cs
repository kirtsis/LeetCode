using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     19. Remove Nth Node From End of List
    ///     https://leetcode.com/problems/remove-nth-node-from-end-of-list/
    /// </summary>
    [TestFixture]
    public class P00019 {

        [PublicAPI]
        public ListNode RemoveNthFromEnd(ListNode head, Int32 n) {
            // find n-th node from the beginning (0-based)
            ListNode nthNode = head;
            while ((nthNode != null) && (n > 0)) {
                nthNode = nthNode.next;
                n--;
            }

            // if there are less than or exactly n nodes
            if (nthNode == null) {
                // remove the head
                head = head.next;
                return head;
            }

            // move a second node until the first reaches the end
            ListNode oneBeforeTheNthNodeFromTheEnd = head;
            while (nthNode.next != null) {
                oneBeforeTheNthNodeFromTheEnd = oneBeforeTheNthNodeFromTheEnd.next;
                nthNode = nthNode.next;
            }

            // remove the n-th node from the end
            oneBeforeTheNthNodeFromTheEnd.next = oneBeforeTheNthNodeFromTheEnd.next.next;
            return head;
        }

        [Test]
        [TestCase("[1,2,3,4,5]", 2, ExpectedResult = "[1,2,3,5]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 1, ExpectedResult = "[1,2,3,4,5,6,7]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 2, ExpectedResult = "[1,2,3,4,5,6,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 3, ExpectedResult = "[1,2,3,4,5,7,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 4, ExpectedResult = "[1,2,3,4,6,7,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 5, ExpectedResult = "[1,2,3,5,6,7,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 6, ExpectedResult = "[1,2,4,5,6,7,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 7, ExpectedResult = "[1,3,4,5,6,7,8]")]
        [TestCase("[1,2,3,4,5,6,7,8]", 8, ExpectedResult = "[2,3,4,5,6,7,8]")]
        public String Test(String input, Int32 n) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            ListNode result = this.RemoveNthFromEnd(head, n);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
