using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    internal class Car
    {
        public string Name { get; set; } = "";
        public string Color { get; set; } = "";
        public int Speed { get; set; }
        public string Make { get; set; } = "";
    }

    internal class ProductInfo {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public int NumberInStock { get; set; } = 0;

        public override string ToString() => $"Name={Name}, Description={Description}, Number in Stock = {NumberInStock}";
    }

    internal class MiniProduct {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public override string ToString() => $"{Name} {Description}";
    }
}
