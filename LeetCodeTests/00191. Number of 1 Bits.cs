using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     191. Number of 1 Bits
    ///     https://leetcode.com/problems/number-of-1-bits/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00191 {

        [PublicAPI]
        public Int32 HammingWeight(UInt32 n) {
            //return this._count1(n);
            //return this._count2(n);
            //return this._count3(n);
            return this._count3(n);
        }

        private Int32 _count1(UInt32 n) {
            return Convert.ToString(n, 2).Count(c => c == '1');
        }

        private Int32 _count2(UInt32 n) {
            Int32 result = 0;

            for (Int32 bit = 0; bit < 32; ++bit) {
                if ((n & (1 << bit)) != 0) result++;
            }

            return result;
        }

        private Int32 _count3(UInt32 n) {
            Int32 result = 0;

            while (n > 0) {
                //result += (Int32)(n & 1);
                if ((n & 1) != 0) result++;
                n >>= 1;
            }

            return result;
        }

        private Int32 _count4(UInt32 n) {
            Int32 result = 0;

            while (n > 0) {
                n &= n - 1;
                result++;
            }

            return result;
        }

        [Test]
        [TestCase("00000000000000000000000000001011", ExpectedResult = 3)]
        [TestCase("00000000000000000000000010000000", ExpectedResult = 1)]
        [TestCase("11111111111111111111111111111101", ExpectedResult = 31)]
        public Int32 Test(String input) {
            UInt32 n = Convert.ToUInt32(input, 2);
            return this.HammingWeight(n);
        }

    }

}
