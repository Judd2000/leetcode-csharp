using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Strings
{
    internal class MaxAvgSubArr
    {
        //You are given an integer array nums consisting of n elements, and an integer k.

        //Find a contiguous subarray whose length is equal to k that has the maximum average value and return this value.
        //Any answer with a calculation error less than 10-5 will be accepted.
        public static double FindMaxAverage(int[] nums, int subArrLen) {

            if (subArrLen > nums.Length) return 0;

            // length of subarray is end - start + 1
            int left = 0;
            int right = 0;

            // get subarray bounds to correct length
            double curSum = nums[right];
            while (right + 1 < subArrLen) {
                right++;
                curSum += nums[right];
            }

            // while right is not at the end of the array
            double maxSum = curSum;
            while (right + 1 < nums.Length) {
                curSum -= nums[left];
                left++;
                right++;
                curSum += nums[right];

                if (curSum > maxSum) maxSum = curSum;
            }

            return maxSum / subArrLen;
        }
    }
}
