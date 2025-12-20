using EmployeeApp;
using StaticDataAndMembers;
using Pointss;

using ObjectInitializers;

Console.WriteLine("Using static data");
SavingsAccount s1 = new SavingsAccount(50);

SavingsAccount s2 = new SavingsAccount(100);

SavingsAccount s3 = new SavingsAccount(10000.75);

// All the above instances share one allocation of
// currInterestRate
Console.WriteLine("Current interest rate is: {0}", SavingsAccount.GetInterestRate());

SavingsAccount s4 = new SavingsAccount(12.12);

Console.WriteLine("Current interest rate is: {0} after object creation", SavingsAccount.GetInterestRate());

s1 = new SavingsAccount(60);
Console.WriteLine("Interest rate after obj creation is {0}", SavingsAccount.GetInterestRate());

SavingsAccount.SetInterestRate(0.08); // Now we set it manually

s2 = new SavingsAccount(100);

// Expectedly, interest rate is 0.04 again

Console.WriteLine("Interest rate is {0}", SavingsAccount.GetInterestRate());

// Solutions: Set the value of the static member where it is initialized.

// using a static class 
//These work
TimeUtilClass.PrintDate();

TimeUtilClass.PrintTime();

//DOES NOT WORK
//TimeUtilClass = new TimeUtilClass(); 

// Pillars of OOP
// Encapsulation, inheritance, polymorphism

// Using Employee Class (encapsulation)

Employee emp = new Employee("Jimbo", 1, 3);

emp.GiveBonus(1_000_000);
emp.DisplayStats();


emp.Name = "Jim";
emp.DisplayStats();

// Notice 15-char limit on name
Employee emp2 = new Employee();
emp2.Name = "I am thinking of a number between 1 and 15.";

// Encapsulate using properties
// with properties we can do awesome things like this (safely) without doing GetAge and setAge +1
Employee joe = new Employee();
joe.Age++;

// Access a static Property:
Console.WriteLine("Interest rate is {0}", Employee.InterestRate);

Employee empwoyee = new Employee("marv", 45, 123, 1000, EmployeePayTypeEnum.Salaried);
Console.WriteLine(empwoyee.Pay);
emp.GiveBonus(100);
Console.WriteLine(empwoyee.Pay);

// Object initializer syntax => create object and assign properties in less lines

Point thePoint = new Point { X = 390, Y = 390 };
// ^ default constructor plus setting values under the hood
thePoint.DisplayStats();

// init-only setters become readonly after init

Point firstReadonlyoint = new Point(20, 20);


Point firstReadOnlyPoint = new Point(20, 20);

// When you are using init syntax you can use any constructor.
Point weirdPoint = new Point(10, 16) { X = 100, Y = 100 }; // => since its actually just setting them after it doesnt matter which constructor is called.

// Now we can use a combination

Point lightBluePoint = new Point(PointColorEnum.LightBlue) { X = 90, Y = 20 };

lightBluePoint.DisplayStats();

Rectangle testRectangle = new Rectangle
{
    TopLeft = new Point { X = 10, Y = 10 },
    BottomRight = new Point { X = 200, Y = 200 }
}; //decreases significantly the number of keystrokes for creating
// classes. It is all shorthand for setting the properties