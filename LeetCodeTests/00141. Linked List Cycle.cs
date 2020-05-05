using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     141. Linked List Cycle
    ///     https://leetcode.com/problems/linked-list-cycle/
    /// </summary>
    [TestFixture]
    public class P00141 {

        [PublicAPI]
        public Boolean HasCycle(ListNode head) {
            //return this._flag(head);
            return this._twoPointers(head);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Boolean _flag(ListNode head) {
            var flag = new ListNode(0);
            ListNode node = head;
            while (node != null) {
                if (node.next == flag) return true;

                ListNode next = node.next;
                node.next = flag;
                node = next;
            }

            return false;
        }

        private Boolean _twoPointers(ListNode head) {
            ListNode slow = head;
            ListNode fast = head;
            while (fast?.next != null) {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast) return true;
            }

            return false;
        }

        [Test]
        [TestCase("[3,2,0,-4]", 1, ExpectedResult = true)]
        [TestCase("[1,2]", 0, ExpectedResult = true)]
        [TestCase("[1]", -1, ExpectedResult = false)]
        public Boolean Test(String input, Int32 pos) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input), pos);
            return this.HasCycle(head);
        }

    }

}
