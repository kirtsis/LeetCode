using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     230. Kth Smallest Element in a BST
    ///     https://leetcode.com/problems/kth-smallest-element-in-a-bst/
    /// </summary>
    [TestFixture]
    public class P00230 {

        [PublicAPI]
        public Int32 KthSmallest(TreeNode root, Int32 k) {
            Int32? result = this._inorder(root, ref k);
            return result ?? -1;
        }

        private Int32? _inorder(TreeNode node, ref Int32 k) {
            Int32? result = null;

            if (node.left != null) result = this._inorder(node.left, ref k);
            if (result != null) return result.Value;

            if (k == 1) result = node.val;
            if (result != null) return result.Value;

            k--;

            if (node.right != null) result = this._inorder(node.right, ref k);
            return result;
        }

        [Test]
        [TestCase("[3,1,4,null,2]", 1, ExpectedResult = 1)]
        [TestCase("[5,3,6,2,4,null,null,1]", 3, ExpectedResult = 3)]
        public Int32 Test(String input, Int32 k) {
            TreeNode root = TreeNode.Make(JsonConvert.DeserializeObject<Int32?[]>(input));
            return this.KthSmallest(root, k);
        }

    }

}
