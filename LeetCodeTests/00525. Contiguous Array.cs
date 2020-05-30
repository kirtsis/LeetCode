using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     525. Contiguous Array
    ///     https://leetcode.com/problems/contiguous-array/
    /// </summary>
    [TestFixture]
    public class P00525 {

        [PublicAPI]
        public Int32 FindMaxLength(Int32[] nums) {
            if (nums == null) return 0;

            Int32 length = nums.Length;
            if (length == 0) return 0;

            Int32 result = 0;

            var firstIndexOfCounter = new Dictionary<Int32, Int32>();

            Int32 counter = 0;
            firstIndexOfCounter.Add(counter, -1);
            for (Int32 index = 0; index < length; ++index) {
                counter += nums[index] == 0 ? -1 : 1;
                if (firstIndexOfCounter.ContainsKey(counter)) result = Math.Max(result, index - firstIndexOfCounter[counter]);
                else firstIndexOfCounter.Add(counter, index);
            }

            return result;
        }

        [Test]
        [TestCase("[0,1]", ExpectedResult = 2)]
        [TestCase("[0,1,0]", ExpectedResult = 2)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.FindMaxLength(nums);
        }

    }

}
