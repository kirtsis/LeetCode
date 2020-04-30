using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     Check If a String Is a Valid Sequence from Root to Leaves Path in a Binary Tree
    ///     (couldn't find it in Problems)
    /// </summary>
    [TestFixture]
    public class TestClass {

        [PublicAPI]
        public Boolean IsValidSequence(TreeNode root, Int32[] arr) {
            if (arr == null) return root == null; // can never happen (Problem Constraints: 1 <= arr.length <= 5000)

            Int32 length = arr.Length;
            if (root == null) return length == 0; // we can also just return false, since length is always bigger than or equal to one (Problem Constraints: 1 <= arr.length <= 5000)

            return this._isValid(root, arr, 0, length);
        }

        private Boolean _isValid(TreeNode node, Int32[] arr, Int32 index, Int32 length) {
            if (node == null) return false;

            // leaf node
            if ((node.left == null) && (node.right == null)) return (index == length - 1) && (node.val == arr[index]);

            return (index < length) && (node.val == arr[index])
                   && (this._isValid(node.left, arr, index + 1, length) || this._isValid(node.right, arr, index + 1, length));
        }

        [Test]
        [TestCase("[0,1,0,0,1,0,null,null,1,0,0]", "[0,1,0,1]", ExpectedResult = true)]
        [TestCase("[0,1,0,0,1,0,null,null,1,0,0]", "[0,0,1]", ExpectedResult = false)]
        [TestCase("[0,1,0,0,1,0,null,null,1,0,0]", "[0,1,1]", ExpectedResult = false)]
        [TestCase("[8,3,null,2,1,5,4]", "[8]", ExpectedResult = false)]
        public Boolean Test(String input1, String input2) {
            TreeNode root = TreeNode.Make(JsonConvert.DeserializeObject<Int32?[]>(input1));
            var arr = JsonConvert.DeserializeObject<Int32[]>(input2);
            return this.IsValidSequence(root, arr);
        }

    }

}
