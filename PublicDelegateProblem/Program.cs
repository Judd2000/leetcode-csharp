using PublicDelegateProblem;

Car myCar = new Car();

myCar.ListOfHandlers = CallWhenExploded;

myCar.Accelerate(1);

// can now overwrite ListOfHandlers and other is lost.
myCar.ListOfHandlers = CallHereAsWell;
myCar.Accelerate(1);

// I can also get in there and invoke the delegate
myCar.ListOfHandlers.Invoke("I am invoking this manually, wow.");

static void CallWhenExploded(string msg) {
    Console.WriteLine(msg);
}

static void CallHereAsWell(string msg) {
    Console.WriteLine(msg);
}

// A shortcut to all of the private list and add and remove handler code is to use an Event. (auto-provided registration and unregistration methods. Private too

// 1. Define a delegate type to hold the methods when event is fired.
// 2. Declare an event in terms of delegate type

// how can we listen to the events?

// Caller can use += and -= for add_ remove_

Console.WriteLine("*** Fun With Events \n");

CarWithEvents carOne = new(0);

//carOne.AboutToBlow += CarAlmostDoomed;
carOne.AboutToBlow += CarAboutToBlow;
//carOne.Exploded += CarExploded;

CarWithEvents.CarEngineHandler d = CarExploded; // register with handler obj
carOne.Exploded += d;

Console.WriteLine("**** Speeding Up ******");

for (int i = 0; i < 4; i++) {
    carOne.Accelerate(40);
}


static void CarAboutToBlow(object sender, CarEventArgs e) { //modified to use EventArgs class
    if (sender is CarWithEvents c) {
        Console.WriteLine("Critical message from {0]: {1}", c.Name, e.message);
    }
}


//static void CarAlmostDoomed(string msg) {
//    Console.WriteLine("Critical Message From Car: {0}", msg);
//}

//static void CarExploded(string msg) {

//    Console.WriteLine("EOF. {0}", msg);
//}
