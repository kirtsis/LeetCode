using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     21. Merge Two Sorted Lists
    ///     https://leetcode.com/problems/merge-two-sorted-lists/
    /// </summary>
    [TestFixture]
    public class P00021 {

        [PublicAPI]
        public ListNode MergeTwoLists(ListNode l1, ListNode l2) {
            if (l1 == null) return l2;
            if (l2 == null) return l1;

            var dummyRoot = new ListNode(0);
            ListNode currentNode = dummyRoot;
            ListNode nextl1 = l1;
            ListNode nextl2 = l2;
            while ((nextl1 != null) || (nextl2 != null)) {
                if ((nextl1 != null) && (nextl2 != null)) {
                    if (nextl1.val <= nextl2.val) {
                        currentNode.next = nextl1;
                        nextl1 = nextl1.next;
                    } else {
                        currentNode.next = nextl2;
                        nextl2 = nextl2.next;
                    }
                } else {
                    if (nextl1 != null) {
                        currentNode.next = nextl1;
                        nextl1 = nextl1.next;
                    } else {
                        currentNode.next = nextl2;
                        nextl2 = nextl2.next;
                    }
                }
                currentNode = currentNode.next;
            }

            return dummyRoot.next;
        }

        [Test]
        [TestCase("[1,2,4]", "[1,3,4]", ExpectedResult = "[1,1,2,3,4,4]")]
        public String Test(String input1, String input2) {
            ListNode l1 = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input1));
            ListNode l2 = ListNode.Make(JsonConvert.DeserializeObject<Int32[]>(input2));
            ListNode result = this.MergeTwoLists(l1, l2);
            return JsonConvert.SerializeObject(ListNode.Make(result));
        }

    }

}
