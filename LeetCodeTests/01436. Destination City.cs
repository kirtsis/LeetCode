using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1436. Destination City
    ///     https://leetcode.com/problems/destination-city/
    /// </summary>
    [TestFixture]
    public class P01436 {

        [PublicAPI]
        public String DestCity(IList<IList<String>> paths) {
            var origins = new HashSet<String>();
            var destinations = new HashSet<String>();
            foreach (IList<String> path in paths) {
                String origin = path[0];
                String destination = path[1];
                origins.Add(origin);
                if (!origins.Contains(destination)) destinations.Add(destination);
                if (destinations.Contains(origin)) destinations.Remove(origin);
            }

            return destinations.Single();
        }

        [Test]
        [TestCase("[[\"London\",\"New York\"],[\"New York\",\"Lima\"],[\"Lima\",\"Sao Paulo\"]]", ExpectedResult = "Sao Paulo")]
        [TestCase("[[\"B\",\"C\"],[\"D\",\"B\"],[\"C\",\"A\"]]", ExpectedResult = "A")]
        [TestCase("[[\"A\",\"Z\"]]", ExpectedResult = "Z")]
        public String Test(String input) {
            var paths = JsonConvert.DeserializeObject<IList<IList<String>>>(input);
            return this.DestCity(paths);
        }

    }

}
