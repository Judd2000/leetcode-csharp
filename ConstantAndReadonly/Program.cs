
using ConstData;
// 'const' keyword.

Console.WriteLine("Fun with Consts");

Console.WriteLine($"Value of pi is {MyMathClass.PI}");

LocalConsttantStringVar();

static void LocalConsttantStringVar() { 
    const string fixedStr = "Fixed String Data";
    Console.WriteLine(fixedStr);

    // fixedStr = "New Data"; // error

    // Constants have to be initialized at declaration time
}

// You can have sring interpolated constants if they are made up of other constants
const string foo = "Foo";
const string bar = "Bar";

const string foobar = $"{foo} and {bar}";
Console.WriteLine(foobar);
