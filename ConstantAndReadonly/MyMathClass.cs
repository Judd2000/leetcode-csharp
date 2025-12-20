using System;

namespace ConstData;
public class MyMathClass
{
	// constant fields are static
	public const double PI = 3.14;
	public MyMathClass()
	{
		PI2 = 3.14;
	}

	// Read-only fields can be assigned in constructor
	public readonly double PI2; //not imp static
                                // public static readonly double PI = 3.14;

    // Static constructor can initialize static readonly fields
	public static readonly double PI3;
    static MyMathClass()
	{
		PI3 = 3.14;
    }
}
