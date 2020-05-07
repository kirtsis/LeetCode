using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     206. Reverse Linked List
    ///     https://leetcode.com/problems/reverse-linked-list/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00206 {

        [PublicAPI]
        public ListNode ReverseList(ListNode head) {
            //return this._recursively(head, head);
            return this._iteratively(head);
        }

        private ListNode _recursively(ListNode head, ListNode current) {
            if (current?.next == null) return head;

            ListNode next = current.next;
            current.next = next.next;
            next.next = head;
            head = next;

            return this._recursively(head, current);
        }

        private ListNode _iteratively(ListNode head) {
            ListNode current = head;
            while (current?.next != null) {
                ListNode next = current.next;
                current.next = next.next;
                next.next = head;
                head = next;
            }

            return head;
        }

        [Test]
        [TestCase("[1,2,3,4,5]", ExpectedResult = "[5,4,3,2,1]")]
        [TestCase("[1]", ExpectedResult = "[1]")]
        [TestCase("[]", ExpectedResult = "[]")]
        public String Test(String input) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            ListNode result = this.ReverseList(head);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
