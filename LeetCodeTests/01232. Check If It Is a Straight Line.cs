using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1232. Check If It Is a Straight Line
    ///     https://leetcode.com/problems/check-if-it-is-a-straight-line/
    /// </summary>
    [TestFixture]
    public class P01232 {

        [PublicAPI]
        public Boolean CheckStraightLine(Int32[][] coordinates) {
            // Constraints:
            // * 2 <= coordinates.length <= 1000
            // * coordinates[i].length == 2
            // * -10^4 <= coordinates[i][0], coordinates[i][1] <= 10^4
            // * coordinates contains no duplicate point.

            Int32 length = coordinates.Length;

            // 2 points always form a line
            if (length == 2) return true;

            // get the first two points
            Int32[] p1 = coordinates[0]; // point p1 with coordinates (x1, y1) = (p1[0], p1[1])
            Int32[] p2 = coordinates[1]; // point p2 with coordinates (x2, y2) = (p2[0], p2[1])

            // and calculate the slope of the line they form
            // slope m = Δy/Δx = (y2 - y1) / (x2 - x1)
            // p1/p2 slope m = (p2[1] - p1[1]) / (p2[0] - p1[0])
            // this fails with DivideByZeroException when the line they form is a vertical line (x1 = x2, p1[0] = p2[0])

            for (Int32 index = 2; index < length; ++index) {
                Int32[] p = coordinates[index];

                // p1/p slope m = (p[1] - p1[1]) / (p[0] - p1[0])
                // same problem with DivideByZeroException

                // the 3 points are collinear (on the same line) if p1/p2 slope is equal to p1/p slope
                // since we only need to know if the 2 slopes are the same
                // we can rearrange things as follows
                //   p1/p2 slope = p1/p slope
                //   => Δy(p2, p1) / Δx(p2, p1) = Δy(p, p1) / Δx(p, p1)
                //   => Δx(p, p1) * Δy(p2, p1) = Δx(p2, p1) * Δy(p, p1)

                // with our variables in c#:
                // Boolean collinear = (p[0] - p1[0]) * (p2[1] - p1[1]) == (p2[0] - p1[0]) * (p[1] - p1[1]);
                // return false if not collinear
                if ((p[0] - p1[0]) * (p2[1] - p1[1]) != (p2[0] - p1[0]) * (p[1] - p1[1])) return false;
            }

            return true;
        }

        [Test]
        [TestCase("[[1,2],[2,3],[3,4],[4,5],[5,6],[6,7]]", ExpectedResult = true)]
        [TestCase("[[1,1],[2,2],[3,4],[4,5],[5,6],[7,7]]", ExpectedResult = false)]
        public Boolean Test(String input) {
            var coordinates = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.CheckStraightLine(coordinates);
        }

    }

}
