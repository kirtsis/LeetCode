using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1277. Count Square Submatrices with All Ones
    ///     https://leetcode.com/problems/count-square-submatrices-with-all-ones/
    /// </summary>
    [TestFixture]
    public class P01277 {

        [PublicAPI]
        public Int32 CountSquares(Int32[][] matrix) {
            // Constraints:
            // * 1 <= arr.length <= 300
            // * 1 <= arr[0].length <= 300
            // * 0 <= arr[i][j] <= 1

            Int32 rows = matrix.Length;
            Int32 cols = matrix[0].Length;
            var dp = new Int32[rows + 1, cols + 1];

            Int32 result = 0;
            for (Int32 row = 1; row <= rows; ++row) {
                for (Int32 col = 1; col <= cols; ++col) {
                    if (matrix[row - 1][col - 1] == 0) continue;

                    dp[row, col] = 1 + Math.Min(dp[row - 1, col], Math.Min(dp[row, col - 1], dp[row - 1, col - 1]));
                    result += dp[row, col];
                }
            }

            return result;
        }

        [Test]
        [TestCase("[[0,1,1,1],[1,1,1,1],[0,1,1,1]]", ExpectedResult = 15)]
        [TestCase("[[1,0,1],[1,1,0],[1,1,0]]", ExpectedResult = 7)]
        public Int32 Test(String input) {
            var matrix = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.CountSquares(matrix);
        }

    }

}
