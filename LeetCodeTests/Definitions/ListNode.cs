using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LeetCodeTests {

    public class ListNode {

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public ListNode next;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
        public Int32 val;

        public ListNode(Int32 x) {
            this.val = x;
        }

        public static ListNode Make(Int32[] digits) {
            var result = new ListNode(digits[0]);

            ListNode current = result;
            for (Int32 i = 1; i < digits.Length; i++) {
                current.next = new ListNode(digits[i]);
                current = current.next;
            }

            return result;
        }

        public static Int32[] Make(ListNode node) {
            var result = new List<Int32>();

            while (node != null) {
                result.Add(node.val);
                node = node.next;
            }

            return result.ToArray();
        }

    }

}
