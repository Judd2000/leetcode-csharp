// Async AWait
using Microsoft.VisualStudio.Threading;

// marking a function as async will cause the .NET Core Runtime to create a new thread to handle it. Await will automatically pause current thread until activity is complete
//Console.WriteLine("**** Fun with Async ****");
//string message = await DoWorkAsync(); //framework sync context
//Console.WriteLine($"0-{message}");
//string secondMessage = await DoWorkAsync().ConfigureAwait(false); //ignore context and scheduler. Use this for non-app code (cmd).
//Console.WriteLine($"1-{secondMessage}");

static async Task<string> DoWorkAsync()
{ //Task<T> for return values, Task for void. Name nonblocking methods with Async suffix.
    return await Task.Run(() =>
    { // without Await in the method, it is synchronous. Await suspends calling thread.
        Thread.Sleep(5000);
        return "Done 'WORKING'... :)";
    });
}

// SynchronizationContext: allow internal async sync operations of common language runtime to behave properly with different synchronization models
// IE Dispatcher in WPF, restrictions on access controls on secondary threads.

// virtual post method, taking a delegate to be executed asynchronously

// when an async method is awaited, SynchronizationContext and TaskScheduler are used.

// By default, awaiting a task results in a synchronization context. 

static async Task VoidMethodAsync() { // async / await should always be with a Task or Task<T>
    await Task.Run(() =>
    {
        Thread.Sleep(4_000);
        throw new Exception("Fire and forget failed.");
    });
    Console.WriteLine("Fire and forget, fire and forget...");
}

//try
//{
//   await VoidMethodAsync();
//}
//catch (Exception ex) {
//    Console.WriteLine(ex);
//}

// Async methods can also have multiple awaits.

static async Task SeveralAwaits() {
    //await Task.Run(() => Thread.Sleep(2_000));
    //Console.WriteLine("First 'task' done");
    //await Task.Run(() => Thread.Sleep(2_000));
    //Console.WriteLine("Second 'task' done");
    //await Task.Run(() => Thread.Sleep(2_000));
    //Console.WriteLine("Third 'task' done");

    // Alternatively you can chain them using Task.WhenAll (think Promise.All).

    //await Task.WhenAll(
    //    Task.Run(() => { 
    //        Thread.Sleep(2_000);
    //        Console.WriteLine("First 'task' done");
    //    }),
    //    Task.Run(() => {
    //        Thread.Sleep(1_000);
    //        Console.WriteLine("Second 'task' done");
    //    }),
    //    Task.Run(() => {
    //        Thread.Sleep(500);
    //        Console.WriteLine("Third 'task' done");
    //    })
    //);

    // WhenAny: return first task, like Promise.Race.

    await Task.WhenAny(
        Task.Run(() => {
            Thread.Sleep(2_000);
            Console.WriteLine("First 'task' done");
        }),
        Task.Run(() => {
            Thread.Sleep(1_000);
            Console.WriteLine("Second 'task' done");
        }),
        Task.Run(() => {
            Thread.Sleep(500);
            Console.WriteLine("Third 'task' done");
        }));
}

await SeveralAwaits();

// You can also pass a collection of Tasks to these two methods.

static async Task SeveralAwaitsWithArray() {
    List<Task> taskList = [
        Task.Run(() => {
            Thread.Sleep(1_000);
            Console.WriteLine("1000ms task done");
        }),
        Task.Run(() => {
            Thread.Sleep(1_500);
            Console.WriteLine("1500ms task done");
        }),
        Task.Run(() => {
            Thread.Sleep(1_250);
            Console.WriteLine("1250ms task done");
        })
    ];

    await Task.WhenAll(taskList); // await all Tasks
}

await SeveralAwaitsWithArray();

// when calling async from synchronous contexts, consider: Wait() on tasks, Result property on Task<T>, GetAwaiter().GetResult() => these: 1. block main thread, process on another, and return back
// to calling thread.

// Neat async await debugging package is Microsoft.VisualStudio.Threading.Analyzers.

// using JoinableTaskFactory.Run

JoinableTaskFactory joinableFact = new(new JoinableTaskContext()); // with this we can run async methods from sync context.
string message2 = joinableFact.Run(async () => await DoWorkAsync());
Console.WriteLine($"Message 2 from joinable factory: {message2}");

// await in catch / finally also supported

// not all async methods need to be of type Task or Task<T>, as long as they follow async pattern, like Value Task:
static async ValueTask<int> ReturnInt() {
    await Task.Delay(1_000);
    return 1;
}

// local functions with async await.

static async Task MethodFixed(int first, int second) {

    Console.WriteLine("In MethodWithProblems");

    if (second < 0)
    {
        Console.WriteLine("Bad data");
        return;
    }

    await FullImplementation(first, second);
}

static async Task FullImplementation(int first, int second) {
    await Task.Run(() => {
        Thread.Sleep(4_000);
        Console.WriteLine("First is done.");

        // no gaurantee when checks will be executed, check BEFORE ENTERING ASYNC CONTEXT. Consider a helper method

        // Call long-running method that fails with parameter issue
        Console.WriteLine("Something Happened.");
    });
}

await MethodFixed(1, 2);

// Cancel async/await 

// Another param to Task.Run is a cancellation token. Consider implementing cancellation of async await using WaitAsync, which also takes a cancellation token OR a time before cancellation (ms)
// await DoWorkAsync().WaitAsync(timespan OR token)

// when calling from synchronous code, use joinabletaskfactory and WaitAsync

JoinableTaskFactory fact = new(new JoinableTaskContext());
CancellationTokenSource tokenSource = new();
try
{
    fact.Run(async () =>
    {
        await VoidMethodAsync().WaitAsync(TimeSpan.FromSeconds(15), tokenSource.Token); //time limit and cancellation
    });
}
catch (Exception ex) {
    Console.WriteLine(ex);
}


// asynchronous streams IAsyncEnumerable<T> with yield return statements
static async IAsyncEnumerable<int> GenSequence() {
    for (int i = 0; i < 10; i++) {
        await Task.Delay(100);
        yield return i;
    }
}

await foreach (int num in GenSequence()) {
    Console.WriteLine(num);
}

// Parallel.ForEach has an async counterpart, too.