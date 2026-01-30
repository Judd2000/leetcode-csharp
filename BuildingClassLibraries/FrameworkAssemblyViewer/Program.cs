using System.Reflection;

Console.WriteLine("**** Framework Assembly Reflector ****\n");

var displayName = "Microsoft.EntityFrameworkCore, Version=6.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60";
Assembly a = Assembly.Load(displayName);

DisplayInfo(a);

static void DisplayInfo(Assembly a) {
    AssemblyName assemblyName = a.GetName();
    Console.WriteLine("Assembly information:");
    Console.WriteLine($"Assembly Name: {assemblyName.Name}");
    Console.WriteLine($"Assembly Version: {assemblyName.Version}");
    Console.WriteLine($"Assembly Culture: {assemblyName.CultureInfo.DisplayName}");
    Console.WriteLine("Public Enums: ");

    var publicEnums = from e in a.GetTypes() where e.IsEnum && e.IsPublic select e;
    foreach (var publicEnum in publicEnums) {
        Console.WriteLine($"-> {publicEnum}");
    }
}
