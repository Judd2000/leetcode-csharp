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

bool correctSolution = CharArraysEqual(inputOne, solutionOne);

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

correctSolution = CharArraysEqual(inputTwo, solutionTwo);

Console.WriteLine($"Solution two is correct: {correctSolution}");

static bool CharArraysEqual(char[] a, char[] b) {
    if (a.Length != b.Length) return false;

    for (int i = 0; i < a.Length; i++) { 
        if (a[i] != b[i]) return false;
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

