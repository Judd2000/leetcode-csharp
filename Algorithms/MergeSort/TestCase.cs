using System;
using System.Collections.Generic;
using System.Text;

namespace MergeSort;

internal class TestCase(int[] array, int[] solution, int testCaseNumber = 0) // primary constructors are neat!
{
    public int TestCaseNumber { get; init; } = testCaseNumber;
    public int[] arrayToSort = array;
    public int[] validSortedArray = solution;
}
