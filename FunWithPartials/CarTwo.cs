using System;

namespace Car;

public class CarTwo
{
	public string Make { get; set; }
	public string Model { get; set; }
	public string Color { get; set; }

	public CarTwo() { }
	public CarTwo(string make, string model, string color)
	{
		Make = make;
		Model = model;
		Color = color;
	}

	public static void DisplayCarStats(CarTwo car)
	{
		Console.WriteLine($"Make: {car.Make}, Model: {car.Model}, Color: {car.Color}");
	
	}
}

record CarRecord
{
	public string Make { get; init; }
	public string Model { get; init; }
	public string Color { get; init; }

	public CarRecord() { }
	public CarRecord(string make, string model, string color)
	{
		Make = make;
		Model = model;
		Color = color;
	}

	public static void DisplayCarRecordStats(CarRecord car) { 
		Console.WriteLine($"Make: {car.Make}, Model: {car.Model}, Color: {car.Color}");
	}
}

// Positional record type - compact syntax

// Drawbacks: can't use object initialization, record must be created with correct order of props
// And all properties are init-only by default

// primary constructor provided
record AbbrevCar(string Make, string Model, string Color);

// Mutable record types! But records are intended to be immutable
record CarRecordMutable {
	public string Make { get; set; }
	public string Model { get; set; }
	public string Color { get; set; }

	public CarRecordMutable() { }
	public CarRecordMutable(string make, string model, string color) {
		Make = make;
		Model = model;
		Color = color;
	}
}

// Record structs.
public record struct Point(double X, double Y, double Z);

public record struct PointProps() { 
	public double X { get; set; }
	public double Y { get; set; }
	public double Z { get; set; }

	public PointProps(double x, double y, double z) : this() {
		X = x; Y = y; Z = z;
	}
}
