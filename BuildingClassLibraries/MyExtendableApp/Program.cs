using System.Reflection;
using CommonSnappableTypes;

// loadexternal module:loads assembly into memory, determines of assembly contains IAppFunctionality implementations
// Creates late-binding types.

// app logic

Console.WriteLine("Welcome to MyTypeViewer\n");
string typeName = "";

do
{
    Console.WriteLine("Enter a snap-in to load.");
    Console.Write(" Or, enter Q to quit:");

    typeName = Console.ReadLine();

    if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase)) {
        break;
    }

    try {
        LoadExternalModule($"{typeName}");
    } catch (Exception ex) {
        Console.WriteLine("Error loading type: {0}", ex);
    }
} while (true);

static void LoadExternalModule(string assemblyName) {
    Assembly? snapInAssembly = null;
    try
    {
        snapInAssembly = Assembly.LoadFrom(assemblyName);
    }
    catch (Exception ex) {
        Console.WriteLine("An error occured: {0}", ex);
        return;
    }

    List<Type> classTypes = [.. snapInAssembly.GetTypes().Where((t) => t.IsClass && (t.GetInterface("IAppFunctionality") != null))]; //thanks, Linq!

    if (!classTypes.Any()) {
        Console.WriteLine("Type does not implement IAppFunctionality.");
    }

    foreach (Type t in classTypes) {
        // late binding
        IAppFunctionality itfApp = (IAppFunctionality)snapInAssembly.CreateInstance(t.FullName, true);
        itfApp?.DoIt();
        DisplayCompanyData(t);
    }
}

static void DisplayCompanyData(Type t) {
    var companyInfo = t.GetCustomAttributes(false)
        .Where((ci) => (ci is CompanyInfoAttribute)); //ensure all LINQ results are CompanyInfoAttribute objects.

    foreach (CompanyInfoAttribute c in companyInfo) {
        Console.WriteLine($"More information about {c.CompanyName} can be found at {c.CompanyUrl}");
    }
}
