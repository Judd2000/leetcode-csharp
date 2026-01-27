
// begin with a nonparallel query

using System.Runtime.Intrinsics.Arm;

Console.WriteLine("Press any key to begin processing");
Console.ReadKey();
Console.WriteLine("Processing...");
Console.ReadLine();

CancellationTokenSource _cancel = new();

Task.Factory.StartNew(ProcessIntData);
void ProcessIntData()
{
    try
    {
        Thread.Sleep(3000);
        int[] src = [.. Enumerable.Range(1, 10_000_000)];
        int[] divisibleByThree = [.. (from num in src.AsParallel().WithCancellation(_cancel.Token) where num % 3 == 0 //withCancellation extension method
                                  orderby num descending
                                  select num)];
        Console.WriteLine($"Found {divisibleByThree.Length} numbers that are divisible by three in the range 1 to 10 million.");
    }
    catch (OperationCanceledException ex) { 
        Console.WriteLine(ex.Message);
    }

}

do {
    Console.WriteLine("Press any key to begin processing.");
    Console.ReadKey();
    Console.WriteLine("Processing...");
    Task.Factory.StartNew(ProcessIntData);
    Console.WriteLine("Press Q to quit");
    string answer = Console.ReadLine();
    if (answer.Equals("Q", StringComparison.OrdinalIgnoreCase)) { 
        _cancel.Cancel();
        break;
    }
} while (true);