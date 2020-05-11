using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     8. String to Integer (atoi)
    ///     https://leetcode.com/problems/string-to-integer-atoi/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00008 {

        [PublicAPI]
        public Int32 MyAtoi(String str) {
            //return this._atoi1(str);
            return this._atoi2(str);
        }

        private Int32 _atoi1(String str) {
            Boolean signFound = false;
            Boolean digitFound = false;
            String toConvert = String.Empty;
            foreach (Char character in str) {
                if (character == ' ') {
                    if (digitFound) break;

                    continue;
                }

                if ((character == '+') || (character == '-')) {
                    if (signFound) break;
                    if (digitFound) break;

                    toConvert += character;
                    signFound = true;
                    digitFound = true;
                    continue;
                }

                if (!Char.IsDigit(character)) break;

                toConvert += character;
                digitFound = true;
            }

            if (toConvert == String.Empty) return 0;
            if (toConvert == "+") return 0;
            if (toConvert == "-") return 0;

            Int32 sign = 1;
            Int32 result = 0;
            foreach (Char character in toConvert) {
                if (character == '+') continue;

                if (character == '-') {
                    sign = -1;
                    continue;
                }

                Int32 digit = character - '0';
                if (result != 0) {
                    if (Int32.MaxValue / 10 < result) return sign == 1 ? Int32.MaxValue : Int32.MinValue;

                    result = result * 10;
                }

                if (Int32.MaxValue - digit < result) return sign == 1 ? Int32.MaxValue : Int32.MinValue;

                result += digit;
            }

            return sign * result;
        }

        private Int32 _atoi2(String str) {
            if (str == null) return 0;

            Int32 length = str.Length;
            if (length == 0) return 0;

            Int32 start = 0;
            while ((start < length) && (str[start] == ' ')) {
                start++;
            }

            Int32 sign = 1;
            if ((start < length) && ((str[start] == '+') || (str[start] == '-'))) {
                sign = str[start] == '-' ? -1 : 1;
                start++;
            }

            Int32 result = 0;
            while ((start < length) && (str[start] >= '0') && (str[start] <= '9')) {
                if (Int32.MaxValue / 10 < result) return sign == 1 ? Int32.MaxValue : Int32.MinValue;

                result *= 10;

                Int32 digit = str[start] - '0';
                if (Int32.MaxValue - digit < result) return sign == 1 ? Int32.MaxValue : Int32.MinValue;

                result += digit;
                start++;
            }

            return sign * result;
        }

        [Test]
        [TestCase("42", ExpectedResult = 42)]
        [TestCase("   -42", ExpectedResult = -42)]
        [TestCase("4193 with words", ExpectedResult = 4193)]
        [TestCase("words and 987", ExpectedResult = 0)]
        [TestCase("-91283472332", ExpectedResult = -2147483648)]
        [TestCase("91283472332", ExpectedResult = 2147483647)]
        [TestCase("-2147483648", ExpectedResult = -2147483648)]
        [TestCase("2147483647", ExpectedResult = 2147483647)]
        [TestCase("-2147483647", ExpectedResult = -2147483647)]
        [TestCase("2147483646", ExpectedResult = 2147483646)]
        [TestCase("   +0 123", ExpectedResult = 0)]
        public Int32 Test(String str) {
            return this.MyAtoi(str);
        }

    }

}
