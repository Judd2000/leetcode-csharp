using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Strings
{
    internal class MaxConsecutiveOnes
    {
        public static int LongestOnes(int[] nums, int k)
        {
            //Given a binary array nums and an integer k, return the maximum number of consecutive 1's in the array if you can flip at most k 0's.
            int left = 0;
            int right = 0;

            int max = 0;

            int currZeros = 0;
            while (right + 1 < nums.Length) {
                if (nums[right] == 0)
                {
                    currZeros++;
                    if (currZeros > k)
                    {
                        while (currZeros > k)
                        {
                            if (nums[left] == 0) currZeros--;
                            left++;
                        }
                    }
                }
                int currLen = right - left + 1;
                if (currLen > max) max = currLen;
                right++;
            }
            return max;
        }
    }
}
