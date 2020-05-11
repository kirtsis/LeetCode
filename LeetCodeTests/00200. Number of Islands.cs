using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     200. Number of Islands
    ///     https://leetcode.com/problems/number-of-islands/
    /// </summary>
    [TestFixture]
    public class P00200 {

        [PublicAPI]
        public Int32 NumIslands(Char[][] grid) {
            if ((grid.Length <= 0) || (grid[0].Length <= 0)) return 0;

            Int32 result = 0;

            Int32 height = grid.Length;
            Int32 width = grid[0].Length;
            var visited = new Boolean[height, width];
            var directions = new List<Tuple<Int32, Int32>> {Tuple.Create(1, 0), Tuple.Create(0, 1), Tuple.Create(-1, 0), Tuple.Create(0, -1)};
            Func<Int32, Int32, Boolean> inbounds = (row, col) => (0 <= row) && (row < height) && (0 <= col) && (col < width);

            for (Int32 row = 0; row < height; row++) {
                for (Int32 col = 0; col < width; col++) {
                    if (visited[row, col] || (grid[row][col] != '1')) continue;

                    visited[row, col] = true;
                    result++;

                    var queue = new Queue<Tuple<Int32, Int32>>();
                    queue.Enqueue(Tuple.Create(row, col));
                    while (queue.Count > 0) {
                        Tuple<Int32, Int32> p = queue.Dequeue();
                        foreach (Tuple<Int32, Int32> dir in directions) {
                            Int32 nrow = p.Item1 + dir.Item1;
                            Int32 ncol = p.Item2 + dir.Item2;
                            if (!inbounds(nrow, ncol) || visited[nrow, ncol] || (grid[nrow][ncol] != '1')) continue;

                            visited[nrow, ncol] = true;
                            queue.Enqueue(Tuple.Create(nrow, ncol));
                        }
                    }
                }
            }

            return result;
        }

        [Test]
        [TestCase("[[\"1\",\"1\",\"1\",\"1\",\"0\"],[\"1\",\"1\",\"0\",\"1\",\"0\"],[\"1\",\"1\",\"0\",\"0\",\"0\"],[\"0\",\"0\",\"0\",\"0\",\"0\"]]", ExpectedResult = 1)]
        [TestCase("[[\"1\",\"1\",\"0\",\"0\",\"0\"],[\"1\",\"1\",\"0\",\"0\",\"0\"],[\"0\",\"0\",\"1\",\"0\",\"0\"],[\"0\",\"0\",\"0\",\"1\",\"1\"]]", ExpectedResult = 3)]
        public Int32 Test(String input) {
            var grid = JsonConvert.DeserializeObject<Char[][]>(input);
            return this.NumIslands(grid);
        }

    }

}
