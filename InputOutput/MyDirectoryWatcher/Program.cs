using System.IO;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("*** Welcome to the Directory Watcher Program ***");

FileSystemWatcher watcher = new();

try
{
    watcher.Path = @"."; //current dir
}
catch (Exception ex) { 
    Console.WriteLine(ex.ToString());
    return;
}

// notify setup
watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName; //bitwise OR -> union.

// only watch .txt
watcher.Filter = "*.txt";

// set up event handlers.

var changeAnonymousFnct = new FileSystemEventHandler((object _, FileSystemEventArgs e) => Console.WriteLine($"File: {e.FullPath} {e.ChangeType}.")); //reusing code! although += (_, e) => is easier...

watcher.Changed += changeAnonymousFnct;
watcher.Created += changeAnonymousFnct;
watcher.Deleted += changeAnonymousFnct;
watcher.Renamed += (_, e) => Console.WriteLine($"File:{e.OldFullPath} renamed to {e.FullPath}");

watcher.EnableRaisingEvents = true; // start watching dir

// wait for user quit
Console.WriteLine("Pess q to quit app.");

using (var sw = File.CreateText("Test.txt")) {
    sw.Write("Some text :)");
}

File.Move("Test.txt", "Test2.txt"); //perform a move
File.Delete("Test2.txt");

// simply wait for q.
while (Console.Read() != 'q');