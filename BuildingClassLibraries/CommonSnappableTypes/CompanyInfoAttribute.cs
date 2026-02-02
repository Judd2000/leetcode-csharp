using System;
using System.Collections.Generic;
using System.Text;

namespace CommonSnappableTypes
{
    [AttributeUsage(AttributeTargets.Class)] //only allow attribute usage on classes
    public sealed class CompanyInfoAttribute : System.Attribute
    {
        public string CompanyName { get; set; } 
        public string CompanyUrl { get; set; }
    }
}
