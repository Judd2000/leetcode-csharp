using System.Net.Mime;

struct Point
{

    // IN C# 10, you can initialize fields directly in the struct like this:
    // public int X = 10;
    // public int Y = 10;
    public int X;
    public int Y;

    // you can also do readonly fields
    public readonly string owner;

    // Even though it's not displayed, this struct has a default constructor that
    // initializes the members to default values (0 for int).

    public Point(int xPos, int yPos, string ownerName = "")
    {
        X = xPos;
        Y = yPos;
    }

    public void Increment()
    {
        X++;
        Y++;
    }

    public void Decrement()
    {
        X--;
        Y--;
    }

    public void Display()
    {
        Console.WriteLine("X: {0}, Y: {1}", X, Y);
    }
}

// Can also do readonly structs

readonly struct ReadonlyPoint
{
    public int X { get; }
    public int Y { get; }

    public void Display()
    {
        Console.WriteLine("X: {0}, Y: {1}", X, Y);
    }

    public ReadonlyPoint(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }
}

// Ref structs

// Requires all instances of struct to be stack allocated, can't be assigned as a prop somewhere else. (can't be referenced from heap)
// Also cannot: be assigned to variable of type Object, dynamic, or interface type; be boxed to System.Object or to any interface type; 
// be a type argument in generic type or method; implement any interfaces; be captured by a lambda expression or a local function.

// Disposable Ref Structs

ref struct DispRefStruct
{
    public int X;

    public readonly int Y;

    public readonly void Display()
    {
        Console.WriteLine("X: {0}, Y: {1}", X, Y);
    }

    public DispRefStruct(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }

    public void Dispose()
    {
        // clean up (coming soon). You can clean up explicitly here.
    }
}

// Local structures popped off stack when method returns

class PointRef
{
    public int X;
    public int Y;
    public PointRef(int xPos, int yPos)
    {
        X = xPos;
        Y = yPos;
    }

    public void Display()
    {
        Console.WriteLine("X: {0}, Y: {1}", X, Y);
    }
}

class ShapeInfo
{
    public string InfoString;

    public ShapeInfo(string info)
    {
        InfoString = info;
    }
}

struct Rectangle
{

    public ShapeInfo RectangleInfo; // reference type field in struct

    public int RectangleTop, RectangleLeft, RectangleBottom, RectangleRight;

    public Rectangle(int top, int left, int bottom, int right, string info)
    {
        RectangleTop = top;
        RectangleLeft = left;
        RectangleBottom = bottom;
        RectangleRight = right;
        RectangleInfo = new ShapeInfo(info);
    }

    public void Display() { Console.WriteLine("String = {0}, Top = {1}, Bottom = {2}, " + "Left = {3}, Right = {4}", RectangleInfo.InfoString, RectangleTop, RectangleBottom, RectangleLeft, RectangleRight); }
}

class Person
{
    public string personName;

    public int personAge;

    public Person(string name, int age)
    {
        personName = name;
        personAge = age;
    }

    public Person() { }

    public void Display()
    {
        Console.WriteLine("Name: {0}, Age: {1}", personName, personAge);
    }

    public static void SendAPersonByValue(Person p)
    {
        p.personAge = 30;
        p = new Person("New Person", 99); //since its pass by value, the original does not get updated to this.
    }

    public static void SendAPersonByReference(ref Person p)
    {
        p.personAge = 120;

        p = new Person("Brand New Person", 88); // since pass by ref, original is completely replaced with this new instance.
    }
}