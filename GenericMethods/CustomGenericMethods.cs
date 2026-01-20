using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GenericMethods
{
    internal static class CustomGenericMethods
    {
        // if we wanted to use this on multipe types and reduce duplication,
        // use generics.
        // Rather than 'boxing' and using an obj type, these can stay on the
        // call stack and not in the heap if they're value types.
        public static void Swap<T>(ref T a, ref T b) {
            Console.WriteLine("Swap ran for {0} type", typeof(T));
            T tmp = a;
            a = b;
            b = tmp;
        }

        public static void DisplayBaseClass<T>() {
            T def = default; // must overwrite a variable of type T // longhand it is default(T)
            Console.WriteLine($"Base class of {typeof(T)} is {typeof(T).BaseType}");
            Console.WriteLine("The default value for your type is {0}", def);
        }

        public static void PatternMatching<T>(List<T> p) { // you can also lock down what T is with : where [condition]:
            // Like this: where T: struct, where T: new() (default constructor) where T: ParentClass
            // (must derive from parentClass) commas can allow you to union the where.

            // You can also comma-separate where statements for multiple generic types where T, where K ex.
            switch (p) {
                // review the craziness of switch statement syntax soon.
                case List<string> pString:
                    Console.WriteLine("List is of strings");
                    return;
                case List<int> pInt:
                    Console.WriteLine("List is of ints");
                    return;
             }
        }
    }
}
