using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     561. Array Partition I
    ///     https://leetcode.com/problems/array-partition-i/
    /// </summary>
    [TestFixture]
    public class P00561 {

        [PublicAPI]
        public Int32 ArrayPairSum(Int32[] nums) {
            // Note:
            // * n is a positive integer, which is in the range of [1, 10000].
            // * All the integers in the array will be in the range of [-10000, 10000].

            if (nums == null) throw new ArgumentNullException(nameof(nums));

            Int32 length = nums.Length;
            if (length % 2 != 0) throw new ArgumentException("nums.Length must be even.", nameof(nums));

            if (length == 0) return 0;

            Array.Sort(nums);

            Int32 result = 0;
            for (Int32 index = 0; index < length; index += 2) {
                result += nums[index];
            }

            return result;
        }

        [Test]
        [TestCase("[1,4,3,2]", ExpectedResult = 4)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.ArrayPairSum(nums);
        }

    }

}
