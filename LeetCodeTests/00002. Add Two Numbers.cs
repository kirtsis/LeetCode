using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     2. Add Two Numbers
    ///     https://leetcode.com/problems/add-two-numbers/
    /// </summary>
    [TestFixture]
    public class P00002 {

        [PublicAPI]
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
            Int32 sum = l1.val + l2.val;
            Int32 digit = sum >= 10 ? sum % 10 : sum;
            Int32 carry = sum / 10;
            var result = new ListNode(digit);
            ListNode current = result;
            ListNode n1 = l1.next;
            ListNode n2 = l2.next;
            while ((n1 != null) || (n2 != null)) {
                sum = (n1?.val ?? 0) + (n2?.val ?? 0);
                sum += carry;
                digit = sum >= 10 ? sum % 10 : sum;
                carry = sum / 10;
                current.next = new ListNode(digit);
                current = current.next;
                n1 = n1?.next;
                n2 = n2?.next;
            }

            if (carry > 0) result.next = new ListNode(carry);
            return result;
        }

        [Test]
        [TestCase("[2,4,3]", "[5,6,4]", ExpectedResult = "[7,0,8]")]
        public String Test(String input1, String input2) {
            ListNode l1 = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input1));
            ListNode l2 = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input2));
            ListNode result = this.AddTwoNumbers(l1, l2);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
