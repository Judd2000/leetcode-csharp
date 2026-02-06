using System;
using System.Collections.Generic;
using System.Text;

namespace MergeSort;

internal class CustomSort
{
    public static int[] MergeSort(int[] arrayToSort) {
        int[] mutableArr = new int[arrayToSort.Length];
        Array.Copy(arrayToSort, mutableArr, arrayToSort.Length);
        MergeHelper(mutableArr, 0, arrayToSort.Length - 1);
        return mutableArr;
    }

    private static void MergeHelper(int[] arrayToSort, int left, int right) {
        // base case: Left is Equal to Right pointer
        if (left < right) {
            int midPoint = (left + right) / 2;
            MergeHelper(arrayToSort, left, midPoint); // left merge
            MergeHelper(arrayToSort, midPoint + 1, right); // right merge
            MergeActionHelper(arrayToSort, left, midPoint, right); //perform the merging action.
        }

        //Left and Right recursion.


        // look at left and right - determine which is greater, merge them.
        // merge all the way back up.
    }

    private static void MergeActionHelper(int[] array, int left, int middle, int right) {
        // increment left and right. We are just filling into the array
        // make array copies of both sides
        int leftLen = middle - left + 1;

        int[] leftArr = new int[leftLen];
        Array.Copy(array, left, leftArr, 0, leftLen);


        int rightLen = right - (middle + 1) + 1; //written the +1's for logic's sake
        int[] rightArr = new int[rightLen];
        Array.Copy(array, middle + 1, rightArr, 0, rightLen);

        // insert them back into array from left - right in order.
        int leftIndex = 0;
        int rightIndex = 0;
        int insertPoint = left;
        // until we exhaust an array, place the next sequential one in the array.
        while (leftIndex < leftArr.Length && rightIndex < rightArr.Length)
        {
            array[insertPoint++] = leftArr[leftIndex] <= rightArr[rightIndex] ? leftArr[leftIndex++] : rightArr[rightIndex++];
        }

        // when one runs out, all the elements of the other will go in.
        while (leftIndex < leftArr.Length)
        {
            array[insertPoint++] = leftArr[leftIndex++];
        }

        while (rightIndex < rightArr.Length)
        {
            array[insertPoint++] = rightArr[rightIndex++];
        }
    }
}
