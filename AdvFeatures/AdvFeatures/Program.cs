// See https://aka.ms/new-console-template for more information
using AdvFeatures;

static void BuildAnonymousType(string make, string color, int currSpeed)
{
    var car = new { Make = make, Color = color, CurrentSpeed = currSpeed }; //all readonly and init only fields

    //Suddenly the program knows about this class (sealed & readonly)
    Console.WriteLine("The anonymous class is a {0} {1} going {2} mph", car.Make, car.Color, car.CurrentSpeed);
}

BuildAnonymousType("chevy", "magenta", 122);

// All anonymous types automatically derive from System.Object and are given Equals, GetHashCode and ToString.
// ToString provides name value pairs
// GetHashCode uses each var as input to EqualityComparer, two types are the same hash if they have
// the same props and values.

// How does equality work?

static void AnonymousEquality()
{
    var firstCar = new { Color = "White", Make = "Lincoln", CurrentSpeed = 0 };
    var secondCar = new { Color = "White", Make = "Lincoln", CurrentSpeed = 0 };
    // Anonymous types use value based checking for equality.
    if (firstCar.Equals(secondCar)) Console.WriteLine("Anonymous objects are determined to be the same");

    if (firstCar == secondCar)
    {
        Console.WriteLine("Memory refs are identical!");
    }
    else Console.WriteLine("Refs are NOT EQUAL (different mem locations)");

    if (firstCar.GetType().Name == secondCar.GetType().Name)
    {
        // Type names the same?
        Console.WriteLine("Definitely the same type to the compiler");
    }
    else
    {
        Console.WriteLine("Definitely NOT the same type to compiler");
    }

}

static void ReflectOverAnonymousType(object o)
{
    Console.WriteLine("Obj instance of {0}", o.GetType().Name);
    Console.WriteLine("Base classof {0} is {1}", o.GetType().Name, o.GetType().BaseType);
    Console.WriteLine("TOString: {0}", o.ToString());
    Console.WriteLine("Hash Code: {0}", o.GetHashCode());
}


// Probably should only be using anonymous types with LINQ.
// Disadvantages: you don't name the type, it only extends Object, readonly props, no events methods operators nor overrides. Implicitly sealed. Default constructor.
static void MoreAnonymousTypes()
{
    var purchasedItem = new
    {
        TimeBought = DateTime.Now,
        ItemBought = new { Color = "Red", Make = "Saab", CurrentSpeed = 55 }
    };

    ReflectOverAnonymousType(purchasedItem);
}

AnonymousEquality();
MoreAnonymousTypes();

// Anonymous Types with Anonymous Types.

// POINTER TYPES AND OPERANDS.
// * - create pointer variable
// & - obtain address of var in memory
// -> Access fields of a type represented by a pointer, unsafe dot operator
// [] index the slot pointed to by a variable (C++ static void main args*[])
// Can increment, decrement, add, subtract, equality with pointer types
// Stackalloc -> allocate c# arrays directly on stack.
// Fixed: used to temporarily fix variable so address can be found.

// WHy? 1. Looking to optimize application by handling memory outside .net runtime management
// 2. Calling methods of C-based .dll or COM server, but you can also use System.IntPtr and System.Runtime.InteropServices.Marshal

// Must enable 'unsafe code'. <PropertyGroup><AllowUnsafeBlocks>true</AllowUnsafeBlocks></PropertyGroup>

// when working with pointers you need an 'unsafe block'.
//unsafe { 

//} => can use structs, classes, type members and params that are unsafe.
// heck, you can even make your entire Main method 'unsafe' with the keyword: static unsafe void Main

unsafe
{
    int myInt;
    int* pointerToInt = &myInt;

    // Assign value with pointer dereferencing
    *pointerToInt = 123;

    Console.WriteLine("Value of myInt {0}", myInt);
    Console.WriteLine("Address of myInt {0:X}", (int)&pointerToInt);
}

// the following are identical, except one is safe and one is not

unsafe static void UnsafeSwap(int* i, int* j)
{
    int temp = *i;
    *i = *j;
    *j = temp;
}

static void SafeSwap(ref int i, ref int j)
{
    int temp = i;
    i = j;
    j = temp;
}

int i = 20;
int j = 10;

Console.WriteLine("Values before safe swap: i = {0}, j = {1}", i, j);
SafeSwap(ref i, ref j); Console.WriteLine("Values after safe swap: i = {0}, j = {1}", i, j);

Console.WriteLine("Values before unsafe swap: i = {0}, j = {1}", i, j);
unsafe
{
    UnsafeSwap(&i, &j);
    Console.WriteLine("Values after unsafe swap: i = {0}, j = {1}", i, j);
}
Console.WriteLine("Values after unsafe swap: i = {0}, j = {1}", i, j);

static unsafe void UsePointerToPrint()
{
    Pointyy point;
    Pointyy* p = &point;

    // pointer to instance access
    p->x = 100;
    p->y = 200;

    Console.WriteLine(p->ToString()); // like an unsafe pointer dot accessor

    // pointer indirection
    Pointyy point2;
    Pointyy* p2 = &point2;

    (*p2).x = 2; // dereference a pointer and use dot works, too
    (*p2).y = 4;
    Console.WriteLine((*p).ToString());
}

UsePointerToPrint();

//stackalloc

static unsafe string UnsafeStackalloc() {
    char* p = stackalloc char[58];
    for (int i = 0; i < 58; i++)
    {
        p[i] = (char)(i+65);
    }
    return new string(p);
}

Console.WriteLine("Making a string with unsafe stackalloc {0}", UnsafeStackalloc());

// How does one interact with heap allocated items with unsafe methods?
// garbage collection automatically moves memory around. Use 'fixed' keyword to PIN it and exempt it from garbage collection cleaning and movement. Can only be done in unsafe context.

unsafe static void UseAndPinPoint() {
    PointRef pt = new PointRef { x = 5, y = 2 };

    // fixed is essentially a method that takes what you want to be fixed. After block is done, it is no longer pinned in the same way
    fixed (int* p = &pt.x) { // lock the reference var in memory
        // now can use int* var (heap stored)
    }

    // After block, pt is ready to be garbage collected
    Console.WriteLine("Point is: {0}", pt);
}

// sizeof operator: used to obtain bytes of INTRINSIC TYPE usually, but not custom types UNLESS IN UNSAFE CONTEXT.

static void UseSizeofOperator() {
    Console.WriteLine("Safe calls to sizeof for intrinsic datatypes");
    Console.WriteLine("The size of 'short' is {0}", sizeof(short));
    Console.WriteLine("The size of 'int' is {0}", sizeof(int));
    Console.WriteLine("The size of 'long' is {0}", sizeof(long));
}

unsafe static void UnsafeUseSizeof() {
    Console.WriteLine("The size of PointRef is: {0}", sizeof(PointRef));
}

UseSizeofOperator();
UnsafeUseSizeof();

class PointRef {
    public int x;
    public int y;

    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
