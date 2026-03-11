using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays_Strings
{
    internal class ReverseString
    {
        public static void Reverse(char[] str) { 
            int end = str.Length - 1;
            int start = 0;
            while (start < end) {
                (str[end], str[start]) = (str[start], str[end]); //tuple swapping is awesome!
                start++;
                end--;
            }
        }
    }
}
