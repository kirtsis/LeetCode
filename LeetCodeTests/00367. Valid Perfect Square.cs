using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     367. Valid Perfect Square
    ///     https://leetcode.com/problems/valid-perfect-square/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00367 {

        [PublicAPI]
        public Boolean IsPerfectSquare(Int32 num) {
            //return this._calc(num);
            return this._binarySearch(num);
        }

        private Boolean _calc(Int32 num) {
            // Given a positive integer num, write a function which returns True if num is a perfect square else False.
            // Note: Do not use any built-in library function such as sqrt.

            if (num <= 0) return false;

            //Double sqrt = Math.Sqrt(num);
            //Int32 approximation = (Int32)Math.Truncate(sqrt);
            //Int32 approximation = (Int32)Math.Sqrt(num);
            Int32 approximation = this._sqrt(num);
            return approximation * approximation == num;
        }

        private Int32 _sqrt(Int32 number) {
            Int32 result = 0;

            Int32 bit = 1 << 30;
            while (bit > number) {
                bit >>= 2;
            }

            while (bit != 0) {
                if (number >= result + bit) {
                    number -= result + bit;
                    result = (result >> 1) + bit;
                } else result >>= 1;

                bit >>= 2;
            }

            return result;
        }

        private Boolean _binarySearch(Int32 num) {
            // Given a positive integer num, write a function which returns True if num is a perfect square else False.
            // Note: Do not use any built-in library function such as sqrt.

            if (num <= 0) return false;

            Int32 left = 1;
            Int32 right = num;
            while (left <= right) {
                Int32 mid = left + (right - left) / 2;
                Int32 value = num / mid;
                if (mid == value) return num % mid == 0;

                if (mid < value) left = mid + 1;
                else right = mid - 1;
            }

            return false;
        }

        [Test]
        [TestCase(-2, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(14, ExpectedResult = false)]
        [TestCase(16, ExpectedResult = true)]
        [TestCase(104976, ExpectedResult = true)]
        public Boolean Test(Int32 num) {
            return this.IsPerfectSquare(num);
        }

    }

}
