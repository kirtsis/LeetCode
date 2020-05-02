using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1433. Check If a String Can Break Another String
    ///     https://leetcode.com/problems/check-if-a-string-can-break-another-string/
    /// </summary>
    [TestFixture]
    public class P01433 {

        [PublicAPI]
        public Boolean CheckIfCanBreak(String s1, String s2) {
            // Constraints:
            // * s1.length == n
            // * s2.length == n
            // * 1 <= n <= 10^5
            // * All strings consist of lowercase English letters.

            // sort s1
            Char[] chars1 = s1.ToCharArray();
            Array.Sort(chars1);
            s1 = new String(chars1);

            // sort s2
            Char[] chars2 = s2.ToCharArray();
            Array.Sort(chars2);
            s2 = new String(chars2);

            // check if s1 can break s2 and vice versa
            Int32 breaks1 = 0;
            Int32 breaks2 = 0;
            Int32 length = s1.Length;
            for (Int32 index = 0; index < length; ++index) {
                if (s1[index] >= s2[index]) breaks1++;
                if (s2[index] >= s1[index]) breaks2++;
            }

            return (breaks1 == length) || (breaks2 == length);
        }

        [Test]
        [TestCase("abc", "xya", ExpectedResult = true)]
        [TestCase("abe", "acd", ExpectedResult = false)]
        [TestCase("leetcodee", "interview", ExpectedResult = true)]
        public Boolean Test(String s1, String s2) {
            return this.CheckIfCanBreak(s1, s2);
        }

    }

}
