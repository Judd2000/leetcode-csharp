using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace SimpleSerialize;

// Custom converters with JsonConverter<T>, override base Read and Write
//public abstract T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
//public abstract void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options);
public class JsonStringNullToEmpty : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // use the Utf8JsonReader instance to read the string value for the node,
        // and if it is null or an empty string, then return null.
        // Otherwise, return the value read:
        var val = reader.GetString();
        if (string.IsNullOrEmpty(val)) {
            return null;
        }
        return val;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        //use the Utf8JsonWriter to write an empty string if the value is null:    
        value ??= string.Empty; // ??= - if left val is null, assign to right.
        writer.WriteStringValue(value);
    }

    public override bool HandleNull => true;
}
