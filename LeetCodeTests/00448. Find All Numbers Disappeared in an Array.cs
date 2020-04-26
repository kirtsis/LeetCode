using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     448. Find All Numbers Disappeared in an Array
    ///     https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/
    /// </summary>
    [TestFixture]
    public class P00448 {

        [PublicAPI]
        public IList<Int32> FindDisappearedNumbers(Int32[] nums) {
            foreach (Int32 num in nums) {
                Int32 targetIndex = num > 0 ? num - 1 : -num - 1;
                if (nums[targetIndex] > 0) nums[targetIndex] *= -1;
            }

            var result = new List<Int32>();
            for (Int32 index = 0; index < nums.Length; index++) {
                if (nums[index] > 0) result.Add(index + 1);
            }

            return result;
        }

        [Test]
        [TestCase("[4,3,2,7,8,2,3,1]", ExpectedResult = "[5,6]")]
        public String Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            IList<Int32> result = this.FindDisappearedNumbers(nums);
            return JsonConvert.SerializeObject(result);
        }

    }

}
