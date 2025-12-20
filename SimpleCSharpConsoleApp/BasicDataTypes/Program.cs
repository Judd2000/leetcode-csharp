Console.WriteLine("***** Fun with Basic Data Types *****");
LocalVarDeclarations();
DefaultDeclarations();
NewDataTypes();
StrangeObjectFunctionality();
DataTypeFunctionality();

static void LocalVarDeclarations() {
    int myInt = 0;

    string myString;

    myString = "Variable reassignment";

    // 3 on one line
    bool b1 = true, b2 = false, b3 = b1;

    // System.Boolean datatype

    System.Boolean b4 = false;

    Console.WriteLine("Data values are: {0}, {1}, {2}, {3}, {4}, {5}", myInt, myString, b1, b2, b3, b4);
    Console.WriteLine();
}

static void DefaultDeclarations() {
    Console.WriteLine("=> Default Declarations:");

    int myInt = default;
    Console.WriteLine(myInt);
}

// New data types
static void NewDataTypes() {
    Console.WriteLine("=> Using new to create variables");
    bool b = new bool(); //false

    int i = new int(); // 0

    double d = new double(); // 0

    DateTime dt = new DateTime(); // 1/1/0001 @ 12:00 AM for some reason

    Console.WriteLine("{0}, {1}, {2}, {3}", b, i, d, dt);
    Console.WriteLine();
}

static void NewNewDataTypes() {
    // Value datatypes extend System.ValueType (special object)
    // Allocated on stack
    Console.WriteLine("=> Using new to create variables:");
    bool b = new(); // false

    int i = new(); // Set to 0.

    double d = new(); // Set to 0.

    DateTime dt = new();

    Console.WriteLine("{0}, {1}, {2}, {3}", b, i, d, dt);
    Console.WriteLine();
}

static void StrangeObjectFunctionality() {
    // int is shorthand for System.Int32, so has object
    // methods

    Console.WriteLine("12.GetHashCode() = {0}", 12.GetHashCode());

    Console.WriteLine("12.Equals(23) = {0}", 12.Equals(23));

    Console.WriteLine("12.ToString() = {0}", 12.ToString());

    Console.WriteLine("12.GetType() = {0}", 12.GetType());

    Console.WriteLine();
}

// Numerical Datatype members (MaxValue & MinValue)

static void DataTypeFunctionality() {
    Console.WriteLine("=> Data type Functionality:");

    Console.WriteLine("Max of int: {0}", int.MaxValue);

    Console.WriteLine("Min int: {0}", int.MinValue);

    Console.WriteLine("Max of double: {0}", double.MaxValue);

    Console.WriteLine("Min of double: {0}", double.MinValue);

    Console.WriteLine("double.Epsilon: {0}", double.Epsilon);

    Console.WriteLine("double.PositiveInfinity: {0}", double.PositiveInfinity);

    Console.WriteLine("double.NegativeInfinity: {0}", double.NegativeInfinity);

    // To change to a long integer, use 'L'. to declare float use F.
    // use m or M to floating point number to get decimal

    Console.WriteLine();

}

static void CharFunctionality() {
    Console.WriteLine("Char type functionality");

    char myChar = 'a';

    Console.WriteLine("char.isDigit('a'):, {0}", char.IsDigit(myChar));

    Console.WriteLine("char.IsLetter('a'): {0}", char.IsLetter(myChar));

    // And many more... isPunctuation, isWhiteSpace etc.
    Console.WriteLine();
}

// .TryParse and .Parse

bool.TryParse("onetwothree", out bool g);
Console.WriteLine("Bool G: {0}", g);

