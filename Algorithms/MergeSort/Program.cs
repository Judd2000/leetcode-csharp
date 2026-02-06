using MergeSort;
using System.Text;


TestCase[] testCases = AssembleTestCases();
RunTests(testCases);

static TestCase[] AssembleTestCases() {
    // Assemble objects in-memory

    // create random arrays between legnths 1 and 250. Do this x250. 
    int maxLength = 250;
    int minLength = 1;
    TestCase[] cases = new TestCase[maxLength];
    for (int i = 0; i < maxLength; i++) {
        Random rand = new();
        int[] arr = new int[rand.Next(minLength, maxLength)];
        for (int j = 0; j < arr.Length; j++)
        {
            arr[j] = rand.Next();
        }
        int[] preSortArray = new int[arr.Length];
        Array.Copy(arr, preSortArray, arr.Length);
        Array.Sort(arr);
        cases[i] = new TestCase(preSortArray, arr, i + 1);
    }

    return cases;
}

static string ArrayToString(int[] arr) {
    StringBuilder builder = new("[ ");
    for (int i = 0; i < arr.Length; i++) { 
        builder.Append($"{arr[i]} ");
    }
    builder.Append(']');
    return builder.ToString();
}

static bool AreArraysEqual(int[] arrOne, int[] arrTwo) {
    if (arrOne.Length != arrTwo.Length) return false;

    for (int i = 0; i < arrOne.Length; i++) {
        if (arrOne[i] != arrTwo[i]) {
            Console.WriteLine("Incongruence at index {0} - {1} does not equal {2}", i, arrOne[i], arrTwo[i]);
            return false;
        }
    }

    return true;
}

static void RunTests(TestCase[] cases) { 
    foreach (var c in cases) {
        int[] customSorted = CustomSort.MergeSort(c.arrayToSort);
        string originalArr = ArrayToString(c.arrayToSort);
        string validSortedString = ArrayToString(c.validSortedArray);
        if (!AreArraysEqual(customSorted, c.validSortedArray)) {
            throw new InvalidDataException($"Array {validSortedString} is correct, custom implementation {ArrayToString(customSorted)} is incorrect. Original array: {originalArr}");
        }
        Console.WriteLine($"Success. Sorting achieved. Array.Sort and CustomSort.MergeSort produced the same result. Case: {c.TestCaseNumber}");
    }
}
