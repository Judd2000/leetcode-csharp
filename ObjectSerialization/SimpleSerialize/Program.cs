global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Xml;
global using System.Xml.Serialization;

using SimpleSerialize;

Console.WriteLine("****** Object Serialization ******");

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

static void JsonSerialize<T>(T objGraph, string fileName) {
    File.WriteAllText(fileName, System.Text.Json.JsonSerializer.Serialize(objGraph)); //one-liners are cool sometimes!
}