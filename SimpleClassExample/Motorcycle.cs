namespace SimpleClassExample;
using System;

public class Motorcycle {

	public int driverIntensity;

	public string driverName;

	public Motorcycle() { }

	// Any additional code after the master constructor has written will be executed.
	public Motorcycle(int intensity): this(intensity, "") {
		Console.WriteLine("intensity constructor called");
	}

	// All work goes to  a single constructor, these just route to it.
	//public Motorcycle(string name) : this(0, name) {
	//	Console.WriteLine("name constructor called");
	//   }

	//   public Motorcycle(int intensity, string name) {
	//	Console.WriteLine("int constructor called.");
	//	if (intensity > 10) { intensity = 10;5 }
	//	driverIntensity = intensity;
	//	driverName = name;
	//}
	//   public void PopAWheely() {
	//	for (int i = 0; i < driverIntensity; i++) {
	//		Console.WriteLine("Popping a wheelie!");
	//	}
	//}

	public Motorcycle(int intensity = 0, string name = "") {
		if (intensity > 10) intensity = 10;
		driverIntensity = 10;
		driverName = name;
	}
	
	public void SetDriverName(string name) {
		//this.name = name; // need to use 'this'
		this.driverName = name;
		// equivalent to driverName = name;
    }

    // Constructor Chaining. greatest number of args as 'master'.

}
