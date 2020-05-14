using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     707. Design Linked List
    ///     https://leetcode.com/problems/design-linked-list/
    /// </summary>
    [TestFixture]
    public class P00707 {

        [PublicAPI]
        public class MyLinkedList : IEnumerable<Int32> {

            private Node _head;
            private Int32 _length;

            public MyLinkedList() {
                /** Initialize your data structure here. */
                this._head = null;
                this._length = 0;
            }

            public Int32 Get(Int32 index) {
                /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
                if ((index < 0) || (index >= this._length)) return -1;

                return this._getNthNode(index).Value;
            }

            public void AddAtHead(Int32 val) {
                /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
                var node = new Node {Value = val};
                if (this._head == null) this._insertOnEmpty(node);
                else this._insertBeforeNode(this._head, node);
                this._head = node;
            }

            public void AddAtTail(Int32 val) {
                /** Append a node of value val to the last element of the linked list. */
                var node = new Node {Value = val};
                if (this._head == null) this._insertOnEmpty(node);
                else this._insertBeforeNode(this._head, node);
            }

            public void AddAtIndex(Int32 index, Int32 val) {
                /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
                // If index is greater than the length, the node will not be inserted.
                if ((index < 0) || (index > this._length)) return;

                // If index equals to the length of linked list, the node will be appended to the end of linked list.
                if (index == this._length) {
                    this.AddAtTail(val);
                    return;
                }

                // Add a node of value val before the index-th node in the linked list.
                Node nthNode = this._getNthNode(index);
                var node = new Node {Value = val};
                this._insertBeforeNode(nthNode, node);
                if (nthNode == this._head) this._head = node;
            }

            public void DeleteAtIndex(Int32 index) {
                /** Delete the index-th node in the linked list, if the index is valid. */
                if ((index < 0) || (index >= this._length)) return;

                Node node = this._getNthNode(index);
                if (node.Next == node) this._head = null;
                else {
                    node.Next.Prev = node.Prev;
                    node.Prev.Next = node.Next;
                    if (this._head == node) this._head = node.Next;
                }
                this._length--;
            }

            private Node _getNthNode(Int32 index) {
                Node result = this._head;
                if (index + 1 <= this._length - index) {
                    for (Int32 i = 0; i < index; i++) {
                        result = result.Next;
                    }
                } else {
                    for (Int32 i = 0; i < this._length - index; i++) {
                        result = result.Prev;
                    }
                }

                return result;
            }

            private void _insertOnEmpty(Node newNode) {
                newNode.Next = newNode;
                newNode.Prev = newNode;
                this._head = newNode;
                this._length++;
            }

            private void _insertBeforeNode(Node node, Node newNode) {
                newNode.Next = node;
                newNode.Prev = node.Prev;
                node.Prev.Next = newNode;
                node.Prev = newNode;
                this._length++;
            }

            private class Node {

                public Node Next;
                public Node Prev;
                public Int32 Value;

            }

            #region IEnumerable<Int32>

            IEnumerator<Int32> IEnumerable<Int32>.GetEnumerator() {
                return this._getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this._getEnumerator();
            }

            private Enumerator _getEnumerator() {
                return new Enumerator(this);
            }

            private struct Enumerator : IEnumerator<Int32> {

                private readonly MyLinkedList _list;
                private Node _node;
                private Int32 _index;

                public Enumerator(MyLinkedList list) {
                    this._list = list;
                    this._node = list._head;
                    this.Current = 0;
                    this._index = 0;
                }

                public Int32 Current { get; private set; }

                Object IEnumerator.Current {
                    get {
                        if ((this._index == 0) || (this._index == this._list._length + 1)) throw new InvalidOperationException();

                        return this.Current;
                    }
                }

                public Boolean MoveNext() {
                    if (this._node == null) {
                        this._index = this._list._length + 1;
                        return false;
                    }

                    this._index++;
                    this.Current = this._node.Value;
                    this._node = this._node.Next;
                    if (this._node == this._list._head) this._node = null;
                    return true;
                }

                void IEnumerator.Reset() {
                    this._node = this._list._head;
                    this._index = 0;
                    this.Current = 0;
                }

                public void Dispose() {}

            }

            #endregion
        }

        [Test]
        [TestCase("[\"MyLinkedList\",\"addAtHead\",\"addAtTail\",\"addAtIndex\",\"get\",\"deleteAtIndex\",\"get\"]", "[[],[1],[3],[1,2],[1],[1],[1]]", ExpectedResult = "[null,null,null,null,2,null,3]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<Int32[][]>(input2);

            var result = new List<Int32?>();

            MyLinkedList linkedList = null;
            Console.WriteLine("MyLinkedList linkedList = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "MyLinkedList":
                        Console.WriteLine("linkedList = new MyLinkedList(); // Initialize empty LinkedList");
                        linkedList = new MyLinkedList();
                        result.Add(null);
                        break;

                    case "get":
                        Console.Write("linkedList.Get({0});", parameters[i][0]);
                        Int32? value = linkedList?.Get(index: parameters[i][0]);
                        if (value != null) Console.WriteLine("\t\t// returns: {0}{1}", value, value == -1 ? " (not found)" : null);
                        else Console.WriteLine();
                        result.Add(value);
                        break;

                    case "addAtHead":
                        Console.Write("linkedList.AddAtHead({0});", parameters[i][0]);
                        linkedList?.AddAtHead(val: parameters[i][0]);
                        if (linkedList != null) Console.WriteLine("\t\t// the linked list is {0}", String.Join("->", linkedList));
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "addAtTail":
                        Console.Write("linkedList.AddAtTail({0});", parameters[i][0]);
                        linkedList?.AddAtTail(val: parameters[i][0]);
                        if (linkedList != null) Console.WriteLine("\t\t// the linked list is {0}", String.Join("->", linkedList));
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "addAtIndex":
                        Console.Write("linkedList.AddAtIndex({0}, {1});", parameters[i][0], parameters[i][1]);
                        linkedList?.AddAtIndex(index: parameters[i][0], val: parameters[i][1]);
                        if (linkedList != null) Console.WriteLine("\t\t// the linked list is {0}", String.Join("->", linkedList));
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "deleteAtIndex":
                        Console.Write("linkedList.DeleteAtIndex({0});", parameters[i][0]);
                        linkedList?.DeleteAtIndex(index: parameters[i][0]);
                        if (linkedList != null) Console.WriteLine("\t\t// the linked list is {0}", String.Join("->", linkedList));
                        else Console.WriteLine();
                        result.Add(null);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
