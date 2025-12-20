using System;

namespace SimpleClassExample;

public class Car
{
	public string petName;

	public int currSpeed;

	public Car()
	{
		petName = "Clock";
		currSpeed = 120;
	}

	public Car(string name) {
		petName = name;
	}

	// Alternatively, 
	/* public Car(string name) => petName = name; */

	// Can use out parameters.
	public Car(string pn, int cs, out bool inDanger) {
		petName = pn;
		currSpeed = cs;
		inDanger = cs > 100;
	}


	// Constructors are 'distinct' due to amount of args.
	public Car(string name, int speed) {
		petName = name;
		currSpeed = speed;
	}

	public void PrintState() => Console.WriteLine($"{petName} is going {currSpeed} MPH.");

	public void SpeedUp(int amount) => currSpeed += amount;
}
