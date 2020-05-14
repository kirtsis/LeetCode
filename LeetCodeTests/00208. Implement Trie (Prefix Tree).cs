using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     208. Implement Trie (Prefix Tree)
    ///     https://leetcode.com/problems/implement-trie-prefix-tree/
    /// </summary>
    [TestFixture]
    public class P00208 {

        [PublicAPI]
        public class Trie : IEnumerable<String> {

            private TrieNode _root;

            public Trie() {
                this._root = new TrieNode();
            }

            public void Insert(String word) {
                if (word == null) return;

                Int32 length = word.Length;
                if (length == 0) return;

                TrieNode node = this._root;
                for (Int32 index = 0; index < length; ++index) {
                    Char currentLetter = word[index];
                    if (!node.ContainsLetter(currentLetter)) node[currentLetter] = new TrieNode();
                    node = node[currentLetter];
                }

                node.WordEndingNode = true;
            }

            public Boolean Search(String word) {
                return this._searchPrefix(word)?.WordEndingNode == true;
            }

            public Boolean StartsWith(String prefix) {
                return this._searchPrefix(prefix) != null;
            }

            private TrieNode _searchPrefix(String word) {
                if (word == null) return null;

                Int32 length = word.Length;
                if (length == 0) return null;

                TrieNode node = this._root;
                for (Int32 index = 0; index < length; ++index) {
                    Char currentLetter = word[index];
                    if (!node.ContainsLetter(currentLetter)) return null;

                    node = node[currentLetter];
                }

                return node;
            }

            private class TrieNode {

                private const Char FirstLetter = 'a';
                private const Int32 Letters = 26;

                private readonly TrieNode[] _children;

                public TrieNode() {
                    this._children = new TrieNode[TrieNode.Letters];
                }

                public Boolean WordEndingNode { get; set; }

                public TrieNode this[Char letter] {
                    get { return this._children[letter - TrieNode.FirstLetter]; }
                    set { this._children[letter - TrieNode.FirstLetter] = value; }
                }

                public Boolean ContainsLetter(Char letter) {
                    return this._children[letter - TrieNode.FirstLetter] != null;
                }

            }

            #region IEnumerable<Char?>

            IEnumerator<String> IEnumerable<String>.GetEnumerator() {
                return this._getEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return this._getEnumerator();
            }

            private IEnumerator<String> _getEnumerator() {
                return this._getValuesRecursively(this._root).GetEnumerator();
            }

            private IEnumerable<String> _getValuesRecursively(TrieNode node) {
                if (node == null) yield break;

                for (Int32 index = 0; index < 26; ++index) {
                    Char letter = (Char)('a' + index);
                    if (!node.ContainsLetter(letter)) continue;

                    String child = "\"" + letter;
                    if (node[letter].WordEndingNode) child += ".";
                    child += "\"";
                    yield return child;

                    foreach (String grandchild in this._getValuesRecursively(node[letter])) {
                        yield return grandchild;
                    }
                }

                yield return "null";
            }

            #endregion
        }

        [Test]
        [TestCase("[\"Trie\",\"insert\",\"search\",\"search\",\"startsWith\",\"insert\",\"search\"]", "[[],[\"apple\"],[\"apple\"],[\"app\"],[\"app\"],[\"app\"],[\"app\"]]", ExpectedResult = "[null,null,true,false,true,null,true]")]
        [TestCase("[\"Trie\",\"insert\",\"insert\",\"insert\",\"insert\",\"insert\"]", "[[],[\"abc\"],[\"acb\"],[\"bac\"],[\"bca\"],[\"cab\"]]", ExpectedResult = "[null,null,null,null,null,null]")]
        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        public String Test(String input1, String input2) {
            var actions = JsonConvert.DeserializeObject<String[]>(input1);
            var parameters = JsonConvert.DeserializeObject<String[][]>(input2);

            var result = new List<Boolean?>();

            Trie trie = null;
            Console.WriteLine("Trie trie = null;");
            for (Int32 i = 0; i < actions.Length; i++) {
                String action = actions[i];
                switch (action) {
                    case "Trie":
                        Console.WriteLine("trie = new Trie();");
                        trie = new Trie();
                        result.Add(null);
                        break;

                    case "insert":
                        Console.Write("trie.Insert({0});", parameters[i][0]);
                        trie?.Insert(word: parameters[i][0]);
                        if (trie != null) Console.WriteLine("\t\t// the trie is [{0}]", String.Join(",", trie));
                        else Console.WriteLine();
                        result.Add(null);
                        break;

                    case "search":
                        Console.Write("trie.Search({0});", parameters[i][0]);
                        Boolean? search = trie?.Search(word: parameters[i][0]);
                        if (search != null) Console.WriteLine("\t\t// returns: {0}", search);
                        else Console.WriteLine();
                        result.Add(search);
                        break;

                    case "startsWith":
                        Console.Write("trie.StartsWith({0});", parameters[i][0]);
                        Boolean? startsWith = trie?.StartsWith(prefix: parameters[i][0]);
                        if (startsWith != null) Console.WriteLine("\t\t// returns: {0}", startsWith);
                        else Console.WriteLine();
                        result.Add(startsWith);
                        break;
                }
            }

            return JsonConvert.SerializeObject(result);
        }

    }

}
