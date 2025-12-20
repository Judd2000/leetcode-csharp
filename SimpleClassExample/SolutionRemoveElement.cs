public class Solution
{
    public static int RemoveElement(int[] nums, int val)
    {
        int endOfArr = nums.Length - 1;
        for (int i = 0; i < nums.Length; i++)
        {
            int elem = nums[i];
            if (elem == val)
            {
                nums[i] = nums[endOfArr];
                nums[endOfArr] = elem;
                endOfArr--;
            }
        }
        return endOfArr;
    }
}