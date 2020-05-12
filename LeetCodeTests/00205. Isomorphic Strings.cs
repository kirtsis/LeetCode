using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     205. Isomorphic Strings
    ///     https://leetcode.com/problems/isomorphic-strings/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00205 {

        [PublicAPI]
        public Boolean IsIsomorphic(String s, String t) {
            // Note:
            // * You may assume both s and t have the same length.

            if (s == null) return true;

            Int32 length = s.Length;
            if (length == 0) return true;

            var map = new Dictionary<Char, Char>();
            var mapped = new HashSet<Char>();
            for (Int32 index = 0; index < length; ++index) {
                if (map.ContainsKey(s[index]) && (map[s[index]] != t[index])) return false;
                if (!map.ContainsKey(s[index]) && mapped.Contains(t[index])) return false;

                if (map.ContainsKey(s[index])) continue;

                map.Add(s[index], t[index]);
                mapped.Add(t[index]);
            }

            return true;
        }

        [Test]
        [TestCase("egg", "add", ExpectedResult = true)]
        [TestCase("foo", "bar", ExpectedResult = false)]
        [TestCase("paper", "title", ExpectedResult = true)]
        [TestCase("ab", "aa", ExpectedResult = false)]
        public Boolean Test(String s, String t) {
            return this.IsIsomorphic(s, t);
        }

    }

}
