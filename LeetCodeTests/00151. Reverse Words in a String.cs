using System;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     151. Reverse Words in a String
    ///     https://leetcode.com/problems/reverse-words-in-a-string/
    /// </summary>
    [TestFixture]
    public class P00151 {

        [PublicAPI]
        public String ReverseWords(String s) {
            // Note:
            // * A word is defined as a sequence of non-space characters.
            // * Input string may contain leading or trailing spaces. However, your reversed string should not contain leading or trailing spaces.
            // * You need to reduce multiple spaces between two words to a single space in the reversed string.

            return String.Join(" ", s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Reverse());
        }

        [Test]
        [TestCase("the sky is blue", ExpectedResult = "blue is sky the")]
        [TestCase("  hello world!  ", ExpectedResult = "world! hello")]
        [TestCase("a good   example", ExpectedResult = "example good a")]
        public String Test(String s) {
            return this.ReverseWords(s);
        }

    }

}
