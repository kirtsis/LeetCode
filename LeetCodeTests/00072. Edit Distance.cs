using System;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     72. Edit Distance
    ///     https://leetcode.com/problems/edit-distance/
    /// </summary>
    [TestFixture]
    public class P00072 {

        [PublicAPI]
        public Int32 MinDistance(String word1, String word2) {
            Int32 length1 = word1.Length;
            Int32 length2 = word2.Length;

            Int32[] dp = Enumerable.Range(0, length2 + 1).ToArray();
            for (Int32 index1 = 1; index1 <= length1; ++index1) {
                Int32 previous = dp[0];
                dp[0] = index1;
                for (Int32 index2 = 1; index2 <= length2; ++index2) {
                    Int32 temp = dp[index2];
                    if (word1[index1 - 1] == word2[index2 - 1]) dp[index2] = previous;
                    else dp[index2] = 1 + Math.Min(previous, Math.Min(dp[index2], dp[index2 - 1]));
                    previous = temp;
                }
            }

            return dp[length2];
        }

        [Test]
        [TestCase("horse", "ros", ExpectedResult = 3)]
        [TestCase("intention", "execution", ExpectedResult = 5)]
        public Int32 Test(String word1, String word2) {
            return this.MinDistance(word1, word2);
        }

    }

}
