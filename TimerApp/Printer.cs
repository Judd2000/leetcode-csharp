using System;
using System.Collections.Generic;
using System.Text;

namespace TimerApp
{
    internal class Printer
    {

        // lock token
        private object threadLock = new();
        public void PrintNumbers()
        {
            lock (threadLock)
            { // this is shorthand for Monitor.Enter(threadLock) try {} finally Monitor.Exit
                Console.WriteLine($"-> {Thread.CurrentThread.Name} is executing PrintNumbers()");

                // with Monitor you can do things like .Wait, inform waiting threads when current is done
                // Monitor.Pulse(), .PulseAll() etc.
                Console.WriteLine("Your numbers:");

                for (int i = 0; i < 10; i++)
                {
                    Random r = new();

                    Thread.Sleep(1000 * r.Next(5)); // random sleep
                    Console.WriteLine($"{i}");
                }
            }
        }
    }

    internal class AddParams(int num1, int num2) // primary constructor at top-level
    {
        public int a = num1, b = num2;
    }
}
