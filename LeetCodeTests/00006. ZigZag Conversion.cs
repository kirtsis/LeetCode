using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     6. ZigZag Conversion
    ///     https://leetcode.com/problems/zigzag-conversion/
    /// </summary>
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class P00006 {

        [PublicAPI]
        public String Convert(String s, Int32 numRows) {
            //return this._lines(s, numRows);
            return this._steps(s, numRows);
        }

        private String _lines(String s, Int32 numRows) {
            if (s == null) return null;
            if (numRows <= 1) return s;

            Int32 length = s.Length;
            if (length <= numRows) return s;

            Int32 row = 0;
            Int32 increment = 1;
            var lines = new StringBuilder[numRows];
            foreach (Char c in s) {
                if (lines[row] == null) lines[row] = new StringBuilder(length);
                lines[row].Append(c);
                row += increment;
                if (row == numRows - 1) increment = -1;
                if (row == 0) increment = 1;
            }

            var result = new StringBuilder();
            foreach (StringBuilder line in lines) {
                result.Append(line);
            }

            return result.ToString();
        }

        private String _steps(String s, Int32 numRows) {
            if (s == null) return null;
            if (numRows <= 1) return s;

            Int32 length = s.Length;
            if (length <= numRows) return s;

            // indexes per row for numRows equal to 5
            // row 5:  00              08              16              24  -  this is step1 each time
            // row 4:  01          07  09          15  17          23  25  -  this is step1, step2, step1, step2, ...
            // row 3:  02      06      10      14      18      22      26  -  this is step1, step2, step1, step2, ...
            // row 2:  03  05          11  13          19  21          ..  -  this is step1, step2, step1, step2, ...
            // row 1:  04              12              20                  -  this is step2 each time
            // result is row 5 + row 4 + row 3 + row 2 + row 1
            // step1 is dependent on row: as the row decreases, so does the step1 (for row 5 00->08 is 8, for row 4 01->07 is 6, for row 3 02->06 is 4, etc.)
            // step2 is also dependent on row: as the row decreases, the step2 increases (for row 4 07->09 is 2, for row 3 06->10 is 4, for row 2 05->11 is 6, etc.)

            // calculate variables for the first row (numRows)
            Int32 row = numRows;
            Boolean isStep1 = true;
            Int32 step1 = row * 2 - 2;
            Int32 step2 = (numRows - row) * 2;
            Int32 index = numRows - row;

            var result = new StringBuilder(length);
            while (true) {
                // append letter
                result.Append(s[index]);

                // calculate next index
                if (row == numRows) {
                    // move step1 each time
                    index += step1;
                } else {
                    if (row == 1) {
                        // move step2 each time
                        index += step2;
                    } else {
                        // move step1, step2, step1, step2, ...
                        index += isStep1 ? step1 : step2;
                        isStep1 = !isStep1;
                    }
                }

                // continue if next index is valid (is inside String s)
                if (index < length) continue;

                // if next index is invalid (is outside String s)
                // go to next row
                row--;
                if (row == 0) break; // break while if we reach row 0

                // recalculate variables for the next row
                isStep1 = true;
                step1 = row * 2 - 2;
                step2 = (numRows - row) * 2;
                index = numRows - row;
            }

            return result.ToString();
        }

        [Test]
        [TestCase("PAYPALISHIRING", 3, ExpectedResult = "PAHNAPLSIIGYIR")]
        [TestCase("PAYPALISHIRING", 4, ExpectedResult = "PINALSIGYAHRPI")]
        [TestCase("AB", 1, ExpectedResult = "AB")]
        [TestCase("", 5, ExpectedResult = "")]
        [TestCase(null, 5, ExpectedResult = null)]
        [TestCase("A", 5, ExpectedResult = "A")]
        public String Test(String s, Int32 numRows) {
            return this.Convert(s, numRows);
        }

    }

}
