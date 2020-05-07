using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1432. Max Difference You Can Get From Changing an Integer
    ///     https://leetcode.com/problems/max-difference-you-can-get-from-changing-an-integer/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01432 {

        [PublicAPI]
        public Int32 MaxDiff(Int32 num) {
            // Constraints:
            // * 1 <= num <= 10^8
            //return this._asString(num);
            return this._asString2(num);
        }

        private Int32 _asString(Int32 num) {
            String number = Convert.ToString(num);

            Int32 min = Int32.MaxValue;
            Int32 max = Int32.MinValue;
            for (Char x = '0'; x <= '9'; ++x) {
                for (Char y = '0'; y <= '9'; ++y) {
                    String candidate = number.Replace(x, y);
                    if (candidate.StartsWith("0")) continue;

                    Int32 value = Convert.ToInt32(candidate);
                    if (value == 0) continue;

                    if (value < min) min = value;
                    if (value > max) max = value;
                }
            }

            return max - min;
        }

        private Int32 _asString2(Int32 num) {
            String number = Convert.ToString(num);

            // replace the first non 9 digit with 9 (all occurrences)
            String max = number;
            foreach (Char digit in max) {
                if (digit == '9') continue;

                max = max.Replace(digit, '9');
                break;
            }

            // replace the first non 0 digit with 0 (all occurrences)
            String min = number;
            if (min[0] != '1') {
                // The new integer cannot have any leading zeros
                // so if the first digit is not 1, replace the first digit with 1 (all occurrences)
                min = min.Replace(min[0], '1');
            } else {
                foreach (Char digit in min) {
                    // we can't replace 1, since the first digit is 1 and that will lead to leading zeros
                    if ((digit == '0') || (digit == '1')) continue;

                    min = min.Replace(digit, '0');
                    break;
                }
            }

            return Convert.ToInt32(max) - Convert.ToInt32(min);
        }

        [Test]
        [TestCase(555, ExpectedResult = 888)]
        [TestCase(9, ExpectedResult = 8)]
        [TestCase(123456, ExpectedResult = 820000)]
        [TestCase(10000, ExpectedResult = 80000)]
        [TestCase(9288, ExpectedResult = 8700)]
        [TestCase(1111, ExpectedResult = 8888)]
        public Int32 Test(Int32 num) {
            return this.MaxDiff(num);
        }

    }

}
