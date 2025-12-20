// See https://aka.ms/new-console-template for more information
using LeetCodeC_;

int ans = Solution1.RemoveElement([3, 2, 2, 3], 3); //expecting an output of 2.
Console.WriteLine("Answer: {0}", Solution1.RemoveElement([3, 2, 2, 3], 3));

int ans2 = Solution1.RemoveElement2([1, 1, 1, 2, 5, 9, 3, 2, 3, 3, 2], 2); //expecting an output of: 8.
Console.WriteLine("Answer: {0}", ans2);