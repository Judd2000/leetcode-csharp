
global using System.Text.Json.Serialization;
global using System.Xml.Serialization;
using ApplyingAttributes;

// Using attributes marked as 'obsolete' nets the following

HorseAndBuggy buggy = new(); // IntelliSense uses reflection to tell you ahead of time it is obsolete

