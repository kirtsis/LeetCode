using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1424. Diagonal Traverse II
    ///     https://leetcode.com/problems/diagonal-traverse-ii/
    /// </summary>
    [TestFixture]
    public class P01424 {

        [PublicAPI]
        public Int32[] FindDiagonalOrder(IList<IList<Int32>> nums) {
            Int32 elements = 0;
            Int32 maxDiagonal = 0;
            var diagonals = new Dictionary<Int32, IList<Int32>>();

            Int32 rows = nums.Count;
            for (Int32 row = rows - 1; row >= 0; row--) {
                Int32 cols = nums[row].Count;
                for (Int32 col = 0; col < cols; col++) {
                    elements++;
                    Int32 diagonal = row + col;
                    maxDiagonal = Math.Max(maxDiagonal, diagonal);
                    if (!diagonals.ContainsKey(diagonal)) diagonals.Add(diagonal, new List<Int32>());
                    diagonals[diagonal].Add(nums[row][col]);
                }
            }

            Int32 resultIndex = 0;
            var result = new Int32[elements];
            for (Int32 diagonal = 0; diagonal <= maxDiagonal; diagonal++) {
                Int32 diagonalLength = diagonals[diagonal].Count;
                for (Int32 index = 0; index < diagonalLength; index++) {
                    result[resultIndex] = diagonals[diagonal][index];
                    resultIndex++;
                }
            }

            return result;
        }

        [Test]
        [TestCase("[[1,2,3],[4,5,6],[7,8,9]]", ExpectedResult = "[1,4,2,7,5,3,8,6,9]")]
        [TestCase("[[1,2,3,4,5],[6,7],[8],[9,10,11],[12,13,14,15,16]]", ExpectedResult = "[1,6,2,8,7,3,9,4,12,10,5,13,11,14,15,16]")]
        [TestCase("[[1,2,3],[4],[5,6,7],[8],[9,10,11]]", ExpectedResult = "[1,4,2,5,3,8,6,9,7,10,11]")]
        [TestCase("[[1,2,3,4,5,6]]", ExpectedResult = "[1,2,3,4,5,6]")]
        public String Test(String input) {
            var nums = JsonConvert.DeserializeObject<IList<IList<Int32>>>(input);
            Int32[] result = this.FindDiagonalOrder(nums);
            return JsonConvert.SerializeObject(result);
        }

    }

}
