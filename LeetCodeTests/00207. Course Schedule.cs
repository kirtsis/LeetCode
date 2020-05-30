using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     207. Course Schedule
    ///     https://leetcode.com/problems/course-schedule/
    /// </summary>
    [TestFixture]
    public class P00207 {

        [PublicAPI]
        public Boolean CanFinish(Int32 numCourses, Int32[][] prerequisites) {
            // Constraints:
            // * The input prerequisites is a graph represented by a list of edges, not adjacency matrices. Read more about how a graph is represented.
            // * You may assume that there are no duplicate edges in the input prerequisites.
            // * 1 <= numCourses <= 10^5

            if (numCourses == 1) return true;

            Int32 length = prerequisites.Length;
            if (length <= 1) return true;

            var dependencies = new Dictionary<Int32, IList<Int32>>();
            for (Int32 course = 0; course < numCourses; ++course) {
                dependencies.Add(course, new List<Int32>());
            }
            for (Int32 index = 0; index < length; ++index) {
                dependencies[prerequisites[index][1]].Add(prerequisites[index][0]);
            }

            var indegree = new Int32[numCourses];
            for (Int32 index = 0; index < length; ++index) {
                indegree[prerequisites[index][0]]++;
            }

            var queue = new Queue<Int32>();
            for (Int32 course = 0; course < numCourses; ++course) {
                if (indegree[course] == 0) queue.Enqueue(course);
            }

            Int32 coursesTaken = 0;
            while (queue.Count > 0) {
                Int32 course = queue.Dequeue();
                coursesTaken++;
                foreach (Int32 dependency in dependencies[course]) {
                    indegree[dependency]--;
                    if (indegree[dependency] == 0) queue.Enqueue(dependency);
                }
            }

            return coursesTaken == numCourses;
        }

        [Test]
        [TestCase(2, "[[1,0]]", ExpectedResult = true)]
        [TestCase(2, "[[1,0],[0,1]]", ExpectedResult = false)]
        public Boolean Test(Int32 numCourses, String input) {
            var prerequisites = JsonConvert.DeserializeObject<Int32[][]>(input);
            return this.CanFinish(numCourses, prerequisites);
        }

    }

}
