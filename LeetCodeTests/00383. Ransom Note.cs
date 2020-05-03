using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     383. Ransom Note
    ///     https://leetcode.com/problems/ransom-note/
    /// </summary>
    [TestFixture]
    public class P00383 {

        [PublicAPI]
        public Boolean CanConstruct(String ransomNote, String magazine) {
            // 26 lowercase letters in the English alphabet
            var available = new Int32[26];
            foreach (Char letter in magazine) {
                available[letter - 'a']++;
            }

            foreach (Char letter in ransomNote) {
                available[letter - 'a']--;
                if (available[letter - 'a'] < 0) return false;
            }

            return true;
        }

        [Test]
        [TestCase("a", "b", ExpectedResult = false)]
        [TestCase("aa", "ab", ExpectedResult = false)]
        [TestCase("aa", "aab", ExpectedResult = true)]
        public Boolean Test(String ransomNote, String magazine) {
            return this.CanConstruct(ransomNote, magazine);
        }

    }

}
