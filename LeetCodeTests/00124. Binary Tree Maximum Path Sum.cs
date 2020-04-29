using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     124. Binary Tree Maximum Path Sum
    ///     https://leetcode.com/problems/binary-tree-maximum-path-sum/
    /// </summary>
    [TestFixture]
    public class P00124 {

        [PublicAPI]
        public Int32 MaxPathSum(TreeNode root) {
            Int32 result = Int32.MinValue;
            this._calculate(root, ref result);
            return result;
        }

        private Int32 _calculate(TreeNode node, ref Int32 result) {
            if (node == null) return 0;

            Int32 left = Math.Max(0, this._calculate(node.left, ref result));
            Int32 right = Math.Max(0, this._calculate(node.right, ref result));
            result = Math.Max(result, node.val + left + right);
            return node.val + Math.Max(left, right);
        }

        [Test]
        [TestCase("[1,2,3]", ExpectedResult = 6)]
        [TestCase("[-10,9,20,null,null,15,7]", ExpectedResult = 42)]
        [TestCase("[5,4,8,11,null,13,4,7,2,null,null,null,1]", ExpectedResult = 48)]
        public Int32 Test(String input) {
            TreeNode root = TreeNode.Make(JsonConvert.DeserializeObject<Int32?[]>(input));
            return this.MaxPathSum(root);
        }

    }

}
