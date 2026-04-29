using System;

namespace HashMaps {
    internal class ContiguousSubarray
    {

        public static int FindMaxLength(int[] nums)
        {
            Dictionary<int, int> seenSums = new(); // Key: Sum, Value: Index Seen

            int maxLen = 0;

            int curSum = 0;

            seenSums.Add(0, -1); // must add a "baseline"


            for (int i = 0; i < nums.Length; i++) {
                curSum += nums[i] == 1 ? 1 : -1; // subtract 1 if 0, add 1 if 1.
                if (seenSums.ContainsKey(curSum)) {
                    maxLen = Math.Max(maxLen, i - seenSums[curSum]);
                } else {
                    // Add
                    seenSums.Add(curSum, i);
                }
            }
            return maxLen;
        }
    }
}