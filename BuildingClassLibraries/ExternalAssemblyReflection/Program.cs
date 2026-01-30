using System.Reflection;

Console.WriteLine("**** External Assembly Viewer ****");
string assemblyName = "";
Assembly assembly = null;
do {
    Console.WriteLine("\nEnter assembly to evaluate, or enter Q to quit:");
    assemblyName = Console.ReadLine();
    if (assemblyName.Equals("Q", StringComparison.OrdinalIgnoreCase)) {
        break;
    }

    try
    {
        assembly = Assembly.LoadFrom(assemblyName);
        DisplayTypesFromAssembly(assembly);
    }
    catch (Exception e) {
        Console.WriteLine($"Sorry, assembly not found: {e.Message}");
    }
} while (true);

static void DisplayTypesFromAssembly(Assembly assembly) {
    Console.WriteLine("*** Assembly Types ***");
    Console.WriteLine("-> {0}", assembly.FullName);

    Type[] types = assembly.GetTypes();
    foreach (Type t in types)
    {
        Console.WriteLine($"Type {t}");
    }
    Console.WriteLine();
}

// Assembly.Load() overloads. Localization value, version number, public key token value. Set of items identifying an assembly is the display name. Comma-delimited string of key values

// Template: Name (,Version = major.minor.build.revision) (,Culture = culture token) (,PublicKeyToken=public key token)

//eg.
Assembly a = Assembly.Load("CarLibrary, Version=1.0.0.1, PublicKeyToken=null, Culture=\"\"");
// Assembly namespace contains AssemblyName type

AssemblyName asmblName = new();
asmblName.Name = "CarLibrary";
asmblName.Version = new Version("1.0.0.1");
a = Assembly.Load(asmblName); //for a framework assembly, load a public key token value
