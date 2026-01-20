using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    internal class LINQFields
    {
        private static string[] currentVideoGames = { "Destiny 3", "Fallout 5", "GTA VI", "Minecraft 2" };

        // Need speciifc type for class field, no 'var'
        private IEnumerable<string> subset = from g in currentVideoGames where g.Contains(' ') orderby g select g;

        public void PrintGames() {
            Console.WriteLine("LINQ Class Member");
            foreach (var item in subset) {
                Console.WriteLine("Item {0}", item);
            }
        }
    }
}
