using System;
using System.ComponentModel.DataAnnotations;

namespace AutoProps;

public class Garage
{

    public int NumberOfCars { get; set; } // default backing val is 0.

    //public Car MyAuto { get; set; } // default backing val is null

    // You can initialize automatic properties like so

    public Car MyAuto { get; set; } = new Car();

    // can assure it's assignment in constructor
    public Garage()
	{
        //MyAuto = new Car(); // ensures no null pointer when using it
    }

    public Garage(Car car, int number) {
        MyAuto = car;
        NumberOfCars = number;
    }
}

public class Car {
    // imagine some actual fields here.
}
