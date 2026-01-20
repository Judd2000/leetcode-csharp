using System;
using System.Collections.Generic;
using System.Text;

namespace AdvFeatures
{
    internal class Point: IComparable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }

        // static operator for unary '+'
        public static Point operator + (Point p1, Point p2) { 
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        // you can even handle cases where an int is added to a Point

        public static Point operator + (Point p1, int increment) { 
            return new Point(p1.X + increment, p1.Y + increment);
        }

        // must define both sides.
        public static Point operator + (int increment, Point p1)
        {
            return new Point(p1.X + increment, p1.Y + increment);
        }

        // Overload the ++ and --

        public static Point operator ++(Point p1) {
            return new Point(p1.X + 1, p1.Y + 1);
        }

        public static Point operator --(Point p1)
        {
            return new Point(p1.X - 1, p1.Y - 1);
        }

        // if you're already overloading Equals, it is easy to add 
        // an overload to the == operator.

        public override bool Equals(object? obj)
        {
            return obj?.ToString() == this.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int CompareTo(Point? other)
        {
            if (this.X > other?.X && this.Y > other.Y) return 1;
            if (this.X < other?.X && this.Y < other.Y) return -1;
            return 0;
        }

        public static bool operator <(Point p1, Point p2) {
            return p1.CompareTo(p2) < 0;
        }

        public static bool operator >(Point p1, Point p2)
        {
            return p1.CompareTo(p2) > 0;
        }

        public static bool operator <=(Point p1, Point p2)
        {
            return p1.CompareTo(p2) <= 0;
        }

        public static bool operator >=(Point p1, Point p2)
        {
            return p1.CompareTo(p2) >= 0;
        }


        // overriding equivalent operators
        public static bool operator == (Point p1, Point p2) {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

    }
}
