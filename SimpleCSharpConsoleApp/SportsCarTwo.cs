using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCSharpConsoleApp
{
    public class SportsCarTwo : Car
    {
        public string GetPetName() {
            PetName = "red";
            return PetName;
        }
    }
}