using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSerialize
{
    public class SpyCar : Car
    {
        public bool CanFly;
        public bool CanSubmerge;
        public override string ToString()
        {
            return $"CanFly: {CanFly} CanSubmerge: {CanSubmerge} {base.ToString()}";
        }
    }
}
