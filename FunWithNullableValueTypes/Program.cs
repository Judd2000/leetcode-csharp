static void LocalNullableVariables() {
    Nullable<int> nullableInt = 10;

    double? nullableDouble = 3.14;

    bool? nullableBool = null;

    char? nullableChar = 'a';

    int?[] arrayOfNullableInts = new int?[10];
    
    // ? is shorthand for Nullable<type>

    // to find out if it is null, try .HasValue or != null
}

Console.WriteLine("*** fun with Nullable Value types ");
DatabaseReader reader = new DatabaseReader();

// Get int from "database".
int? i = reader.GetIntFromDatabase();

if (i.HasValue)
{
    Console.WriteLine("Value of 'i' is {0}", i.Value);
}
else {
    Console.WriteLine("Value of 'i' is undefined.");
}

// Get bool from 'database'
bool? b = reader.GetBoolFromDatabase();

if (b != null)
{
    Console.WriteLine("Value of b is: {0}", b.Value);
}
else {
    Console.WriteLine("Value of b is undefined");
}

// Nullable reference types need to be enabled explicitly

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

    // Null coalescing assignment operator (we have one in JS)
    // assign right to left only if left side is null.

    int? nullableInt = null;
    nullableInt ??= 12;
    nullableInt ??= 14;

    Console.WriteLine("Value of nullable int {0}", nullableInt);

    // Null conditional operator like in JS. ?
}

TesterMethod(null);
static void TesterMethod(string[] args) { 
    Console.WriteLine($"You sent me {args?.Length} arguments")
}
