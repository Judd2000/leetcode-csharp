// Use BigInteger
using System;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using System.Text;
// using FunWithBitwiseOperations;

Console.WriteLine("Pro C# Chapter 3");
// UseDatesAndTimes();

static void UseDatesAndTimes()
{
    Console.WriteLine("=> Dates and Times");

    // Constructors Y M D
    DateTime dt = new DateTime(2025, 12, 4);

    Console.WriteLine("The day of {0} is {1}", dt.Date, dt.DayOfWeek);

    dt = dt.AddMonths(2);

    Console.WriteLine("Daylight Savings: {0}", dt.IsDaylightSavingTime());

    // Constructor: Hrs mins seconds

    TimeSpan ts = new TimeSpan(4, 30, 0);

    Console.WriteLine(ts);

    // DateOnly and TimeOnly are half of DateTime each.

    DateOnly d = new DateOnly(2025, 9, 25);

    TimeOnly t = new TimeOnly(12, 30);

    Console.WriteLine("Date {0}, Time: {1}", d, t);

    //Declare BigInt through BigInteger.Parse and a string OR a Byte Array
    BigInteger hugeInteger = BigInteger.Parse("88888888888888888888888888888888888888888888888888888888888888888888888888888");
    Console.WriteLine("This is a BIG Int: {0}", hugeInteger);

    // underscore is a digit separator for readability (still valid number).
    // YOU CAN USE THIS WITH BINARY DATA I.E 0b_0001_0000
    Console.WriteLine(123_000_000_000);
}

// String interpolation
// string name = "Bill";
// string contact = "111-222-3344";
// string interpolated = $"{name}'s phone number is {contact}";

// Console.WriteLine(interpolated);

// verbatim strings have an '@' - meaning, ignore escape chars. Useful for paths. You can also insert "" for a double quote. It can also be both verbatim and interpolated
// string verbatim = @"\C:\Users\user_one\Desktop - ""How""";
// Console.WriteLine(@$"Verbatim string: {verbatim} ever!");

// you can override default string comparison
// StringEqualitySpecifyingCompareRules();

static void StringEqualitySpecifyingCompareRules()
{
    Console.WriteLine("Equality with default rules.");
    string s1 = "Hello";
    string s2 = "HELLO";
    Console.WriteLine($"Are s1 {s1} and s2 {s2} equal? {s1.Equals(s2)}");

    Console.WriteLine("Ignore case");
    // add ignore case 'flag'
    Console.WriteLine($"Are s1 {s1} and s2 {s2} equal? {s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase)}");

    // You can also pass these string comparison arguments into other String functions that use comparison
    Console.WriteLine("Ignore case with .IndexOf");
    Console.WriteLine($"Using IndexOf with ignoreCase flag, s1 {s1} has an 'h' or 'H' case insensitive, at index {s1.IndexOf('h', StringComparison.InvariantCultureIgnoreCase)}");


}

// System.Text.StringBuilder is much more efficient than string "mutation" if you're working with large amounts of text data
// FunWithStringBuilder();

static void FunWithStringBuilder()
{
    Console.WriteLine("FUN With stringbuilder!");
    StringBuilder builder = new StringBuilder("Starting Characters!!!");
    builder.Append(" WOW!");
    builder.AppendLine(" WOAH HOW NEAT!");

    builder.Append("NOW we are on a new line.");

    builder.Replace("WOW!", "WOWZA!");
    Console.WriteLine($"Stringbuilder output: {builder.ToString()}");
}

// Narrowing and Widening Data Type Conversions.

// typeConversionCode();

static void typeConversionCode()
{
    short numb1 = 9, numb2 = 10;
    Console.WriteLine("{0} + {1} = {2}", numb1, numb2, Add(numb1, numb2));

    numb1 = 30000;

    numb2 = 30000;

    // Error because of NARROWING.
    // short answer = Add(numb1, numb2);

    // If you want to do a narrowing anyway, need explicit cast ()
    short answer = (short)Add(numb1, numb2);

    Console.WriteLine("Answer: {0}", answer);

    // CHECKED keyword. You can also do a block or a project wide project file setting:

    /*
    <PropertyGroup>
<CheckForOverflowUnderflow>true</CheckForOverflowUnderflow> </PropertyGroup>
    */

    // answer = checked((short) Add(numb1, numb2));

    checked
    {
        answer = (short)Add(numb1, numb2);
    }

}

// WIDENING causes no loss of data. 32,767 to 2,147,483,647
static int Add(int x, int y) { return x + y; }
;

// Implicit typing using 'var'. NOT dynamic typing like in JS

// You can't use implicit types for fields in a class nor as a return value / param type.
// No default or undeclared. Reassigning it to a different type results in an error. Use 'dynamic' for dynamically typed.

// DeclareImplicitVars();
static void DeclareImplicitVars()
{
    var myBool = true;
    var myString = "A string";
    Console.WriteLine("myBool is a: {0}", myBool.GetType().Name);
    Console.WriteLine("myString is a: {0}", myString.GetType().Name);

    var myUInt = 0u;
    var myInt = 0;
    var myLong = 0L;
    var myDouble = 0.5;
    var myFloat = 0.5F;
    var myDecimal = 0.5M;

    Console.WriteLine("myUInt is a: {0}", myUInt.GetType().Name);
    Console.WriteLine("myInt is a: {0}", myInt.GetType().Name);
    Console.WriteLine("myLong is a: {0}", myLong.GetType().Name);
    Console.WriteLine("myDouble is a: {0}", myDouble.GetType().Name);
    Console.WriteLine("myFloat is a: {0}", myFloat.GetType().Name);
    Console.WriteLine("myDecimal is a: {0}", myDecimal.GetType().Name);
}

// Iterations and Loops
// Loops();

static void Loops()
{
    // for loop
    for (int i = 0; i < 4; i++)
    {
        Console.WriteLine("Number is {0}", i);
    }

    // for each (all items in container) Anything that implements IEnumerable interface
    string[] carTypes = { "Ford", "BMW", "Yugo", "Honda" };

    foreach (string c in carTypes)
    {
        Console.WriteLine(c);
    }

    // while
    // WhileLoopExamples();
    static void WhileLoopExamples()
    {
        string isDone = "";
        while (isDone.ToLower() != "yes")
        {
            Console.WriteLine("In while loop");
            Console.Write("Done? [yes] [no]:");
            isDone = Console.ReadLine();
        }
        Console.WriteLine("Done? {0}", isDone);

        // do while

        isDone = "no";

        do
        {
            Console.WriteLine("In do while");
            Console.Write("Done?: ");
            isDone = Console.ReadLine();
        } while (isDone.ToLower() != "yes");
    }
}

// Branching constructs (if else and switch)
// Branches();
static void Branches()
{
    // conditions have to be boolean, -1 and 0 aren't valid.
    string stringData = "Text";
    if (stringData.Length > 0)
    {
        Console.WriteLine("String longer than 1");
    }

    // If Else with pattern matching.

    object testItem1 = 123;
    object testItem2 = "HI";

    if (testItem2 is string)
    {
        Console.WriteLine("I am a string, {0}", testItem2);
    }
    if (testItem2 is int)
    {
        Console.WriteLine("I am an int. {0}", testItem1);
    }
    else
    {
        Console.WriteLine("I am a {0}", testItem1.GetType());
    }
}

static void IfElsePatternMatchingUpdatedInCSharp9()
{
    Console.WriteLine("======= C# 9 If Else Pattern Matching Improvements =======");
    object testItem1 = 123;
    Type t = typeof(string);
    char c = 'f';
    //Type patterns
    if (t is Type)
    {
        Console.WriteLine($"{t} is a Type");
    }
    //Relational, Conjuctive, and Disjunctive patterns
    if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
    {
        Console.WriteLine($"{c} is a character");
    }

    if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',')
    {
        Console.WriteLine($"{c} is a character or separator");
    }
    ;
    //Negative patterns
    if (testItem1 is not string)
    {
        Console.WriteLine($"{testItem1} is not a string");
    }
    if (testItem1 is not null)
    {
        Console.WriteLine($"{testItem1} is not null");
    }
    Console.WriteLine();
}
//Parenthesized patterns

// ternary operator

// object inteeger = 2;
// Console.WriteLine("Is inteeger an int? {0}", inteeger is int ? "yes" : "no");

// SwitchExample();

static void SwitchExample()
{
    Console.WriteLine("1 [C#], 2 [VB]");
    Console.Write("Please pick your language preference: ");
    string langChoice = Console.ReadLine();
    int n = int.Parse(langChoice);
    switch (n)
    {
        case 1:

            Console.WriteLine("Good choice, C# is a fine language.");

            break;
        case 2:

            Console.WriteLine("VB: OOP, multithreading, and more!");

            break;
        default:

            Console.WriteLine("Well...good luck with that!");

            break;
    }

    //compact return version

    //     static string FromRainbow(string colorBand) {
    // return colorBand switch
    // {

    // "Red" => "#FF0000",

    // "Orange" => "#FF7F00",

    // "Yellow" => "#FFFF00",

    // "Green" => "#00FF00",

    // "Blue" => "#0000FF",

    // "Indigo" => "#4B0082",

    // "Violet" => "#9400D3",

    // _ => "#FFFFFF",
    // }; }

    // with tuples, (tock, paper) => 'paper wins'
}

// YOU CAN COMBINE CASE STATEMENTS:
// case DayOfWeek.Saturday:
// case DayOfWeek.Sunday:
//  your logic
//  break;

// You can also do a type switch: case string s:, case int s:

// you can add a 'when' match that will match the first if the second holds: case int i when i == 2: (then the order matches if you have multiple that start with case int i)


// CHAPTER 4

// Array initilization

// ArrayInitialiation();

static void ArrayInitialiation()
{

    Console.WriteLine("=> Array Initialization");

    string[] stringArray = new string[] { "one", "two", "three" };

    bool[] boolArray = new bool[] { false, false, true };

    Console.WriteLine("Length of arrays {0}, {1}", stringArray.Length, boolArray.Length);

    // be careful to not specify a size and overflow it with the initialization:

    // bool[] boolArray2 = new bool[3]{true, true, true, false};

    // Implicit typing of local arrays
    var a = new[] { 1, 10, 12 };
    Console.WriteLine("a is a: {0}", a.ToString());

    // b is really double[]
    var b = new[] { 1, 1.5, 2, 2.5 };
    Console.WriteLine("b is a: {0}", b.ToString());

    // c is really string[] since string can be null. Mismatches throw errors
    var c = new[] { "hello", null, "world" };

    // array of Objects allows it to be of any type (it's the parent)
    object[] myObjects = new object[4];
    myObjects[0] = 10;
    myObjects[1] = false;
    myObjects[2] = new DateTime(1969, 3, 24);
    myObjects[3] = "Form & Void";

    foreach (object obj in myObjects)
    {
        Console.WriteLine("Type: {0}, Value: {1}", obj.GetType(), obj);
    }

    //Multidimensional arrays

    // Shorthand for symmetrical matrix
    int[,] myMatrix;
    myMatrix = new int[3, 4];

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            myMatrix[i, j] = i * j;
        }
    }

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            Console.Write(myMatrix[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

    // Jagged Multidimensional array

    Console.WriteLine("Jagged multidimensional array");
    int[][] myJagArray = new int[5][];

    for (int i = 0; i < myJagArray.Length; i++)
    {
        myJagArray[i] = new int[i + 2];
    }

    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < myJagArray[i].Length; j++)
        {
            Console.Write(myJagArray[i][j] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

// System.Array base class.

// SystemArrayFunctionality();

static void printArray(object[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.Write(array[i] + ",");
    }
    Console.WriteLine("\n");
}

static void SystemArrayFunctionality()
{
    Console.WriteLine("=> System.Array time! Gothic bands array");

    string[] gothicBands = { "Tones on Tail", "Bauhaus", "Sisters of Mercy" };

    // Print out in order declared.
    Console.WriteLine("In order declared:::");
    for (int i = 0; i < gothicBands.Length; i++)
    {
        Console.Write(gothicBands[i] + ",");
    }
    Console.WriteLine("\n");

    Console.WriteLine("Reversed:");

    Array.Reverse(gothicBands);
    printArray(gothicBands);

    // empty array of all but first
    Console.WriteLine("Cleared all but one");
    Array.Clear(gothicBands, 1, 2);
    printArray(gothicBands);

    // System.Index -> index into a sequence
    // System.Range -> subrange of indices, allowing you to do fun things like this

    Index idx = ^1; // instead of .Length - 1
    gothicBands = ["Tones on Tail", "Bauhaus", "Sisters of Mercy"];
    Console.WriteLine("Array at -1 {0}", gothicBands[idx]);

    // You can also do a rannge with .. (last is not inclusive)
    Console.WriteLine("array 0-1");
    // foreach (string item in gothicBands[0..2]) Console.WriteLine("Element {0}", item);

    // Yoiu can also declare a range
    Range range = 0..2;
    foreach (string item in gothicBands[range]) Console.WriteLine("Element {0}", item);

    // A range can also contain indices
    Index ind1 = 0;
    Index ind2 = ^1;
    Range range2 = ind1..ind2;

    Console.WriteLine("Range of two index objs {0}", range);

    // if no start, range assumes start. If no end, range assumes end

    Range range3 = ind1..;
    Console.WriteLine("Range of 1 index obj and end {0}", range);

    //ElementAt element a location, which can also take an index
    Console.WriteLine("Last element using ElementAt and an index (last) {0}", gothicBands.ElementAt(ind2));
}

// functions and methods (fn returns, method does not.)

// you can also use functions inside functions (think JS)

// Console.WriteLine("Calling function in function {0}", AddWrapper(1,100));

static int AddWrapper(int x, int y)
{
    return Add();
    int Add() => x + y;
}

// Static Local Functions
static int AddWapperWithSideEffect(int x, int y)
{
    return Add(x, y);

    // can mark local functions as static to prevent side effects.
    // static int Add () {
    //     x += 1;
    //     return x + y;
    // }

    int Add(int x, int y) => x + y;
}

// Methods pass data as a copy by default. modifiers: out -> pass by reference. ref -> initially assigned by caller and optionally modified, no error if method didn't set it. in -> readonly ref
// params: send variable number of args as a single param. Needs to be final param.

// FunWithMethods();

static void FunWithMethods()
{
    int x = 1;
    int y = 2;
    // Console.WriteLine("Pass by ref {0} ({1} + {2})", Add(x, y), x, y);
    static int Add(int x, int y)
    {
        int ans = x + y;
        // these won't hold
        x = 1000;
        y = 8888;
        return ans;
    }

    // reference types are not pass by value (except string)
    // output params -> obligated to assign them before exiting method. The calling code DOES NOT NEED TO DEFINE IT.

    // AddUsingOut(x, y, out int answer);

    // Console.WriteLine("Answer {0}", answer);

    static void AddUsingOut(int x, int y, out int ans)
    {
        ans = x + y;
    }

    // Return multiple outputs!

    FillValues(out int a, out string b, out bool c);
    // Console.WriteLine("Multiple return values using out params {0}, {1}, {2}", a, b, c);
    static void FillValues(out int a, out string b, out bool c)
    {
        a = 1;
        b = "2";
        c = true;
    }

    // Discard variables - you can pass _ as an out to not care about it. The method is still populating them, they are just discarded.
    FillValues(out int d, out _, out _);
    // Console.WriteLine("I only care about the first out var {0}", d);

    // "Ref"
    // What is it? Parameter that can be changed by called method and c. MUST BE INITIALIZED BEFORE PASSED. Like a 'reference' to an existing variable.

    // string s1 = "fleep";
    // string s2 = "florp";
    // Console.WriteLine("S1 before swap {0}, S2 Before Swap {1}", s1, s2);
    // SwapStrings(ref s1, ref s2);

    static void SwapStrings(ref string s1, ref string s2)
    {
        string temp = s1;
        s1 = s2;
        s2 = temp;
    }

    // Console.WriteLine("S1 after swap {0} s2 after swap {1}", s1, s2);

    // in modifier -> pass by reference and called method can't modify. (Can use less memory because copies don't have to be made)
    // parameter arrays!

    // this is cool! you can also pass in a predefined array if you realllly want to

    // Console.WriteLine("Average {0}", Average(1.0, 2.3, 2.5, 2.6));

    static double Average(params double[] values)
    {
        double sum = 0;
        if (values.Length == 0) return sum;

        for (int i = 0; i < values.Length; i++)
        {
            sum += values[i];
        }

        return (sum / values.Length);
    }

    // Optional Params, def values must be known at COMPILE TIME (not a result of a method)
    // static void EnterLogData(string message, string owner = "P. Sherman 42 Wallaby Way, Sydney") {
    //     Console.WriteLine("Error: {0}", message);
    //     Console.WriteLine("Owner of error: {0}", owner);
    // }
    // Console.WriteLine("Default val, optional. With explicit owner:");
    // EnterLogData("Error err rorrr rorror", "12345");
    // Console.WriteLine("Default value fallback:");
    // EnterLogData("BIG ISSUE");

    // named arguments -> order does not matter.

    static void DisplayFancy(ConsoleColor textColor, ConsoleColor backgroundColor, string message)
    {
        ConsoleColor oldTextColor = Console.ForegroundColor;
        ConsoleColor oldbgColor = Console.BackgroundColor;

        Console.ForegroundColor = textColor;
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(message);

        Console.ForegroundColor = oldTextColor;
        Console.BackgroundColor = oldbgColor;
    }

    // DisplayFancy(ConsoleColor.Green, ConsoleColor.DarkBlue, "Wow this is so fancy");
    // Console.WriteLine("BACk to normal");

    // NAMED VERSION you can also put position params before the named ones.00

    // DisplayFancy(message: "WAWAWA", backgroundColor: ConsoleColor.Red, textColor: ConsoleColor.White);

    // method overloading - same name, different type or parameters.

    // overload with optional -> compiler matches signatures that make sense. Don't make methods that only vary on optional params...
    // In, out and ref are not considered distinct (not part of fun signature)


    //ENUMS
}

// Console.WriteLine("Dang it enums");

// AskForBonus(EmployeeTypeEnum.Manager);
// AskForBonus(EmployeeTypeEnum.VicePresident);
// AskForBonus(EmployeeTypeEnum.Grunt);

static void AskForBonus(EmployeeTypeEnum e)
{
    // Enums need to be at the top of a file otherwise it has a hissy fit.
    switch (e)
    {
        case EmployeeTypeEnum.Manager:
            Console.WriteLine("How about a 10% bonus?");
            break;
        case EmployeeTypeEnum.VicePresident:
            Console.WriteLine("How about a 20% bonus?");
            break;
        case EmployeeTypeEnum.Grunt:
            Console.WriteLine("You get what you deserve!");
            break;
        default:
            break;
    }
}

// You can call obj methods on an enum.

// Console.WriteLine("Enum EmployeeType uses a {0} for storage", Enum.GetUnderlyingType(typeof(EmployeeTypeEnum))); //typeof and typename works, you can also use the static GetType for a class.

// // Enum toString.

// EmployeeTypeEnum emp = EmployeeTypeEnum.Manager;

// Console.WriteLine("emp is a {0}", emp.ToString());

// // if you want value and not the shorthand name, cast the enum to the underlying type

// Console.WriteLine("Under the hood, the enum's val is {0}", (int) emp);

// // GetValues -> array

// Array enumData = Enum.GetValues(emp.GetType());

// for (int i = 0; i < enumData.Length; i++) {
//     // getvalue returns the object which default toString shows the name, casting its underlying variable type is how to get the value.
//     Console.WriteLine("Name: {0}, Value: {1}", enumData.GetValue(i), (int) enumData.GetValue(i));
// }


// Bitwise operations: &, |, ^, ~, <<, >>
// ~ flips bits. ^ is like and not but for bits. << >> are shifts.

// Bits and Enums

// ContactPreferenceEnum emailAndPhone = ContactPreferenceEnum.Email | ContactPreferenceEnum.Phone;

// must always be after top-level statements. Not a problem in a class.
// Can check if one of the values exists in your combined value

// Console.WriteLine("None? {0}", (emailAndPhone | ContactPreferenceEnum.None) == emailAndPhone);
// Console.WriteLine("Email? {0}", (emailAndPhone | ContactPreferenceEnum.Email) == emailAndPhone);
// Console.WriteLine("Phone? {0}", (emailAndPhone | ContactPreferenceEnum.Phone) == emailAndPhone);

// Struct! Lightweight class. No inheritance, Value Type, stored on stack.
// FunWithStructs();

static void FunWithStructs()
{
    Point newPoint = new Point();
    newPoint.X = 349;
    newPoint.Y = 76;
    newPoint.Display();

    // Now use the constructor
    Point p2 = new Point(123, 456);
    Console.WriteLine("Now use the constructor to make a new Point");
    p2.Display();
}

// FunWithValueAndReferenceTypes();

static void FunWithValueAndReferenceTypes()
{
    Console.WriteLine("Fun with value and reference types");
    Console.WriteLine("Assigning value types");

    Point p1 = new Point(10, 10);
    Point p2 = p1; // copy by value two copies on the stack, not a reference to a memory location

    p1.Display();
    p2.Display();

    p1.X = 25;
    Console.WriteLine("After changing p1.X");
    p1.Display();
    p2.Display();

    // With class instances, you are pointing at the same memory location, not a true copy.
    // Now with point Class and not struct

    PointRef pee1 = new PointRef(10, 10);
    PointRef pee2 = pee1; // copy the reference, not the object itself.

    pee1.Display();
    pee2.Display();

    pee2.Y = 99;
    Console.WriteLine("After changing pee2.Y");
    pee1.Display();
    pee2.Display();

    // What about a value type that contains a reference type...confusing!

    // now what if we mix and match.

    Rectangle r1 = new Rectangle(1, 2, 3, 4, "The first one");

    Rectangle r2 = r1; // copy by value of the struct, but reference type inside is still a reference.

    r2.RectangleInfo.InfoString = "New info wowza";
    r2.RectangleBottom = 6;

    r1.Display();
    r2.Display();

    // What happens when we pass reference types by value vs by reference

    // Person fred = new Person("Fred", 23);
    // Console.WriteLine("Before SendAPersonByValue");
    // fred.Display();

    // Person.SendAPersonByValue(fred);
    // Console.WriteLine("After SendAPersonByValue");
    // fred.Display();

    // now pass by reference
    Person fred = new Person("Fred", 23);
    Console.WriteLine("Before SendAPersonByRef");
    fred.Display();

    Person.SendAPersonByReference(ref fred);
    Console.WriteLine("After SendAPersonByReference");
    fred.Display();
}

// Nullable types.

FunWithNullableTypes();

static void FunWithNullableTypes()
{
    // Value types can never be null.

    // Nullable types can be all the value of it's types, plus null.

    // Nullable data variable types have ? 

    DatabaseReader dr = new DatabaseReader();

    int data = dr.GetIntFromDatabase() ?? 100; // if null, use 100
    Console.WriteLine("Value of data: {0}", data);
}