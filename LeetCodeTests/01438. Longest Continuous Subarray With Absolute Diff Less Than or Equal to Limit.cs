using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1438. Longest Continuous Subarray With Absolute Diff Less Than or Equal to Limit
    ///     https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01438 {

        [PublicAPI]
        public Int32 LongestSubarray(Int32[] nums, Int32 limit) {
            // Constraints:
            // * 1 <= nums.length <= 10^5
            // * 1 <= nums[i] <= 10^9
            // * 0 <= limit <= 10^9

            //return this._loops(nums, limit);
            return this._linkedList(nums, limit);
        }

        private Int32 _loops(Int32[] nums, Int32 limit) {
            Int32 result = 0;
            Int32 length = nums.Length;
            for (Int32 start = 0; start < length; ++start) {
                for (Int32 end = start; end < length; ++end) {
                    Int32 min = Int32.MaxValue;
                    Int32 max = Int32.MinValue;
                    for (Int32 index = start; index <= end; ++index) {
                        min = Math.Min(min, nums[index]);
                        max = Math.Max(max, nums[index]);
                    }

                    if (max - min <= limit) result = Math.Max(result, end - start + 1);
                }
            }

            return result;
        }

        private Int32 _linkedList(Int32[] nums, Int32 limit) {
            Int32 result = 0;
            Int32 start = 0;
            Int32 end = start;
            Int32 length = nums.Length;
            var minIndexes = new LinkedList<Int32>();
            var maxIndexes = new LinkedList<Int32>();
            while (end < length) {
                // remove indexes from the back, that points to elements bigger than current element
                while ((minIndexes.Count > 0) && (nums[minIndexes.Last.Value] >= nums[end])) {
                    minIndexes.RemoveLast();
                }

                minIndexes.AddLast(end);

                // remove indexes from the back, that points to elements smaller than current element
                while ((maxIndexes.Count > 0) && (nums[maxIndexes.Last.Value] <= nums[end])) {
                    maxIndexes.RemoveLast();
                }

                maxIndexes.AddLast(end);

                Int32 min = nums[minIndexes.First.Value];
                Int32 max = nums[maxIndexes.First.Value];
                if (max - min <= limit) {
                    result = Math.Max(result, end - start + 1);
                    end++;
                } else {
                    start++;
                    if (start > minIndexes.First.Value) minIndexes.RemoveFirst();
                    if (start > maxIndexes.First.Value) maxIndexes.RemoveFirst();
                }
            }

            return result;
        }

        [Test]
        [TestCase("[8,2,4,7]", 4, ExpectedResult = 2)]
        [TestCase("[10,1,2,4,7,2]", 5, ExpectedResult = 4)]
        [TestCase("[4,2,2,2,4,4,2,2]", 0, ExpectedResult = 3)]
        public Int32 Test(String input, Int32 limit) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.LongestSubarray(nums, limit);
        }

    }

}
