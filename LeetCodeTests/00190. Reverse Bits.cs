using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     190. Reverse Bits
    ///     https://leetcode.com/problems/reverse-bits/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00190 {

        [PublicAPI]
        public UInt32 reverseBits(UInt32 n) {
            //return this._reverse1(n);
            return this._reverse2(n);
        }

        private UInt32 _reverse1(UInt32 n) {
            Char[] chars = Convert.ToString(n, 2).PadLeft(32, '0').ToCharArray();
            Array.Reverse(chars);
            return Convert.ToUInt32(new String(chars), 2);
        }

        private UInt32 _reverse2(UInt32 n) {
            UInt32 result = 0;

            Int32 bit = 0;
            while (bit < 32) {
                result <<= 1;
                result |= n & 1;
                n >>= 1;
                bit++;
            }

            return result;
        }

        [Test]
        [TestCase("00000010100101000001111010011100", ExpectedResult = "00111001011110000010100101000000")]
        [TestCase("11111111111111111111111111111101", ExpectedResult = "10111111111111111111111111111111")]
        [TestCase("00000000000000000000000000000001", ExpectedResult = "10000000000000000000000000000000")]
        [TestCase("10000000000000000000000000000000", ExpectedResult = "00000000000000000000000000000001")]
        public String Test(String input) {
            UInt32 n = Convert.ToUInt32(input, 2);
            UInt32 result = this.reverseBits(n);
            return Convert.ToString(result, 2).PadLeft(32, '0');
        }

    }

}
