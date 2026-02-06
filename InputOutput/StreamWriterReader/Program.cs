using System.Text;

Console.WriteLine("Fun with StreamWriter and StreamReader *****\n");

using (StreamWriter writer = File.CreateText("reminders.txt")) { // or new StreamWriter()
    writer.WriteLine("Don't forget Mother's Day this year...");
    writer.WriteLine("Don't forget Father's Day this year...");
    writer.WriteLine("Don't forget these numbers:");
    for (int i = 0; i < 10; i++) {
        writer.Write(i + ""); //cast i to string
    }

    writer.Write(writer.NewLine); //newline based on OS
}
Console.WriteLine("Created file and wrote to it.");
Console.ReadLine();
//File.Delete("reminders.txt");

Console.WriteLine("Here is the written content:\n");
using StreamReader sr = File.OpenText("reminders.txt"); // or new StreamReader()

string input = null;
while ((input = sr.ReadLine()) != null)
{ //iteratively read each line.
    Console.WriteLine(input);
}

//there are many ways to do the same thing.

//StringWriter and Reader can be used to handle characters. Helpful when you need to append character info to a buffer.

Console.WriteLine("**** Fun with StringWriter / StringReader ****\n");
using StringWriter strWriter = new();

strWriter.WriteLine("Don't forget Mother's Day this year...."); //stream to stringbuilder
// get copy of content and dump
Console.WriteLine("Contents of StringWriter:\n{0}", strWriter);
StringBuilder sb = strWriter.GetStringBuilder();
sb.Insert(0, "Hey!");
Console.WriteLine("-> {0}", sb.ToString());
sb.Remove(0, "Hey!".Length);
Console.WriteLine("-> {0}", sb.ToString());

// when you want to read from a stream of chars, use StringReader.
using StringWriter stringWriter = new();

stringWriter.WriteLine("Don't forget MOTHERS DAY MOTHERS DAY MOTHERS DAY");
Console.WriteLine("Contents of StringWriter:\n{0}", stringWriter);

// Now, read data from string writer.
using StringReader stringReader = new(stringWriter.ToString());
input = null;
while ((input = stringReader.ReadLine()) != null) //read from the string.
{
    Console.WriteLine(input);
}
