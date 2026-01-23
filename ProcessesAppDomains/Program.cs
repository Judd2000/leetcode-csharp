// See https://aka.ms/new-console-template for more information

// AppDomains - divisions within process that host set of .net core assemblies
// separated into contextual boundaries, group like minded .NET core objects

// thread: path of execution in a process.

// Primary thread can spawn additional secondary threads to reduce "blocking".
// The CPU is still switching between these, but it is more fluid to the end user

// Each rheead has Thread Local Storage to remember where it was when execution was paused.

// System.Diangostics namespace: programmatically interact with processes etc


// PROCESS:
// ExitTime: timestamp of exit (DateTime)
// Handle: IntPtr associated by the process ,good for unmanaged code
// id: PID
// MachineName: computer process is running on
// MainWindowTitle: caption of main window, empty if no window
// Modules - strongly typed ProcessModuleCollection type
// ProcessName - app name
// Responding
// StartTime (DateTime)
// Threads - set of threads, collection of ProcessThreads.

// Methods: CloseMainWindow()
// GetCurrentProcess()
// GetProcesses()
// Kill()
// Start()

using System.Diagnostics;

static int ListAllRunningProcesses() {
    var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;

    int pid = 0;

    foreach (var p in runningProcs) {
        string info = $"-> PID: {p.Id}\tName: {p.ProcessName}";
        Console.WriteLine(info);

        pid = p.Id;
    }

    return pid;
}

int processId = ListAllRunningProcesses();

// GetProcessById();

static void GetSpecificProcess() {
    Process proc = null;
    try
    {
        proc = Process.GetProcessById(30592); // throws exception if not found.
        Console.WriteLine(proc?.ProcessName);
    }
    catch (Exception ex) {
        Console.WriteLine(ex.Message);
    }
}

GetSpecificProcess();

// investigate thread set.

static void EnumThreadsAtPID(int pID) {
    Process tProcess = null;
    try { 
        tProcess = Process.GetProcessById(pID);
    } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        return;
    }

    Console.WriteLine("Threads used by: {0}", tProcess.ProcessName);
    ProcessThreadCollection threads = tProcess.Threads;

    foreach (ProcessThread pt in threads) {
        string info = $"-> Thread ID: {pt.Id}\tStart Time: {pt.StartTime.ToShortTimeString()}\tPriority:{pt.PriorityLevel}";
        Console.WriteLine(info);
    }
}

EnumThreadsAtPID(processId);

// Other members of interest of ProcessThread type
// CurrentPriority, Id, IdealProcessor (preferred processor for it to rn on)
// Priority level
// ProcessorAffinity: sets up thread to run on specific processors
// StartAddress - mem address of function that started the thread.
// Start time, thread state, total processor time, wait reason

// this type is for obtaining diagnostics for the active windows threads in a process

// Process Modules

// iterate over loaded modules

// module = .dll or .exe hosted by a process

static void EnumModulesForPID(int pID) {
    Process process = null;
    try
    {
        process = Process.GetProcessById(pID);
    }
    catch (Exception ex) {
        Console.WriteLine(ex.Message);
        return;
    }

    Console.WriteLine("Loaded modules for {0}", process.ProcessName);
    ProcessModuleCollection modules = process.Modules;

    foreach (ProcessModule pm in modules) {
        Console.WriteLine($"-> Module Name: {pm.ModuleName}");
    }
}

EnumModulesForPID(processId);

// start and stop processes programatically

static void StartEndProcess() {
    Process process = null;
    try
    {
        process = Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "www.facebook.com");
    }
    catch (InvalidOperationException ex) { Console.WriteLine(ex.Message); }

    Console.WriteLine("-> Hit enter to kill {0}", process.ProcessName);
    Console.ReadLine();

    try
    {
        foreach (var p in Process.GetProcessesByName("MsEdge"))
        {
            p.Kill(true);
        }
    }
    catch (InvalidOperationException ex) {
        Console.WriteLine(ex.Message);
    }
}

StartEndProcess();

// start overloads: Specify path and filename of launching process. After Start(), call instance level kill to stop.

// Start is often ran with System.Diagnostics.ProcessStartInfo object to specify information on how to start the program.

// Start and Kill using 

static void StartAndKillProcess() {
    Process process = null;
    try
    {

        ProcessStartInfo startinfo = new ProcessStartInfo("MsEdge", "www.facebook.com");
        startinfo.UseShellExecute = true; //defaults to false

        process = Process.Start(startinfo);


    }
    catch (Exception ex) {
        Console.WriteLine(ex.Message);
    }

    Console.WriteLine("-> Hit enter to kill {0}", process.ProcessName);
    Console.ReadLine();

    try
    {
        foreach (var p in Process.GetProcessesByName("MsEdge"))
        {
            p.Kill(true);
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }

}

StartAndKillProcess();

// OS verbs with application start info

static void UseApplicationVerbs() {
    int i = 0;

    ProcessStartInfo startInfo = new(@"D:\Work_Projects\DataAnnotationProjects\what.txt");
    foreach (var verb in startInfo.Verbs) {
        Console.WriteLine($"{i++}.{verb}");
    }

    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
    startInfo.Verb = "Edit";

    startInfo.UseShellExecute = true;

    Process.Start(startInfo);
}

UseApplicationVerbs();

// .NET app domain - .NET and .NET Core executables hosted by logical partition inside a process called
// application domain. OS-Neutral. CoreCLR can load and unload them quickly. Completely isolated from other app domains
// even in the same process.

// System.AppDomain