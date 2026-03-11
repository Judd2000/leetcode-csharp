// See https://aka.ms/new-console-template for more information
using Arrays_Strings;
Console.WriteLine("Reverse String - Basic Test Cases");

char[] inputOne = ['h', 'e', 'l', 'l', 'o'];
char[] solutionOne = ['o', 'l', 'l', 'e', 'h'];

ReverseString.Reverse(inputOne);

bool correctSolution = CharArraysEqual(inputOne, solutionOne);

Console.WriteLine($"Solution one is correct: {correctSolution}");


char[] inputTwo = ['H', 'a', 'n', 'n', 'a', 'h'];
char[] solutionTwo = ['h', 'a', 'n', 'n', 'a', 'H'];

ReverseString.Reverse(inputTwo);

correctSolution = CharArraysEqual(inputTwo, solutionTwo);

Console.WriteLine($"Solution two is correct: {correctSolution}");

static bool CharArraysEqual(char[] a, char[] b) {
    if (a.Length != b.Length) return false;

    for (int i = 0; i < a.Length; i++) { 
        if (a[i] != b[i]) return false;
    }
    return true;
}
