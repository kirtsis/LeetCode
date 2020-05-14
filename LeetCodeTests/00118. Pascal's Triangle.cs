using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     118. Pascal's Triangle
    ///     https://leetcode.com/problems/pascals-triangle/
    /// </summary>
    [TestFixture]
    public class P00118 {

        [PublicAPI]
        public IList<IList<Int32>> Generate(Int32 numRows) {
            var result = new List<IList<Int32>>();
            this._build(result, numRows);
            return result;
        }

        private void _build(IList<IList<Int32>> result, Int32 row) {
            if (row == 0) return;

            if (row == 1) {
                result.Add(new List<Int32> {1});
                return;
            }

            this._build(result, row - 1);

            var list = new List<Int32>();

            list.Add(1);

            IList<Int32> prev = result[result.Count - 1];
            for (Int32 i = 0; i < prev.Count - 1; i++) {
                list.Add(prev[i] + prev[i + 1]);
            }

            list.Add(1);

            result.Add(list);
        }

        [Test]
        [TestCase(0, ExpectedResult = "[]")]
        [TestCase(1, ExpectedResult = "[[1]]")]
        [TestCase(2, ExpectedResult = "[[1],[1,1]]")]
        [TestCase(3, ExpectedResult = "[[1],[1,1],[1,2,1]]")]
        [TestCase(4, ExpectedResult = "[[1],[1,1],[1,2,1],[1,3,3,1]]")]
        [TestCase(5, ExpectedResult = "[[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]")]
        [TestCase(15, ExpectedResult = "[[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1],[1,5,10,10,5,1],[1,6,15,20,15,6,1],[1,7,21,35,35,21,7,1],[1,8,28,56,70,56,28,8,1],[1,9,36,84,126,126,84,36,9,1],[1,10,45,120,210,252,210,120,45,10,1],[1,11,55,165,330,462,462,330,165,55,11,1],[1,12,66,220,495,792,924,792,495,220,66,12,1],[1,13,78,286,715,1287,1716,1716,1287,715,286,78,13,1],[1,14,91,364,1001,2002,3003,3432,3003,2002,1001,364,91,14,1]]")]
        public String Test(Int32 numRows) {
            IList<IList<Int32>> result = this.Generate(numRows);
            return JsonConvert.SerializeObject(result);
        }

    }

}
