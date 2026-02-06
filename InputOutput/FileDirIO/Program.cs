using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Text;

//DirectoryInfo dir3 = new(@"D:\Work"); // better to use Path.VolumeSeparatorChar and Path.DirectorySeparatorChar for cross-platform development
//DirectoryInfo dir3 = new($@"D{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}Work");
//dir3.Create();

static void ShowWindowsDirectoryInfo() {
    DirectoryInfo dir = new($@"C{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}Windows");
    Console.WriteLine("**** Directory Information ****");
    Console.WriteLine($"FullName: {dir.FullName}");
    Console.WriteLine($"Name: {dir.Name}");
    Console.WriteLine($"Parent: {dir.Parent}");
    Console.WriteLine($"Creation: {dir.CreationTime}");
    Console.WriteLine($"Attributes: {dir.Attributes}");
    Console.WriteLine($"Root: {dir.Root}");
    Console.WriteLine("***************\n");
}

ShowWindowsDirectoryInfo();

static void DisplayImages() {
    DirectoryInfo dir = new($@"C{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}Windows{Path.DirectorySeparatorChar}Web{Path.DirectorySeparatorChar}Wallpaper{Path.DirectorySeparatorChar}Windows");
    FileInfo[] files = dir.GetFiles("*.jpg");
    Console.WriteLine("Found {0} files (jpg).", files.Length);

    foreach (FileInfo file in files) {
        Console.WriteLine("File name: {0}", file.Name);
        Console.WriteLine("File size: {0}", file.Length);
        Console.WriteLine("File creation time: {0}", file.CreationTime);
        Console.WriteLine("File Attributes: {0}", file.Attributes);
        Console.WriteLine("***************\n");
    }
}

DisplayImages();


static void RuinTheAppDirectory() {
    DirectoryInfo dir = new DirectoryInfo(".");
    dir.CreateSubdirectory("Foldah");
    DirectoryInfo dirInfo = dir.CreateSubdirectory($@"FoldahTwo{Path.DirectorySeparatorChar}Data"); //Directory info is returned on successful creation, or if the directory exists.
    Console.WriteLine($"New folder information: {dirInfo}");
}

RuinTheAppDirectory();

// Directory type (static methods).

static void DirectoryTypeUsage()
{
    string[] drives = Directory.GetLogicalDrives();
    Console.WriteLine("***** Drives on System:\n");
    foreach (string s in drives) {
        Console.WriteLine($"-> {s}");
    }

    // Clean up files from prev function
    try {
        Directory.Delete("Foldah");
        Directory.Delete("FoldahTwo", true); //second parameter is if you want to destroy subdirectories
    } catch (IOException ex) {
        Console.WriteLine("Error deleting: {0}", ex);
    }
}

DirectoryTypeUsage();

static void DriveInfoUsage()
{
    DriveInfo[] myDrives = DriveInfo.GetDrives();
    foreach (DriveInfo driveInfo in myDrives) { 
        Console.WriteLine("Name: {0}", driveInfo.Name);
        Console.WriteLine("Type: {0}", driveInfo.DriveType);

        // check if mounted
        if (driveInfo.IsReady) {
            Console.WriteLine("Available Space: {0}", driveInfo.TotalFreeSpace);
            Console.WriteLine("Drive format: {0}", driveInfo.DriveFormat);
            Console.WriteLine("Label: {0}", driveInfo.VolumeLabel);
        }
    }
}

DriveInfoUsage();

// FileInfo class
static void FileIo() {
    Console.WriteLine("*** Simple File I/O with File Type ***\n");

    string fileName = $@"B{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}temp{Path.DirectorySeparatorChar}Test.dat";

    FileInfo file = new(fileName); //consider 'using' for automatic teardown
    FileStream fileStream = file.Create();

    fileStream.Close();

    // use using
    FileInfo fileTwo = new(fileName);
    using (FileStream fileStream2 = fileTwo.Create()) { 
        // do stuff
    }
    fileTwo.Delete();
}

FileIo();

// Open returns filestream

static void UsingOpen() {
    string fileName = $@"B{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}temp{Path.DirectorySeparatorChar}Test.dat";

    FileInfo file2 = new(fileName);
    using (FileStream fs = file2.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) { //share is how to share among other handlers
        Console.WriteLine("File made! Or read.");
    }
    file2.Delete();
}

UsingOpen();

// Consider OpenRead or OpenWrite to 1. open and 2. get a FileStream configured for you.

static void UseOpenReadWrite() {
    Console.WriteLine("Use openRead and openWrite :)   -=-=-=-=-=-=-=-=");
    string fileName = $@"B{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}temp{Path.DirectorySeparatorChar}Test.dat";
    FileInfo file3 = new(fileName);
    file3.Create().Close();

    using (FileStream readOnly = file3.OpenRead()) {
        Console.WriteLine("File stream obtained {0}", readOnly);
    }
    file3.Delete();

    //write-only
    FileInfo file4 = new(fileName);
    using (FileStream writeOnly = file4.OpenWrite()) {
        Console.WriteLine("File stream obtained {0}", writeOnly);
    } // use filestream
    file4.Delete();
}

UseOpenReadWrite();

// Using OpenText() returns StreamReader - write character data to file.

static void UseOpenText() {
    string fileName = $@"B{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}temp{Path.DirectorySeparatorChar}Test.dat";
    FileInfo file5 = new(fileName);
    file5.Create().Close();

    using (StreamReader sreader = file5.OpenText()) {
        Console.WriteLine("Stream reader from OpenText acquired {0}", sreader);
    }
    file5.Delete();
}

UseOpenText();

// You can perform all of the above calls to FileInfo with the static File methods. File.Open, etc.

// You also get additional methods like: ReadAllBytes() ReadAllLines(), ReadAllText(), WriteAllBytes() and WriteAllLines() \ WriteAllText() - they also close their own file handles.

static void UsingFileStaticMethods () {
    Console.WriteLine("**** Simple I/O With File ****\n");

    string[] tasks = { "Fix bathroom sink", "Jump", "Sit", "Stand" };
    File.WriteAllLines(@"tasks.txt", tasks);

    // now read it
    foreach (string task in File.ReadAllLines(@"tasks.txt")) {
        Console.WriteLine("TODO: {0}", task);
    }

    //Console.ReadLine();
    File.Delete(@"tasks.txt");
}

UsingFileStaticMethods();

// Streams describe data as raw stream of bytes. Seek() if supported, sets the position in the stream

// Filestream implements Stream - you will probably interact with it with stream wrappers.
static void FunWithFileStreams() {
    Console.WriteLine("**** Fun with FileStreams ****\n");
    using (FileStream fStream = File.Open("myMessage.dat", FileMode.Create)) {
        // encode string as byte array
        string msg = "HELLO";
        byte[] msgAsBytes = Encoding.Default.GetBytes(msg);

        fStream.Write(msgAsBytes, 0, msgAsBytes.Length); //bytes, offset, count

        fStream.Position = 0;

        Console.Write("Message as byte array:");
        byte[] bytesFromFile = new byte[msgAsBytes.Length];
        for (int i = 0; i < msgAsBytes.Length; i++)
        {
            bytesFromFile[i] = (byte)fStream.ReadByte();
            Console.Write(bytesFromFile[i]);
        }

        // Decode
        Console.Write("\nDecoded Message:");
        Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
    }
    File.Delete("myMessage.dat");
}

FunWithFileStreams();

//StreamWriter and StreamReaders. TextReader base class provides ability to read and peek into character stream
