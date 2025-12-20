using System;
using Pointss;

namespace ObjectInitializers;

public class Rectangle
{

	private Point topLeft = new Point();

	private Point bottomRight = new Point();

	public Point TopLeft {
		get => topLeft;
		set => topLeft = value;
    }

    public Point BottomRight
    {
        get => bottomRight;
        set => bottomRight = value;
    }

    public void DisplayStats()
	{
		Console.WriteLine("TopLeft: ");
		TopLeft.DisplayStats();
		Console.WriteLine("BottomRight: ");
		bottomRight.DisplayStats();
    }
}
