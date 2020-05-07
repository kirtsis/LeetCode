using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     993. Cousins in Binary Tree
    ///     https://leetcode.com/problems/cousins-in-binary-tree/
    /// </summary>
    [TestFixture]
    public class P00993 {

        [PublicAPI]
        public Boolean IsCousins(TreeNode root, Int32 x, Int32 y) {
            // Problem Notes:
            // * The number of nodes in the tree will be between 2 and 100.
            // * Each node has a unique integer value from 1 to 100.
            Debug.Assert(root != null, "root != null");
            Debug.Assert((root.left != null) || (root.right != null), "(root.left != null) || (root.right != null)");

            // Tuple<TreeNode, Int32> for each found node (x and y):
            // * Item1: the parent of the found node
            // * Item2: the depth of the found node
            Tuple<TreeNode, Int32> xFound = root.val == x ? Tuple.Create<TreeNode, Int32>(null, 0) : null;
            Tuple<TreeNode, Int32> yFound = root.val == y ? Tuple.Create<TreeNode, Int32>(null, 0) : null;

            // depth: the depth of the current node
            // count: the number of elements we need to Dequeue until we have to increase the depth
            // nextCount: same as count, but for the next depth
            Int32 depth = 0; // the depth of the current node, initial value 0 (the depth of the root)
            Int32 count = 1; // initial value 1, because we Enqueue the root at the start
            Int32 nextCount = 0;

            // Breadth-first search/traversal (BFS) (or Level Order Traversal)
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0) {
                TreeNode currentNode = queue.Dequeue();

                // check if the value of a child is x or y,
                // store the found child's parent and depth,
                // and break when we have found both of them
                if (currentNode.left?.val == x) xFound = Tuple.Create(currentNode, depth + 1);
                if (currentNode.left?.val == y) yFound = Tuple.Create(currentNode, depth + 1);
                if (currentNode.right?.val == x) xFound = Tuple.Create(currentNode, depth + 1);
                if (currentNode.right?.val == y) yFound = Tuple.Create(currentNode, depth + 1);
                if ((xFound != null) && (yFound != null)) break;

                // calculate depth
                if (currentNode.left != null) nextCount++;
                if (currentNode.right != null) nextCount++;
                if (--count == 0) {
                    depth++;
                    count = nextCount;
                    nextCount = 0;
                }

                // Enqueue the children
                if (currentNode.left != null) queue.Enqueue(currentNode.left);
                if (currentNode.right != null) queue.Enqueue(currentNode.right);
            }

            // The problem states: We are given the root of a binary tree with unique values, and the values x and y of two different nodes in the tree.
            // Problem Notes: 1. The number of nodes in the tree will be between 2 and 100.
            // From the above, we can conclude that x and y should always be present in the binary tree, and we should be able to find them.
            Debug.Assert(xFound != null, "xFound != null");
            Debug.Assert(yFound != null, "yFound != null");
            return (xFound.Item1 != yFound.Item1) && (xFound.Item2 == yFound.Item2);
        }

        [Test]
        [TestCase("[1,2,3,4]", 4, 3, ExpectedResult = false)]
        [TestCase("[1,2,3,null,4,null,5]", 5, 4, ExpectedResult = true)]
        [TestCase("[1,2,3,null,4]", 2, 3, ExpectedResult = false)]
        [TestCase("[1,null,2,3,null,null,4,null,5]", 1, 3, ExpectedResult = false)]
        public Boolean Test(String input, Int32 x, Int32 y) {
            TreeNode root = TreeNode.Make(JsonConvert.DeserializeObject<Int32?[]>(input));
            return this.IsCousins(root, x, y);
        }

    }

}
