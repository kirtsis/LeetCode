using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     414. Third Maximum Number
    ///     https://leetcode.com/problems/third-maximum-number/
    /// </summary>
    [TestFixture]
    public class P00414 {

        [PublicAPI]
        public Int32 ThirdMax(Int32[] nums) {
            Int32? max1 = null;
            Int32? max2 = null;
            Int32? max3 = null;
            foreach (Int32 num in nums) {
                if ((num == max1) || (num == max2) || (num == max3)) continue;

                if ((num > max1) || (max1 == null)) {
                    max3 = max2;
                    max2 = max1;
                    max1 = num;
                    continue;
                }

                if ((num > max2) || (max2 == null)) {
                    max3 = max2;
                    max2 = num;
                    continue;
                }

                if ((num > max3) || (max3 == null)) max3 = num;
            }

            return max3 ?? max1 ?? -1;
        }

        [Test]
        [TestCase("[3, 2, 1]", ExpectedResult = 1)]
        [TestCase("[1, 2]", ExpectedResult = 2)]
        [TestCase("[2, 2, 3, 1]", ExpectedResult = 1)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.ThirdMax(nums);
        }

    }

}
