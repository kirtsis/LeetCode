using System;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1029. Two City Scheduling
    ///     https://leetcode.com/problems/two-city-scheduling/
    /// </summary>
    [TestFixture]
    public class P01029 {

        [PublicAPI]
        public Int32 TwoCitySchedCost(Int32[][] costs) {
            // Note:
            // * 1 <= costs.length <= 100
            // * It is guaranteed that costs.length is even.
            // * 1 <= costs[i][0], costs[i][1] <= 1000

            //return costs.OrderBy(cost => cost[0] - cost[1]).Take(costs.Length / 2)
            //            .Zip(costs.OrderBy(cost => cost[0] - cost[1]).Skip(costs.Length / 2), (costA, costB) => new {cityA = costA[0], cityB = costB[1]})
            //            .Aggregate(0, (sum, cost) => sum + cost.cityA + cost.cityB);
            return costs.OrderBy(cost => cost[0] - cost[1])
                        .Select((cost, index) => index < costs.Length / 2 ? cost[0] : cost[1])
                        .Sum();
        }

        [Test]
        [TestCase("[[10,20],[30,200],[400,50],[30,20]]", ExpectedResult = 110)]
        [TestCase("[[33,135],[849,791],[422,469],[310,92],[253,489],[995,760],[852,197],[658,216],[679,945],[197,341],[362,648],[22,324],[408,25],[505,734],[463,279],[885,512],[922,850],[784,500],[557,860],[528,367],[877,741],[554,545],[598,888],[558,104],[426,427],[449,189],[113,51],[201,221],[251,62],[981,897],[392,519],[115,70],[961,109],[512,678],[476,708],[28,902],[763,282],[787,774],[925,475],[253,532],[100,502],[110,857],[822,942],[231,186],[869,491],[651,344],[239,806],[651,193],[830,360],[427,69],[776,875],[466,81],[520,959],[798,775],[875,199],[110,396]]", ExpectedResult = 20269)]
        public Int32 Test(String input) {
            var costs = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.TwoCitySchedCost(costs);
        }

    }

}
