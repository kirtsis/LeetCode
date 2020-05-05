using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     142. Linked List Cycle II
    ///     https://leetcode.com/problems/linked-list-cycle-ii/
    /// </summary>
    [TestFixture]
    public class P00142 {

        [PublicAPI]
        public ListNode DetectCycle(ListNode head) {
            ListNode slow = head;
            ListNode fast = head;
            while (fast?.next != null) {
                slow = slow.next;
                fast = fast.next.next;
                if (slow != fast) continue;

                fast = head;
                while (slow != fast) {
                    slow = slow.next;
                    fast = fast.next;
                }

                return fast;
            }

            return null;
        }

        [Test]
        [TestCase("[3,2,0,-4]", 1, ExpectedResult = "tail connects to node index 1")]
        [TestCase("[1,2]", 0, ExpectedResult = "tail connects to node index 0")]
        [TestCase("[1]", -1, ExpectedResult = "no cycle")]
        public String Test(String input, Int32 pos) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input), pos);
            ListNode cycleStart = this.DetectCycle(head);
            Int32 index = ListNode.FindIndex(head, cycleStart);
            return index == -1 ? "no cycle" : $"tail connects to node index {index}";
        }

    }

}
