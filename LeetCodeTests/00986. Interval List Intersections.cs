using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     986. Interval List Intersections
    ///     https://leetcode.com/problems/interval-list-intersections/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class P00986 {

        [PublicAPI]
        public Int32[][] IntervalIntersection(Int32[][] A, Int32[][] B) {
            // Note:
            // * 0 <= A.length < 1000
            // * 0 <= B.length < 1000
            // * 0 <= A[i].start, A[i].end, B[i].start, B[i].end < 10^9
            // NOTE: input types have been changed on April 15, 2019. Please reset to default code definition to get new method signature.

            var result = new List<Int32[]>();

            Int32 lengthA = A.Length;
            Int32 lengthB = B.Length;
            if ((lengthA == 0) || (lengthB == 0)) return result.ToArray();

            Int32 startIndexB = 0;
            for (Int32 indexA = 0; indexA < lengthA; ++indexA) {
                Int32 startA = A[indexA][0];
                Int32 endA = A[indexA][1];
                for (Int32 indexB = startIndexB; indexB < lengthB; ++indexB) {
                    Int32 startB = B[indexB][0];
                    Int32 endB = B[indexB][1];

                    if (startA > endB) continue;

                    if (endA < startB) break;

                    var intersection = new[] {
                        Math.Max(startA, startB),
                        Math.Min(endA, endB)
                    };
                    result.Add(intersection);

                    if (indexA + 1 >= lengthA) continue;

                    Int32 startOfNextA = A[indexA + 1][0];
                    if (endB < startOfNextA) startIndexB++;
                }
            }

            return result.ToArray();
        }

        [Test]
        [TestCase("[[0,2],[5,10],[13,23],[24,25]]", "[[1,5],[8,12],[15,24],[25,26]]", ExpectedResult = "[[1,2],[5,5],[8,10],[15,23],[24,24],[25,25]]")]
        [TestCase("[]", "[[1,5],[8,12],[15,24],[25,26]]", ExpectedResult = "[]")]
        [TestCase("[[0,2],[5,10],[13,23],[24,25]]", "[]", ExpectedResult = "[]")]
        [TestCase("[[0,2],[5,10]]", "[[15,24],[25,26]]", ExpectedResult = "[]")]
        [TestCase("[]", "[]", ExpectedResult = "[]")]
        [TestCase("[[14,16]]", "[[7, 13],[16, 20]]", ExpectedResult = "[[16,16]]")]
        public String Test(String input1, String input2) {
            var A = JsonConvert.DeserializeObject<Int32[][]>(input1);
            var B = JsonConvert.DeserializeObject<Int32[][]>(input2);
            Int32[][] result = this.IntervalIntersection(A, B);
            return JsonConvert.SerializeObject(result);
        }

    }

}
