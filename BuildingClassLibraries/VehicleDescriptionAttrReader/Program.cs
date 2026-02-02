using ApplyingAttributes;

Console.WriteLine("*** Value of VehicleDescription Attribute ***\n");

static void ReflectWithEarlyBinding() {
    // get type representing Sled.
    Type t = typeof(Sled);
    object[] sledAttributes = t.GetCustomAttributes(false);

    foreach (VehicleDescriptionAttribute v in sledAttributes.Cast<VehicleDescriptionAttribute>()) {
        Console.WriteLine($"=> {v.Description}");
    }
}

ReflectWithEarlyBinding();