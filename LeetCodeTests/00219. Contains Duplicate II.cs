using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     219. Contains Duplicate II
    ///     https://leetcode.com/problems/contains-duplicate-ii/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00219 {

        [PublicAPI]
        public Boolean ContainsNearbyDuplicate(Int32[] nums, Int32 k) {
            if (nums == null) return false;
            if (k <= 0) return false;

            Int32 length = nums.Length;
            if (length == 0) return false;

            //return this._check1(nums, length, k);
            return this._check2(nums, length, k);
        }

        private Boolean _check1(Int32[] nums, Int32 length, Int32 k) {
            var lastSeenIndex = new Dictionary<Int32, Int32>();
            for (Int32 index = 0; index < length; ++index) {
                if (lastSeenIndex.ContainsKey(nums[index]) && (index - lastSeenIndex[nums[index]] <= k)) return true;

                lastSeenIndex[nums[index]] = index;
            }

            return false;
        }

        private Boolean _check2(Int32[] nums, Int32 length, Int32 k) {
            var lastKElements = new HashSet<Int32>();
            for (Int32 index = 0; index < length; ++index) {
                if (lastKElements.Contains(nums[index])) return true;

                lastKElements.Add(nums[index]);
                if (index - k >= 0) lastKElements.Remove(nums[index - k]);
            }

            return false;
        }

        [Test]
        [TestCase("[1,2,3,1]", 3, ExpectedResult = true)]
        [TestCase("[1,0,1,1]", 1, ExpectedResult = true)]
        [TestCase("[1,2,3,1,2,3]", 2, ExpectedResult = false)]
        public Boolean Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.ContainsNearbyDuplicate(nums, k);
        }

    }

}
