using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     451. Sort Characters By Frequency
    ///     https://leetcode.com/problems/sort-characters-by-frequency/
    /// </summary>
    [TestFixture]
    public class P00451 {

        [PublicAPI]
        public String FrequencySort(String s) {
            if (s == null) return null;

            Int32 length = s.Length;
            if (length == 0) return String.Empty;

            var dictionary = new Dictionary<Char, Int32>();
            for (Int32 index = 0; index < length; ++index) {
                Char character = s[index];
                if (!dictionary.ContainsKey(character)) dictionary.Add(character, 0);
                dictionary[character]++;
            }

            var sb = new StringBuilder();
            foreach (KeyValuePair<Char, Int32> pair in dictionary.OrderByDescending(o => o.Value)) {
                sb.Append(pair.Key, pair.Value);
            }

            return sb.ToString();
        }

        [Test]
        [TestCase("tree", ExpectedResult = "eetr")]
        [TestCase("cccaaa", ExpectedResult = "cccaaa")]
        [TestCase("Aabb", ExpectedResult = "bbAa")]
        public String Test(String s) {
            return this.FrequencySort(s);
        }

    }

}
