using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace ExtensionMethods
{
    internal static class ExtensionMethods
    {
        // The first param of an extension method is the type being extended
        public static void DisplayDefiningAssembly(this object obj) { 
            Console.WriteLine("{0} lives => {1}\n", obj.GetType().FullName, Assembly.GetAssembly(obj.GetType())?.GetName().Name);
        }

        // We have effectively extended the integer type I think.
        public static int ReverseDigits(this int i)
        {
            char[] digits = i.ToString().ToCharArray();
            Array.Reverse(digits);
            string newDigits = new string(digits);

            return int.Parse(newDigits);
         }

        public static void PrintData_Beep(this System.Collections.IEnumerable iterator) {
            foreach (var item in iterator) {
                Console.WriteLine(item);
                Console.Beep();
            }
        }
    }

    // Before, classes needed a direct imp of GetEnumerator() to use foreach

    // Now, extension methods are scanned and if GetEnumerator is found it will
    // be used.

    class Car {
        public int CurrentSpeed { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public Car() { }

        public Car(string name, int speed) {
            CurrentSpeed = speed;
            Name = name;
        }
    }

    class Garage {
        public Car[] CarsInGarage { get; set; } = [];

        public Garage() {
            CarsInGarage = new Car[3];
            CarsInGarage[0] = new Car("Ra", 25);
            CarsInGarage[1] = new Car("Wra", 26);
            CarsInGarage[2] = new Car("Hammy", 55);
        }
    }

    // now let's extend Garage with IEnumerable's GetEnumerator
    static class GarageExtensions {
        public static IEnumerator GetEnumerator(this Garage garage) { 
            return garage.CarsInGarage.GetEnumerator();
        }
    }
}
