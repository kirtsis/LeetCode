using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     61. Rotate List
    ///     https://leetcode.com/problems/rotate-list/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00061 {

        [PublicAPI]
        public ListNode RotateRight(ListNode head, Int32 k) {
            //return this._rotate1(head, k);
            return this._rotate2(head, k);
        }

        private ListNode _rotate1(ListNode head, Int32 k) {
            if ((head?.next == null) || (k == 0)) return head;

            Int32 length = 1;
            ListNode tail = head;
            while (tail.next != null) {
                length++;
                tail = tail.next;
            }

            Int32 rotation = (length - k % length) % length;
            if (rotation == 0) return head;

            ListNode current = head;
            while (rotation > 1) {
                rotation--;
                current = current.next;
            }

            tail.next = head;
            head = current.next;
            current.next = null;
            return head;
        }

        private ListNode _rotate2(ListNode head, Int32 k) {
            if ((head?.next == null) || (k == 0)) return head;

            Int32 length = 1;
            ListNode tail = head;
            while (tail.next != null) {
                length++;
                tail = tail.next;
            }

            Int32 rotation = (length - k % length) % length;
            if (rotation == 0) return head;

            tail.next = head;
            while (rotation > 0) {
                rotation--;
                tail = tail.next;
            }

            head = tail.next;
            tail.next = null;
            return head;
        }

        [Test]
        [TestCase("[1,2,3,4,5]", 2, ExpectedResult = "[4,5,1,2,3]")]
        [TestCase("[0,1,2]", 4, ExpectedResult = "[2,0,1]")]
        [TestCase("[1,2,3,4,5]", 5, ExpectedResult = "[1,2,3,4,5]")]
        [TestCase("[1,2,3,4,5]", 4, ExpectedResult = "[2,3,4,5,1]")]
        [TestCase("[1,2,3,4,5]", 3, ExpectedResult = "[3,4,5,1,2]")]
        [TestCase("[1,2,3,4,5]", 2, ExpectedResult = "[4,5,1,2,3]")]
        [TestCase("[1,2,3,4,5]", 1, ExpectedResult = "[5,1,2,3,4]")]
        [TestCase("[1,2,3,4,5]", 0, ExpectedResult = "[1,2,3,4,5]")]
        public String Test(String input, Int32 k) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            ListNode result = this.RotateRight(head, k);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
