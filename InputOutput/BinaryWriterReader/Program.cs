
Console.WriteLine("**** Fun with Binary Writers and Readers ****\n");

FileInfo f = new("BinFile.dat");
using (BinaryWriter bw = new(f.OpenWrite()))
{
    Console.WriteLine("base stream is: {0}", bw.BaseStream);

    // data to save
    double theDouble = 1234.567;
    int theInt = 3455677;
    string theString = "A,BC,DEF,G";

    bw.Write(theDouble);
    bw.Write(theInt);
    bw.Write(theString);
}
// layer in a data stream before writing

// print type of base stream


// Now let's read with BinaryReader.
FileInfo fileInfo = new// layer in a data stream before writing
("BinFile.dat");
using BinaryReader br = new(fileInfo.OpenRead());

Console.WriteLine(br.ReadDouble());
Console.WriteLine(br.ReadInt32());
Console.WriteLine(br.ReadString());