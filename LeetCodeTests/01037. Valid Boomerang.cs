using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1037. Valid Boomerang
    ///     https://leetcode.com/problems/valid-boomerang/
    /// </summary>
    [TestFixture]
    public class P01037 {

        [PublicAPI]
        public Boolean IsBoomerang(Int32[][] points) {
            // Note:
            // * points.length == 3
            // * points[i].length == 2
            // * 0 <= points[i][j] <= 100

            // 2 points, p0(x0,y0) and p1(x1,y1), always form a line with slope m = Δy/Δx = (y1 - y0) / (x1 - x0)
            // 3 points, p0(x0,y0), p1(x1,y1) and p2(x2,y2), are collinear (on the same line) if and only if p0/p1 slope is equal to p0/p2 slope (or to p1/p2 slope, it doesn't matter what we choose)
            // since we only need to know if the 2 slopes are the same,
            // to avoid DivideByZeroException when any 2 points form a vertical line (have equal x),
            // we can rearrange things as follows
            //   p0/p1 slope = p0/p2 slope
            //   => Δy(p1, p0) / Δx(p1, p0) = Δy(p2, p0) / Δx(p2, p0)
            //   => Δx(p2, p0) * Δy(p1, p0) = Δx(p1, p0) * Δy(p2, p0)

            // with p0 = points[0], p1 = points[1] and p2 = points[2]
            // the 3 points are collinear if: (p2[0] - p0[0]) * (p1[1] - p0[1]) == (p1[0] - p0[0]) * (p2[1] - p0[1]);
            // so, the 3 points form a boomerang if and only if they are not collinear (we just have to change == with !=)
            return (points[2][0] - points[0][0]) * (points[1][1] - points[0][1]) != (points[1][0] - points[0][0]) * (points[2][1] - points[0][1]);
        }

        [Test]
        [TestCase("[[1,1],[2,3],[3,2]]", ExpectedResult = true)]
        [TestCase("[[1,1],[2,2],[3,3]]", ExpectedResult = false)]
        public Boolean Test(String input) {
            var points = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.IsBoomerang(points);
        }

    }

}
