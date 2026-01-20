using System;
using System.Collections.Generic;
using System.Text;

namespace AdvFeaturesTwo
{
    internal class CustomConversionRoutines
    {
    }

    public struct Rectangle { 
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int width, int height) { Width = width; Height = height; }

        public void Draw() {
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }

        public override string ToString() => $"[Width: {Width}, Height: {Height}]";

        // explicit cast to a square! Convert FROM Rect TO square
        public static explicit operator Square(Rectangle rect) {
            return new Square { Length = rect.Height };
        }

        public static implicit operator Rectangle(Square s) {
            return new Rectangle { Height = s.Length, Width = s.Length };
        }
    }

    public struct Square { 
        public int Length { get; set; }

        public Square(int length): this() { Length = length; }


        public void Draw()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }
        }

        public override string ToString() => $"[Length: {Length}]";

        // Let's allow conversions FROM INT and TO INT, since it is just 1 numerical val

        public static explicit operator Square(int sideLength) { 
            return new Square { Length = sideLength };
        }

        public static explicit operator int(Square square) { return square.Length; }

    }
}
