using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LeetCodeTests {

    /// <summary>
    ///     480. Sliding Window Median
    ///     https://leetcode.com/problems/sliding-window-median/
    /// </summary>
    [TestFixture]
    public class P00480 {

        [PublicAPI]
        public Double[] MedianSlidingWindow(Int32[] nums, Int32 k) {
            //return this._loops(nums, k); // Time Limit Exceeded
            //return this._sortedList(nums, k);
            return this._list(nums, k);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Double[] _loops(Int32[] nums, Int32 k) {
            if (nums == null) return new Double[0];

            Int32 length = nums.Length;
            Int32 resultLength = length - k + 1;
            if (resultLength <= 0) return new Double[0];

            Int32 mid = k / 2;
            Boolean odd = k % 2 == 1;
            var result = new Double[resultLength];
            for (Int32 index = 0; index < resultLength; index++) {
                Int32[] window = this._sortedWindow(nums, index, index + k - 1);
                result[index] = odd ? window[mid]
                                    : window[mid - 1] + (window[mid] - window[mid - 1]) / 2d;
            }

            return result;
        }

        private Int32[] _sortedWindow(Int32[] array, Int32 start, Int32 end) {
            if (start > end) return null;
            if (start == end) return new[] {array[start]};

            Int32 mid = start + (end - start) / 2;
            Int32[] leftArray = this._sortedWindow(array, start, mid);
            Int32[] rightArray = this._sortedWindow(array, mid + 1, end);

            if ((leftArray == null) && (rightArray == null)) throw new InvalidOperationException();

            if (leftArray == null) return rightArray;
            if (rightArray == null) return leftArray;

            Int32 leftIndex = 0;
            Int32 leftLength = leftArray.Length;

            Int32 rightIndex = 0;
            Int32 rightLength = rightArray.Length;

            Int32 mergedIndex = 0;
            var mergedArray = new Int32[leftLength + rightLength];

            while ((leftIndex < leftLength) && (rightIndex < rightLength)) {
                if (leftArray[leftIndex] <= rightArray[rightIndex]) {
                    mergedArray[mergedIndex] = leftArray[leftIndex];
                    leftIndex++;
                } else {
                    mergedArray[mergedIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < leftLength) {
                mergedArray[mergedIndex] = leftArray[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < rightLength) {
                mergedArray[mergedIndex] = rightArray[rightIndex];
                rightIndex++;
                mergedIndex++;
            }

            return mergedArray;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Double[] _sortedList(Int32[] nums, Int32 k) {
            if (nums == null) return new Double[0];

            Int32 length = nums.Length;
            Int32 resultLength = length - k + 1;
            if (resultLength <= 0) return new Double[0];

            var sortedList = new SortedList<Int64, Int32>();
            Func<Int32, Int32[], Int32, Int64> key = (i, n, l) => i + (Int64)n[i] * l;

            Int32 mid = k / 2;
            Boolean odd = k % 2 == 1;
            var result = new Double[resultLength];
            for (Int32 index = 0; index < length; index++) {
                sortedList.Add(key(index, nums, length), nums[index]);
                if (sortedList.Count > k) sortedList.Remove(key(index - k, nums, length));

                if (index - k + 1 >= 0) {
                    result[index - k + 1] = odd ? sortedList[sortedList.Keys[mid]]
                                                : sortedList[sortedList.Keys[mid - 1]] / 2d + sortedList[sortedList.Keys[mid]] / 2d;
                }
            }

            return result;
        }

        private Double[] _list(Int32[] nums, Int32 k) {
            if (nums == null) return new Double[0];

            Int32 length = nums.Length;
            Int32 resultLength = length - k + 1;
            if (resultLength <= 0) return new Double[0];

            var window = new List<Int32>();

            Int32 mid = k / 2;
            Boolean odd = k % 2 == 1;
            var result = new Double[resultLength];
            for (Int32 index = 0; index < length; index++) {
                // 
                if (index - k >= 0) {
                    Int32 target = nums[index - k];
                    Int32 l = 0;
                    Int32 r = window.Count - 1;
                    while (l <= r) {
                        Int32 m = l + (r - l) / 2;
                        if (window[m] == target) {
                            window.RemoveAt(m);
                            break;
                        }

                        if (window[m] > target) r = m - 1;
                        else l = m + 1;
                    }
                }

                //
                {
                    Int32 target = nums[index];
                    Int32 l = 0;
                    Int32 r = window.Count - 1;
                    while (l <= r) {
                        Int32 m = l + (r - l) / 2;
                        if (window[m] > target) r = m - 1;
                        else l = m + 1;
                    }

                    window.Insert(l, target);
                }

                if (index - k + 1 >= 0) {
                    result[index - k + 1] = odd ? window[mid]
                                                : window[mid - 1] / 2d + window[mid] / 2d;
                }
            }

            return result;
        }

        [Test]
        [TestCase("[1,3,-1,-3,5,3,6,7]", 3, ExpectedResult = "[1.00000,-1.00000,-1.00000,3.00000,5.00000,6.00000]")]
        [TestCase("[2147483647,2147483647]", 2, ExpectedResult = "[2147483647.00000]")]
        [TestCase("[-2147483648,-2147483648,2147483647,-2147483648,-2147483648,-2147483648,2147483647,2147483647,2147483647,2147483647,-2147483648,2147483647,-2147483648]", 2, ExpectedResult = "[-2147483648.00000,-0.50000,-0.50000,-2147483648.00000,-2147483648.00000,-0.50000,2147483647.00000,2147483647.00000,2147483647.00000,-0.50000,-0.50000,-0.50000]")]
        public String Test(String input, Int32 k) {
            var nums = JsonConvert.DeserializeObject<Int32[]>(input);
            Double[] result = this.MedianSlidingWindow(nums, k);
            return JsonConvert.SerializeObject(result, new FloatingPointFormatConverter(5));
        }

    }

}
