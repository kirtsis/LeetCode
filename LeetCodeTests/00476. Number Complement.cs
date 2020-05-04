using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     476. Number Complement
    ///     https://leetcode.com/problems/number-complement/
    /// </summary>
    [TestFixture]
    public class P00476 {

        [PublicAPI]
        public Int32 FindComplement(Int32 num) {
            Int32 numBase2Digits = (Int32)Math.Log(num, 2) + 1;
            Int32 max = (1 << numBase2Digits) - 1;
            return max - num;
        }

        [Test]
        [TestCase(5, ExpectedResult = 2)]
        [TestCase(1, ExpectedResult = 0)]
        public Int32 Test(Int32 num) {
            return this.FindComplement(num);
        }

    }

}
