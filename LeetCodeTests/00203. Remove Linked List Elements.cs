using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     203. Remove Linked List Elements
    ///     https://leetcode.com/problems/remove-linked-list-elements/
    /// </summary>
    [TestFixture]
    public class P00203 {

        [PublicAPI]
        public ListNode RemoveElements(ListNode head, Int32 val) {
            while ((head != null) && (head.val == val)) {
                head = head.next;
            }

            ListNode previous = head;
            ListNode current = previous?.next;
            while (current != null) {
                if (current.val == val) previous.next = current.next;
                else previous = current;
                current = current.next;
            }

            return head;
        }

        [Test]
        [TestCase("[1,2,6,3,4,5,6]", 6, ExpectedResult = "[1,2,3,4,5]")]
        [TestCase("[1,2,1,3,4,5,6]", 1, ExpectedResult = "[2,3,4,5,6]")]
        [TestCase("[1,2,3,4,5]", 6, ExpectedResult = "[1,2,3,4,5]")]
        [TestCase("[1]", 6, ExpectedResult = "[1]")]
        [TestCase("[1]", 1, ExpectedResult = "[]")]
        [TestCase("[]", 1, ExpectedResult = "[]")]
        public String Test(String input, Int32 val) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            ListNode result = this.RemoveElements(head, val);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
