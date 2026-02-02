using System;
using System.Collections.Generic;
using System.Text;

namespace ApplyingAttributes;

[VehicleDescription(Description = "This is the description of the Motorcycle vehicle.")] //named property, you've seen these in the shorthand object notation.
internal class Motorcycle
{
    [JsonIgnore]
    public float passengerWeight;

    public bool hasRadioSystem;

    public bool hasHeadset;

    public bool hasTailset;
}

[XmlRoot(Namespace ="umm.umm.com"), Obsolete("Use a newer vehicle.")] // can be combined. Even when passing 'constructor' args, this is not an instance of a class in memory until referenced.
// or you can stack them [Obsolete("Nope")]

// All Attributes are suffixed with Attribute by convention. C# doesn't require you to type Attribute
internal class HorseAndBuggy { }
