using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Strings
{
    internal class RunningSum
    {
        public static int[] RunningSumFunc(int[] nums)
        {
            int[] runningSum = new int[nums.Length];

            runningSum[0] = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                runningSum[i] = runningSum[i - 1] + nums[i];
            }

            return runningSum;
        }
    }
}
