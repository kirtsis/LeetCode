using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1449. Form Largest Integer With Digits That Add up to Target
    ///     https://leetcode.com/problems/form-largest-integer-with-digits-that-add-up-to-target/
    /// </summary>
    [TestFixture]
    public class P01449 {

        [PublicAPI]
        public String LargestNumber(Int32[] cost, Int32 target) {
            // Constraints:
            // * cost.length == 9
            // * 1 <= cost[i] <= 5000
            // * 1 <= target <= 5000

            // memory[0] is not used (it is cleaner if we use memory[remainingMoney] than using memory[remainingMoney - 1])
            var memory = new String[target + 1];
            return this._dfs(cost, target, memory);
        }

        private String _dfs(Int32[] digitCosts, Int32 remainingMoney, String[] memory) {
            if (remainingMoney < 0) return "0"; // no answer
            if (remainingMoney == 0) return String.Empty; // perfect answer
            if (memory[remainingMoney] != null) return memory[remainingMoney]; // answer in memory already

            String result = "0";
            for (Int32 digit = 9; digit >= 1; --digit) {
                // calculate best answer for the remaining money if we use that digit (remainingMoney - digitCosts[digit - 1])
                String answer = this._dfs(digitCosts, remainingMoney - digitCosts[digit - 1], memory);

                // if we use that digit and the best answer for the remaining money is "0" (no answer), we cannot use that digit
                if (answer == "0") continue; // so, continue to the next digit

                // if we can use that digit,
                // and we already have a result (the result is not "0")
                // check if the result that we have already, is of length equal or greater, from the best answer for the remaining money plus one for the new digit we can use
                // since we start from digit 9 and we move downwards, a result that we have already is bound to be better than the one with the new digit if both have the same length
                // and obviously is also better if it's longer
                if ((result != "0") && (result.Length >= answer.Length + 1)) continue; // if so, continue to the next digit

                // update the result with the new best answer (best answer for the remaining money with the digit we can use in front of it)
                result = digit + answer;
            }

            // set in memory the best answer for that amount of money
            memory[remainingMoney] = result;
            return result;
        }

        [Test]
        [TestCase("[4,3,2,5,6,7,2,5,5]", 9, ExpectedResult = "7772")]
        [TestCase("[7,6,5,5,5,6,8,7,8]", 12, ExpectedResult = "85")]
        [TestCase("[2,4,6,2,4,6,4,4,4]", 5, ExpectedResult = "0")]
        [TestCase("[6,10,15,40,40,40,40,40,40]", 47, ExpectedResult = "32211")]
        public String Test(String input, Int32 target) {
            var cost = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.LargestNumber(cost, target);
        }

    }

}
