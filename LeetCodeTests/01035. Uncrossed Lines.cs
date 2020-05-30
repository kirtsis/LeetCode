using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1035. Uncrossed Lines
    ///     https://leetcode.com/problems/uncrossed-lines/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P01035 {

        [PublicAPI]
        public Int32 MaxUncrossedLines(Int32[] A, Int32[] B) {
            // Note:
            // * 1 <= A.length <= 500
            // * 1 <= B.length <= 500
            // * 1 <= A[i], B[i] <= 2000

            Int32 lengthA = A.Length;
            Int32 lengthB = B.Length;

            //return this._recursive(A, lengthA, B, lengthB);
            return this._dp(A, lengthA, B, lengthB);
        }

        private Int32 _recursive(Int32[] A, Int32 lengthA, Int32[] B, Int32 lengthB) {
            var memory = new Int32?[lengthA, lengthB];
            return this._calculate(0, 0, A, lengthA, B, lengthB, memory);
        }

        private Int32 _calculate(Int32 indexA, Int32 indexB, Int32[] A, Int32 lengthA, Int32[] B, Int32 lengthB, Int32?[,] memory) {
            if (indexA >= lengthA) return 0;
            if (indexB >= lengthB) return 0;
            if (memory[indexA, indexB] != null) return memory[indexA, indexB].Value;

            Int32 result = A[indexA] == B[indexB]
                               ? 1 + this._calculate(indexA + 1, indexB + 1, A, lengthA, B, lengthB, memory)
                               : Math.Max(
                                   this._calculate(indexA + 1, indexB, A, lengthA, B, lengthB, memory),
                                   this._calculate(indexA, indexB + 1, A, lengthA, B, lengthB, memory)
                               );
            memory[indexA, indexB] = result;
            return result;
        }

        private Int32 _dp(Int32[] A, Int32 lengthA, Int32[] B, Int32 lengthB) {
            var dp = new Int32[lengthA + 1, lengthB + 1];

            for (Int32 indexA = 1; indexA <= lengthA; ++indexA) {
                for (Int32 indexB = 1; indexB <= lengthB; ++indexB) {
                    dp[indexA, indexB] = A[indexA - 1] == B[indexB - 1]
                                             ? 1 + dp[indexA - 1, indexB - 1]
                                             : Math.Max(dp[indexA - 1, indexB], dp[indexA, indexB - 1]);
                }
            }

            return dp[lengthA, lengthB];
        }

        [Test]
        [TestCase("[1,4,2]", "[1,2,4]", ExpectedResult = 2)]
        [TestCase("[2,5,1,2,5]", "[10,5,2,1,5,2]", ExpectedResult = 3)]
        [TestCase("[1,3,7,1,7,5]", "[1,9,2,5,1]", ExpectedResult = 2)]
        public Int32 Test(String input1, String input2) {
            var A = JsonConvert.DeserializeObject<Int32[]>(input1);
            var B = JsonConvert.DeserializeObject<Int32[]>(input2);
            return this.MaxUncrossedLines(A, B);
        }

    }

}
