using System;

struct Point
{
    public int x; public int y;

    public Point(int xPos, int yPos)
    {
        x = xPos; y = yPos;
    }

    // Custom destructuring syntax.
    public (int xPos, int yPos) Deconstruct() => (x, y);
}
