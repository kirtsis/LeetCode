using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1143. Longest Common Subsequence
    ///     https://leetcode.com/problems/longest-common-subsequence/
    /// </summary>
    [TestFixture]
    public class P01143 {

        [PublicAPI]
        public Int32 LongestCommonSubsequence(String text1, String text2) {
            //return this._recursive(text1, text2, text1.Length, text2.Length);
            //return this._dp(text1, text2);
            return this._dp2(text1, text2);
        }

        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Int32 _recursive(String text1, String text2, Int32 length1, Int32 length2) {
            // if text1 or text2 have zero length, then there can be no common subsequence (return 0)
            if ((length1 == 0) || (length2 == 0)) return 0;

            // if the last character of both is the same, then the length of the common subsequence is: one (for the common character) plus the the length of the common subsequence of the other characters (length - 1)
            if (text1[length1 - 1] == text2[length2 - 1]) return 1 + this._recursive(text1, text2, length1 - 1, length2 - 1);

            // otherwise, is the max common subsequence (length) between val1 and val2
            return Math.Max(
                val1: this._recursive(text1, text2, length1 - 1, length2), // val1 is the length of the common subsequence if we ignore the last character of text1
                val2: this._recursive(text1, text2, length1, length2 - 1) // val2 is the length of the common subsequence if we ignore the last character of text2
            );
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Int32 _dp(String text1, String text2) {
            Int32 length1 = text1.Length;
            Int32 length2 = text2.Length;

            // dp[x,y] is the length of the common subsequence of text1[0..x-1] and text2[0..y-1]
            // dp[x,y] is already 0 when x or y are zero, there can be no common subsequence
            // e.g. dp[0,y] is the length of the common subsequence of text1[0..-1] (no characters taken from text1, length is zero) and text2[0..y-1]
            var dp = new Int32[length1 + 1, length2 + 1];

            // starting from length one, up to the length of each string (bottom-up dynamic programming)
            for (Int32 length1ToCheck = 1; length1ToCheck <= length1; length1ToCheck++) {
                for (Int32 length2ToCheck = 1; length2ToCheck <= length2; length2ToCheck++) {
                    // if the last character of both is the same
                    if (text1[length1ToCheck - 1] == text2[length2ToCheck - 1]) {
                        // then the length of the common subsequence is: one (for the common character) plus the the length of the common subsequence of the other characters (length - 1)
                        dp[length1ToCheck, length2ToCheck] = 1 + dp[length1ToCheck - 1, length2ToCheck - 1];
                        continue;
                    }

                    // otherwise, is the max common subsequence (length) between val1 and val2
                    // where val1 is the length of the common subsequence if we ignore the last character of text1
                    // and val2 is the length of the common subsequence if we ignore the last character of text2
                    dp[length1ToCheck, length2ToCheck] = Math.Max(dp[length1ToCheck - 1, length2ToCheck], dp[length1ToCheck, length2ToCheck - 1]);
                }
            }

            return dp[length1, length2];
        }

        private Int32 _dp2(String text1, String text2) {
            Int32 length1 = text1.Length;
            Int32 length2 = text2.Length;

            // we want text2 to be the smallest to achieve space complexity of O(min(m, n))
            if (length2 > length1) return this._dp2(text2, text1);

            // 
            var dp = new Int32[length2 + 1];

            // starting from length one, up to the length of each string (bottom-up dynamic programming)
            for (Int32 length1ToCheck = 1; length1ToCheck <= length1; length1ToCheck++) {
                // 
                Int32 prev = 0;
                for (Int32 length2ToCheck = 1; length2ToCheck <= length2; length2ToCheck++) {
                    // 
                    Int32 temp = dp[length2ToCheck];

                    // if the last character of both is the same
                    if (text1[length1ToCheck - 1] == text2[length2ToCheck - 1]) {
                        // 
                        dp[length2ToCheck] = prev + 1;
                    } else {
                        // otherwise, is the max common subsequence (length) between val1 and val2
                        // where val1 is 
                        // and val2 is 
                        dp[length2ToCheck] = Math.Max(dp[length2ToCheck], dp[length2ToCheck - 1]);
                    }

                    // 
                    prev = temp;
                }
            }

            return dp[length2];
        }

        [Test]
        [TestCase("abcde", "ace", ExpectedResult = 3)]
        [TestCase("abc", "abc", ExpectedResult = 3)]
        [TestCase("abc", "def", ExpectedResult = 0)]
        public Int32 Test(String text1, String text2) {
            return this.LongestCommonSubsequence(text1, text2);
        }

    }

}
