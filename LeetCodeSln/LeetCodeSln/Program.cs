using LeetCodeSln;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Test Cases
int[] prices = new int[]{7,1,5,3,6,4};
int expected = 5;

int actual = BuySellStock.MaxProfit(prices);
Console.WriteLine("Expected: {0}, Actual: {1}, Equal? {2}", expected, actual, expected == actual);
// Console.WriteLine("Expected: {0}, Actual: {1}, Equal? {3}", expected, actual, expected == actual);

// Test Cases
prices = new int[]{4,1,5,2,7};
expected = 5;

actual = BuySellStock.MaxProfit(prices);
Console.WriteLine("Expected: {0}, Actual: {1}, Equal? {2}", expected, actual, expected == actual);