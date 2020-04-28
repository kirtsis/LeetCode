using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     171. Excel Sheet Column Number
    ///     https://leetcode.com/problems/excel-sheet-column-number/
    /// </summary>
    [TestFixture]
    public class P00171 {

        [PublicAPI]
        public Int32 TitleToNumber(String s) {
            Int32 length = s.Length;

            Int32 result = 0;
            for (Int32 i = length - 1; i >= 0; i--) {
                result += (s[i] - 'A' + 1) * (Int32)Math.Pow(26, length - 1 - i);
            }

            return result;
        }

        [Test]
        [TestCase("A", ExpectedResult = 1)]
        [TestCase("AB", ExpectedResult = 28)]
        [TestCase("ZY", ExpectedResult = 701)]
        public Int32 Test(String input) {
            return this.TitleToNumber(input);
        }

    }

}
