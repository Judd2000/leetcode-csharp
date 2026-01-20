using System;
using System.Collections.Generic;
using System.Text;

namespace AdvFeatures
{
    internal class IndexableClass
    {
        public int[] IntArr {get; init;}

        public IndexableClass(int arrLength) {
            IntArr = new int[arrLength];
        }

        // if we had a 2-d array or 2D data, we could add another int param
        // to the 'this' portion.
        public int this[int index] {
            get => IntArr.Length > 0 ? IntArr[index] : -1;
            set => IntArr[index] = value;
        }
    }

    // Wrapper of a dictionary.
    internal class IndexableStringClass {
        private Dictionary<string, int> privateDict = [];


        // you can also do multiple indexers for a class (see pg 837 of pro c#).
        public int this[string key] {
            get => privateDict[key];
            set => privateDict[key] = value;
        }

        public void ClearDictionary() {
            privateDict.Clear();
        }

        public int Count => privateDict.Count;
    }

    // Interface types can also describe indexers that implementers must define
    public interface IStringContainer { 
        string this[int index] { get; set; }
    }
}
