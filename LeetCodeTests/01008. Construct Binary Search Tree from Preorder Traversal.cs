using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     1008. Construct Binary Search Tree from Preorder Traversal
    ///     https://leetcode.com/problems/construct-binary-search-tree-from-preorder-traversal/
    /// </summary>
    [TestFixture]
    public class P01008 {

        [PublicAPI]
        public TreeNode BstFromPreorder(Int32[] preorder) {
            var root = new TreeNode(preorder[0]);

            var stack = new Stack<TreeNode>();
            stack.Push(root);

            Int32 n = preorder.Length;
            for (Int32 i = 1; i < n; i++) {
                TreeNode node = null;
                while ((stack.Count > 0) && (preorder[i] > stack.Peek().val)) {
                    node = stack.Pop();
                }

                if (node != null) {
                    node.right = new TreeNode(preorder[i]);
                    stack.Push(node.right);
                } else {
                    node = stack.Peek();
                    node.left = new TreeNode(preorder[i]);
                    stack.Push(node.left);
                }
            }

            return root;
        }

        [Test]
        [TestCase("[8,5,1,7,10,12]", ExpectedResult = "[8,5,10,1,7,null,12]")]
        public String Test(String input) {
            var preorder = JsonConvert.DeserializeObject<Int32[]>(input);
            TreeNode result = this.BstFromPreorder(preorder);
            return JsonConvert.SerializeObject(TreeNode.Make(result));
        }

    }

}
