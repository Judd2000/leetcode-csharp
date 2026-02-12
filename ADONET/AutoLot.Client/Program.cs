using AutoLot.DalLib.Models;
using AutoLot.DalLib.DataOperations;
using AutoLot.DalLib.BulkImport;


InventoryDal inventory = new();

List<CarViewModel> allInv = inventory.GetAllInventory();
Console.WriteLine("******************** ALL CARS IN DB **********************");
Console.WriteLine("Id\tMake\tColor\tName");

foreach (CarViewModel car in allInv)
{
    Console.WriteLine($"{car.Id}\t{car.Make}\t{car.Color}\t{car.PetName}");
}
Console.WriteLine();
CarViewModel firstCarByColor = inventory.GetCar(allInv.OrderBy((c) => c.Color).Select((c) => c.Id).First());
Console.WriteLine("**** First Car By Color ****");
Console.WriteLine("CarId\tMake\tColor\tName");
Console.WriteLine($"{firstCarByColor.Id}\t{firstCarByColor.Make}\t{firstCarByColor.Color}\t{firstCarByColor.PetName}");

// failed delete due to related data constraints

try {
    inventory.DeleteCar(5);
    Console.WriteLine("Car with ID of 5 deleted.");
} catch (Exception ex) {
    Console.WriteLine($"An exception occured: {ex}");
}

// Insert OP
inventory.InsertAuto(new Car { Color = "Blue", MakeId = 5, PetName = "Tow Monster" });
allInv = inventory.GetAllInventory(); // refetch inventory.
CarViewModel newCar = allInv.First((x) => x.PetName == "Tow Monster");

Console.WriteLine("**** New Car Created ****");
Console.WriteLine("CarId\tMake\tColor\tName");
Console.WriteLine($"{newCar.Id}\t{newCar.Make}\t{newCar.Color}\t{newCar.PetName}");

// Delete newly added Car
inventory.DeleteCar(newCar.Id);
string name = inventory.LookUpName(firstCarByColor.Id); // get first car by color's name

Console.WriteLine("First car *********");
Console.WriteLine($"Car name: {name}");
Console.WriteLine("Press 'enter' to Continue");

Console.ReadLine();

static void FlagCustomer() {
    Console.WriteLine("** Simple Transaction **");

    bool throwEx = true;
    Console.WriteLine("Do you want to sim this transaction with an exception? (Y or N): ");
    var userAnswer = Console.ReadLine();

    if (string.IsNullOrEmpty(userAnswer) || userAnswer.Equals("N", StringComparison.OrdinalIgnoreCase)) {
        throwEx = false;
    }

    InventoryDal dataAccess = new();
    dataAccess.ProcessCreditRisk(throwEx, 1); // add credit risk to user with ID 1
    Console.WriteLine("Check CreditRisk table for results.");
}
FlagCustomer();

static void DoBulkCopy() {
    Console.WriteLine("******** Do Bulk Copy *******");
    var cars = new List<Car> {
        new() { Color = "Blue", MakeId = 1, PetName = "MyCar1"},
        new() { Color = "Red", MakeId = 2, PetName = "MyCar2"},
        new() { Color = "White", MakeId = 3, PetName = "MyCar3"},
        new() { Color = "Yellow", MakeId = 4, PetName = "MyCar4"},
    };

    // execute bulk import
    ProcessBulkImport.ExecuteBulkImport(cars, "Inventory");

    // verify they were added
    InventoryDal dal = new();
    List<CarViewModel> fullInv = dal.GetAllInventory();
    Console.WriteLine("Full car Inventory *******");

    Console.WriteLine("CarId\tMake\tColor\tName");

    foreach (CarViewModel item in fullInv) {
        Console.WriteLine($"{item.Id}\t{item.Make}\t{item.Color}\t{item.PetName}");
    }

    Console.WriteLine();
}

DoBulkCopy();