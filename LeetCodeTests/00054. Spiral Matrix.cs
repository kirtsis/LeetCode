using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     54. Spiral Matrix
    ///     https://leetcode.com/problems/spiral-matrix/
    /// </summary>
    [TestFixture]
    public class P00054 {

        [PublicAPI]
        public IList<Int32> SpiralOrder(Int32[][] matrix) {
            var result = new List<Int32>();
            if (matrix == null) return result;

            Int32 rows = matrix.Length;
            if (rows == 0) return result;

            Int32 cols = matrix[0].Length;
            if (cols == 0) return result;

            Int32 minRow = 0;
            Int32 maxRow = rows - 1;
            Int32 minCol = 0;
            Int32 maxCol = cols - 1;
            var directions = new[] {new[] {0, 1}, new[] {1, 0}, new[] {0, -1}, new[] {-1, 0}}; // 4 directions: (> right), (v down), (< left), (^ up)

            Int32 row = 0;
            Int32 col = 0;
            Int32 direction = 0; // direction 0 (> right)
            Int32 totalElements = rows * cols;
            while (totalElements > 0) {
                result.Add(matrix[row][col]);

                // move towards the direction
                row += directions[direction][0];
                col += directions[direction][1];

                // check if after the move we went out of bounds
                if ((row > maxRow) || (row < minRow) || (col > maxCol) || (col < minCol)) {
                    // adjust bounds edges
                    // we can also use switch on direction to do the same (not sure what is clearer)
                    if (row > maxRow) maxCol--; // move was in direction 1 (v down)
                    else if (row < minRow) minCol++; // move was in direction 3 (^ up)
                    else if (col > maxCol) minRow++; // move was in direction 0 (> right)
                    else if (col < minCol) maxRow--; // move was in direction 2 (< left)

                    // undo move
                    row -= directions[direction][0];
                    col -= directions[direction][1];

                    // change direction
                    direction = ++direction % 4;

                    // move towards the new direction
                    row += directions[direction][0];
                    col += directions[direction][1];
                }

                --totalElements;
            }

            return result;
        }

        [Test]
        [TestCase("[]", ExpectedResult = "[]")]
        [TestCase("[[]]", ExpectedResult = "[]")]
        [TestCase("[[],[]]", ExpectedResult = "[]")]
        [TestCase("[[1,2]]", ExpectedResult = "[1,2]")]
        [TestCase("[[1],[2],[3]]", ExpectedResult = "[1,2,3]")]
        [TestCase("[[1,2,3],[4,5,6],[7,8,9]]", ExpectedResult = "[1,2,3,6,9,8,7,4,5]")]
        [TestCase("[[1,2,3,4],[5,6,7,8],[9,10,11,12]]", ExpectedResult = "[1,2,3,4,8,12,11,10,9,5,6,7]")]
        public String Test(String input) {
            var matrix = JsonConvert.DeserializeObject<Int32[][]>(input);
            IList<Int32> result = this.SpiralOrder(matrix);
            return JsonConvert.SerializeObject(result);
        }

    }

}
