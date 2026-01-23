using System.Reflection;
using System.Runtime.Loader;

Console.WriteLine("**** Fun with Default AppDomain ****\n");

DisplayDADStats();

Console.ReadLine();

static void DisplayDADStats() { 
    // get appdomain for current thread
    AppDomain defaultDomain = AppDomain.CurrentDomain;

    //stats
    Console.WriteLine("Name of domain: {0}", defaultDomain.FriendlyName); // identical to the name of the .exe contained within

    Console.WriteLine("ID of domain: {0}", defaultDomain.Id);

    Console.WriteLine("Default domain? {0}", defaultDomain.IsDefaultAppDomain());

    Console.WriteLine("base dir {0}", defaultDomain.BaseDirectory); // current location of deployed executable

    Console.WriteLine("Setup info for this domain:");

    Console.WriteLine("\t Application Base: {0}", defaultDomain.SetupInformation.ApplicationBase);

    Console.WriteLine($"Target Framework: {defaultDomain.SetupInformation.TargetFrameworkName}");
}

DisplayDADStats();

// GetAssembiles() => array of assembly objects, all .NET core assemblies in a given app domain

static void ListAllAssembilesInAppDomain() {
    AppDomain defaultDomain = AppDomain.CurrentDomain;
    // all loaded assemblies
    //Assembly[] loadedAssemblies = defaultDomain.GetAssemblies();

    var loadedAssemblies = defaultDomain.GetAssemblies().OrderBy((x) => x.GetName().Name);

    Console.WriteLine("Assemblies loaded in {0}", defaultDomain.FriendlyName);

    foreach (Assembly assembly in loadedAssemblies) {
        Console.WriteLine($"-> Name, Version:  {assembly.GetName().Name} {assembly.GetName().Version}");
    }
}

ListAllAssembilesInAppDomain();

// App domains may have further divisions called load context boundaries. 

// AssemblyLoadContext

// Won't run and not worth fixing.
static void LoadAdditionalAssembliesDifferentContexts() {
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClassLibrary2.dll");
    AssemblyLoadContext lc1 = new("NewContext1", false);

    var cl1 = lc1.LoadFromAssemblyPath(path);

    var c1 = cl1.CreateInstance("ClassLibrary2.Car");

    AssemblyLoadContext lc2 = new("NewContext2", false);

    var cl2 = lc2.LoadFromAssemblyPath(path);

    var c2 = cl2.CreateInstance("ClassLibrary2.Car");
    Console.WriteLine("*** Loading Additional Assemblies in Different Contexts ***");
    Console.WriteLine($"Assembly 1 equals(Assembly2) {cl1.Equals(cl2)}");

    Console.WriteLine($"Assembly1 == Assembly2 {cl1 == cl2}");

    Console.WriteLine($"Class1.Equals(Class2) {c1.Equals(c2)}");
    Console.WriteLine($"Class1 == Class2 {c1 == c2}");
}

//LoadAdditionalAssembliesDifferentContexts();