// See https://aka.ms/new-console-template for more information
using DelegatesEventsLambdaExpressions;
using System.Dynamic;

Console.WriteLine("Hello, World!");

// Delegate type is means of responding to callbacks.

// Invoke() invokes all methods maintained by the delegate obj synchronously
// Simple Delegate Ex.

//BinaryOp b = new BinaryOp(SimpleMath.Add);
BinaryOp b = new BinaryOp(SimpleMath.Add); // Pass the non-invoked method to the constructor
Console.WriteLine($"TEST 10+10 is {0}", b(10,10)); // this is shorthand for calling Invoke()
DisplayDelegateInfo(b);

// Delegates are type safe and inforce the types of the methods they were set up to support. This produces errors
//BinaryOp b2  = new BinaryOp(SimpleMath.SquareNumber); 

// Iterate over GetInvocationList()
static void DisplayDelegateInfo(Delegate delegateObject) {
    foreach (Delegate d in delegateObject.GetInvocationList()) {
        Console.WriteLine("Method name: {0}", d.Method);
        Console.WriteLine("Type name: {0}", d.Target); //since our example delegate is referencing a static class method, there is no instance to display.
    }
}

// Obj state notifications with delegates.
// 1. Declare new delegate that will send notifications to the caller
// 2. Declare a member variable of the delegate from within the class
// 3. Helper function in class to allow caller to specify method to callback on.
// 4. Implement class method to invoke delegate under circumstances you want.

Console.WriteLine("*****************Using delegates as event handlers********************\n");
Car newCar = new Car("Hunk of Literal Junk", 40, 2);

newCar.RegisterCarEngineHandler(new Car.CarEngineHandler(OnCarEngineEvent));

Car.CarEngineHandler secondHandler = new Car.CarEngineHandler(OnCarEngineEventTwo);
newCar.RegisterCarEngineHandler(secondHandler);

static void OnCarEngineEvent(string msg) {
    Console.WriteLine("***Incoming message from Car object***");
    Console.WriteLine($"=> {msg}");
    Console.WriteLine();
}


// Delegates have multicast abilities, meaning we can give several callbacks.
// Example time:
static void OnCarEngineEventTwo(string msg) {
    Console.WriteLine("***Incoming message from Car object (Second Handler)***");
    Console.WriteLine($"=> {msg}");
    Console.WriteLine();
}
// Let's run our car and see what is reported.
Console.WriteLine("***Speeding our {0} up...", newCar.Name);

for (int i = 0; i < 6; i++) {
    if (i == 4) newCar.DeregisterCarEngineHandler(secondHandler); // stop seeing second handler logs halfway.
    newCar.Accelerate(8);
}

// Method group conversion syntax - supply a direct method name instead of a delegate object

Car secondNewCar = new Car("Billy", 100, 10);
secondNewCar.RegisterCarEngineHandler(OnCarEngineEvent); // valid since the method matches the delegate object's signature!

for (int i = 0; i < 6; i++)
{
    secondNewCar.Accelerate(20);
}

secondNewCar.DeregisterCarEngineHandler(OnCarEngineEvent); // you can unregister just using the function name as well

secondNewCar.Accelerate(10); // => no notification!

// generic delegates

MyGenericDelegate<string> strTarget = new(StringTarget); //shorthand yay!

// method group conversion
MyGenericDelegate<int> intTarget = IntTarget;

static void IntTarget(int arg) {
    Console.WriteLine("++arg is: {0}", ++arg);
}

static void StringTarget(string arg)
{
    Console.WriteLine("Arg in uppercase {0}", arg.ToUpper());
}

// framework has built-in Action<> and Func<> delegate types if you don't need to define your own.

// Action<>: up to 16 args, reutrns void

static void DisplayMessage(string msg, ConsoleColor textColor, int printCount)
{
    ConsoleColor previous = Console.ForegroundColor;
    Console.ForegroundColor = textColor;

    for (int i = 0; i < printCount; i++) {
        Console.WriteLine(msg);
    }

    Console.ForegroundColor = previous;
}

Console.WriteLine("***Using Built-In Action<> and Func<>");
Action<string, ConsoleColor, int> actionTarget = DisplayMessage;

actionTarget("Woah!", ConsoleColor.Green, 4);

// Use Func<> for handlers with return values. 16 params and custom return value

static int Add(int x, int y) {
    return x + y;
}

// takes 3 type parameters and THE LAST ONE IS THE RETURN VALUE OF YOUR METHOD.

static string SumToString(int x, int y) {
    return (x + y).ToString();
}

Func<int, int, int> funcTarget = Add;
int result = funcTarget(40, 40);
Console.WriteLine("40 + 40 = {0}", result);

Func<int, int, string> funcTargetTwo = SumToString;

string sum = funcTargetTwo.Invoke(100, 100);
Console.WriteLine(sum);



public delegate void MyGenericDelegate<T>(T arg); // accepts void return functions and any arg type