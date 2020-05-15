using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     918. Maximum Sum Circular Subarray
    ///     https://leetcode.com/problems/maximum-sum-circular-subarray/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00918 {

        [PublicAPI]
        public Int32 MaxSubarraySumCircular(Int32[] A) {
            // Note:
            // * -30000 <= A[i] <= 30000
            // * 1 <= A.length <= 30000

            Int32 length = A.Length;

            //return this._max1(A, length);
            return this._max2(A, length);
        }

        private Int32 _max1(Int32[] nums, Int32 length) {
            Int32 maxInMiddle = this._kadane(nums, length);
            Int32 maxSplitAcross = nums.Sum() + this._kadane(nums, length, -1);
            return maxSplitAcross == 0 ? maxInMiddle : Math.Max(maxInMiddle, maxSplitAcross);
        }

        private Int32 _kadane(Int32[] nums, Int32 length, Int32 sign = 1) {
            Int32 currentSum = 0;
            Int32 bestSum = -30000;
            for (Int32 index = 0; index < length; ++index) {
                currentSum = sign * nums[index] + Math.Max(0, currentSum);
                bestSum = Math.Max(bestSum, currentSum);
            }

            return bestSum;
        }

        private Int32 _max2(Int32[] nums, Int32 length) {
            Int32 total = 0;
            Int32 curMax = 0, maxSum = -30000;
            Int32 curMin = 0, minSum = 30000;
            for (Int32 index = 0; index < length; ++index) {
                Int32 num = nums[index];
                total += num;
                curMax = num + Math.Max(0, curMax);
                maxSum = Math.Max(maxSum, curMax);
                curMin = num + Math.Min(0, curMin);
                minSum = Math.Min(minSum, curMin);
            }

            return total == minSum ? maxSum : Math.Max(maxSum, total - minSum);
        }

        [Test]
        [TestCase("[1,-2,3,-2]", ExpectedResult = 3)]
        [TestCase("[5,-3,5]", ExpectedResult = 10)]
        [TestCase("[3,-1,2,-1]", ExpectedResult = 4)]
        [TestCase("[3,-2,2,-3]", ExpectedResult = 3)]
        [TestCase("[-2,-3,-1]", ExpectedResult = -1)]
        public Int32 Test(String input) {
            var A = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.MaxSubarraySumCircular(A);
        }

    }

}
