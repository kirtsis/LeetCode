using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     540. Single Element in a Sorted Array
    ///     https://leetcode.com/problems/single-element-in-a-sorted-array/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00540 {

        [PublicAPI]
        public Int32 SingleNonDuplicate(Int32[] nums) {
            Int32 length = nums.Length;
            if (length == 1) return nums[0];

            //return this._loop(nums, length);
            return this._binarySearch(nums, length);
        }

        private Int32 _loop(Int32[] nums, Int32 length) {
            Int32 prev = nums[0];
            for (Int32 index = 1; index < length; index += 2) {
                if (nums[index] != prev) return prev;

                prev = nums[index + 1];
            }

            return 0;
        }

        private Int32 _binarySearch(Int32[] nums, Int32 length) {
            Int32 left = 0;
            Int32 right = length - 1;
            while (left < right) {
                Int32 mid = left + (right - left) / 2;
                if (mid % 2 == 1) mid--;
                if (nums[mid] == nums[mid + 1]) left = mid + 2;
                else right = mid;
            }

            return nums[left];
        }

        [Test]
        [TestCase("[1,1,2,3,3,4,4,8,8]", ExpectedResult = 2)]
        [TestCase("[3,3,7,7,10,11,11]", ExpectedResult = 10)]
        [TestCase("[1,2,2,3,3,4,4,5,5]", ExpectedResult = 1)]
        [TestCase("[1,1,2,3,3,4,4,5,5]", ExpectedResult = 2)]
        [TestCase("[1,1,2,2,3,4,4,5,5]", ExpectedResult = 3)]
        [TestCase("[1,1,2,2,3,3,4,5,5]", ExpectedResult = 4)]
        [TestCase("[1,1,2,2,3,3,4,4,5]", ExpectedResult = 5)]
        [TestCase("[1]", ExpectedResult = 1)]
        [TestCase("[1,2,2]", ExpectedResult = 1)]
        [TestCase("[1,1,2]", ExpectedResult = 2)]
        [TestCase("[1,2,2,3,3]", ExpectedResult = 1)]
        [TestCase("[1,1,2,3,3]", ExpectedResult = 2)]
        [TestCase("[1,1,2,2,3]", ExpectedResult = 3)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.SingleNonDuplicate(nums);
        }

    }

}
