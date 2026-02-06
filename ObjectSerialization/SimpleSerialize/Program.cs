global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Xml;
global using System.Xml.Serialization;

using SimpleSerialize;

Console.WriteLine("****** Object Serialization ******");

// Best-practice tells us to declare an options for further use without recreating it frequently.
JsonSerializerOptions options = new()
{
    IncludeFields = true,
    WriteIndented = true,
    PropertyNamingPolicy = null // pascal case.
                                // PropertyNameCaseInsensitive = true //Include
                                // ReferenceHandler = ReferenceHandler.IgnoreCycles // => avoid cycles in serialization
                                // NumberHandling = JsonNumberHandling.AllowReadingFromString & JsonNumberHandling.WriteAsString // => handle numbers differerntly
}; // human readable

// there are some default web / general options you can get out-of-the-box.
//JsonSerializerOptions defOptionsForWeb = new(JsonSerializerDefaults.Web); //.General also works

JsonSerializerOptions customConverterOptions = new()
{
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = null,
    IncludeFields = true,
    WriteIndented = true,
    Converters = { new JsonStringNullToEmpty() }
};

Radio radio = new()
{
    StationPresets = [89.3, 106.9, 91.1, 98.5],
    HasTweeters = true
};

SpyCar spyCar = new()
{
    CanFly = true,
    CanSubmerge = false,
    radio = new() { StationPresets = [1.1], HasSubwoofers = true },
};

List<SpyCar> cars = [
    new() { CanFly = true, CanSubmerge = true, radio=radio},
    new() { CanFly = false, CanSubmerge = false, radio=radio},
    new() { CanFly = true, CanSubmerge = false, radio=radio},
    new() { CanFly = false, CanSubmerge = true, radio=radio},
];

Person person = new()
{
    FirstName = "James",
    Alive = true,
    back = new Back()
};

// xml serializer
static void SaveAsXml<T>(T objGraph, string fileName) {
    XmlSerializer xmlFormat = new(typeof(T));
    using (Stream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None)) { 
        xmlFormat.Serialize(fileStream, objGraph);
    }
}

SaveAsXml(spyCar, "CarData.xml");
Console.WriteLine("=> Car Saved in Xml Format");

SaveAsXml(person, "PersonData.xml");
Console.WriteLine("=> Person Saved in Xml Format");


SaveAsXml(cars, "CarCollection.xml");
Console.WriteLine("=> Carsss Saved in Xml Format");

//Deserializing
static T? ReadFromXml<T>(string fileName) {
    XmlSerializer xmlFormat = new(typeof(T));
    using Stream fStream = new FileStream(fileName, FileMode.Open);
    var deserialized = xmlFormat.Deserialize(fStream);

    return (deserialized is T typedDeserialized) ? typedDeserialized : default(T);
}

SpyCar xmlCar = ReadFromXml<SpyCar>("CarData.xml");
Console.WriteLine("Original Car Obj: {0}, Read Car Object: {1}", spyCar.ToString(), xmlCar?.ToString());

List<SpyCar> savedCars = ReadFromXml<List<SpyCar>>("CarCollection.xml");
Console.WriteLine("Num cars before serialization: {0}, Num cars read from serialized obj: {1}", cars.Count, savedCars != null ? savedCars.Count : 0);

static void JsonSerialize<T>(T objGraph, string fileName, JsonSerializerOptions options) {
    File.WriteAllText(fileName, System.Text.Json.JsonSerializer.Serialize(objGraph, options)); //one-liners are cool sometimes!
}

JsonSerialize(spyCar, "CarData.json", options);
Console.WriteLine("Saved spyCar in JSON format");

JsonSerialize(person, "PersonData.json", options);
Console.WriteLine("Saved person in JSON format");

JsonSerialize(cars, "CarCollection.json", options);
Console.WriteLine("Saved cars in JSON format");

// To include FIELDS, pass param to JsonSerializer or ADD JSONINCLUDE to each public field. If you want to omit some, name the ones you want with:
// [JsonInclude] attribute

// IAsyncEnumerable
static async IAsyncEnumerable<int> PrintNums(int n) {
    for (int i = 0; i < n; i++) {
        yield return i;
    }
}

// serialize the yield func
async static void SerializeAsync() {
    Console.WriteLine("Async Serialize");
    using Stream stream = Console.OpenStandardOutput(); //the stream we are writing to is standard output so we see it as if we had Written a line.
    var data = new { Data = PrintNums(3) };
    await JsonSerializer.SerializeAsync(stream, data);
}

SerializeAsync();

// Stream deserialization

async static void DeserializeAsync() {
    Console.WriteLine("Async Deserialize");
    MemoryStream stream = new(System.Text.Encoding.UTF8.GetBytes("[0,1,2,3,4]"));
    await foreach (int item in JsonSerializer.DeserializeAsyncEnumerable<int>(stream)) {
        Console.Write(item);
    }
    Console.WriteLine();
}

DeserializeAsync();

JsonSerialize(cars, "CarCollection.json", options);

// Deserializing objects from Files.

static T? DeserializeFromJson<T>(JsonSerializerOptions opts, string fileName) {
    var deserialized = System.Text.Json.JsonSerializer.Deserialize<T>(File.ReadAllText(fileName), opts);
    return deserialized != null ? deserialized : default(T);
}

SpyCar savedCar = DeserializeFromJson<SpyCar>(options, "CarData.json");
Console.WriteLine("Object deserialized from JSON: {0}", savedCar);

// Add custom null converter into options

static void HandleNullStrings(JsonSerializerOptions opts) {
    Console.WriteLine("***** Handle Null Strings *****");


    // new obj that contains null string.
    Radio radio = new()
    {
        HasSubwoofers = true,
        HasTweeters = true,
        RadioId = null //spooky
    };

    var json = JsonSerializer.Serialize(radio, opts);
    Console.WriteLine("Serialized obj {0}", json);
}

HandleNullStrings(customConverterOptions);