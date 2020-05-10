using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1441. Build an Array With Stack Operations
    ///     https://leetcode.com/problems/build-an-array-with-stack-operations/
    /// </summary>
    [TestFixture]
    public class P01441 {

        [PublicAPI]
        public IList<String> BuildArray(Int32[] target, Int32 n) {
            // Constraints:
            // * 1 <= target.length <= 100
            // * 1 <= target[i] <= 100
            // * 1 <= n <= 100
            // * target is strictly increasing.

            return this._build1(target, n);
        }

        private IList<String> _build1(Int32[] target, Int32 n) {
            var result = new List<String>();

            Int32 index = 0;
            Int32 length = target.Length;
            for (Int32 candidate = 1; candidate <= n; ++candidate) {
                if (candidate == target[index]) {
                    result.Add("Push");
                    index++;
                    if (index == length) break;
                } else {
                    result.Add("Push");
                    result.Add("Pop");
                }
            }

            return result;
        }

        [Test]
        [TestCase("[1,3]", 3, ExpectedResult = "[\"Push\",\"Push\",\"Pop\",\"Push\"]")]
        [TestCase("[1,2,3]", 3, ExpectedResult = "[\"Push\",\"Push\",\"Push\"]")]
        [TestCase("[1,2]", 4, ExpectedResult = "[\"Push\",\"Push\"]")]
        [TestCase("[2,3,4]", 4, ExpectedResult = "[\"Push\",\"Pop\",\"Push\",\"Push\",\"Push\"]")]
        public String Test(String input, Int32 n) {
            var target = JsonConvert.DeserializeObject<Int32[]>(input);
            IList<String> result = this.BuildArray(target, n);
            return JsonConvert.SerializeObject(result);
        }

    }

}
