using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     901. Online Stock Span
    ///     https://leetcode.com/problems/online-stock-span/
    /// </summary>
    [TestFixture]
    public class P00901 {

        [PublicAPI]
        public class StockSpanner : IEnumerable<Int32> {

            private readonly List<Int32> _list;
            private readonly Stack<Int32[]> _stack;

            public StockSpanner() {
                this._list = new List<Int32>();
                this._stack = new Stack<Int32[]>();
            }

            public Int32 Next(Int32 price) {
                this._list.Add(price);

                Int32 span = 1;
                while ((this._stack.Count != 0) && (this._stack.Peek()[0] <= price)) {
                    Int32[] pair = this._stack.Pop();
                    span += pair[1];
                }

                this._stack.Push(new[] {price, span});
                return span;
            }

            #region IEnumerable<Int32>

            IEnumerator<Int32> IEnumerable<Int32>.GetEnumerator() {
                return this._getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this._getEnumerator();
            }

            private List<Int32>.Enumerator _getEnumerator() {
                return this._list.GetEnumerator();
            }

            #endregion
        }

        [Test]
        [TestCase("[\"StockSpanner\",\"next\",\"next\",\"next\",\"next\",\"next\",\"next\",\"next\"]", "[[],[100],[80],[60],[70],[60],[75],[85]]", ExpectedResult = "[null,1,1,1,2,1,4,6]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<Int32[][]>(input2);

            var result = new List<Int32?>();

            StockSpanner stockSpanner = null;
            Console.WriteLine("StockSpanner stockSpanner = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "StockSpanner":
                        Console.WriteLine("stockSpanner = new StockSpanner();");
                        stockSpanner = new StockSpanner();
                        result.Add(null);
                        break;

                    case "next":
                        Console.Write("stockSpanner.Next({0});", parameters[i][0]);
                        Int32? next = stockSpanner?.Next(price: parameters[i][0]);
                        if (next != null) Console.Write("\t\t// returns: {0}", next);
                        if (stockSpanner != null) Console.WriteLine(" - the stock prices are [{0}]", String.Join(",", stockSpanner));
                        else Console.WriteLine();
                        result.Add(next);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
