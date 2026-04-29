using System;
using System.Collections.Generic;

namespace HashMaps;
internal class RansomNote
{
    public static bool CanConstruct_ArrayVer(string ransomNote, string magazine)
    {
        int[] letters = new int[26]; // Default is 0

        foreach (char c in magazine)
        {
            letters[c - 'a']++;
        }

        foreach (char c in ransomNote) {
            if (letters[c - 'a'] > 0)
            {
                letters[c - 'a']--;
            }
            else {
                return false;
            }
        }
        return true;
    }

    // SLOW
    public static bool CanConstruct_HashVer(string ransomNote, string magazine)
    {
        HashSet<char> ransomNoteChars = new();
        foreach (char c in ransomNote) {
            ransomNoteChars.Add(c);
        }

        foreach (char c in ransomNoteChars)
        {
            if (ransomNote.Count(cha => cha == c) > magazine.Count((cha => cha == c))) {
                return false;
            }
        }

        return true;
    }

    // FAST
    public static bool CanConstruct_HashVerFASTER(string ransomNote, string magazine) {

        Dictionary<char, int> frequency = new();

        foreach (char c in magazine) {
            if (frequency.TryGetValue(c, out int count))
            {
                frequency[c] = count + 1; // Increment existing
            }
            else
            {
                frequency[c] = 1; // Add new
            }
        }

        foreach (char c in ransomNote) {
            if (frequency.TryGetValue(c, out int count) && count > 0)
            {
                frequency[c]--;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
