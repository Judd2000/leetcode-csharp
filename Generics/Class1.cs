using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    internal class GenericsEx : IComparable<GenericsEx> // can reference generic interfaces
    {
        public int CompareTo(GenericsEx? other)
        {
            throw new NotImplementedException();
        }
    }

    internal class Person { 
        public int Age { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public Person() { }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Age: {Age}";
        }
    }

    // sort by age comparer class
    internal class SortPeopleByAge: IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x?.Age > y?.Age) return 1;
            if (x?.Age < y?.Age) return -1;

            return 0;
        }
    }
}
