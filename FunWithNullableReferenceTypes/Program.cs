
#nullable disable
string? nullableString = null;
TestClass? myNullableClass = null;

TestClass myNonNullableClass = myNullableClass;

//EnterLogData(null);

static void EnterLogData(string message, string owner = "Programmer") {
    ArgumentNullException.ThrowIfNull(message);
    Console.WriteLine("Error: {0}", message);
    Console.WriteLine("Owner of Error: {0}", owner);
}

// Null coalescing operators ??
// IE assign a value if something is null


public class TestClass
{
    public string Name { get; set; }

    public int Age { get; set; }
}