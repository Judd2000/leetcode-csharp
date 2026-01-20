using AnonymousMethods;
using System.Security.Cryptography;

Console.WriteLine("*** Anonymous Methods ***\n");

CarWithEvents c1 = new CarWithEvents(40);

c1.AboutToBlow += delegate
{
    Console.WriteLine("We are definitely going TOO FAST FELLAS");
};

c1.AboutToBlow += delegate (object? sender, CarEventArgs e)
{
    Console.WriteLine($"Message from car {e.message}");
};

c1.Exploded += delegate (object? sender, CarEventArgs e)
{
    // These can 'see' the variables of the code that defines them, mostly
    // CANNOT: access ref or out params of defining method, cannot have a local var with the same name as defining method, can't access instance variables in outer
    // class scope. CAN Declare local variables with the same name as outer class member variables.
    Console.WriteLine($"Fatal message from car {e.message}");
}; //inline anonymous methods!

for (int i = 0; i < 4; i++) {
    c1.Accelerate(40);
}

int aboutToBlowCounter = 0;
CarWithEvents newCarTwo = new(10);

newCarTwo.AboutToBlow += delegate
{ //by not specifying the other params we are essentially saying we don't need / care about them.
    aboutToBlowCounter++;
    Console.WriteLine("Too fast buddy, toooooooo fast");
};

newCarTwo.AboutToBlow += delegate (object? sender, CarEventArgs e)
{
    aboutToBlowCounter++;
    Console.WriteLine("Critical message from Car. {0}", e.message);
};

for (int i = 0; i < 7; i++)
{
    newCarTwo.Accelerate(25);
}

Console.WriteLine("about to blow was fired {0} times", aboutToBlowCounter);

// static anonymous methods (can be isolated from containing code this way)

static int AddWrapperWithStatic(int x, int y) {
    return Add(x, y);

    // now these won't clash with outer x and y
    static int Add(int x, int y) {
        return x + y;
    }
}

newCarTwo.AboutToBlow += static delegate
{
    //aboutToBlowCounter++; // ERROR c# 9 => static methods are guarded and cannot introduce side effects from containing code.
    Console.WriteLine("TOo fast ToO Fast");
};

// discards with anyonmous methods. Since underscore was a legal variable identifier in previous C# versions, there needs to be 2+ discards used in anonymous method for them
// to be true discards. Weird.

Console.WriteLine("-=-=-=-=-=-=-=-=-=-=- Discards with Anonymous Methods -=-=-=-=-=-=-=-=-=\n");

Func<int, int, int> constant = delegate (int _, int _) { return 42; };
Console.WriteLine("constant(3,4) is {0}", constant(3, 4)); //successfully ignored the heck out of the inputs.

// Lambda expressions => more concise way to use anonymous methods.

// Considering List<T>'s findAll.
// public List<T> FindAll(Predicate<T> match);

// predicate is a delegate type that points to any method returning bool and takes single parameter.

// With FindAlL, each item in the list is passed to the method pointed to by Predicate object. Returns true or false, if true the item is added to new list.

static void TraditionalDelegateSyntax() {
    // Longhand dealing with FindAll
    List<int> list = new List<int>();
    list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
    Predicate<int> callbackFn = IsEvenNumber;
    List<int> evenNumbers = list.FindAll(callbackFn);

    foreach (int evenNumber in evenNumbers) {
        Console.WriteLine("{0}\t", evenNumber);
    }
    Console.WriteLine();

    static bool IsEvenNumber(int x) { 
        return (x % 2 == 0);
    }
}

TraditionalDelegateSyntax();

// anonymous method
static void AnonymousMethodSyntax() {
    List<int> list = new();

    list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
    List<int> evenNums = list.FindAll(delegate (int i)
    {
        return (i % 2 == 0);
    });

    foreach (int evenNumber in evenNums)
    {
        Console.WriteLine("{0}\t", evenNumber);
    }
}

AnonymousMethodSyntax();

// Even EASIER WAY

static void LambdaExpressionSyntax() {
    List<int> list = new();

    list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });

    List<int> evenNumbers = list.FindAll((i) =>  (i % 2) == 0); // lambda function! You can do multiline (code block), specify the type of the params, etc.

    Console.WriteLine("Even numbers:");

    foreach (int evenNumber in evenNumbers)
    {
        Console.WriteLine("{0}\t", evenNumber);
    }
}

LambdaExpressionSyntax();

// lambda expression with multiple or zero parameters: (int j, string w) or (j, w) if it can be inferred. Empty: () => 
// Can also be 'static' static () =>

// Same as delegates, there must be 2 discards for it to be valid.

// Let's go back to the Car example using lambdas.

Console.WriteLine("****** CAR EXAMPLE WITH LAMBDAS ******* \n");

CarWithEvents newCar = new(10);

newCar.AboutToBlow += (object? sender, CarEventArgs args) => {
    Console.WriteLine(args.message);
};

newCar.Exploded += (sender, e) => Console.WriteLine(e.message);

for (int i = 0; i < 7; i++)
{
    newCar.Accelerate(25);
}

// for '=>', you can do this for any single line that would normally be a block {};