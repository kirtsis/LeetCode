using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     733. Flood Fill
    ///     https://leetcode.com/problems/flood-fill/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00733 {

        [PublicAPI]
        public Int32[][] FloodFill(Int32[][] image, Int32 sr, Int32 sc, Int32 newColor) {
            // Note:
            // * The length of image and image[0] will be in the range [1, 50].
            // * The given starting pixel will satisfy 0 <= sr < image.length and 0 <= sc < image[0].length.
            // * The value of each color in image[i][j] and newColor will be an integer in [0, 65535].

            //return this._fill1(image, sr, sc, newColor);
            return this._fill2(image, sr, sc, newColor);
        }

        private Int32[][] _fill1(Int32[][] image, Int32 sr, Int32 sc, Int32 newColor) {
            Int32 oldColor = image[sr][sc];
            if (oldColor == newColor) return image;

            image[sr][sc] = newColor;

            Int32 height = image.Length;
            Int32 width = image[0].Length;
            Func<Int32, Int32, Boolean> inbounds = (row, col) => (0 <= row) && (row < height) && (0 <= col) && (col < width);
            var directions = new List<Tuple<Int32, Int32>> {Tuple.Create(1, 0), Tuple.Create(0, 1), Tuple.Create(-1, 0), Tuple.Create(0, -1)};
            foreach (Tuple<Int32, Int32> direction in directions) {
                Int32 nrow = sr + direction.Item1;
                Int32 ncol = sc + direction.Item2;
                if (!inbounds(nrow, ncol) || (image[nrow][ncol] != oldColor)) continue;

                image = this._fill1(image, nrow, ncol, newColor);
            }

            return image;
        }

        private Int32[][] _fill2(Int32[][] image, Int32 sr, Int32 sc, Int32 newColor) {
            Int32 oldColor = image[sr][sc];
            if (oldColor == newColor) return image;

            Int32 height = image.Length;
            Int32 width = image[0].Length;
            var directions = new[] {new[] {1, 0}, new[] {0, 1}, new[] {-1, 0}, new[] {0, -1}};

            var queue = new Queue<Int32[]>();
            image[sr][sc] = newColor;
            queue.Enqueue(new[] {sr, sc});
            while (queue.Count > 0) {
                Int32[] pixel = queue.Dequeue();
                foreach (Int32[] dir in directions) {
                    Int32 row = pixel[0] + dir[0];
                    Int32 col = pixel[1] + dir[1];
                    if ((row < 0) || (col < 0) || (row >= height) || (col >= width)) continue;
                    if (image[row][col] != oldColor) continue;

                    image[row][col] = newColor;
                    queue.Enqueue(new[] {row, col});
                }
            }

            return image;
        }

        [Test]
        [TestCase("[[1,1,1],[1,1,0],[1,0,1]]", 1, 1, 2, ExpectedResult = "[[2,2,2],[2,2,0],[2,0,1]]")]
        [TestCase("[[0,0,0],[0,1,1]]", 1, 1, 1, ExpectedResult = "[[0,0,0],[0,1,1]]")]
        [TestCase("[[0,0,0],[0,1,0]]", 1, 1, 2, ExpectedResult = "[[0,0,0],[0,2,0]]")]
        public String Test(String input, Int32 sr, Int32 sc, Int32 newColor) {
            var image = JsonConvert.DeserializeObject<Int32[][]>(input);
            Int32[][] result = this.FloodFill(image, sr, sc, newColor);
            return JsonConvert.SerializeObject(result);
        }

    }

}
