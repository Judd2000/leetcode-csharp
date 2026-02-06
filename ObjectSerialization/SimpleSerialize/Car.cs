using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSerialize
{
    public class Car
    {
        public Radio radio = new();

        public bool isHatchback = false;
        public override string ToString()
        {
            return $"IsHatchback: {isHatchback} Radio: {radio}";
        }
    }
}
