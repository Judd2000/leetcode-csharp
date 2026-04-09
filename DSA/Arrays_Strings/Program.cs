// See https://aka.ms/new-console-template for more information
using Arrays_Strings;
Console.WriteLine("*** Reverse String - Basic Test Cases ***");

static void PrintArr<T>(T[] arr) {
    for (int i = 0; i < arr.Length; i++) {
        Console.Write($" {arr[i]}");
    }
    Console.WriteLine();
}

char[] inputOne = ['h', 'e', 'l', 'l', 'o'];
char[] solutionOne = ['o', 'l', 'l', 'e', 'h'];

Console.WriteLine("Input:");
PrintArr(inputOne);

ReverseString.Reverse(inputOne);

Console.WriteLine($"Received solution:");
PrintArr(inputOne);


Console.WriteLine("Expected output:");
PrintArr(solutionOne);

bool correctSolution = ArraysEqual(inputOne, solutionOne);

Console.WriteLine($"Solution one is correct: {correctSolution}");


char[] inputTwo = ['H', 'a', 'n', 'n', 'a', 'h'];
char[] solutionTwo = ['h', 'a', 'n', 'n', 'a', 'H'];

Console.WriteLine("Input:");
PrintArr(inputTwo);

ReverseString.Reverse(inputTwo);

Console.WriteLine($"Received solution:");
PrintArr(inputTwo);


Console.WriteLine("Expected output:");
PrintArr(solutionTwo);

correctSolution = ArraysEqual(inputTwo, solutionTwo);

Console.WriteLine($"Solution two is correct: {correctSolution}");

static bool ArraysEqual<T>(T[] a, T[] b) {
    if (a.Length != b.Length) return false;

    for (int i = 0; i < a.Length; i++) { 
        if (!a[i].Equals(b[i])) return false;
    }
    return true;
}

Console.WriteLine("******** Max average subarray Basic Test Cases ********");

int[] avgInputOne = [1, 12, -5, -6, 50, 3];
int inputSubLen = 4;

double expectedAvg = 12.75;

double maxAvg = MaxAvgSubArr.FindMaxAverage(avgInputOne, inputSubLen);

correctSolution = maxAvg == expectedAvg;

Console.WriteLine("Input array:");
PrintArr(avgInputOne);

Console.WriteLine($"Expected solution: {expectedAvg}");
Console.WriteLine($"Actual solution: {maxAvg}");
Console.WriteLine($"Correct solution? {correctSolution}");

Console.WriteLine("**** Squares of Sorted Array ****");

int[] squareInputOne = [-7, -3, 2, 3, 11];
int[] squareSolutionOne = [4, 9, 9, 49, 121];

Console.WriteLine("Input array:");
PrintArr(squareInputOne);

int[] actual = SquaresSorted.SortedSquares(squareInputOne);
Console.WriteLine("Actual solution:");
PrintArr(actual);

Console.WriteLine($"Expected solution:");
PrintArr(squareSolutionOne);

correctSolution = ArraysEqual(actual, squareSolutionOne);
Console.WriteLine($"Correct solution? {correctSolution}");

int[] squareInputTwo = [-4, -1, 0, 3, 10];
int[] squareSolutionTwo = [0, 1, 9, 16, 100];

Console.WriteLine("Input array:");
PrintArr(squareInputTwo);

actual = SquaresSorted.SortedSquares(squareInputTwo);
Console.WriteLine("Actual solution:");
PrintArr(actual);

Console.WriteLine($"Expected solution:");
PrintArr(squareSolutionTwo);

correctSolution = ArraysEqual(actual, squareSolutionTwo);
Console.WriteLine($"Correct solution? {correctSolution}");

Console.WriteLine("**** Max Consecutive Ones ****");

int[] numInputOne = [1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0];
int kInputOne = 2;
int numExpectedOne = 6;

Console.WriteLine("Input array:");
PrintArr(numInputOne);
int actualResOne = MaxConsecutiveOnes.LongestOnes(numInputOne, kInputOne);
Console.WriteLine($"Actual solution: {actualResOne}, expected {numExpectedOne}. Correct? {actualResOne == numExpectedOne}");

int[] numInputtwo = [0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1];
int kInputTwo = 3;

int numExpectedTwo = 10;

Console.WriteLine("input array two: ");
PrintArr(numInputtwo);

int actualResTwo = MaxConsecutiveOnes.LongestOnes(numInputtwo, kInputTwo);
Console.WriteLine($"Actual solution: {actualResTwo}, expected {numExpectedTwo}. Correct? {actualResTwo == numExpectedTwo}");

Console.WriteLine("***** Running Sum *****");

int[] arrOne = [1, 2, 3, 4];
int[] expectedRunningSum = [1, 3, 6, 10];

Console.WriteLine("Input array:");
PrintArr(arrOne);

int[] actualRunningSum = RunningSum.RunningSumFunc(arrOne);

correctSolution = ArraysEqual(actualRunningSum, expectedRunningSum);

Console.WriteLine("Given solution:");
PrintArr(actualRunningSum);
Console.WriteLine("Real correct solution:");
PrintArr(expectedRunningSum);

Console.WriteLine($"Correct? {correctSolution}");

int[] arrTwo = [1, 1, 1, 1, 1];
expectedRunningSum = [1, 2, 3, 4, 5];

Console.WriteLine("Input array:");
PrintArr(arrTwo);

actualRunningSum = RunningSum.RunningSumFunc(arrTwo);

correctSolution = ArraysEqual(actualRunningSum, expectedRunningSum);

Console.WriteLine("Given solution:");
PrintArr(actualRunningSum);
Console.WriteLine("Real correct solution:");
PrintArr(expectedRunningSum);

Console.WriteLine($"Correct? {correctSolution}");

int[] arrThree = [3, 1, 2, 10, 1];
expectedRunningSum = [3, 4, 6, 16, 17];

Console.WriteLine("Input array:");
PrintArr(arrThree);

actualRunningSum = RunningSum.RunningSumFunc(arrThree);

correctSolution = ArraysEqual(actualRunningSum, expectedRunningSum);

Console.WriteLine("Given solution:");
PrintArr(actualRunningSum);
Console.WriteLine("Real correct solution:");
PrintArr(expectedRunningSum);

Console.WriteLine($"Correct? {correctSolution}");

Console.WriteLine("**** Minimum Value to Get Positive Step by Step Sum ****");

//Input: nums = [-3, 2, -3, 4, 2]
//Output: 5

int[] nums = [-3, 2, -3, 4, 2];
int expected = 5;

int actualValue = MinSum.MinStartValue(nums);

Console.WriteLine("Input array:");
PrintArr(nums);

Console.WriteLine($"Computed solution: {actualValue}. Expected: {expected}. Equal? {actualValue == expected}");

//Input: nums = [1, 2]
//Output: 1

nums = [1, 2];
expected = 1;

actualValue = MinSum.MinStartValue(nums);

Console.WriteLine("Input array:");
PrintArr(nums);

Console.WriteLine($"Computed solution: {actualValue}. Expected: {expected}. Equal? {actualValue == expected}");

//Input: nums = [1, -2, -3]
//Output: 5

nums = [1, -2, -3];
expected = 5;

actualValue = MinSum.MinStartValue(nums);

Console.WriteLine("Input array:");
PrintArr(nums);

Console.WriteLine($"Computed solution: {actualValue}. Expected: {expected}. Equal? {actualValue == expected}");

//Input: nums = [7, 4, 3, 9, 1, 8, 5, 2, 6], k = 3
//Output: [-1,-1,-1,5,4,4,-1,-1,-1]
int[] kRadiusInput = [7, 4, 3, 9, 1, 8, 5, 2, 6];
int radius = 3;
int[] expectedKRadius = [-1, -1, -1, 5, 4, 4, -1, -1, -1];

int[] actualKRadius = KRadiusSub.GetAverages(kRadiusInput, radius);

Console.WriteLine("Input array:");
PrintArr(kRadiusInput);

correctSolution = ArraysEqual(actualKRadius, expectedKRadius);

Console.WriteLine($"Computed solution:");
PrintArr(actualKRadius);
Console.WriteLine("Expected:");
PrintArr(expectedKRadius);

Console.WriteLine($"Equal? {correctSolution}");

//Input: nums = [100000], k = 0
//Output: [100000]
//Explanation:
//-The sum of the subarray centered at index 0 with radius 0 is: 100000.
//  avg[0] = 100000 / 1 = 100000.
//Example 3:

kRadiusInput = [100000];
radius = 0;
expectedKRadius = [100000];

actualKRadius = KRadiusSub.GetAverages(kRadiusInput , radius);

Console.WriteLine("Input array:");
PrintArr(kRadiusInput);

correctSolution = ArraysEqual(actualKRadius, expectedKRadius);

Console.WriteLine($"Computed solution:");
PrintArr(actualKRadius);
Console.WriteLine("Expected:");
PrintArr(expectedKRadius);

Console.WriteLine($"Equal? {correctSolution}");

//Input: nums = [8], k = 100000
//Output: [-1]
//Explanation:
//-avg[0] is -1 because there are less than k elements before and after index 0.

kRadiusInput = [8];
radius = 100000;
expectedKRadius = [-1];

actualKRadius = KRadiusSub.GetAverages(kRadiusInput, radius);

Console.WriteLine("Input array:");
PrintArr(kRadiusInput);

correctSolution = ArraysEqual(actualKRadius, expectedKRadius);

Console.WriteLine($"Computed solution:");
PrintArr(actualKRadius);
Console.WriteLine("Expected:");
PrintArr(expectedKRadius);

Console.WriteLine($"Equal? {correctSolution}");
