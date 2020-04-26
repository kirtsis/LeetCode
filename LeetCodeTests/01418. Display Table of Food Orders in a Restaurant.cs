using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1418. Display Table of Food Orders in a Restaurant
    ///     https://leetcode.com/problems/display-table-of-food-orders-in-a-restaurant/
    /// </summary>
    [TestFixture]
    public class P01418 {

        [PublicAPI]
        public IList<IList<String>> DisplayTable(IList<IList<String>> orders) {
            var result = new List<IList<String>>();

            // order[1]: tableNumber, order[2]: foodItem
            // g1: foodItems per tableNumber | Key: tableNumber, Elements: foodItems
            // g2: foodItems per foodItem | Key: foodItem, Elements: foodItems
            IOrderedEnumerable<KeyValuePair<String, Dictionary<String, Int32>>> data = orders.GroupBy(order => order[1], order => order[2])
                                                                                             .ToDictionary(g1 => g1.Key, g1 => g1.GroupBy(foodItem => foodItem).ToDictionary(g2 => g2.Key, g2 => g2.Count()))
                                                                                             .OrderBy(table => Convert.ToInt32(table.Key));

            var headers = new List<String> {"Table"};
            headers.AddRange(data.SelectMany(table => table.Value.Select(food => food.Key)).Distinct().OrderBy(foodItem => foodItem, StringComparer.Ordinal));
            result.Add(headers);

            foreach (KeyValuePair<String, Dictionary<String, Int32>> table in data) {
                var tableRow = new List<String> {table.Key};
                tableRow.AddRange(headers.Skip(1).Select(foodItem => table.Value.ContainsKey(foodItem) ? Convert.ToString(table.Value[foodItem]) : "0"));
                result.Add(tableRow);
            }

            return result;
        }

        [Test]
        [TestCase("[[\"David\",\"3\",\"Ceviche\"],[\"Corina\",\"10\",\"Beef Burrito\"],[\"David\",\"3\",\"Fried Chicken\"],[\"Carla\",\"5\",\"Water\"],[\"Carla\",\"5\",\"Ceviche\"],[\"Rous\",\"3\",\"Ceviche\"]]", ExpectedResult = "[[\"Table\",\"Beef Burrito\",\"Ceviche\",\"Fried Chicken\",\"Water\"],[\"3\",\"0\",\"2\",\"1\",\"0\"],[\"5\",\"0\",\"1\",\"0\",\"1\"],[\"10\",\"1\",\"0\",\"0\",\"0\"]]")]
        [TestCase("[[\"pKKgO\",\"1\",\"qgGxK\"],[\"ZgW\",\"3\",\"XfuBe\"]]", ExpectedResult = "[[\"Table\",\"XfuBe\",\"qgGxK\"],[\"1\",\"0\",\"1\"],[\"3\",\"1\",\"0\"]]")]
        public String Test(String input) {
            var orders = JsonConvert.DeserializeObject<IList<IList<String>>>(input);
            IList<IList<String>> result = this.DisplayTable(orders);
            return JsonConvert.SerializeObject(result);
        }

    }

}
