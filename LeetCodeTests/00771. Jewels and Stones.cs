using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     771. Jewels and Stones
    ///     https://leetcode.com/problems/jewels-and-stones/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00771 {

        [PublicAPI]
        public Int32 NumJewelsInStones(String J, String S) {
            //return this._loops(J, S);
            //return this._linqJoin(J, S);
            //return this._linqCount(J, S);
            //return this._hashSet(J, S);
            //return this._hashSetLinq(J, S);
            return this._array(J, S);
        }

        private Int32 _loops(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            Int32 result = 0;
            foreach (Char stone in S) {
                foreach (Char jewel in J) {
                    if (stone == jewel) result++;
                }
            }

            return result;
        }

        private Int32 _linqJoin(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            //return (from stone in S
            //        from jewel in J
            //        where stone == jewel
            //        select jewel).Count();

            //return (from stone in S
            //        join jewel in J on stone equals jewel
            //        select jewel).Count();

            return S.Join(J, stone => stone, jewel => jewel, (stone, jewel) => jewel).Count();
        }

        private Int32 _linqCount(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            return S.Count(stone => J.Any(jewel => stone == jewel));
        }

        private Int32 _hashSet(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            Int32 result = 0;
            var set = new HashSet<Char>(J);
            foreach (Char stone in S) {
                if (set.Contains(stone)) result++;
            }

            return result;
        }

        private Int32 _hashSetLinq(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            var set = new HashSet<Char>(J);
            return S.Count(stone => set.Contains(stone));
        }

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        private Int32 _array(String J, String S) {
            if ((J == null) || (J.Length == 0)) return 0;
            if ((S == null) || (S.Length == 0)) return 0;

            // 26 English alphabet letters
            // times 2 for upper- and lower-case
            // plus 6 for the characters [\]^_` that reside between upper- and lower-case
            // total of 58 "flags" needed to indicate all possible jewel types that J can have
            // or just, in ascii: 'z' - 'A' => in decimal: 122 - 65 => max needed index: 57 => needed length: 58
            var set = new Boolean[58];
            for (Int32 index = 0; index < J.Length; ++index) {
                set[J[index] - 'A'] = true; // indicate that J contains that jewel type
            }

            Int32 result = 0;
            for (Int32 index = 0; index < S.Length; ++index) {
                if (set[S[index] - 'A']) result++;
            }

            return result;
        }

        [Test]
        [TestCase("aA", "aAAbbbb", ExpectedResult = 3)]
        [TestCase("z", "ZZ", ExpectedResult = 0)]
        public Int32 Test(String J, String S) {
            return this.NumJewelsInStones(J, S);
        }

    }

}
