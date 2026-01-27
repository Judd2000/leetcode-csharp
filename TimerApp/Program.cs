using System.Threading;
using TimerApp;

static void PrintTime(object? state)
{ //timer callback can only call methods with this signature
    Console.WriteLine("Time is: {0}. Param: {1}", DateTime.Now.ToLongTimeString(), state?.ToString());
}
// create instance of TimerCallback and pass into Timer object.

Console.WriteLine("***** Working with Timer Type ****\n");
TimerCallback timerCallback = new(PrintTime);

// Timer is not even used, consider a discard.
//_ = new Timer(timerCallback, new List<string>() { "aba", "baba" }, 0, 1000); // params are: delegate, info to pass to method, time to wait before start, and time between calls (ms).
//Console.WriteLine("Hit Enter to terminate.");
//Console.ReadLine();

//ThreadPool - holds onto created inactive threads until needed. 
// System.Threading.ThreadPool

// can queue a method call for processing by a thread in the pool: QueueUserWorkItem, optional state
Console.WriteLine("**** Fun with .NET Core Runtime Thread Pool ****\n");

Console.WriteLine("In Main Thread ID: {0}", Environment.CurrentManagedThreadId);

Printer p = new Printer();

WaitCallback workItem = new WaitCallback(PrintNums);

//for (int i = 0; i < 10; i++) { 
//    ThreadPool.QueueUserWorkItem(workItem, p);
//}
Console.WriteLine("All tasks queued");
//Console.ReadLine();

static void PrintNums(object? state) {
    // taking printer obj as input
    if (state is Printer printer) {
        printer.PrintNumbers();
    }
}

// ThreadPool threads are always background threads with ThreadPriority.Normal



//Consider Parallelism using Task Parallel Library using System.Threading.Tasks - automatically distribute app workload across available CPU's dynamically using thread pool

// Tasks.Parallel => iterate over IEnumerable<T> in parallel fashion. Parallel.For() Parallel.ForEach()
// Using the Func<T> and Action<T> delegates to specify processing method. Func<T> returns a type and Action<T> returns void