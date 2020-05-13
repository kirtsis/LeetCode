using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     747. Largest Number At Least Twice of Others
    ///     https://leetcode.com/problems/largest-number-at-least-twice-of-others/
    /// </summary>
    [TestFixture]
    public class P00747 {

        [PublicAPI]
        public Int32 DominantIndex(Int32[] nums) {
            // Note:
            // * nums will have a length in the range [1, 50].
            // * Every nums[i] will be an integer in the range [0, 99].

            if (nums == null) return -1;

            Int32 length = nums.Length;
            if (length == 0) return -1;
            if (length == 1) return 0;

            Int32 max1 = nums[0];
            Int32 max1Index = 0;
            Int32 max2 = Int32.MinValue;
            for (Int32 index = 1; index < length; ++index) {
                Int32 num = nums[index];
                if ((num == max1) || (num == max2)) continue;

                if (num > max1) {
                    max2 = max1;
                    max1 = num;
                    max1Index = index;
                    continue;
                }

                max2 = Math.Max(max2, num);
            }

            return max1 >= 2 * max2 ? max1Index : -1;
        }

        [Test]
        [TestCase("[3,6,1,0]", ExpectedResult = 1)]
        [TestCase("[1,2,3,4]", ExpectedResult = -1)]
        [TestCase("[3,6]", ExpectedResult = 1)]
        [TestCase("[6,3]", ExpectedResult = 0)]
        [TestCase("[0,0,0,1]", ExpectedResult = 3)]
        [TestCase("[1,0,0,0]", ExpectedResult = 0)]
        [TestCase("[1]", ExpectedResult = 0)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.DominantIndex(nums);
        }

    }

}
