using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1447. Simplified Fractions
    ///     https://leetcode.com/problems/simplified-fractions/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01447 {

        [PublicAPI]
        public IList<String> SimplifiedFractions(Int32 n) {
            // Constraints:
            // * 1 <= n <= 100

            //return this._fractions1(n);
            return this._fractions2(n);
        }

        private IList<String> _fractions1(Int32 n) {
            var result = new List<String>();

            var set = new HashSet<Decimal>();
            for (Int32 numerator = 1; numerator <= n; ++numerator) {
                for (Int32 denominator = numerator + 1; denominator <= n; ++denominator) {
                    if (!set.Add(numerator / (Decimal)denominator)) continue;

                    result.Add($"{numerator}/{denominator}");
                }
            }

            return result;
        }

        private IList<String> _fractions2(Int32 n) {
            var result = new List<String>();

            for (Int32 numerator = 1; numerator <= n; ++numerator) {
                for (Int32 denominator = numerator + 1; denominator <= n; ++denominator) {
                    if (this._gcd(numerator, denominator) != 1) continue;

                    result.Add($"{numerator}/{denominator}");
                }
            }

            return result;
        }

        private Int32 _gcd(Int32 a, Int32 b) {
            while ((a != 0) && (b != 0)) {
                if (a > b) a %= b;
                else b %= a;
            }

            return a == 0 ? b : a;
        }

        [Test]
        [TestCase(1, ExpectedResult = "[]")]
        [TestCase(2, ExpectedResult = "[\"1/2\"]")]
        [TestCase(3, ExpectedResult = "[\"1/2\",\"1/3\",\"2/3\"]")]
        [TestCase(4, ExpectedResult = "[\"1/2\",\"1/3\",\"1/4\",\"2/3\",\"3/4\"]")]
        [TestCase(20, ExpectedResult = "[\"1/2\",\"1/3\",\"1/4\",\"1/5\",\"1/6\",\"1/7\",\"1/8\",\"1/9\",\"1/10\",\"1/11\",\"1/12\",\"1/13\",\"1/14\",\"1/15\",\"1/16\",\"1/17\",\"1/18\",\"1/19\",\"1/20\",\"2/3\",\"2/5\",\"2/7\",\"2/9\",\"2/11\",\"2/13\",\"2/15\",\"2/17\",\"2/19\",\"3/4\",\"3/5\",\"3/7\",\"3/8\",\"3/10\",\"3/11\",\"3/13\",\"3/14\",\"3/16\",\"3/17\",\"3/19\",\"3/20\",\"4/5\",\"4/7\",\"4/9\",\"4/11\",\"4/13\",\"4/15\",\"4/17\",\"4/19\",\"5/6\",\"5/7\",\"5/8\",\"5/9\",\"5/11\",\"5/12\",\"5/13\",\"5/14\",\"5/16\",\"5/17\",\"5/18\",\"5/19\",\"6/7\",\"6/11\",\"6/13\",\"6/17\",\"6/19\",\"7/8\",\"7/9\",\"7/10\",\"7/11\",\"7/12\",\"7/13\",\"7/15\",\"7/16\",\"7/17\",\"7/18\",\"7/19\",\"7/20\",\"8/9\",\"8/11\",\"8/13\",\"8/15\",\"8/17\",\"8/19\",\"9/10\",\"9/11\",\"9/13\",\"9/14\",\"9/16\",\"9/17\",\"9/19\",\"9/20\",\"10/11\",\"10/13\",\"10/17\",\"10/19\",\"11/12\",\"11/13\",\"11/14\",\"11/15\",\"11/16\",\"11/17\",\"11/18\",\"11/19\",\"11/20\",\"12/13\",\"12/17\",\"12/19\",\"13/14\",\"13/15\",\"13/16\",\"13/17\",\"13/18\",\"13/19\",\"13/20\",\"14/15\",\"14/17\",\"14/19\",\"15/16\",\"15/17\",\"15/19\",\"16/17\",\"16/19\",\"17/18\",\"17/19\",\"17/20\",\"18/19\",\"19/20\"]")]
        public String Test(Int32 n) {
            IList<String> result = this.SimplifiedFractions(n);
            return JsonConvert.SerializeObject(result);
        }

    }

}
