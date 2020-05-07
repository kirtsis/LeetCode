using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;
using Node = LeetCodeTests.RandomListNode;

namespace LeetCodeTests {

    /// <summary>
    ///     138. Copy List with Random Pointer
    ///     https://leetcode.com/problems/copy-list-with-random-pointer/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00138 {

        [PublicAPI]
        public Node CopyRandomList(Node head) {
            //return this._copy1(head);
            //return this._copy2(head);
            return this._copy3(head);
        }

        private Node _copy1(Node head) {
            Node result = null;

            Int32 index = 0;
            var dictionary1 = new Dictionary<Node, Int32>();
            Node current = head;
            while (current != null) {
                dictionary1.Add(current, index);
                current = current.next;
                index++;
            }

            var dictionary = new Dictionary<Int32, Tuple<Node, Int32?>>();
            index = 0;
            current = head;
            Node previous = null;
            while (current != null) {
                var node = new Node(current.val);
                if (result == null) result = node;
                if (previous != null) previous.next = node;
                dictionary.Add(index, Tuple.Create(node, current.random != null ? dictionary1[current.random] : (Int32?)null));
                previous = node;
                current = current.next;
                index++;
            }

            foreach (KeyValuePair<Int32, Tuple<Node, Int32?>> pair in dictionary) {
                Node node = pair.Value.Item1;
                Int32? randomIndex = pair.Value.Item2;
                if (randomIndex != null) node.random = dictionary[randomIndex.Value].Item1;
            }

            return result;
        }

        private Node _copy2(Node head) {
            if (head == null) return null;

            var dictionary = new Dictionary<Node, Node>();

            Node current = head;
            while (current != null) {
                dictionary.Add(current, new Node(current.val));
                current = current.next;
            }

            current = head;
            while (current != null) {
                Node copy = dictionary[current];
                if (current.next != null) copy.next = dictionary[current.next];
                if (current.random != null) copy.random = dictionary[current.random];
                current = current.next;
            }

            return dictionary[head];
        }

        private Node _copy3(Node head) {
            if (head == null) return null;

            Node current = head;
            while (current != null) {
                Node next = current.next;
                current.next = new Node(current.val) {
                    next = next
                };
                current = next;
            }

            current = head;
            while (current != null) {
                if (current.next != null) current.next.random = current.random?.next;
                current = current.next?.next;
            }

            current = head;
            Node copy = head.next;
            Node copyHead = copy;
            while ((current != null) && (copy != null)) {
                current.next = current.next?.next;
                copy.next = copy.next?.next;
                current = current.next;
                copy = copy.next;
            }

            return copyHead;
        }

        [Test]
        [TestCase("[[7,null],[13,0],[11,4],[10,2],[1,0]]", ExpectedResult = "[[7,null],[13,0],[11,4],[10,2],[1,0]]")]
        [TestCase("[[1,1],[2,1]]", ExpectedResult = "[[1,1],[2,1]]")]
        [TestCase("[[3,null],[3,0],[3,null]]", ExpectedResult = "[[3,null],[3,0],[3,null]]")]
        [TestCase("[]", ExpectedResult = "[]")]
        public String Test(String input) {
            Node head = Node.Make(JsonConvert.DeserializeObject<Int32?[][]>(input));
            Node result = this.CopyRandomList(head);

            // Assert not equal references
            var instances = new ConditionalWeakTable<Node, Object>();

            Node node = head;
            while (node != null) {
                instances.Add(node, Guid.NewGuid());
                node = node.next;
            }

            node = result;
            while (node != null) {
                Object value;
                Boolean foundNode = instances.TryGetValue(node, out value);
                Assert.That(foundNode, Is.False, "Node with label {0} was not copied but a reference to the original one.", node.val);
                if (node.random != null) {
                    Boolean foundRandom = instances.TryGetValue(node.random, out value);
                    Assert.That(foundRandom, Is.False, "Node with label {0} was not copied but a reference to the original one.", node.random.val);
                }
                node = node.next;
            }

            return JsonConvert.SerializeObject(Node.Make(result));
        }

    }

}
