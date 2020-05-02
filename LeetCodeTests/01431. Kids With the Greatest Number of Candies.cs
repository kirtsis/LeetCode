using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1431. Kids With the Greatest Number of Candies
    ///     https://leetcode.com/problems/kids-with-the-greatest-number-of-candies/
    /// </summary>
    [TestFixture]
    public class P01431 {

        [PublicAPI]
        public IList<Boolean> KidsWithCandies(Int32[] candies, Int32 extraCandies) {
            // Constraints:
            // * 2 <= candies.length <= 100
            // * 1 <= candies[i] <= 100
            // * 1 <= extraCandies <= 50

            Int32 length = candies.Length;

            // find max
            Int32 max = Int32.MinValue;
            for (Int32 index = 0; index < length; ++index) {
                if (candies[index] > max) max = candies[index];
            }

            var result = new Boolean[length];
            for (Int32 index = 0; index < length; ++index) {
                result[index] = candies[index] + extraCandies >= max;
            }

            return result;
        }

        [Test]
        [TestCase("[2,3,5,1,3]", 3, ExpectedResult = "[true,true,true,false,true]")]
        [TestCase("[4,2,1,1,2]", 1, ExpectedResult = "[true,false,false,false,false]")]
        [TestCase("[12,1,12]", 10, ExpectedResult = "[true,false,true]")]
        public String Test(String input, Int32 extraCandies) {
            var candies = JsonConvert.DeserializeObject<Int32[]>(input);
            IList<Boolean> result = this.KidsWithCandies(candies, extraCandies);
            return JsonConvert.SerializeObject(result);
        }

    }

}
