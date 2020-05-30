using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     886. Possible Bipartition
    ///     https://leetcode.com/problems/possible-bipartition/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class P00886 {

        [PublicAPI]
        public Boolean PossibleBipartition(Int32 N, Int32[][] dislikes) {
            // Note:
            // * 1 <= N <= 2000
            // * 0 <= dislikes.length <= 10000
            // * 1 <= dislikes[i][j] <= N
            // * dislikes[i][0] < dislikes[i][1]
            // * There does not exist i != j for which dislikes[i] == dislikes[j].

            if (dislikes == null) return true;

            Int32 length = dislikes.Length;
            if (length == 0) return true;

            var graph = new Dictionary<Int32, ISet<Int32>>();
            for (Int32 index = 0; index < length; ++index) {
                Int32 person1 = dislikes[index][0];
                Int32 person2 = dislikes[index][1];

                if (!graph.ContainsKey(person1)) graph.Add(person1, new HashSet<Int32>());
                graph[person1].Add(person2);

                if (!graph.ContainsKey(person2)) graph.Add(person2, new HashSet<Int32>());
                graph[person2].Add(person1);
            }

            var colors = new Dictionary<Int32, Boolean>();
            for (Int32 person = 1; person <= N; ++person) {
                if (colors.ContainsKey(person)) continue;

                if (!this._dfs(graph, colors, person, true)) return false;
            }

            return true;
        }

        private Boolean _dfs(IDictionary<Int32, ISet<Int32>> graph, IDictionary<Int32, Boolean> colors, Int32 person, Boolean color) {
            if (colors.ContainsKey(person)) return colors[person] == color;

            colors.Add(person, color);
            return !graph.ContainsKey(person) || graph[person].All(disliked => this._dfs(graph, colors, disliked, !color));
        }

        [Test]
        [TestCase(4, "[[1,2],[1,3],[2,4]]", ExpectedResult = true)]
        [TestCase(3, "[[1,2],[1,3],[2,3]]", ExpectedResult = false)]
        [TestCase(5, "[[1,2],[2,3],[3,4],[4,5],[1,5]]", ExpectedResult = false)]
        public Boolean Test(Int32 N, String input) {
            var dislikes = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.PossibleBipartition(N, dislikes);
        }

    }

}
