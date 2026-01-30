using CarLibrary;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
Console.WriteLine("Probing Paths ***");
Console.WriteLine("Trusted platform assemblies:");

// : on non windows
//var list = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")?.ToString()?.Split(';');

//foreach (var dir in list) {
//    Console.WriteLine("Dir: {0}", dir);
//}

//Console.WriteLine("Platform Resource Roots");
//Console.WriteLine(AppContext.GetData("PLATFORM_RESOURCE_ROOTS"));
//Console.WriteLine($"NATIVE_DLL_SEARCH_DIRECTORIES: {AppContext.GetData("NATIVE_DLL_SEARCH_DIRECTORIES")}");
//Console.WriteLine($"APP_PATHS: {AppContext.GetData("APP_PATHS")}");
//Console.WriteLine($"APP_NI_PATHS: {AppContext.GetData("APP_NI_PATHS")}");

//// System.Reflection -> obtain info of all types in .dll or .exe assembly. System.Type grants methods to examine type metadata.

//// IsAbstract, IsArray, IsClass, IsCOMObject, IsEnum, IsInterface, IsSealed, etc....
//// GetConstructors() GetEvents(), GetMembers(), GetType()

//// Obtain a type reference using System.Object.GetType()

////Get type information (Type Abstract Class) with .GetType() or typeof() operator
//Type t = typeof(SportsCar);
//Console.WriteLine("Typeof t {0}", t);
//var car = new SportsCar();
//t = car.GetType();
//Console.WriteLine("Typeof t (TypeOf) {0}", t);

//// overloaded boolean ops in GetType: throw exception if type cannot be found, ignore case
//t = Type.GetType("CarLibrary.SportsCar", false, true);
//Console.WriteLine("Typeof t Type.GetType with bools {0}", t); // this is incorrect, you want:
//t = Type.GetType("CarLibrary.SportsCar,CarLibrary");
//Console.WriteLine("Typeof t Type.GetType with external assembly naming {0}", t); 
// get nested types with + => CarLibrary.JamesBondCar+SpyOptions

// List methods, takes a type and prints name of each method defined by the type. System.Reflection.MethodInfo[] => Type.GetMethods
// Many of these have been overloaded to provide fine-grained control (only static methods, public members, etc).
static void ListMethods(Type t) {
    Console.WriteLine("****** Methods ******");
    MethodInfo[] mi = t.GetMethods();
    foreach(MethodInfo mi2 in mi)
    {
        Console.WriteLine($"-> {mi2.Name}");
    }
}

// Rewritten using LINQ // when you find blocks of looping or decision logic, consider a LINQ query.
static void ListMethodsLINQ(Type t) {
    Console.WriteLine("****** Methods ******");
    var methodInformation = (from n in t.GetMethods() orderby n.Name select n).ToArray();
    foreach (var met in methodInformation) {
        StringBuilder strB = new StringBuilder("(");
        foreach (ParameterInfo pi in met.GetParameters()) {
            strB.Append($"{pi.ParameterType} {pi.Name}");
        }
        strB.Append(")");

        Console.WriteLine($"-> {met.ReturnType.FullName} {met.Name} {strB.ToString()}");
    }
}

static void ListFields(Type t) {
    Console.WriteLine("***** Fields *****");
    var fields = from f in t.GetFields() orderby f.Name select f.Name;
    foreach (var name in fields) {
        Console.WriteLine($"-> {name}");
    }
}

static void ListProps(Type t)
{
    Console.WriteLine("***** Properties *****");
    var props = from f in t.GetProperties() orderby f.Name select f.Name;
    foreach (var name in props)
    {
        Console.WriteLine($"-> {name}");
    }
}

// listing interface gives an array of System.Type
static void ListInterfaces(Type t) {
    Console.WriteLine("***** Interfaces *****");
    var interfaces = from i in t.GetInterfaces() orderby i.Name select i;

    foreach (Type i in interfaces) {
        Console.WriteLine($"-> {i.Name}");
    }
}

static void ListOtherStats(Type t) {
    Console.WriteLine("**** OTHER TYPE INFORMATION ****");
    Console.WriteLine("Base class is: {0}", t.BaseType);
    Console.WriteLine("Is abstract? {0}", t.IsAbstract);
    Console.WriteLine("Is sealed? {0}", t.IsSealed);
    Console.WriteLine("Is generic? {0}", t.IsGenericTypeDefinition);
    Console.WriteLine("Is a class type? {0}", t.IsClass);
}

Console.WriteLine("Welcome to Type Viewer.....");
string typeName = "";
do
{
    Console.WriteLine("\nEnter a type name:");
    Console.Write("Or enter Q to quit:");

    typeName = Console.ReadLine();
    if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    // display type
    try
    {
        Type type = Type.GetType(typeName);
        if (type == null && typeName.Equals("System.Console", StringComparison.OrdinalIgnoreCase)) type = typeof(System.Console); //handle static type System.Console
        Console.WriteLine();
        ListOtherStats(type);
        ListFields(type);
        ListInterfaces(type);
        ListMethods(type);
        ListMethodsLINQ(type);
        ListProps(type);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Cannot find type: {0}", e.Message);
    }
} while (true);

// Generic types: use ` => for System.Collections.Generic.List<T>, pass: System.Collections.Generic.List`1 (keeps counting up with variable type params).

// you many need to load assemblies on the fly programmatically. System.Reflection.Assembly