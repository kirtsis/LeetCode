using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     66. Plus One
    ///     https://leetcode.com/problems/plus-one/
    /// </summary>
    [TestFixture]
    public class P00066 {

        [PublicAPI]
        public Int32[] PlusOne(Int32[] digits) {
            if (digits == null) return null; // can never happen (the problem states: Given a non-empty array of digits [...])

            Int32 length = digits.Length;
            if (length == 0) return digits; // can never happen (the problem states: Given a non-empty array of digits [...])

            // the problem states: [...] plus one to the integer [...]
            // the following implementation can also work for any increment
            return this._plus(digits, length, 1);
        }

        private Int32[] _plus(Int32[] digits, Int32 length, Int32 increment) {
            Int32 carry = increment;
            for (Int32 index = length - 1; index >= 0; --index) {
                digits[index] = digits[index] + carry;
                carry = digits[index] / 10;
                digits[index] = digits[index] % 10;
                if (carry == 0) return digits;
            }

            // the following is not really needed
            // we only have it so we can skip the extra work needed for the any increment implementation that can affect the leetcode's judgment
            // when the increment is one, we can reach this point if and only if all digits are 9
            // in that case the carry can only be one
            if (carry == 1) {
                var answer = new Int32[length + 1];
                answer[0] = 1;
                if (increment != 1) Array.Copy(digits, 0, answer, 1, length); // if increment is one, all other digits will be zero
                return answer;
            }

            // extra code for the any increment implementation
            var result = new List<Int32>();
            while (carry > 0) {
                result.Insert(0, carry % 10); // prepend the last digit of the carry
                carry /= 10; // and throw it away
            }

            result.AddRange(digits);
            return result.ToArray();
        }

        [Test]
        [TestCase("[]", ExpectedResult = "[]")]
        [TestCase("[1]", ExpectedResult = "[2]")]
        [TestCase("[9]", ExpectedResult = "[1,0]")]
        [TestCase("[1,2,3]", ExpectedResult = "[1,2,4]")]
        [TestCase("[4,3,2,1]", ExpectedResult = "[4,3,2,2]")]
        [TestCase("[9,9,9]", ExpectedResult = "[1,0,0,0]")]
        public String Test(String input) {
            var digits = JsonConvert.DeserializeObject<Int32[]>(input);
            Int32[] result = this.PlusOne(digits);
            return JsonConvert.SerializeObject(result);
        }

    }

}
