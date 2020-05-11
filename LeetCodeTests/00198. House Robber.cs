using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     198. House Robber
    ///     https://leetcode.com/problems/house-robber/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00198 {

#pragma warning disable 649
        private Int32?[] _memory;
#pragma warning restore 649

        [PublicAPI]
        public Int32 Rob(Int32[] nums) {
            if (nums == null) return 0;

            Int32 length = nums.Length;
            if (length == 0) return 0;

            //return this._recursiveForwards(nums, length, 0); // Time Limit Exceeded
            //return this._recursiveBackwards(nums, length - 1); // Time Limit Exceeded

            //this._memory = new Int32?[length];
            //return this._recursiveDpForwards(nums, length, 0);
            //return this._recursiveDpBackwards(nums, length - 1);

            //return this._dpForwards(nums, length);
            //return this._dpBackwards(nums, length);

            //return this._variablesForwards(nums, length);
            return this._variablesBackwards(nums, length);
        }

        private Int32 _recursiveForwards(Int32[] nums, Int32 length, Int32 index) {
            if (index >= length) return 0;

            Int32 robThis = nums[index] + this._recursiveForwards(nums, length, index + 2);
            Int32 robNext = this._recursiveForwards(nums, length, index + 1);
            return Math.Max(robThis, robNext);
        }

        private Int32 _recursiveBackwards(Int32[] nums, Int32 index) {
            if (index < 0) return 0;

            Int32 robThis = nums[index] + this._recursiveBackwards(nums, index - 2);
            Int32 robPrevious = this._recursiveBackwards(nums, index - 1);
            return Math.Max(robThis, robPrevious);
        }

        private Int32 _recursiveDpForwards(Int32[] nums, Int32 length, Int32 index) {
            if (index >= length) return 0;
            if (this._memory[index] != null) return this._memory[index].Value;

            Int32 robThis = nums[index] + this._recursiveDpForwards(nums, length, index + 2);
            Int32 robNext = this._recursiveDpForwards(nums, length, index + 1);
            Int32 result = Math.Max(robThis, robNext);
            this._memory[index] = result;
            return result;
        }

        private Int32 _recursiveDpBackwards(Int32[] nums, Int32 index) {
            if (index < 0) return 0;
            if (this._memory[index] != null) return this._memory[index].Value;

            Int32 robThis = nums[index] + this._recursiveDpBackwards(nums, index - 2);
            Int32 robPrevious = this._recursiveDpBackwards(nums, index - 1);
            Int32 result = Math.Max(robThis, robPrevious);
            this._memory[index] = result;
            return result;
        }

        private Int32 _dpForwards(Int32[] nums, Int32 length) {
            var dp = new Int32[length + 1];
            dp[0] = 0;
            dp[1] = nums[0];
            for (Int32 index = 1; index < length; ++index) {
                dp[index + 1] = Math.Max(dp[index], dp[index - 1] + nums[index]);
            }

            return dp[length];
        }

        private Int32 _dpBackwards(Int32[] nums, Int32 length) {
            var dp = new Int32[length + 1];
            dp[length] = 0;
            dp[length - 1] = nums[length - 1];
            for (Int32 index = length - 1; index > 0; --index) {
                dp[index - 1] = Math.Max(dp[index], dp[index + 1] + nums[index - 1]);
            }

            return dp[0];
        }

        private Int32 _variablesForwards(Int32[] nums, Int32 length) {
            // previous1, previous2, nums[index]
            Int32 previous1 = 0;
            Int32 previous2 = 0;
            for (Int32 index = 0; index < length; ++index) {
                Int32 temp = previous1;
                previous1 = Math.Max(previous2 + nums[index], previous1);
                previous2 = temp;
            }

            return previous1;
        }

        private Int32 _variablesBackwards(Int32[] nums, Int32 length) {
            // nums[index], next1, next2
            Int32 next2 = 0;
            Int32 next1 = 0;
            for (Int32 index = length - 1; index >= 0; --index) {
                Int32 temp = next2;
                next2 = Math.Max(next2, next1 + nums[index]);
                next1 = temp;
            }

            return next2;
        }

        [Test]
        [TestCase("[1,2,3,1]", ExpectedResult = 4)]
        [TestCase("[2,7,9,3,1]", ExpectedResult = 12)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.Rob(nums);
        }

    }

}
