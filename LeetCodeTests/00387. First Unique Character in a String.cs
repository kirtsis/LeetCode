using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     387. First Unique Character in a String
    ///     https://leetcode.com/problems/first-unique-character-in-a-string/
    /// </summary>
    [TestFixture]
    public class P00387 {

        [PublicAPI]
        public Int32 FirstUniqChar(String s) {
            if (s == null) return -1;

            Int32 length = s.Length;
            if (length <= 0) return -1;

            var letters = new Int32[26];
            for (Int32 index = 0; index < length; ++index) {
                letters[s[index] - 'a']++;
            }

            for (Int32 index = 0; index < length; ++index) {
                if (letters[s[index] - 'a'] == 1) return index;
            }

            return -1;
        }

        [Test]
        [TestCase("leetcode", ExpectedResult = 0)]
        [TestCase("loveleetcode", ExpectedResult = 2)]
        public Int32 Test(String s) {
            return this.FirstUniqChar(s);
        }

    }

}
