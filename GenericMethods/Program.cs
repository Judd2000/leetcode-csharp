using GenericMethods;
using System.Globalization;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int a = 10;
int b = 25;

Console.WriteLine($"Pre-swap: a {a}, b {b}");
CustomGenericMethods.Swap<int>(ref a, ref b);
Console.WriteLine($"After swap: a {a}, b {b}");

string w = "Double EEW";
string x = "ECKSS";

Console.WriteLine($"Pre-swap: w {w}, x {x}");
CustomGenericMethods.Swap<string>(ref w, ref x);
Console.WriteLine($"Post-swap: w {w}, x {x}");

//Display base classes
CustomGenericMethods.DisplayBaseClass<string>();
CustomGenericMethods.DisplayBaseClass<PersianCalendar>();

// using default(T) will reset a value to it's default for that data type.


// pattern match on generics.
CustomGenericMethods.PatternMatching(new List<int>());
CustomGenericMethods.PatternMatching(new List<string>());
