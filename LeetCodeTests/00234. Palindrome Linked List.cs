using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     234. Palindrome Linked List
    ///     https://leetcode.com/problems/palindrome-linked-list/
    /// </summary>
    [TestFixture]
    public class P00234 {

        [PublicAPI]
        public Boolean IsPalindrome(ListNode head) {
            // first parameter will be modified (ref)
            // in order to leave head unmodified, we pass a new variable start instead of head
            ListNode start = head;
            return this._isPalindrome(ref start, head);
        }

        private Boolean _isPalindrome(ref ListNode start, ListNode end) {
            // head is null, or we reached the end of the linked list
            if (end == null) return true;

            // recursively call for the next node (until we reach the end of the linked list)
            Boolean recursionResult = this._isPalindrome(ref start, end.next);

            // after we return from each recursion
            // check if values are equal
            Boolean equalValues = start.val == end.val;

            // and move start to the next node for the next recursion
            start = start.next;

            // return true if both are true
            return recursionResult && equalValues;
        }

        [Test]
        [TestCase("[1,2]", ExpectedResult = false)]
        [TestCase("[1,2,2,1]", ExpectedResult = true)]
        [TestCase("[1]", ExpectedResult = true)]
        [TestCase("[]", ExpectedResult = true)]
        public Boolean Test(String input) {
            ListNode head = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input));
            return this.IsPalindrome(head);
        }

    }

}
