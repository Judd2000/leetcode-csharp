namespace Arrays_Strings
{
    internal class SquaresSorted
    {
        public static int[] SortedSquares(int[] nums)
        {
            int[] sortedSquares = new int[nums.Length];
            int left = 0;
            int right = nums.Length - 1;
            int insertIndex = right;

            while (left <= right) { 
                int leftSq = nums[left] * nums[left];
                int rightSq = nums[right] * nums[right];

                if (leftSq > rightSq) {
                    sortedSquares[insertIndex] = leftSq;
                    left++;
                }
                else
                {
                    sortedSquares[insertIndex] = rightSq;
                    right--;
                }
                insertIndex--;
            }

            return sortedSquares;
        }
    }
}
