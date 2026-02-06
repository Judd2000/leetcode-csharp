using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSerialize
{
    public class Person
    {
        [JsonPropertyOrder(1)] // determine order JSON will be placed in serialized file
        public bool Alive = true;
        public Back? back;
        private int PersonAge = 63;
        private string _firstName = string.Empty;
        public string FirstName {
            get { return _firstName; }
            set { _firstName = value;  }
        }

        public override string ToString()
        {
            return $"Alive: {Alive} FirstName: {FirstName} Age: {PersonAge}";
        }
    }

    public class Back { 
        
    }
}
