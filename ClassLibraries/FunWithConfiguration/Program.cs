using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile("appsettings.dev.json", true, true) //adding another causes addition of properties, second will overwrite first on name clash
    .Build();

Console.WriteLine($"From config, CarName is: {config["CarName"]}"); //when the value is not there, config[] yields null.

// GetValue => retrieve primitives from config

Console.WriteLine($"Getting carname with primitive accessors 0 {config.GetValue(typeof(string), "CarName")}");


Console.WriteLine($"Getting carname with primitive accessors 1 {config.GetValue<string>("CarName")}"); // this is preferred.
// The above will throw errors if they can't be intrinsically cast to specified data type. e.g:

try
{
    Console.WriteLine($"Getting carname with primitive accessors (incorrect type): {config.GetValue<int>("CarName")}");
}
catch (InvalidOperationException ex) {
    Console.WriteLine($"Config accessor failed: {ex.Message}");
    //Console.WriteLine(ex);
}

// when using 1+ configuration files, properties are addative. For name conflicts, last one added wins (think .env).

Console.WriteLine($"My car object: {config["Car"]}");

// access nested JSON attributes like so:
Console.WriteLine($"{config["Car:Make"]} is named {config["Car:Name"]}");

// Or, if you don't want to drill for attributes, consider GetSection and then attribute access
IConfigurationSection carSection = config.GetSection("Car");
Console.WriteLine($"(Using IConfigurationSection GetSection) Car object is {carSection["Color"]} car");
Console.WriteLine($"(Using IConfigurationSection GetSection) Make {carSection["Make"]} name {carSection["Name"]}");

// can use .Bind to bind config to existing object instance or Get() to create new obj instance.

Car c = new();
carSection.Bind(c);
Console.WriteLine("After binding config to Car object:");
Console.WriteLine($"Car object is color {c.Color}");
Console.WriteLine("Car object is a {0} named {1}", c.Make, c.Name);

// If the section is not configured, nothing on the original type will be overwritten.
Car notFoundCar = new() { Color = "Rojo" };
config.GetSection("CarTwo").Bind(notFoundCar);
Console.WriteLine($"notFoundCar attributes: Color: {notFoundCar.Color}, Make: {notFoundCar.Make}, Name: {notFoundCar.Name}");

// .Get creates new instance of specified type from config section. .Get returns object, Get<T> returns typed obj. Returning null if not found.
var carFromGet = config.GetSection(nameof(Car)).Get<Car>();
var objFromGet = config.GetSection(nameof(Car)).Get(typeof(Car)) as Car; // cast required.

Console.WriteLine($"(obj .Get) carFromGet attributes: Color: {objFromGet.Color}, Make: {objFromGet.Make}, Name: {objFromGet.Name}");

Console.WriteLine($"(generic .Get) carFromGet attributes: Color: {carFromGet.Color}, Make: {carFromGet.Make}, Name: {carFromGet.Name}");

// even with casing changes, the reflection will still cast correctly.If the property in config doesn't exist in the class, it is ignored.

// .Bind .Get<T> and .Get can all take an Action<BinderOptions> delegate as well. BindNonPublicProperties (bool) and ErrorOnUnknownConfiguration (bool) def false

try {
    config.GetSection(nameof(Car)).Get<Car>((o) => o.ErrorOnUnknownConfiguration = true); // this will fail now that we error on unknown attributes.
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("An exception occured: {0}", ex.Message);
}

// GetRequiredSection is like GetSection, but it will throw an error if the section is not found

public class Car { // this matches our configuration type :)
    public string Make { get; set; } = "";
    public string Color { get; set; } = "";

    public string Name { get; set; } = "";
}