using AutoLot.DalLib.Models;
using AutoLot.DalLib.DataOperations;
//using AutoLot.Dal.BulkImport;


InventoryDal inventory = new();

List<CarViewModel> allInv = inventory.GetAllInventory();
Console.WriteLine("******************** ALL CARS IN DB **********************");
Console.WriteLine("Id\tMake\tColor\tName");

foreach (CarViewModel car in allInv)
{
    Console.WriteLine($"{car.Id}\t{car.Make}\t{car.Color}\t{car.Name}");
}
Console.WriteLine();
CarViewModel firstCarByColor = inventory.GetCar(allInv.OrderBy((c) => c.Color).Select((c) => c.Id).First());
Console.WriteLine("**** First Car By Color ****");
Console.WriteLine("CarId\tMake\tColor\tName");
Console.WriteLine($"{firstCarByColor.Id}\t{firstCarByColor.Make}\t{firstCarByColor.Color}\t{firstCarByColor.Name}");

// failed delete due to related data constraints

try {
    inventory.DeleteCar(5);
    Console.WriteLine("Car with ID of 5 deleted.");
} catch (Exception ex) {
    Console.WriteLine($"An exception occured: {ex}");
}

// Insert OP
inventory.InsertAuto(new Car { Color = "Blue", MakeId = 5, Name = "Tow Monster" });
allInv = inventory.GetAllInventory(); // refetch inventory.
CarViewModel newCar = allInv.First((x) => x.Name == "Tow Monster");

Console.WriteLine("**** New Car Created ****");
Console.WriteLine("CarId\tMake\tColor\tName");
Console.WriteLine($"{newCar.Id}\t{newCar.Make}\t{newCar.Color}\t{newCar.Name}");

// Delete newly added Car
inventory.DeleteCar(newCar.Id);
string name = inventory.LookUpName(firstCarByColor.Id); // get first car by color's name

Console.WriteLine("First car *********");
Console.WriteLine($"Car name: {name}");
Console.WriteLine("Press 'enter' to Continue");

Console.ReadLine();