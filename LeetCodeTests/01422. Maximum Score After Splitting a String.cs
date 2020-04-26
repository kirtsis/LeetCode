using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1422. Maximum Score After Splitting a String
    ///     https://leetcode.com/problems/maximum-score-after-splitting-a-string/
    /// </summary>
    [TestFixture]
    public class P01422 {

        [PublicAPI]
        public Int32 MaxScore(String s) {
            Int32 length = s.Length;

            var prefixSum = new Int32[length];
            prefixSum[0] = s[0] - 48;
            for (Int32 index = 1; index < s.Length; index++) {
                prefixSum[index] = prefixSum[index - 1] + s[index] - 48;
            }

            Int32 result = 0;
            Int32 zeroCounter = 0;
            for (Int32 index = 0; index < s.Length - 1; index++) {
                if (s[index] == '0') zeroCounter++;
                result = Math.Max(result, zeroCounter + prefixSum[length - 1] - prefixSum[index]);
            }

            return result;
        }

        [Test]
        [TestCase("011101", ExpectedResult = 5)]
        [TestCase("00111", ExpectedResult = 5)]
        [TestCase("1111", ExpectedResult = 3)]
        public Int32 Test(String input) {
            return this.MaxScore(input);
        }

    }

}
