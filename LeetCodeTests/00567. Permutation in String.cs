using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     567. Permutation in String
    ///     https://leetcode.com/problems/permutation-in-string/
    /// </summary>
    [TestFixture]
    public class P00567 {

        [PublicAPI]
        public Boolean CheckInclusion(String s1, String s2) {
            // Note:
            // * The input strings only contain lower case letters.
            // * The length of both given strings is in range [1, 10,000].

            Int32 patternLength = s1.Length;
            Int32 length = s2.Length;

            var windowAppearances = new Int32[26];
            var patternAppearances = new Int32[26];
            for (Int32 index = 0; index < length; ++index) {
                windowAppearances[s2[index] - 'a']++;
                if (index < patternLength) patternAppearances[s1[index] - 'a']++;

                if (index - patternLength + 1 < 0) continue;

                if (index - patternLength >= 0) windowAppearances[s2[index - patternLength] - 'a']--;

                Boolean areDifferent = false;
                for (Int32 i = 0; i < 26; ++i) {
                    if (windowAppearances[i] == patternAppearances[i]) continue;

                    areDifferent = true;
                    break;
                }

                if (areDifferent) continue;

                return true;
            }

            return false;
        }

        [Test]
        [TestCase("ab", "eidbaooo", ExpectedResult = true)]
        [TestCase("ab", "eidboaoo", ExpectedResult = false)]
        public Boolean Test(String s1, String s2) {
            return this.CheckInclusion(s1, s2);
        }

    }

}
