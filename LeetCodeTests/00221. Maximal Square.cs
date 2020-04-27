using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     221. Maximal Square
    ///     https://leetcode.com/problems/maximal-square/
    /// </summary>
    [TestFixture]
    public class P00221 {

        [PublicAPI]
        public Int32 MaximalSquare(Char[][] matrix) {
            //return this._dp(matrix);
            return this._dp2(matrix);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Int32 _dp(Char[][] matrix) {
            if ((matrix == null) || (matrix.Length == 0)) return 0;

            // 2D binary matrix implies not jagged array, so we can assume all rows have the same number of columns
            Int32 rows = matrix.Length;
            Int32 cols = matrix[0].Length;

            // dp[x,y] is the size (i.e. side) of the maximum square containing only one's and ends at matrix[x-1][y-1], that is all elements in matrix[x-1-side..x-1][y-1-side..y-1] are one
            // dp[x,y] for x=0 or y=0 is zero (the default value of Int32), that is outside our matrix (one row before row one and one column before column one)
            var dp = new Int32[rows + 1, cols + 1];

            Int32 maxSide = 0;
            for (Int32 row = 1; row <= rows; row++) {
                for (Int32 col = 1; col <= cols; col++) {
                    // if the element is zero, we can not end a square here, so the side is zero
                    // dp[row, col] is already zero, the default value of Int32
                    if (matrix[row - 1][col - 1] == '0') continue;

                    // otherwise, calculate the side of the square
                    // the side is one (a square containing only this element) plus one if all diagonal before elements are also one
                    // dp[row, col] = 1 + ((dp[row - 1, col] == 1) && (dp[row, col - 1] == 1) && (dp[row - 1, col - 1] == 1) ? 1 : 0);

                    // remember, we have one extra row and one extra column before our matrix coordinates filled with zero's
                    // so all diagonal before elements are zero for row=1 or col=1
                    // dp[1,1] is one if and only if matrix[0][0] is also one
                    // dp[1,y] is the same as matrix[0,y-1], we only have room to make a square with side one if and only if the element at matrix[0,y-1] is one
                    // dp[x,1] is the same as matrix[x-1,0], we only have room to make a square with side one if and only if the element at matrix[x-1,0] is one

                    // since we only have zero's and one's we can take the minimum of the diagonal before elements
                    dp[row, col] = 1 + Math.Min(dp[row - 1, col], Math.Min(dp[row, col - 1], dp[row - 1, col - 1]));

                    // remember the max side we found so far
                    maxSide = Math.Max(maxSide, dp[row, col]);
                }
            }

            // the problem asks for the area
            return maxSide * maxSide;
        }

        private Int32 _dp2(Char[][] matrix) {
            if ((matrix == null) || (matrix.Length == 0)) return 0;

            Int32 rows = matrix.Length;
            Int32 cols = matrix[0].Length;

            // the results of the calculations for just one row
            var dp = new Int32[cols + 1];

            Int32 maxSide = 0;
            for (Int32 row = 1; row <= rows; row++) { // could also go from row = 0 to row < rows
                // when we change rows, the previous row and col is 0
                Int32 prev = 0; // that is dp[row - 1, col - 1] from Approach #1
                for (Int32 col = 1; col <= cols; col++) {
                    // dp[col] holds the result from the previous row for this col, or 0 (the default value of Int32) if this is the first row
                    // remember it, because we are going to overide it when we calculate the result for this row and col
                    Int32 temp = dp[col]; // that is dp[row - 1, col] from Approach #1

                    // update the dp[col] for this row and col
                    if (matrix[row - 1][col - 1] == '1') {
                        // from Approach #1: dp[row, col] = 1 + Math.Min(dp[row - 1, col], Math.Min(dp[row, col - 1], dp[row - 1, col - 1]));
                        dp[col] = 1 + Math.Min(temp, Math.Min(dp[col - 1], prev));

                        // remember the max side we found so far
                        maxSide = Math.Max(maxSide, dp[col]);
                    } else {
                        // we need to explicitly set it 0, the previous row may have something else here
                        dp[col] = 0;
                    }

                    // remember what dp[col] was before the calculation (temp), for when we move to the next col
                    prev = temp; // that is dp[row - 1, col - 1] from Approach #1
                }
            }

            return maxSide * maxSide;
        }

        [Test]
        [TestCase("[[\"1\",\"0\",\"1\",\"0\",\"0\"],[\"1\",\"0\",\"1\",\"1\",\"1\"],[\"1\",\"1\",\"1\",\"1\",\"1\"],[\"1\",\"0\",\"0\",\"1\",\"0\"]]", ExpectedResult = 4)]
        [TestCase("[[\"1\"]]", ExpectedResult = 1)]
        public Int32 Test(String input) {
            var matrix = JsonConvert.DeserializeObject<Char[][]>(input);
            return this.MaximalSquare(matrix);
        }

    }

}
