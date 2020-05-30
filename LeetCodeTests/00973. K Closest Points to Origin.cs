using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     973. K Closest Points to Origin
    ///     https://leetcode.com/problems/k-closest-points-to-origin/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00973 {

        [PublicAPI]
        public Int32[][] KClosest(Int32[][] points, Int32 K) {
            // Note:
            // * 1 <= K <= points.length <= 10000
            // * -10000 < points[i][0] < 10000
            // * -10000 < points[i][1] < 10000

            Int32 length = points.Length;
            if (length <= K) return points;

            //return this._linq(points, K);
            return this._quickSelect(points, length, K);
        }

        private Int32[][] _linq(Int32[][] points, Int32 K) {
            return points.OrderBy(point => point[0] * point[0] + point[1] * point[1])
                         .Take(K)
                         .ToArray();
        }

        private Int32[][] _quickSelect(Int32[][] points, Int32 length, Int32 K) {
            Int32 left = 0;
            Int32 right = length - 1;
            while (left <= right) {
                Int32 partitioningIndex = this._quickSortPartition(points, left, right);
                if (partitioningIndex == K - 1) break;

                if (partitioningIndex < K - 1) left = partitioningIndex + 1;
                else right = partitioningIndex - 1;
            }

            return points.Take(K).ToArray();
        }

        private Int32 _quickSortPartition(Int32[][] points, Int32 left, Int32 right) {
            Int32 pivotDistance = points[right][0] * points[right][0] + points[right][1] * points[right][1];

            Int32 partitioningIndex = left - 1;
            for (Int32 index = left; index < right; ++index) {
                Int32 distance = points[index][0] * points[index][0] + points[index][1] * points[index][1];
                if (distance >= pivotDistance) continue;

                partitioningIndex++;
                Int32[] temp1 = points[partitioningIndex];
                points[partitioningIndex] = points[index];
                points[index] = temp1;
            }

            partitioningIndex++;
            Int32[] temp2 = points[partitioningIndex];
            points[partitioningIndex] = points[right];
            points[right] = temp2;

            return partitioningIndex;
        }

        [Test]
        [TestCase("[[1,3],[-2,2]]", 1, ExpectedResult = "[[-2,2]]")]
        [TestCase("[[3,3],[5,-1],[-2,4]]", 2, ExpectedResult = "[[3,3],[-2,4]]")]
        [TestCase("[[3,3],[5,-1],[-2,4]]", 3, ExpectedResult = "[[3,3],[5,-1],[-2,4]]")]
        public String Test(String input, Int32 K) {
            var points = JsonConvert.DeserializeObject<Int32[][]>(input);
            Int32[][] result = this.KClosest(points, K);
            return JsonConvert.SerializeObject(result);
        }

    }

}
