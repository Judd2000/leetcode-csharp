using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Arrays_Strings
{
    internal class KRadiusSub
    {

        /*You are given a 0 - indexed array nums of n integers, and an integer k.

The k-radius average for a subarray of nums centered at some index i with the radius k is the average of all elements in nums between the indices i - k and i + k (inclusive). If there are less than k elements before or after the index i, then the k-radius average is -1.

Build and return an array avgs of length n where avgs[i] is the k-radius average for the subarray centered at index i.

The average of x elements is the sum of the x elements divided by x, using integer division.The integer division truncates toward zero, which means losing its fractional part.

For example, the average of four elements 2, 3, 1, and 5 is (2 + 3 + 1 + 5) / 4 = 11 / 4 = 2.75, which truncates to 2.
        
Input: nums = [7,4,3,9,1,8,5,2,6], k = 3
Output: [-1,-1,-1,5,4,4,-1,-1,-1]
         */
        public static int[] GetAverages(int[] nums, int k)
        {
            if (k == 0) return nums;

            int sum = 0;
            int[] avgs = new int[nums.Length];

            int diameter = 2 * k + 1;

            Array.Fill(avgs, -1);

            if (diameter > nums.Length) return avgs;

            for (int i = 0; i < diameter; i++) {
                sum += nums[i];
            }

            avgs[k] = sum / diameter;

            for (int i = diameter; i < nums.Length; i++) {
                sum -= nums[i - diameter];
                sum += nums[i];
                avgs[i - k] = sum / diameter;
            }

            return avgs;
        }
    }
}
