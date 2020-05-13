using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     724. Find Pivot Index
    ///     https://leetcode.com/problems/find-pivot-index/
    /// </summary>
    [TestFixture]
    public class P00724 {

        [PublicAPI]
        public Int32 PivotIndex(Int32[] nums) {
            if (nums == null) return -1;

            Int32 length = nums.Length;
            if (length == 0) return -1;
            if (length == 1) return 1;

            Int32 totalSum = 0;
            for (Int32 index = 0; index < length; ++index) {
                totalSum += nums[index];
            }

            Int32 prefixSum = 0;
            for (Int32 index = 0; index < length; ++index) {
                if (prefixSum == totalSum - prefixSum - nums[index]) return index;

                prefixSum += nums[index];
            }

            return -1;
        }

        [Test]
        [TestCase("[1,7,3,6,5,6]", ExpectedResult = 3)]
        [TestCase("[1,2,3]", ExpectedResult = -1)]
        [TestCase("[-1,-1,-1,0,1,1]", ExpectedResult = 0)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.PivotIndex(nums);
        }

    }

}
