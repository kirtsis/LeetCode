using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     209. Minimum Size Subarray Sum
    ///     https://leetcode.com/problems/minimum-size-subarray-sum/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00209 {

        [PublicAPI]
        public Int32 MinSubArrayLen(Int32 s, Int32[] nums) {
            //return this._twoPointers(s, nums);
            return this._prefixes(s, nums);
        }

        private Int32 _twoPointers(Int32 s, Int32[] nums) {
            Int32 length = nums.Length;
            Int32 left = 0;
            Int32 right = 0;
            Int32 sum = 0;
            Int32 result = Int32.MaxValue;
            while (right < length) {
                sum += nums[right];
                right++;
                while (sum >= s) {
                    result = Math.Min(result, right - left);
                    sum -= nums[left];
                    left++;
                }
            }

            return result == Int32.MaxValue ? 0 : result;
        }

        private Int32 _prefixes(Int32 s, Int32[] nums) {
            Int32 length = nums.Length;

            var prefixSum = new Int32[length + 1];
            for (Int32 index = 1; index <= length; ++index) {
                prefixSum[index] = prefixSum[index - 1] + nums[index - 1];
            }

            Int32 result = Int32.MaxValue;
            for (Int32 start = 0; start <= length; ++start) {
                Int32 left = start + 1;
                Int32 right = length;
                while (left <= right) {
                    Int32 mid = left + (right - left) / 2;
                    if (prefixSum[mid] - prefixSum[start] < s) left = mid + 1;
                    else right = mid - 1;
                }

                if (left == length + 1) break;

                result = Math.Min(result, left - start);
            }

            return result == Int32.MaxValue ? 0 : result;
        }

        [Test]
        [TestCase(7, "[2,3,1,2,4,3]", ExpectedResult = 2)]
        [TestCase(2, "[1,2,3,4]", ExpectedResult = 1)]
        [TestCase(11, "[1,2,3,4]", ExpectedResult = 0)]
        [TestCase(2, "[4,3,2,1]", ExpectedResult = 1)]
        [TestCase(11, "[4,3,2,1]", ExpectedResult = 0)]
        public Int32 Test(Int32 s, String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.MinSubArrayLen(s, nums);
        }

    }

}
