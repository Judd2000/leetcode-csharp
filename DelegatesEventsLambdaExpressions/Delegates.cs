using System;
using System.Collections.Generic;
using System.Text;

namespace DelegatesEventsLambdaExpressions
{
    internal class SimpleMath
    {
        public static int Add(int x, int y) => x + y;

        public static int Subtract(int x, int y) => x - y;

        public static int SquareNumber(int a) => a * a;
    }

    // when you build a C# delegate, you are indirectly declaring a class that derives from MulticastDelegate. Common attr:
    // Method, Target, Combine(), GetInvocationList(), Remove(), RemoveAll()
    
    public delegate int BinaryOp(int x, int y); //auto generated sealed class deriving from MulticastDelegate

    // can also point to methods with out or ref params

    // Delegate gets THESE from MulticastDelegate: GetInvocationList(), overloaded operators. Combine, Remove, RemoveAll, 

    public delegate string MyOtherDelegate(out bool a, ref bool b, int c);
    // some select members: GetInvocationList, == operator, != operator, COmbine, params, delegate)

    // results in a sealed class with compiler-generated Invoke method that match the delegate's declaration signature



    // Let's use a class that notifies observers about state changes

    internal class Car {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;

        public string Name { get; set; }

        private bool _carIsDead;

        // new delegate
        public delegate void CarEngineHandler(string msg);

        private CarEngineHandler _handlerList;

        // registration function for calling code
        public void RegisterCarEngineHandler(CarEngineHandler methodToCall) {
            //_handlerList = methodToCall; // you could do += or use .Combine (which += uses under the hood).
            if (_handlerList == null) _handlerList = methodToCall;
            else {
                //_handlerList = (CarEngineHandler)Delegate.Combine(_handlerList, methodToCall); // += looks better though
                _handlerList += methodToCall;
            }
        }

        public void DeregisterCarEngineHandler(CarEngineHandler methodToCall) {
            if (_handlerList != null) _handlerList -= methodToCall;
        }

        public Car() { }

        public Car(string name, int maxSpeed, int currentSpeed) { 
            CurrentSpeed = currentSpeed;
            MaxSpeed = maxSpeed;    
            Name = name;
        }

        // In Accelerate, send engine-related messages to the subscribed listeners
        public void Accelerate(int amount) {
            if (_carIsDead) _handlerList?.Invoke("Car has died, cannot accelerate.");
            else {
                CurrentSpeed += amount;
                // warning if car is getting close to max speed.
                if (MaxSpeed - CurrentSpeed <= 10) {
                    _handlerList?.Invoke("Engine getting hot...");
                }
                if (CurrentSpeed > MaxSpeed)
                {
                    _carIsDead = true;
                }
                else {
                    Console.WriteLine("Current Speed = {0}", CurrentSpeed);
                }
            }
        }
    }
}
