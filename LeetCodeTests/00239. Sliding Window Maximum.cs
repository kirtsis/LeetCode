using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     239. Sliding Window Maximum
    ///     https://leetcode.com/problems/sliding-window-maximum/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00239 {

        [PublicAPI]
        public Int32[] MaxSlidingWindow(Int32[] nums, Int32 k) {
            //return this._twoPointers(nums, k);
            return this._linkedList(nums, k);
        }

        private Int32[] _twoPointers(Int32[] nums, Int32 k) {
            // Problem Constraints: 1 <= k <= nums.length
            if (k == 1) return nums;

            Int32 length = nums.Length;
            var result = new Int32[length - k + 1];

            // window: nums[start..end]
            Int32 start = 0;
            Int32 end = k - 1; // k is the window's length, so k - 1 is the end index of the window

            // iterator in the window
            Int32 iterator = start;

            // asume max is the end element of the window
            Int32 max = nums[end];

            // while end is before the last element
            while (end < length) {
                // check iterator element for max
                if (nums[iterator] > max) max = nums[iterator];

                // move iterator
                iterator++;

                // continue until the iterator has reached the end of the window
                if (iterator != end) continue;

                // store max
                result[end - k + 1] = max;

                // move window
                start++;
                end++;

                // reset iterator to the start of the window
                iterator = start;

                // asume max is the end element of the window, if we have more windows
                if (end < length) max = nums[end];
            }

            return result;
        }

        private Int32[] _linkedList(Int32[] nums, Int32 k) {
            if (k == 1) return nums;

            Int32 length = nums.Length;
            var result = new Int32[length - k + 1];

            var indexes = new LinkedList<Int32>();
            for (Int32 index = 0; index < length; index++) {
                // remove indexes from the front, that are before our window
                while ((indexes.Count > 0) && (indexes.First.Value < index - k + 1)) {
                    indexes.RemoveFirst();
                }

                // remove indexes from the back, that points to elements smaller than current element
                while ((indexes.Count > 0) && (nums[indexes.Last.Value] <= nums[index])) {
                    indexes.RemoveLast();
                }

                // save current index at the back
                indexes.AddLast(index);

                // save result
                if (index - k + 1 >= 0) result[index - k + 1] = nums[indexes.First.Value];
            }

            return result;
        }

        [Test]
        [TestCase("[1,3,-1,-3,5,3,6,7]", 3, ExpectedResult = "[3,3,5,5,6,7]")]
        [TestCase("[1,-1]", 1, ExpectedResult = "[1,-1]")]
        [TestCase("[1]", 1, ExpectedResult = "[1]")]
        [TestCase("[-1]", 1, ExpectedResult = "[-1]")]
        [TestCase("[10,9,8,7,6,5,4,3,2,1]", 3, ExpectedResult = "[10,9,8,7,6,5,4,3]")]
        [TestCase("[1,2,3,4,5,6,7,8,9,10]", 3, ExpectedResult = "[3,4,5,6,7,8,9,10]")]
        public String Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            Int32[] result = this.MaxSlidingWindow(nums, k);
            return JsonConvert.SerializeObject(result);
        }

    }

}
