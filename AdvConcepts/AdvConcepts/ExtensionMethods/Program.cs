// See https://aka.ms/new-console-template for more information
using ExtensionMethods;
using System.Drawing;
using System.Reflection;

Console.WriteLine("Hello, World!");

// Now that we have extended int and Object, we can use them!!

int theInteger = 12345678;
theInteger.DisplayDefiningAssembly();

System.Data.DataSet d = new();

d.DisplayDefiningAssembly(); //even this type has DisplayDefiningAssembly now

Console.WriteLine("Value of integer {0}", theInteger);
Console.WriteLine("New value of integer after reversal {0}", theInteger.ReverseDigits());

// In order to use extension methods be sure that you are 'using' the
// associated namespace. They are not globally scoped.

// You can also extend types extending specific interfaces.

// We added an extension to IEnumerable. Let's test on an array
string[] data = { "W", "Oh hecky", "Sheeeee" };
data.PrintData_Beep();

Console.WriteLine("Support for class with IEnumerator extension");

Garage lot = new Garage();

foreach (Car car in lot) { //now valid!
    Console.WriteLine("{0} going {1} MPH", car.Name, car.CurrentSpeed);
}

// Anonymous Types with 'var' - makes a definition on the fly at compilation.




