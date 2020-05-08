using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1439. Find the Kth Smallest Sum of a Matrix With Sorted Rows
    ///     https://leetcode.com/problems/find-the-kth-smallest-sum-of-a-matrix-with-sorted-rows/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01439 {

        [PublicAPI]
        public Int32 KthSmallest(Int32[][] mat, Int32 k) {
            //return this._smallest1(mat, k);
            return this._smallest2(mat, k);
        }

        private Int32 _smallest1(Int32[][] mat, Int32 k) {
            // Constraints:
            // * m == mat.length
            // * n == mat.length[i]
            // * 1 <= m, n <= 40
            // * 1 <= k <= min(200, n ^ m)
            // * 1 <= mat[i][j] <= 5000
            // * mat[i] is a non decreasing array.

            Int32 rows = mat.Length;
            Int32 cols = mat[0].Length;

            // start with one smallest sum that has value zero
            var smallestSums = new Int32[1];
            for (Int32 row = 0; row < rows; ++row) {
                // calculate new possible smallest sums
                // that is the sum of all possible pairs
                // between all values of our current smallest sums (smallestSums[] size can be 1, cols, cols^2, cols^3, ..., cols^n with maximum size of k)
                // and all the values of the columns of this row (mat[row][] size is cols)
                Int32 length = smallestSums.Length;
                var newSums = new Int32[cols * length];
                for (Int32 col = 0; col < cols; ++col) {
                    for (Int32 index = 0; index < length; ++index) {
                        newSums[col * length + index] = smallestSums[index] + mat[row][col];
                    }
                }

                // sort the new possible smallest sums
                // and keep up to k smallest sums at most
                //Array.Sort(sums);
                //Array.Resize(ref sums, Math.Min(k, cols * length));
                smallestSums = newSums.OrderBy(o => o).Take(k).ToArray();
            }

            return smallestSums[k - 1];
        }

        private Int32 _smallest2(Int32[][] mat, Int32 k) {
            //return mat.Aggregate(new List<Int32> {0}, (sums, row) => row.SelectMany(col => sums.Select(sum => sum + col)).OrderBy(sum => sum).Take(k).ToList()).Last();
            return mat.Aggregate(new[] {0}, (sums, row) => row.SelectMany(col => sums.Select(sum => sum + col)).OrderBy(sum => sum).Take(k).ToArray()).Last();
        }

        [Test]
        [TestCase("[[1,3,11],[2,4,6]]", 5, ExpectedResult = 7)]
        [TestCase("[[1,3,11],[2,4,6]]", 9, ExpectedResult = 17)]
        [TestCase("[[1,10,10],[1,4,5],[2,3,6]]", 7, ExpectedResult = 9)]
        [TestCase("[[1,1,10],[2,2,9]]", 7, ExpectedResult = 12)]
        [TestCase("[[21,33,38,41,42,44,48,54,62],[8,51,56,60,80,81,83,88,92],[2,40,54,56,68,92,93,94,98],[28,35,41,52,55,59,62,70,99],[2,16,25,30,32,38,54,73,92],[1,11,14,35,35,55,59,66,66],[5,9,13,29,47,49,60,73,74],[19,29,32,36,59,64,66,98,100]]", 29, ExpectedResult = 106)]
        public Int32 Test(String input, Int32 k) {
            var mat = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.KthSmallest(mat, k);
        }

    }

}
