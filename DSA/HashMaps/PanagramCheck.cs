using System;
using System.Collections.Generic;
using System.Text;

namespace HashMaps
{
    internal class PanagramCheck
    {
        public static bool CheckIfPangram(string sentence)
        {
            HashSet<char> letters = new();
            foreach (char c in sentence)
            {
                letters.Add(c);
            }

            return letters.Count == 26;
        }

        public static bool CheckIfPanagram_Optimized(string sentence)
        {
            bool[] charachters = new bool[26];
            foreach (char ch in sentence)
            {
                charachters[(int)ch - 97] = true; // convert array index to character
            }
            foreach (bool ch in charachters)
            {
                if (ch == false) return false;
            }
            return true;
        }
    }
}
