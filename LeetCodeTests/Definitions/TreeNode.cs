using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    [PublicAPI]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class TreeNode {

        public TreeNode left;
        public TreeNode right;
        public Int32 val;

        public TreeNode(Int32 x) {
            this.val = x;
        }

        public static TreeNode Make([NotNull] IEnumerable<Int32?> values) {
            if (values == null) throw new ArgumentNullException(nameof(values));

            IEnumerable<TreeNode> nodes = values.Select(value => value != null ? new TreeNode(value.Value) : null).ToArray();
            var queue = new Queue<TreeNode>(nodes);
            if (queue.Count <= 0) return null;

            TreeNode root = queue.Dequeue();
            if (queue.Count <= 0) return root;

            foreach (TreeNode node in nodes) {
                if (node == null) continue;

                node.left = queue.Dequeue();
                if (queue.Count <= 0) return root;

                node.right = queue.Dequeue();
                if (queue.Count <= 0) return root;
            }

            return root;
        }

        public static IEnumerable<Int32?> Make(TreeNode root) {
            var result = new List<Int32?>();

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0) {
                TreeNode node = queue.Dequeue();
                result.Add(node?.val);
                if (node == null) continue;

                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }

            return result.Take(1 + result.FindLastIndex(value => value != null));
        }

    }

    [TestFixture]
    public class TreeNodeTests {

        [Test]
        [TestCase("[]")]
        [TestCase("[1,2,3]")]
        [TestCase("[1,null,2,3]")]
        [TestCase("[5,4,7,3,null,2,null,-1,null,9]")]
        [TestCase("[-10,9,20,null,null,15,7]")]
        [TestCase("[5,4,8,11,null,13,4,7,2,null,null,null,1]")]
        [TestCase("[3,9,20,null,null,15,7]")]
        [TestCase("[1,2,2,3,3,null,null,4,4]")]
        [TestCase("[0,-3,9,-10,null,5]")]
        [TestCase("[1,2,2,3,4,4,3]")]
        [TestCase("[1,2,2,null,3,null,3]")]
        [TestCase("[1,null,2]")]
        public void Test(String input) {
            // ARRANGE
            var values = JsonConvert.DeserializeObject<Int32?[]>(input);

            // ACT
            TreeNode root = TreeNode.Make(values);

            // ASSERT
            String output = JsonConvert.SerializeObject(TreeNode.Make(root));
            Assert.That(output, Is.EqualTo(input));
        }

    }

}
