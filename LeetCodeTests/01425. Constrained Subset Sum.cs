using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1425. Constrained Subset Sum
    ///     https://leetcode.com/problems/constrained-subset-sum/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01425 {

        [PublicAPI]
        public Int32 ConstrainedSubsetSum(Int32[] nums, Int32 k) {
            //return this._loops(nums, k); // Time Limit Exceeded
            return this._linkedList(nums, k);
        }

        private Int32 _loops(Int32[] nums, Int32 k) {
            Int32 length = nums.Length;

            var dp = new Int32[length];

            Int32 result = Int32.MinValue;
            for (Int32 index = 0; index < length; index++) {
                Int32 max = 0;
                for (Int32 stepBack = 1; (stepBack <= k) && (stepBack <= index); stepBack++) {
                    max = Math.Max(max, dp[index - stepBack]);
                }

                dp[index] = max + nums[index];

                result = Math.Max(result, dp[index]);
            }

            return result;
        }

        private Int32 _linkedList(Int32[] nums, Int32 k) {
            Int32 length = nums.Length;

            var dp = new Int32[length];
            var indexes = new LinkedList<Int32>();

            Int32 result = Int32.MinValue;
            for (Int32 index = 0; index < length; index++) {
                // ???
                if ((index > k) && (indexes.First.Value == index - k - 1)) indexes.RemoveFirst();

                // update dp
                dp[index] = nums[index] + (indexes.Count > 0 ? Math.Max(0, dp[indexes.First.Value]) : 0);

                // remove indexes from the back, that points to dp's smaller than current dp
                while ((indexes.Count > 0) && (dp[indexes.Last.Value] <= dp[index])) {
                    indexes.RemoveLast();
                }

                // // save current index at the back
                indexes.AddLast(index);

                // update result
                result = Math.Max(result, dp[index]);
            }

            return result;
        }

        [Test]
        [TestCase("[10,2,-10,5,20]", 2, ExpectedResult = 37)]
        [TestCase("[-1,-2,-3]", 1, ExpectedResult = -1)]
        [TestCase("[10,-2,-10,-5,20]", 2, ExpectedResult = 23)]
        public Int32 Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.ConstrainedSubsetSum(nums, k);
        }

    }

}
