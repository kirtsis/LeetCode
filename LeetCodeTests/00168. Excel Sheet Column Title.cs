using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     168. Excel Sheet Column Title
    ///     https://leetcode.com/problems/excel-sheet-column-title/
    /// </summary>
    [TestFixture]
    public class P00168 {

        [PublicAPI]
        public String ConvertToTitle(Int32 n) {
            String result = "";

            while (n > 0) {
                n--;
                Int32 digit = n % 26;
                result = (Char)('A' + digit) + result;
                n /= 26;
            }

            return result;
        }

        [Test]
        [TestCase(1, ExpectedResult = "A")]
        [TestCase(28, ExpectedResult = "AB")]
        [TestCase(701, ExpectedResult = "ZY")]
        public String Test(Int32 input) {
            return this.ConvertToTitle(input);
        }

    }

}
