using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     204. Count Primes
    ///     https://leetcode.com/problems/count-primes/
    /// </summary>
    [TestFixture]
    public class P00204 {

        [PublicAPI]
        public Int32 CountPrimes(Int32 n) {
            if (n < 3) return 0;

            var isComposite = new Boolean[n];

            Int32 count = n / 2;
            for (Int32 candidate = 3; candidate * candidate < n; candidate += 2) {
                if (isComposite[candidate]) continue;

                for (Int32 multiples = candidate * candidate; multiples < n; multiples += 2 * candidate) {
                    if (isComposite[multiples]) continue;

                    --count;
                    isComposite[multiples] = true;
                }
            }

            return count;
        }

        [Test]
        [TestCase(10, ExpectedResult = 4)]
        [TestCase(100, ExpectedResult = 25)]
        [TestCase(1000, ExpectedResult = 168)]
        [TestCase(10000, ExpectedResult = 1229)]
        [TestCase(100000, ExpectedResult = 9592)]
        [TestCase(1000000, ExpectedResult = 78498)]
        [TestCase(10000000, ExpectedResult = 664579)]
        [TestCase(100000000, ExpectedResult = 5761455)]
        public Int32 Test(Int32 n) {
            return this.CountPrimes(n);
        }

    }

}
