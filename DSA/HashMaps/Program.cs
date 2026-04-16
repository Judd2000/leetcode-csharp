using HashMaps;

using ArrayFncts;

Console.WriteLine("****** IS SENTENCE PANAGRAM *****");

//Example 1:

//Input: sentence = "thequickbrownfoxjumpsoverthelazydog"
//Output: true
//Explanation: sentence contains at least one of every letter of the English alphabet.

string inputSentence = "thequickbrownfoxjumpsoverthelazydog";
bool isPanagramExpected = true;

bool isPanagram = PanagramCheck.CheckIfPangram(inputSentence);

Console.WriteLine($"Input string: {inputSentence}");
Console.WriteLine($"Is panagram? Expected: {isPanagramExpected}, Actual: {isPanagram}. Correct? {isPanagramExpected == isPanagram}");
//Example 2:

//Input: sentence = "leetcode"
//Output: false

inputSentence = "leetcode";
isPanagramExpected = false;

isPanagram = PanagramCheck.CheckIfPangram(inputSentence);

Console.WriteLine($"Input string: {inputSentence}");
Console.WriteLine($"Is panagram? Expected: {isPanagramExpected}, Actual: {isPanagram}. Correct? {isPanagramExpected == isPanagram}");

Console.WriteLine($"Extremely Optimized Solution using array access");

//Example 1:

//Input: sentence = "thequickbrownfoxjumpsoverthelazydog"
//Output: true
//Explanation: sentence contains at least one of every letter of the English alphabet.

inputSentence = "thequickbrownfoxjumpsoverthelazydog";
isPanagramExpected = true;

isPanagram = PanagramCheck.CheckIfPanagram_Optimized(inputSentence);

Console.WriteLine($"Input string: {inputSentence}");
Console.WriteLine($"Is panagram? Expected: {isPanagramExpected}, Actual: {isPanagram}. Correct? {isPanagramExpected == isPanagram}");
//Example 2:

//Input: sentence = "leetcode"
//Output: false

inputSentence = "leetcode";
isPanagramExpected = false;

isPanagram = PanagramCheck.CheckIfPanagram_Optimized(inputSentence);

Console.WriteLine($"Input string: {inputSentence}");
Console.WriteLine($"Is panagram? Expected: {isPanagramExpected}, Actual: {isPanagram}. Correct? {isPanagramExpected == isPanagram}");

Console.WriteLine("***** MISSING NUMBER SOLUTION *****");

//Input: nums = [3,0,1]

//Output: 2

//Explanation:

//n = 3 since there are 3 numbers, so all numbers are in the range [0,3]. 2 is the missing number in the range since it does not appear in nums.Input: nums = [3,0,1]

int[] missingNumInput = [3, 0, 1];
int expected = 2;

int givenSolution = MissingNumberSolution.MissingNumber(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");



//Input: nums = [0,1]

//Output: 2

//Explanation:

//n = 2 since there are 2 numbers, so all numbers are in the range [0,2]. 2 is the missing number in the range since it does not appear in nums.Input: nums = [0,1]

missingNumInput = [0, 1]; //same solution ... 2

givenSolution = MissingNumberSolution.MissingNumber(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");



//Input: nums = [9,6,4,2,3,5,7,0,1]

//Output: 8

//Explanation:

//n = 9 since there are 9 numbers, so all numbers are in the range [0,9]. 8 is the missing number in the range since it does not appear in nums.Input: nums = [9,6,4,2,3,5,7,0,1]

missingNumInput = [9, 6, 4, 2, 3, 5, 7, 0, 1];

expected = 8;

givenSolution = MissingNumberSolution.MissingNumber(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");


Console.WriteLine("***** MISSING NUMBER SOLUTION - OPTIMIZED *****");

//Input: nums = [3,0,1]

//Output: 2

//Explanation:

//n = 3 since there are 3 numbers, so all numbers are in the range [0,3]. 2 is the missing number in the range since it does not appear in nums.Input: nums = [3,0,1]

missingNumInput = [3, 0, 1];
expected = 2;

givenSolution = MissingNumberSolution.MissingNumber_Optimized(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");



//Input: nums = [0,1]

//Output: 2

//Explanation:

//n = 2 since there are 2 numbers, so all numbers are in the range [0,2]. 2 is the missing number in the range since it does not appear in nums.Input: nums = [0,1]

missingNumInput = [0, 1]; //same solution ... 2

givenSolution = MissingNumberSolution.MissingNumber_Optimized(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");



//Input: nums = [9,6,4,2,3,5,7,0,1]

//Output: 8

//Explanation:

//n = 9 since there are 9 numbers, so all numbers are in the range [0,9]. 8 is the missing number in the range since it does not appear in nums.Input: nums = [9,6,4,2,3,5,7,0,1]

missingNumInput = [9, 6, 4, 2, 3, 5, 7, 0, 1];

expected = 8;

givenSolution = MissingNumberSolution.MissingNumber_Optimized(missingNumInput);

Console.WriteLine("Input array:");
ArrayFncts.ArrayFncts.PrintArr(missingNumInput);

Console.WriteLine($"Given solution: {givenSolution}. Expected: {expected}. Equal? {givenSolution == expected}");