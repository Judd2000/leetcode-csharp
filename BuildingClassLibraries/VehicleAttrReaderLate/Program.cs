using System.Reflection;

Console.WriteLine("**** Value of VehicleDescription attribute ****\n");
ReflectLateBinding();

static void ReflectLateBinding()
{
    try
    {
        Assembly assembly = Assembly.LoadFrom("ApplyingAttributes");

        Type vehicleDescription = assembly.GetType("ApplyingAttributes.VehicleDescriptionAttribute");

        // Get description prop
        PropertyInfo propDescription = vehicleDescription.GetProperty("Description");

        Type[] types = assembly.GetTypes();

        foreach (Type type in types)
        {
            object[] customAttrs = type.GetCustomAttributes(vehicleDescription, false);

            foreach (object attr in customAttrs) {
                Console.WriteLine($"=> {type.Name}:{propDescription.GetValue(attr, null)}\n"); // GetValue used to trigger prop accessor.
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}