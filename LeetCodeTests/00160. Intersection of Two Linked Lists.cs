using System;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     160. Intersection of Two Linked Lists
    ///     https://leetcode.com/problems/intersection-of-two-linked-lists/
    /// </summary>
    [TestFixture]
    public class P00160 {

        [PublicAPI]
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
            ListNode nodeA = headA;
            ListNode nodeB = headB;
            while (nodeA != nodeB) {
                nodeA = nodeA != null ? nodeA.next : headB;
                nodeB = nodeB != null ? nodeB.next : headA;
            }

            return nodeA;
        }

        [Test]
        [TestCase(8, "[4,1,8,4,5]", "[5,0,1,8,4,5]", 2, 3, ExpectedResult = "Intersected at '8'")]
        [TestCase(2, "[0,9,1,2,4]", "[3,2,4]", 3, 1, ExpectedResult = "Intersected at '2'")]
        [TestCase(0, "[2,6,4]", "[1,5]", 3, 2, ExpectedResult = "No intersection")]
        public String Test(Int32 intersectVal, String listA, String listB, Int32 skipA, Int32 skipB) {
            var valuesA = JsonConvert.DeserializeObject<Int32[]>(listA);
            ListNode headA = intersectVal == 0 ? ListNode.Make(valuesA) : ListNode.Make(valuesA.Take(skipA));

            var valuesB = JsonConvert.DeserializeObject<Int32[]>(listB);
            ListNode headB = intersectVal == 0 ? ListNode.Make(valuesB) : ListNode.Make(valuesB.Take(skipB));

            if (intersectVal != 0) {
                ListNode commonTail = ListNode.Make(valuesA.Skip(skipA));
                ListNode.Concat(headA, commonTail);
                ListNode.Concat(headB, commonTail);
            }

            ListNode node = this.GetIntersectionNode(headA, headB);
            return node == null ? "No intersection" : $"Intersected at '{node.val}'";
        }

    }

}
