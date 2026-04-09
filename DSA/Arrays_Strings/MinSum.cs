using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Strings
{
    internal class MinSum
    {
        public static int MinStartValue(int[] nums)
        {
            // return min number to make every prefix sum >= 1
            int minStart = 1;
            int sum = 0;

            for (int i = 0; i < nums.Length; i++) { 
                sum += nums[i];
                if (sum < minStart) { 
                    minStart = sum;
                }
            }

            return minStart > 0 ? 1 : -1 * minStart + 1;
        }
    }
}
