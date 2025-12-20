using System;

namespace Pointss;
public class Point 
{
	public int X { get; init; }
	public int Y { get; init; }

	public PointColorEnum Color { get; set; }

	public Point (int xVal, int yVal)
	{
		X = xVal;
		Y = yVal;
		Color = PointColorEnum.Gold;
	}

	public Point (PointColorEnum ptColor)
	{
		Color = ptColor;
	}

	public Point () : this(PointColorEnum.LightBlue)
	{
	}

    public void DisplayStats()
    {
        Console.WriteLine("X: {0}, Y: {1}, Color: {2}", X, Y, Color);
    }
}

public enum PointColorEnum
{
    LightBlue,
    LightRed,
    Gold
}
