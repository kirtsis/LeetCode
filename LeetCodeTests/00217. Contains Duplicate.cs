using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     217. Contains Duplicate
    ///     https://leetcode.com/problems/contains-duplicate/
    /// </summary>
    [TestFixture]
    public class P00217 {

        [PublicAPI]
        public Boolean ContainsDuplicate(Int32[] nums) {
            return new HashSet<Int32>(nums).Count != nums.Length;
        }

        [Test]
        [TestCase("[1,2,3,1]", ExpectedResult = true)]
        [TestCase("[1,2,3,4]", ExpectedResult = false)]
        [TestCase("[1,1,1,3,3,4,3,2,4,2]", ExpectedResult = true)]
        public Boolean Test(String input) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.ContainsDuplicate(nums);
        }

    }

}
