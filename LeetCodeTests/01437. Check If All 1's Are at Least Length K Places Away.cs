using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1437. Check If All 1's Are at Least Length K Places Away
    ///     https://leetcode.com/problems/check-if-all-1s-are-at-least-length-k-places-away/
    /// </summary>
    [TestFixture]
    public class P01437 {

        [PublicAPI]
        public Boolean KLengthApart(Int32[] nums, Int32 k) {
            // Constraints:
            // * 1 <= nums.length <= 10^5
            // * 0 <= k <= nums.length
            // * nums[i] is 0 or 1

            Int32? lastOneIndex = null;
            for (Int32 index = 0; index < nums.Length; index++) {
                if (nums[index] == 0) continue;

                if ((lastOneIndex != null) && (index - lastOneIndex <= k)) return false;

                lastOneIndex = index;
            }

            return true;
        }

        [Test]
        [TestCase("[1,0,0,0,1,0,0,1]", 2, ExpectedResult = true)]
        [TestCase("[1,0,0,1,0,1]", 2, ExpectedResult = false)]
        [TestCase("[1,1,1,1,1]", 0, ExpectedResult = true)]
        [TestCase("[0,1,0,1]", 1, ExpectedResult = true)]
        public Boolean Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.KLengthApart(nums, k);
        }

    }

}
