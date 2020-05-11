using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     189. Rotate Array
    ///     https://leetcode.com/problems/rotate-array/
    /// </summary>
    [TestFixture]
    public class P00189 {

        [PublicAPI]
        public void Rotate(Int32[] nums, Int32 k) {
            // Constraints:
            // * 1 <= nums.length <= 2 * 10^4
            // * It's guaranteed that nums[i] fits in a 32 bit-signed integer.
            // * k >= 0

            this._rotate1(nums, k);
        }

        private void _rotate1(Int32[] nums, Int32 k) {
            Int32 length = nums.Length;
            k %= length;
            if (k == 0) return;

            // [1,2,3,4,5,6,7]
            Int32 start = 0;
            Int32 end = length - 1;
            while (start < end) {
                Int32 temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }

            // [7,6,5,4,3,2,1]

            start = 0;
            end = k - 1;
            while (start < end) {
                Int32 temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }

            // [5,6,7,4,3,2,1]

            start = k;
            end = length - 1;
            while (start < end) {
                Int32 temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }

            // [5,6,7,1,2,3,4]
        }

        [Test]
        [TestCase("[1,2,3,4,5,6,7]", 3, ExpectedResult = "[5,6,7,1,2,3,4]")]
        [TestCase("[-1,-100,3,99]", 2, ExpectedResult = "[3,99,-1,-100]")]
        [TestCase("[1,2,3]", 2, ExpectedResult = "[2,3,1]")]
        [TestCase("[1,2,3,4,5,6]", 1, ExpectedResult = "[6,1,2,3,4,5]")]
        public String Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            this.Rotate(nums, k);
            return JsonConvert.SerializeObject(nums);
        }

    }

}
