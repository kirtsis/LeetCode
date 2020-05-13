using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     225. Implement Stack using Queues
    ///     https://leetcode.com/problems/implement-stack-using-queues/
    /// </summary>
    [TestFixture]
    public class P00225 {

        [PublicAPI]
        public class MyStack : IEnumerable<Int32> {

            private Queue<Int32> _queue;

            public MyStack() {
                this._queue = new Queue<Int32>();
            }

            public void Push(Int32 x) {
                this._queue.Enqueue(x);
                Int32 length = this._queue.Count;
                while (length > 1) {
                    this._queue.Enqueue(this._queue.Dequeue());
                    length--;
                }
            }

            public Int32 Pop() {
                return this._queue.Dequeue();
            }

            public Int32 Top() {
                return this._queue.Peek();
            }

            public Boolean Empty() {
                return this._queue.Count == 0;
            }

            #region IEnumerable<Int32>

            IEnumerator<Int32> IEnumerable<Int32>.GetEnumerator() {
                return this._getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this._getEnumerator();
            }

            private List<Int32>.Enumerator _getEnumerator() {
                List<Int32> list = this._queue.ToList();
                list.Reverse();
                return list.GetEnumerator();
            }

            #endregion
        }

        [Test]
        [TestCase("[\"MyStack\",\"push\",\"push\",\"top\",\"pop\",\"empty\"]", "[[],[1],[2],[],[],[]]", ExpectedResult = "[null,null,null,2,2,false]")]
        [TestCase("[\"MyStack\",\"push\",\"push\",\"push\",\"push\",\"push\",\"top\",\"pop\",\"pop\",\"pop\",\"empty\"]", "[[],[1],[2],[3],[4],[5],[],[],[],[],[]]", ExpectedResult = "[null,null,null,null,null,null,5,5,4,3,false]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<Object[][]>(input2);

            var result = new List<Object>();

            MyStack stack = null;
            Console.WriteLine("MyStack stack = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "MyStack":
                        Console.WriteLine("stack = new MyStack();");
                        stack = new MyStack();
                        result.Add(null);
                        break;

                    case "push":
                        Console.Write("stack.push({0})", (Int32)(Int64)parameters[i][0]);
                        stack?.Push(x: (Int32)(Int64)parameters[i][0]);
                        if (stack != null) Console.WriteLine("\t\t// the stack is [{0}] <- top", String.Join(",", stack));
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "pop":
                        Console.Write("stack.Pop()");
                        Int32? pop = stack?.Pop();
                        if (pop != null) Console.Write("\t\t// returns: {0}", pop);
                        if (stack != null) Console.WriteLine(" - the stack is [{0}] <- top", String.Join(",", stack));
                        else Console.WriteLine();
                        result.Add(pop);
                        break;

                    case "top":
                        Console.Write("stack.Top()");
                        Int32? top = stack?.Top();
                        if (top != null) Console.WriteLine("\t\t// returns: {0}", top);
                        else Console.WriteLine();
                        result.Add(top);
                        break;

                    case "empty":
                        Console.Write("stack.Empty()");
                        Boolean? empty = stack?.Empty();
                        if (empty != null) Console.WriteLine("\t\t// returns: {0}", empty);
                        else Console.WriteLine();
                        result.Add(empty);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
