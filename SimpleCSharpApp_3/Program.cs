ShowEnvironmentDetails();

Console.ReadLine();
return -1;

static void ShowEnvironmentDetails() {
    foreach (string drive in Environment.GetLogicalDrives()) {
        Console.WriteLine("Drive: {0}", drive);
    }

    Console.WriteLine("OS Version: {0}", Environment.OSVersion);

    Console.WriteLine("Number of processors: {0}", Environment.ProcessorCount);

    Console.WriteLine(".NET Core Version: {0}", Environment.Version);

    Console.Beep();
}


