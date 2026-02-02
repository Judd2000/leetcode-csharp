
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;

// original reflection method

static void AddUsingReflection() {
    Assembly assembly = Assembly.LoadFrom("MathLibrary");
    try {
        Type math = assembly.GetType("MathLibrary.SimpleMath");
        object obj = Activator.CreateInstance(math);

        MethodInfo mi = math.GetMethod("Add");
        object[] args = { 10, 2 };
        Console.WriteLine($"Result: {mi.Invoke(obj, args)}");
    } catch (Exception ex) {
        Console.WriteLine($"Error: {ex}");
    }    
}

AddUsingReflection();

static void AddUsingDynamic() {
    Assembly assembly = Assembly.LoadFrom("MathLibrary");
    try
    {
        Type math = assembly.GetType("MathLibrary.SimpleMath");

        // dynamic object immediately
        dynamic obj = Activator.CreateInstance(math);
        Console.WriteLine($"Result with dynamic: {obj.Add(10, 2)}");
    }
    catch (Exception ex) {
        Console.WriteLine($"Error: {ex}");
    }
}

AddUsingDynamic();