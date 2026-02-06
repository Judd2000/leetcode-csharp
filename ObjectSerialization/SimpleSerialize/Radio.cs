using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSerialize
{
    public class Radio
    {
        // [JsonInclude] 
        public bool HasTweeters;
        public bool HasSubwoofers;
        public List<double> StationPresets = [];
        public string RadioId = "XMX1028M";

        public override string ToString()
        {
            var presets = string.Join(",", StationPresets.Select((i) => i.ToString()).ToList());
            return $"HasTweeters:{HasTweeters} HasSubwoofers: {HasSubwoofers} Station Presets: {presets}";
        }
    }
}
