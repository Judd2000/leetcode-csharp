using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCodeC_
{
    public class Solution1
    {
        public static int RemoveElement(int[] nums, int val)
        {
            int end = nums.Length;
            int start = 0;
            while (start < end)
            {
                int elem = nums[start];
                if (elem == val)
                {
                    nums[start] = nums[end - 1];
                    nums[end - 1] = elem;
                    end--;
                }
                else
                {
                    start++;
                }
            }
            return end;
        }

        public static int RemoveElement2(int[] nums, int val)
        {
            int sizeOfNewArray = 0;
            for (int i = 0; i < nums.Length; i++) {
                // When we DON'T WANT TO REMOVE IT, move it to the front.
                if (nums[i] != val)
                {
                    nums[sizeOfNewArray] = nums[i];
                    sizeOfNewArray++;
                }
            }
            return sizeOfNewArray;
        }
    }
}
