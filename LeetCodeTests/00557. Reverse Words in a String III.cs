using System;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     557. Reverse Words in a String III
    ///     https://leetcode.com/problems/reverse-words-in-a-string-iii/
    /// </summary>
    [TestFixture]
    public class P00557 {

        [PublicAPI]
        public String ReverseWords(String s) {
            // Note: In the string, each word is separated by single space and there will not be any extra space in the string.

            return String.Join(" ", s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(word => new String(word.Reverse().ToArray())));
        }

        [Test]
        [TestCase("Let's take LeetCode contest", ExpectedResult = "s'teL ekat edoCteeL tsetnoc")]
        public String Test(String s) {
            return this.ReverseWords(s);
        }

    }

}
