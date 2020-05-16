using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1446. Consecutive Characters
    ///     https://leetcode.com/problems/consecutive-characters/
    /// </summary>
    [TestFixture]
    public class P01446 {

        [PublicAPI]
        public Int32 MaxPower(String s) {
            // Constraints:
            // * 1 <= s.length <= 500
            // * s contains only lowercase English letters.

            Int32 length = s.Length;
            if (length <= 1) return 1;

            Int32 result = 1;
            Int32 counter = 1;
            for (Int32 index = 1; index < length; ++index) {
                if (s[index] == s[index - 1]) counter++;
                else counter = 1;
                result = Math.Max(result, counter);
            }

            return result;
        }

        [Test]
        [TestCase("leetcode", ExpectedResult = 2)]
        [TestCase("abbcccddddeeeeedcba", ExpectedResult = 5)]
        [TestCase("triplepillooooow", ExpectedResult = 5)]
        [TestCase("hooraaaaaaaaaaay", ExpectedResult = 11)]
        [TestCase("tourist", ExpectedResult = 1)]
        [TestCase("j", ExpectedResult = 1)]
        [TestCase("cc", ExpectedResult = 2)]
        public Int32 Test(String s) {
            return this.MaxPower(s);
        }

    }

}
