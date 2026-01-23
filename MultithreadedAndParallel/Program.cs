using System.Diagnostics;
using System.Threading;
using MultithreadedAndParallel;

static void ExtractExecutingThread() {
    Thread currThread = Thread.CurrentThread;
}

static void ExtractAppDomainHostingThread() { 
    AppDomain appDomain = Thread.GetDomain();
}

// When you want to get the current execution context a thread is executing in,
// use Thread.CurrentThread.ExecutionContext

// Need to be mindful of which parts of your code are subject to multithreaded access, and which
// are atomic.

// Be aware of what is threadsafe and what isn't. .NET documentation will call them ATOMIC.

// threading primitives can help. locks, monitors, [Synchronization] attr

// .NET delegate asynchronous invocation


// Thread: object oriented wrapper around given path of execution in AppDomain

Console.WriteLine("*** Main Thread Stats ***\n");

Thread primaryThread = Thread.CurrentThread;
primaryThread.Name = "The Primary Thread."; // => can be very handy to name this thread. Debugging etc.

// info & stats
Console.WriteLine($"Id of current thread {primaryThread.ManagedThreadId}");
Console.WriteLine("Thread Name: {0}", primaryThread.Name);

Console.WriteLine("Has thread started? {0}", primaryThread.IsAlive);

Console.WriteLine("Priority Level: {0}", primaryThread.Priority);
// Priority is an enum of System.Threading.ThreadPriority
Console.WriteLine("ThreadState {0}", primaryThread.ThreadState);

// How to manually spawn new threads

//1. Create method to be the entry point for the new thread. (must be void)

// 2. Create new ParameterizedThreadStart delegate, pass the address of the entry point method

// 3. Create a thread object and pass ParameterizedThreadStart to it.

// 4. Set initial thread characteristics (name, priority etc).

// 5. Call Thread.Start()

//Console.WriteLine("Threaded Application");
//Console.WriteLine("[1] or [2] threads?");

//string threadCount = Console.ReadLine();

//Thread primThread = Thread.CurrentThread;
    
//primThread.Name = "Primary";

//Printer p = new Printer();

//switch (threadCount) {
//    case "2":
//        Thread bgThread = new Thread(new ThreadStart(p.PrintNumbers));
//        bgThread.Name = "Secondary";
//        bgThread.Start();
//        break;
//    case "1":
//        p.PrintNumbers();
//        break;
//    default:
//        Console.WriteLine("Defaulting to 1 thread.");
//        goto case "1";

//}

//Console.WriteLine("This is the main thread speaking!");

// Parameterized thread start delegate - pass data to thread entry function.

// this is our thread target
static void Add(object data) {
    if (data is AddParams addParams) {
        Console.WriteLine($"Id of Thread in Add(): {Environment.CurrentManagedThreadId}");
        Console.WriteLine($"{addParams.a} + {addParams.b} is {addParams.a + addParams.b}");
    }
}

Console.WriteLine("Calling methods with params on second thread");
Console.WriteLine("ID of thread in Main(): {0}", Environment.CurrentManagedThreadId);

AddParams addP = new(10, 10);
Thread secondThread = new(new ParameterizedThreadStart(Add));

secondThread.Start(addP); // pass custom object as a param to "start"

// force wait to let other thread terminate

Thread.Sleep(5);

// AutoResetEvent class. How to force thread to wait for another in a safe way:
// AutoResetEvent class. Can call WaitOne() method.

AutoResetEvent _waitHandle = new(false);

Console.WriteLine("*** Adding with Thread Objects ***");
Console.WriteLine("Thread ID in Main {0}", Environment.CurrentManagedThreadId);
AddParams addParameters = new AddParams(10, 10);

Thread anotherThread = new Thread(new ParameterizedThreadStart(AddTwo));

anotherThread.Start(addParameters);

// now we can wait
_waitHandle.WaitOne();

void AddTwo(object data)
{
    if (data is AddParams addParams)
    {
        Console.WriteLine($"Id of Thread in Add(): {Environment.CurrentManagedThreadId}");
        Console.WriteLine($"{addParams.a} + {addParams.b} is {addParams.a + addParams.b}");

        // notify to other thread that we are done
        _waitHandle.Set();
    }
}

// Background Threads. By default, threads are foreground threads, meaning that the program itself will
// persist until all threads stop

Console.WriteLine("*** BG Threads ***\n");
Printer p = new Printer();

Thread bgThread = new Thread(new ThreadStart(p.PrintNumbers));

//bgThread.IsBackground = true; // toggle on and off to see program waiting or not for this to complete.
bgThread.Start();

Console.WriteLine();

// Concurrency. All threads have concurrent access to shared app data.

// Synchronizing threads
Console.WriteLine("*** Thread Mania ***");
p = new();

// 10 threads all pointing to same method on same P object

Thread[] threads = new Thread[10];

for (int i = 0; i < threads.Length; i++) // extremely volatile results, random amounts of accessed time on SAME RESOURCE
{
    threads[i] = new Thread(new ThreadStart(p.PrintNumbers)) { Name = $"Worker Thread #{i}" };
}

foreach (Thread thr in threads) {
    thr.Start();
}

// programatically enforced access. C# Lock.
// token required to enter within lock scope.

// Interlocked class: operate on single datapoint atomically with less overhead than Monitor.
/* Instead of:
 * 
 * int intVal = 5
 * object lockToken = new();
 * lock(lockToken) { intVal++ }
 */

// You can do: intVal = Interlocked.Increment(ref intVal)

// For setting values, use .Exchange.
var myInt = 27;
Interlocked.Exchange(ref myInt, 83);
Console.WriteLine(myInt.ToString());

// thread-safe equality check plus assignment
Interlocked.CompareExchange(ref myInt, 99, 83); //if value of i is 83, set to 99.

// Timer callbacks
// Timer type in conjunction with TimerCallback delegate