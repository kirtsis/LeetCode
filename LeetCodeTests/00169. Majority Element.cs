using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     169. Majority Element
    ///     https://leetcode.com/problems/majority-element/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00169 {

        [PublicAPI]
        public Int32 MajorityElement(Int32[] nums) {
            //return this._hashMah(nums);
            return this._boyerMooreVotingAlgorithm(nums);
        }

        private Int32 _hashMah(Int32[] nums) {
            Int32 limit = nums.Length / 2;
            var appearances = new Dictionary<Int32, Int32>();
            foreach (Int32 num in nums) {
                if (!appearances.ContainsKey(num)) appearances.Add(num, 0);
                appearances[num]++;
                if (appearances[num] > limit) return num;
            }

            return -1;
        }

        private Int32 _boyerMooreVotingAlgorithm(Int32[] nums) {
            Int32 count = 0;
            Int32 candidate = -1;
            foreach (Int32 num in nums) {
                if (count == 0) candidate = num;
                count += num == candidate ? 1 : -1;
            }

            return candidate;
        }

        [Test]
        [TestCase("[3,2,3]", ExpectedResult = 3)]
        [TestCase("[2,2,1,1,1,2,2]", ExpectedResult = 2)]
        public Int32 Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.MajorityElement(nums);
        }

    }

}
