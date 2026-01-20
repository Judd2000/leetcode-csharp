using System;
using System.Collections.Generic;
using System.Text;

namespace AnonymousMethods
{
    internal class CarWithEvents
    {
        public delegate void CarEngineHandler(object sender, CarEventArgs e); // expose the delegate type. => for custom event sending, obj sender and EventArgs

        public event EventHandler<CarEventArgs>? Exploded; // BETTER => no need for a delegate at all
        public event EventHandler<CarEventArgs>? AboutToBlow;

        // Calling code would then use EventHandler anywhere we used to use CarEventHandler

        private bool _carDead;

        // There is even more you can leverage to simplify this process.

        public int CurrentSpeed { get; set; }

        private int _maxSpeed = 120;

        public readonly string Name = "The Name You Shall Use";

        public CarWithEvents() { }

        public CarWithEvents(int curSpeed)
        {
            CurrentSpeed = curSpeed;
        }

        // now sending an event to caller is easy, specify event by name and any required params.

        public void Accelerate(int delta)
        {
            if (_carDead)
            {
                Exploded?.Invoke(this, new CarEventArgs("Dead DEAD DEAD DEAD"));
            }
            else
            {
                CurrentSpeed += delta;

                if (40 >= _maxSpeed - CurrentSpeed)
                {
                    AboutToBlow?.Invoke(this, new CarEventArgs("Careful, within 40 of blowing up."));
                }

                if (CurrentSpeed >= _maxSpeed)
                {
                    _carDead = true;
                }
                else
                {
                    Console.WriteLine("Current speed is now {0}", CurrentSpeed);
                }
            }
        }


    }

    // custom event args.
    /// when you have simple events, consider using EventArgs class. Otherwise, derive from EventArgs
    /// 
    internal class CarEventArgs : EventArgs
    {
        public readonly string message;
        public CarEventArgs(string message)
        {
            this.message = message;
        }
    }

}
