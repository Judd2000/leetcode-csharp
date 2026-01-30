using System.Reflection;

Console.WriteLine("*** Late Binding ***\n");

Assembly a = null;

// attempt to load CarLibrary
try
{
    a = Assembly.LoadFrom("CarLibraryNJudd");
}
catch (Exception ex) {
    Console.WriteLine($"Failed to load assembly: {ex.Message} {ex}");
    return;
}

if (a != null) {
    CreateUsingLateBinding(a);
}

static void CreateUsingLateBinding(Assembly a) {
    try
    {
        // obtain minivan metadata
        Type miniVan = a.GetType("CarLibrary.MiniVan");

        object van = Activator.CreateInstance(miniVan); // create instances of objects where no compile-time information is available.
        Console.WriteLine("Created a {0} using Late Binding", van);

        // Leverage MethodInfo to call methods on this obj
        MethodInfo info = miniVan.GetMethod("TurboBoost");

        // invoke
        info?.Invoke(van, null);
    }
    catch (Exception ex) {
        Console.WriteLine(ex.Message);
    }
}

// when invoking a method with parameters, declare them as a loosely typed array of objects.
static void LateBindingWithArgs(Assembly a) {
    try
    {
        // Get metadata description of sports car
        Type sport = a.GetType("CarLibrary.SportsCar");

        // create sports car
        object sportsCar = Activator.CreateInstance(sport);
        MethodInfo radioInfo = sport.GetMethod("TurnOnRadio");
        radioInfo?.Invoke(sportsCar, [true, 2]);
    }
    catch (Exception ex) {
        Console.WriteLine(ex);
    }
}

LateBindingWithArgs(a);