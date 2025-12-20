// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");

// Tuples are easy! Lightweight and easy-to-use multivariable return method.

(string, int, string) tupleVals = ("1", 2, "3");

var otherTupleVals = ("4", 5, "6");

// Access by ItemX

Console.WriteLine($"Item 1 {otherTupleVals.Item1}, Item 2 {otherTupleVals.Item2}, Item 3 {otherTupleVals.Item3}");

// You can also assign specific names to each tuple value.
// If you define names on the left and right, only the left will hold

(string firstLetter, int firstNum, string secondLetter) valsWithNames = ("7", 8, "9");
Console.WriteLine($"Item 1 {valsWithNames.firstLetter}, Item 2 {valsWithNames.firstNum}, Item 3 {valsWithNames.secondLetter}");

// when you want to set with names on the right you need to use 'var'

var valsWithNames2 = (FirstLetter: "firstLetter", FirstInt: 1, SecondLetter: "sedcgewa");
Console.WriteLine($"Item 1 {valsWithNames2.FirstLetter}, Item 2 {valsWithNames2.FirstInt}, Item 3 {valsWithNames2.SecondLetter}");

// You can also do tuples inside of tuples.

Console.WriteLine("=> Nested Tuples");
var nt = (5, 4, ("a", "b"));

// Inferred tuple names.
var foo = new { Prop1 = "first", Prop2 = "second" };
var bar = (foo.Prop1, foo.Prop2);
Console.WriteLine($"{bar.Prop1};{bar.Prop2}");

// Tuple comparisons perform implicit typecasts.

Console.WriteLine("=> Tuples Equality/Inequality"); // lifted conversions
var left = (a: 5, b: 10);
(int? a, int? b) nullableMembers = (5, 10);
Console.WriteLine(left == nullableMembers); // Also true

// converted type of left is (long, long)
(long a, long b) longTuple = (5, 10);
Console.WriteLine(left == longTuple); // Also true

// great for returning sectioned data

static (string first, string middle, string last) SplitNames (string fullName) {
    return ("Forge", "Mod", "loader");
}

Console.WriteLine(SplitNames("wawa"));

// discards and tuples.
// do a discard like you would with an out var!

var (first, _, last) = SplitNames("");
Console.WriteLine($"{first}:{last}");

// Tuples with switches

static string RockPaperScissors(string first, string second) { 
    return (first, second) switch { 
        ("rock", "paper") => "Paper wins",
        ("rock", "scissors") => "Scissors wins",
        ("paper", "rock") => "Paper emerges victorious",
        ("paper", "scissors") => "Scissors wins",
        ("scissors", "rock") => "Rock wins.",
        ("scissors", "paper") => "Scissors wins.",
        (_, _) => "Tie.",
    };
}

// Tuple deconstruction.
(int x, int y) myTuple = (4, 5);
int x = 0;
int y = 0;

(x, y) = myTuple;

(int x1, int y1) = myTuple; // => this works too, declare vars on same line.

// You can also mix assignment and declaratino

int x2 = 0;
(x2, int y2) = myTuple;

//Destructuring custom types.
Point p = new Point(7, 7);

var pointVals = p.Deconstruct();

Console.WriteLine($"After destructure, let's get the values. xPos: {pointVals.xPos}, yPos: {pointVals.yPos}");

// Destructure with positional pattern matching
// implicit deconstruct

Point p2 = new Point(8, 3);

int xp2 = 0;
int yp2 = 0;
//(int xPos, int yPos) = p2; -> supposed to work but doesn't