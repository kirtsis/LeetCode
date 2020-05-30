using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     338. Counting Bits
    ///     https://leetcode.com/problems/counting-bits/
    /// </summary>
    [TestFixture]
    public class P00338 {

        [PublicAPI]
        public Int32[] CountBits(Int32 num) {
            var result = new Int32[num + 1];
            for (Int32 number = 1; number <= num; ++number) {
                // by default: result[0] = 0
                //
                //     number: 00000001 = 1
                //  left part: 0000000  = 0 = 1 / 2 = 1 >> 1
                // right part:        1     = 1 % 2 = 1 & 1 (00000001 & 00000001 = 1)
                // result[1] = result[0] + 1
                //
                //     number: 00000010 = 2
                //  left part: 0000001  = 1 = 2 / 2 = 2 >> 1
                // right part:        0     = 2 % 2 = 2 & 1 (00000010 & 00000001 = 0)
                // result[2] = result[1] + 0
                //
                //     number: 00000011 = 3
                //  left part: 0000001  = 1 = 3 / 2 = 3 >> 1
                // right part:        1     = 3 % 2 = 3 & 1 (00000011 & 00000001 = 1)
                // result[3] = result[1] + 1
                //
                //     number: 01010111 = 87
                //  left part: 0101011  = 43 = 87 / 2 = 87 >> 1
                // right part:        1      = 87 % 2 = 87 & 1 (01010111 & 00000001 = 1)
                // result[87] = result[43] + 1
                //
                //     number: 11010100 = 212
                //  left part: 1101010  = 106 = 212 / 2 = 212 >> 1
                // right part:        0       = 212 % 2 = 212 & 1 (11010100 & 00000001 = 0)
                // result[212] = result[106] + 0

                result[number] = result[number >> 1] + (number & 1);
            }

            return result;
        }

        [Test]
        [TestCase(2, ExpectedResult = "[0,1,1]")]
        [TestCase(5, ExpectedResult = "[0,1,1,2,1,2]")]
        [TestCase(64, ExpectedResult = "[0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,1]")]
        [TestCase(19, ExpectedResult = "[0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,1,2,2,3]")]
        [TestCase(20, ExpectedResult = "[0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,1,2,2,3,2]")]
        public String Test(Int32 num) {
            Int32[] result = this.CountBits(num);
            return JsonConvert.SerializeObject(result);
        }

    }

}
