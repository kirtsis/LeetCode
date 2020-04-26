using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1051. Height Checker
    ///     https://leetcode.com/problems/height-checker/
    /// </summary>
    [TestFixture]
    public class P01051 {

        [PublicAPI]
        public Int32 HeightChecker(Int32[] heights) {
            // HashMap/Dictionary of each height's occurrences
            // Key: height, Value: occurrences of that height (count)
            var heightsCount = new Dictionary<Int32, Int32>();
            foreach (Int32 height in heights) {
                if (!heightsCount.ContainsKey(height)) heightsCount.Add(height, 1);
                else heightsCount[height]++;
            }

            Int32 result = 0;

            // starting from height one (Problem Constraints: 1 <= heights[i] <= 100)
            Int32 heightToCheck = 1;

            // loop through our sorted heights in order
            foreach (Int32 height in heights) {
                // find the smallest height that has occurrences, by gradually increasing the height to check
                // no infinite loop here, we have at least one height (Problem Constraints: 1 <= heights.length <= 100)
                while (!heightsCount.ContainsKey(heightToCheck)) {
                    heightToCheck++;
                }

                // increase our result, if our height is not the smallest height that has occurrences
                if (height != heightToCheck) result++;

                // decrease the occurrences of that height and remove it when reach zero
                if (--heightsCount[heightToCheck] == 0) heightsCount.Remove(heightToCheck);
            }

            return result;
        }

        [Test]
        [TestCase("[1,1,4,2,1,3]", ExpectedResult = 3)]
        [TestCase("[5,1,2,3,4]", ExpectedResult = 5)]
        [TestCase("[1,2,3,4,5]", ExpectedResult = 0)]
        public Int32 Test(String input) {
            var heights = JsonConvert.DeserializeObject<Int32[]>(input);
            return this.HeightChecker(heights);
        }

    }

}
