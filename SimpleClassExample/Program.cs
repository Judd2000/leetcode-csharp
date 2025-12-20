using SimpleClassExample;

Console.WriteLine("Fun with Class Types");

Car myCar = new Car();
myCar.petName = "Henry";
myCar.currSpeed = 100;

for (int i = 0; i < 10; i++)
{
    myCar.SpeedUp(10);
    myCar.PrintState();
}

// Objects 'allocated' with New. This creates a reference to the object

// Constructors establish class state at creation time
// Default constructors initialize all fields with default vals

Car chuck = new Car();
chuck.PrintState();

Car mary = new Car("Mary");
mary.PrintState();

Car daisy = new Car("Daisy", 75);

daisy.PrintState();

// Default constructor is available up until you define your own
// custom constructor.
//Motorcycle mc = new Motorcycle();
//mc.PopAWheely();

//// .this keyword. Helps resolve scope ambiguity if a varname is the same as a field
//// 

//Motorcycle c = new Motorcycle(5);
//c.SetDriverName("Tiny");
//c.PopAWheely();

//Console.WriteLine($"Rider name is {c.driverName}");

static void MakeSomeBikes()
{
    // driverName = "";
    driverIntensity = 0;
}