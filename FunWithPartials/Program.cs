// See https://aka.ms/new-console-template for more information
using Car;

Console.WriteLine("Hello, World!");

Console.WriteLine(SayHello());

// Record types

// obj initialization
CarTwo myCar = new CarTwo
{
    Make = "Toyota",
    Model = "Corolla",
    Color = "Blue"
};

Console.WriteLine("My Car:");
CarTwo.DisplayCarStats(myCar);

// custom constructor
CarTwo anotherCar = new CarTwo("Honda", "Civic", "Red");
Console.WriteLine("Another car:");
CarTwo.DisplayCarStats(anotherCar);

// Immutable Record types with standard property syntax
// Car record behaves like a class with init=only settings
Console.WriteLine("*****RECORDS");
CarRecord myCarRecord = new CarRecord
{
    Make = "Subaru",
    Model = "Outback",
    Color = "Gray"
};
Console.WriteLine("my car record");
CarRecord.DisplayCarRecordStats(myCarRecord);

//custom constructor

CarRecord anotherCarrecord = new CarRecord("Chevy", "Silverado", "Black");
Console.WriteLine("Another car record");

// ToString is implemented fancily for Records.
Console.WriteLine(anotherCarrecord.ToString());

// Will error on property changes
// anotherCarRecord.Color = "Red";

// Built-in Deconstruct method for positional record types
AbbrevCar myCarRec = new AbbrevCar("Honda", "Oddyssey", "Silver");
myCarRec.Deconstruct(out string make, out string model, out string color);

Console.WriteLine($"Deconstructed: Make: {make}, Model {model}, Color: {color}");

// with Deconstruct above, the only thing about the out variables was that they had to positionally match the record.

// Can also use tuple syntax

var (make2, model2, color2) = myCarRec;
Console.WriteLine("Make2 {0}, Model2 {1}, Color2: {2}", make2, model2, color2);

// Record types IMPLICITLY override Equals and == for value equality
AbbrevCar myCarRec2 = new AbbrevCar("Honda", "Oddyssey", "Silver");

Console.WriteLine($"Car records are the same when value equal? {myCarRec.Equals(myCarRec2)};");

// Copying record types using 'with' expressions.
CarRecord carRecordCopy = anotherCarrecord;
Console.WriteLine("Copy results");
Console.WriteLine($"Car records are the same? {carRecordCopy.Equals(anotherCarrecord)}");

Console.WriteLine($"Car records are THE SAME INSTANCE? {ReferenceEquals(carRecordCopy, anotherCarrecord)}");

// Copy using WITH
CarRecord otherCar = myCarRecord with { Model = "Yer Mum" };
Console.WriteLine("My copied, and modified, Car");
Console.WriteLine(otherCar.ToString());
// With this, you can take complex record type instances and initialize 
// new ones with updated props.

// Record structs.

