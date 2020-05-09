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
            //return this._digitByDigitCalculation(num);
            //return this._newtonRaphsonMethod(num);
            return this._binarySearch(num);
        }

        private Boolean _digitByDigitCalculation(Int32 num) {
            if (num <= 0) return false;

            Int32 number = num;
            Int32 approximation = 0;

            Int32 bit = 1 << 30;
            while (bit > number) {
                bit >>= 2;
            }

            while (bit != 0) {
                if (number >= approximation + bit) {
                    number -= approximation + bit;
                    approximation = (approximation >> 1) + bit;
                } else approximation >>= 1;

                bit >>= 2;
            }

            return approximation * approximation == num;
        }

        private Boolean _newtonRaphsonMethod(Int32 num) {
            if (num <= 0) return false;

            Int32 approximation = num;
            while (approximation > num / approximation) {
                approximation = (approximation + num / approximation) >> 1;
            }

            return approximation * approximation == num;
        }

        private Boolean _binarySearch(Int32 num) {
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
