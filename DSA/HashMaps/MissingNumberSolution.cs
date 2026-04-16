using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HashMaps
{
    internal class MissingNumberSolution
    {
        public static int MissingNumber(int[] nums)
        {
            // Given an array nums containing n distinct numbers in the range[0, n], return the only number in the range that is missing from the array.
            Dictionary<int, bool> counts = new();
            for (int i = 0; i < nums.Length + 1; i++)
            {
                counts[i] = false;
            }

            foreach (int num in nums)
            {
                counts[num] = true;
            }

            foreach (var entry in counts) {
                if (!entry.Value) return entry.Key;
            }

            return -1;
        }

        public static int MissingNumber_Optimized(int[] nums) {
            // find sum of both the expected and actual, the remainder will be the missing number!
            int realSum = 0;
            int expectedSum = 0;
            for (int i = 0; i <= nums.Length; i++) { 
                if (i < nums.Length) realSum += nums[i];
                expectedSum += i;
            }

            return expectedSum - realSum;
        }
    }
}
