using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     977. Squares of a Sorted Array
    ///     https://leetcode.com/problems/squares-of-a-sorted-array/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00977 {

        [PublicAPI]
        public Int32[] SortedSquares(Int32[] nums) {
            //return this._sort(nums);
            //return this._twoPointer(nums);
            //return this._inPlaceSort(nums);
            return this._inPlace(nums);
        }

        private Int32[] _sort(Int32[] nums) {
            Int32 length = nums.Length;
            var result = new Int32[length];
            for (Int32 i = 0; i < length; i++) {
                result[i] = nums[i] * nums[i];
            }

            Array.Sort(result);
            return result;
        }

        private Int32[] _twoPointer(Int32[] nums) {
            Int32 length = nums.Length;
            var result = new Int32[length];
            Int32 left = 0;
            Int32 right = length - 1;
            for (Int32 i = length - 1; i >= 0; i--) {
                Int32 leftA = nums[left];
                Int32 rightA = nums[right];
                if (Math.Abs(leftA) > Math.Abs(rightA)) {
                    result[i] = leftA * leftA;
                    left++;
                } else {
                    result[i] = rightA * rightA;
                    right--;
                }
            }

            return result;
        }

        private Int32[] _inPlaceSort(Int32[] nums) {
            Int32 length = nums.Length;
            for (Int32 i = 0; i < length; i++) {
                nums[i] = nums[i] * nums[i];
            }

            Array.Sort(nums);
            return nums;
        }

        private Int32[] _inPlace(Int32[] nums) {
            Int32 indexOfTheLargerNumber = 0;
            Int32 target = nums.Length - 1;
            while (target >= 0) {
                // swap large number to the end
                while ((target >= indexOfTheLargerNumber) && (Math.Abs(nums[indexOfTheLargerNumber]) >= Math.Abs(nums[target]))) {
                    Int32 tmp = nums[target];
                    nums[target] = nums[indexOfTheLargerNumber];
                    nums[indexOfTheLargerNumber] = tmp;
                    indexOfTheLargerNumber++;
                }

                nums[target] = nums[target] * nums[target];
                target--;
                indexOfTheLargerNumber = 0; // reset to zero because after swap, the first number might be not smallest.

                // get index of front larger number
                while ((target >= indexOfTheLargerNumber) && (Math.Abs(nums[indexOfTheLargerNumber]) <= Math.Abs(nums[target]))) {
                    indexOfTheLargerNumber++;
                }
            }

            return nums;
        }

        [Test]
        [TestCase("[-4,-1,0,3,10]", ExpectedResult = "[0,1,9,16,100]")]
        [TestCase("[-7,-3,2,3,11]", ExpectedResult = "[4,9,9,49,121]")]
        [TestCase("[-2,0]", ExpectedResult = "[0,4]")]
        [TestCase("[-3,-3,-2,1]", ExpectedResult = "[1,4,9,9]")]
        public String Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            Int32[] result = this.SortedSquares(nums);
            return JsonConvert.SerializeObject(result);
        }

    }

}
