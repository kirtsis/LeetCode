using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     328. Odd Even Linked List
    ///     https://leetcode.com/problems/odd-even-linked-list/
    /// </summary>
    [TestFixture]
    public class P00328 {

        [PublicAPI]
        public ListNode OddEvenList(ListNode head) {
            ListNode oddsPointer = head;
            ListNode evensPointer = head?.next;
            ListNode evensHead = evensPointer; // store evens head for later
            while ((oddsPointer?.next != null) || (evensPointer?.next != null)) {
                // need to store the nexts before assign them
                ListNode nextOdd = oddsPointer?.next?.next;
                ListNode nextEven = evensPointer?.next?.next;
                if (oddsPointer != null) oddsPointer.next = nextOdd;
                if (evensPointer != null) evensPointer.next = nextEven;

                // move the pointers if next is available
                oddsPointer = oddsPointer?.next ?? oddsPointer;
                evensPointer = evensPointer?.next ?? evensPointer;
            }

            // join the last odd with the evens head, oddsPointer can be null if head is null
            if (oddsPointer != null) oddsPointer.next = evensHead;

            return head;
        }

        [Test]
        [TestCase("[1,2,3,4,5]", ExpectedResult = "[1,3,5,2,4]")]
        [TestCase("[2,1,3,5,6,4,7]", ExpectedResult = "[2,3,6,7,1,5,4]")]
        [TestCase("[]", ExpectedResult = "[]")]
        [TestCase("[1]", ExpectedResult = "[1]")]
        [TestCase("[1,2]", ExpectedResult = "[1,2]")]
        [TestCase("[1,2,3]", ExpectedResult = "[1,3,2]")]
        public String Test(String input) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            ListNode result = this.OddEvenList(head);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
