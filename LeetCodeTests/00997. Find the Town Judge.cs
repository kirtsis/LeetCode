using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     997. Find the Town Judge
    ///     https://leetcode.com/problems/find-the-town-judge/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class P00997 {

        [PublicAPI]
        public Int32 FindJudge(Int32 N, Int32[][] trust) {
            // Note:
            // * 1 <= N <= 1000
            // * trust.length <= 10000
            // * trust[i] are all different
            // * trust[i][0] != trust[i][1]
            // * 1 <= trust[i][0], trust[i][1] <= N

            return this._find1(N, trust);
        }

        private Int32 _find1(Int32 N, Int32[][] trust) {
            if (N == 1) return 1;

            var cannotBeJudge = new HashSet<Int32>();
            var judgeCandidates = new Dictionary<Int32, Int32>();
            Int32 length = trust.Length;
            for (Int32 index = 0; index < length; ++index) {
                Int32 person = trust[index][0];
                cannotBeJudge.Add(person);
                if (judgeCandidates.ContainsKey(person)) judgeCandidates.Remove(person);

                Int32 trusty = trust[index][1];
                if (cannotBeJudge.Contains(trusty)) continue;

                if (!judgeCandidates.ContainsKey(trusty)) judgeCandidates.Add(trusty, 0);
                judgeCandidates[trusty]++;
            }

            if (judgeCandidates.Count != 1) return -1;

            KeyValuePair<Int32, Int32> judgeCandidate = judgeCandidates.Single();
            return judgeCandidate.Value == N - 1 ? judgeCandidate.Key : -1;
        }

        [Test]
        [TestCase(2, "[[1,2]]", ExpectedResult = 2)]
        [TestCase(3, "[[1,3],[2,3]]", ExpectedResult = 3)]
        [TestCase(3, "[[1,3],[2,3],[3,1]]", ExpectedResult = -1)]
        [TestCase(3, "[[1,2],[2,3]]", ExpectedResult = -1)]
        [TestCase(4, "[[1,3],[1,4],[2,3],[2,4],[4,3]]", ExpectedResult = 3)]
        [TestCase(1, "[]", ExpectedResult = 1)]
        public Int32 Test(Int32 N, String input) {
            var trust = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.FindJudge(N, trust);
        }

    }

}
