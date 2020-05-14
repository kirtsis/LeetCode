using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     498. Diagonal Traverse
    ///     https://leetcode.com/problems/diagonal-traverse/
    /// </summary>
    [TestFixture]
    public class P00498 {

        [PublicAPI]
        public Int32[] FindDiagonalOrder(Int32[][] matrix) {
            // Note:
            // The total number of elements of the given matrix will not exceed 10,000.

            if (matrix == null) return new Int32[0];

            Int32 rows = matrix.Length;
            if (rows == 0) return new Int32[0];

            Int32 elements = 0;
            Int32 maxDiagonal = 0;
            var diagonals = new Dictionary<Int32, IList<Int32>>();
            for (Int32 row = 0; row < rows; ++row) {
                if (matrix[row] == null) continue;

                Int32 cols = matrix[row].Length;
                for (Int32 col = 0; col < cols; ++col) {
                    elements++;
                    Int32 diagonal = row + col;
                    maxDiagonal = Math.Max(maxDiagonal, diagonal);
                    if (!diagonals.ContainsKey(diagonal)) diagonals.Add(diagonal, new List<Int32>());
                    diagonals[diagonal].Add(matrix[row][col]);
                }
            }

            Int32 resultIndex = 0;
            var result = new Int32[elements];
            if (elements == 0) return result;

            Boolean up = true;
            for (Int32 diagonal = 0; diagonal <= maxDiagonal; ++diagonal) {
                if (!diagonals.ContainsKey(diagonal)) continue;

                Int32 diagonalLength = diagonals[diagonal].Count;
                for (Int32 index = 0; index < diagonalLength; ++index) {
                    result[resultIndex] = up ? diagonals[diagonal][diagonalLength - 1 - index] : diagonals[diagonal][index];
                    resultIndex++;
                }

                up = !up;
            }

            return result;
        }

        [Test]
        [TestCase("[]", ExpectedResult = "[]")]
        [TestCase("[[]]", ExpectedResult = "[]")]
        [TestCase("[null,[]]", ExpectedResult = "[]")]
        [TestCase("[[1]]", ExpectedResult = "[1]")]
        [TestCase("[null,[1]]", ExpectedResult = "[1]")]
        [TestCase("[[1,2,3],[4,5,6],[7,8,9]]", ExpectedResult = "[1,2,4,7,5,3,6,8,9]")]
        public String Test(String input) {
            var matrix = JsonConvert.DeserializeObject<Int32[][]>(input);
            Int32[] result = this.FindDiagonalOrder(matrix);
            return JsonConvert.SerializeObject(result);
        }

    }

}
