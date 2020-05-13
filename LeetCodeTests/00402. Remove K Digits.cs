using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     402. Remove K Digits
    ///     https://leetcode.com/problems/remove-k-digits/
    /// </summary>
    [TestFixture]
    public class P00402 {

        [PublicAPI]
        public String RemoveKdigits(String num, Int32 k) {
            // Note:
            // * The length of num is less than 10002 and will be ≥ k.
            // * The given num does not contain any leading zero.
            if (num == null) return "0";

            Int32 length = num.Length;
            if (length <= k) return "0";

            Int32 firstZeroIndex = num.IndexOf("0", StringComparison.Ordinal);
            while ((firstZeroIndex != -1) && (firstZeroIndex <= k)) {
                num = num.Substring(firstZeroIndex).TrimStart('0');
                k -= firstZeroIndex;
                firstZeroIndex = num.IndexOf("0", StringComparison.Ordinal);
            }

            for (Int32 index = 0; (index < num.Length - 1) && (k > 0); ++index) {
                if (num[index] <= num[index + 1]) continue;

                num = num.Remove(index, 1);
                index = -1;
                k--;
            }

            num = num.Substring(0, num.Length - k).TrimStart('0');
            return num == String.Empty ? "0" : num;
        }

        [Test]
        [TestCase("1432219", 3, ExpectedResult = "1219")]
        [TestCase("10200", 1, ExpectedResult = "200")]
        [TestCase("10", 2, ExpectedResult = "0")]
        [TestCase("1000200", 1, ExpectedResult = "200")]
        [TestCase("14032219", 3, ExpectedResult = "2219")]
        [TestCase("14302219", 2, ExpectedResult = "102219")]
        [TestCase("10", 1, ExpectedResult = "0")]
        [TestCase("112", 1, ExpectedResult = "11")]
        [TestCase("110", 1, ExpectedResult = "10")]
        public String Test(String num, Int32 k) {
            return this.RemoveKdigits(num, k);
        }

    }

}
