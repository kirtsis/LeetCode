using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     438. Find All Anagrams in a String
    ///     https://leetcode.com/problems/find-all-anagrams-in-a-string/
    /// </summary>
    [TestFixture]
    public class P00438 {

        [PublicAPI]
        public IList<Int32> FindAnagrams(String s, String p) {
            var result = new List<Int32>();
            if (s == null) return result;

            Int32 length = s.Length;
            if (length == 0) return result;

            Int32 patternLength = p.Length; // pattern p cannot be null (the problem states: [...] a non-empty string p [...])

            var windowAppearances = new Int32[26];
            var patternAppearances = new Int32[26];
            for (Int32 index = 0; index < length; ++index) {
                // count letter in window
                windowAppearances[s[index] - 'a']++;

                // if index not is after the last letter of pattern (we haven't count them all already)
                if (index < patternLength) patternAppearances[p[index] - 'a']++; // count letter in pattern

                // if we don't already have a full window (we still building it)
                // (index - patternLength + 1) is the start index of the window
                if (index - patternLength + 1 < 0) continue; // continue with the next letter

                // if window is full and we just count a new letter
                // (index - patternLength) is one index before the window
                if (index - patternLength >= 0) windowAppearances[s[index - patternLength] - 'a']--; // remove oldest letter from window

                // check appearances
                Boolean areDifferent = false;
                for (Int32 i = 0; i < 26; ++i) {
                    if (windowAppearances[i] == patternAppearances[i]) continue;

                    areDifferent = true;
                    break;
                }

                // if appearances are different
                if (areDifferent) continue; // continue with the next letter

                // add the start index of the window to the results
                result.Add(index - patternLength + 1);
            }

            return result;
        }

        [Test]
        [TestCase("cbaebabacd", "abc", ExpectedResult = "[0,6]")]
        [TestCase("abab", "ab", ExpectedResult = "[0,1,2]")]
        public String Test(String s, String p) {
            IList<Int32> result = this.FindAnagrams(s, p);
            return JsonConvert.SerializeObject(result);
        }

    }

}
