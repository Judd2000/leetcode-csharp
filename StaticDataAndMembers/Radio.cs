using System;

// Class is internal by default
public class Radio
{
    // default constructor and fields are private by default
    public Radio() { }

    // nested types can have these access modifiers
    private enum  RadioBrand
    {
        Main,
        Generic,
        Luxury
    }
}
