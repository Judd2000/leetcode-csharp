using System;
using System.Collections.Generic;
using System.Text;

namespace ApplyingAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)] //inherited if inner classes have this, too.
public sealed class VehicleDescriptionAttribute : Attribute
{
    public string Description { get; set; } = "";

    public VehicleDescriptionAttribute(string desc) => Description = desc;

    public VehicleDescriptionAttribute() { }
}
