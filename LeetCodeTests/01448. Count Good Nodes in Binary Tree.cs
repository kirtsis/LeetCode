using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1448. Count Good Nodes in Binary Tree
    ///     https://leetcode.com/problems/count-good-nodes-in-binary-tree/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class P01448 {

        [PublicAPI]
        public Int32 GoodNodes(TreeNode root) {
            // Constraints:
            // * The number of nodes in the binary tree is in the range [1, 10^5].
            // * Each node's value is between [-10^4, 10^4].

            Int32 result = 0;
            this._dfs(root, root.val, ref result);
            return result;
        }

        private void _dfs(TreeNode node, Int32 max, ref Int32 result) {
            if (node == null) return;

            if (node.val >= max) result++;
            max = Math.Max(max, node.val);
            this._dfs(node.left, max, ref result);
            this._dfs(node.right, max, ref result);
        }

        [Test]
        [TestCase("[3,1,4,3,null,1,5]", ExpectedResult = 4)]
        [TestCase("[3,3,null,4,2]", ExpectedResult = 3)]
        [TestCase("[1]", ExpectedResult = 1)]
        [TestCase("[2,null,4,10,8,null,null,4]", ExpectedResult = 4)]
        public Int32 Test(String input) {
            TreeNode root = TreeNode.Make(JsonConvert.DeserializeObject<Int32?[]>(input));
            return this.GoodNodes(root);
        }

    }

}
