using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     172. Factorial Trailing Zeroes
    ///     https://leetcode.com/problems/factorial-trailing-zeroes/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00172 {

        [PublicAPI]
        public Int32 TrailingZeroes(Int32 n) {
            //return this._divisor(n);
            return this._division(n);
        }

        private Int32 _divisor(Int32 n) {
            Int32 result = 0;
            Int64 divisor = 5;
            while (true) {
                Int32 counter = (Int32)(n / divisor);
                if (counter == 0) break;

                result += counter;
                divisor *= 5;
            }

            return result;
        }

        private Int32 _division(Int32 n) {
            Int32 result = 0;
            while (n >= 5) {
                result += n / 5;
                n /= 5;
            }

            return result;
        }

        [Test]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(3, ExpectedResult = 0)]
        [TestCase(5, ExpectedResult = 1)]
        [TestCase(23, ExpectedResult = 4)]
        [TestCase(101, ExpectedResult = 24)]
        [TestCase(1000, ExpectedResult = 249)]
        [TestCase(4617, ExpectedResult = 1151)]
        [TestCase(1808548329, ExpectedResult = 452137076)]
        public Int32 Test(Int32 n) {
            return this.TrailingZeroes(n);
        }

    }

}
